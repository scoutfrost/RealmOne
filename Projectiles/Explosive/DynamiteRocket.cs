using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Common.DamageClasses;
using RealmOne.Common;
using System.Linq;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.DataStructures;
using Terraria.GameContent;
using ReLogic.Content;
using RealmOne.Items.Weapons.Demolitionist;
using RealmOne.RealmPlayer;

namespace RealmOne.Projectiles.Explosive
{
    public class DynamiteRocket : ModProjectile
    {



        public bool Exploded { get => Projectile.ai[0] != 0; set => Projectile.ai[0] = !value ? 0 : 1; }
        private bool Hit = false;

        private Player Owner => Main.player[Projectile.owner];


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dynamite Rocket");
            Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode

        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 240;
            Projectile.damage = 30;
            Projectile.penetrate = 1;
            Projectile.DamageType = ModContent.GetInstance<DemolitionClass>();
            Projectile.ownerHitCheck = true;
            Projectile.tileCollide = true;
            Projectile.scale = 1f;
            Projectile.aiStyle = ProjAIStyleID.GroundProjectile;
            Projectile.timeLeft = 150;
            Projectile.CloneDefaults(ProjectileID.MolotovCocktail);

        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f);
            if (++Projectile.frameCounter >= 15f)
            {
                Projectile.frameCounter = 0;

                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            var Hitbox = new Rectangle((int)Projectile.Center.X - 30, (int)Projectile.Center.Y - 30, 60, 60);
            IEnumerable<Projectile> list = Main.projectile.Where(x => x.Hitbox.Intersects(Hitbox));
            foreach (Projectile proj in list)
                if (proj.GetGlobalProjectile<ASledgeHammer.ShatteredGemBladeProjTest>().shotFromGun && Projectile.timeLeft > 20 && proj.active)
                {
                    Hit = true;
                    Projectile.timeLeft = 20;
                    proj.active = false;

                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 velocity = Vector2.Normalize(proj.velocity).RotatedBy(Main.rand.NextFloat(-0.6f, 0.6f)) * Main.rand.NextFloat(1.3f, 3);
                        Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ProjectileID.MolotovFire3, Projectile.damage, 0, Owner.whoAmI).scale = Main.rand.NextFloat(0.85f, 1.15f);
                    }
                }


        }



        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.tileCollide = true;
            // Set to transparant. This projectile technically lives as  transparant for about 3 frames
            Projectile.alpha = 255;
            // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
            Projectile.position.X = Projectile.position.X + Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            Projectile.damage = 30;
            Projectile.knockBack = 2f;
            Projectile.penetrate = 1;
            Projectile.ownerHitCheck = false;
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);

            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)

        {
            target.AddBuff(BuffID.OnFire, 140);

        }

        public override bool? CanHitNPC(NPC target) => !target.friendly;


        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            for (int i = 0; i < 30; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1f;

                int dustIndex1 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex1].velocity *= 2f;
            }
            // Fire Dust spawn

            player.GetModPlayer<Screenshake>().SmallScreenshake = true;

            Projectile.tileCollide = true;
            // Set to transparant. This projectile technically lives as  transparant for about 3 frames
            Projectile.alpha = 255;
            // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
            Projectile.position.X = Projectile.position.X + Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y + Projectile.height / 2;
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            Projectile.damage = 30;
            Projectile.knockBack = 2f;
            Projectile.penetrate = 1;
            Projectile.ownerHitCheck = false;
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);


            // These gores work by simply existing as a texture inside any folder which path contains "Gores/"
            int RumGore1 = Mod.Find<ModGore>("CopperGore1").Type;
            int RumGore2 = Mod.Find<ModGore>("CopperGore2").Type;
            int RumGore3 = Mod.Find<ModGore>("CopperGore3").Type;

            var entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 3; i++)
            {
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore1);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore2);
                Gore.NewGore(entitySource, Projectile.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), RumGore3);

            }

        }
    }
}






