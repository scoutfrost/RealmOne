using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.PreHM.Forest.Wattles
{
    public class WattleArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wattle Arrow");

        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 320;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void Kill(int timeleft)
        {
            for (var i = 0; i < 10; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Plantera_Green, 0f, 0f, 0, default, 1f);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.t_Lihzahrd, 0f, 0f, 0, default, 1f);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.NettleBurstLeft, Projectile.damage, Projectile.knockBack, Main.myPlayer);


        }
        public override void AI()
        {
            int dust1 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Plantera_Green, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Main.dust[dust1].noLight = false;
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].scale = 0.8f;
            Main.dust[dust1].velocity *= 0.5f;


            int dust2 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.t_Lihzahrd, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Main.dust[dust2].noLight = false;
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].scale = 0.8f;
            Main.dust[dust2].velocity *= 0.5f;

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] = 1f;
            Projectile.ai[1] = target.whoAmI;
            target.AddBuff(BuffID.Poisoned, Projectile.timeLeft);
            Projectile.velocity = (target.Center - Projectile.Center) * 0.75f;
            Projectile.netUpdate = true;
            Projectile.damage = 10;

        }
    }
}

