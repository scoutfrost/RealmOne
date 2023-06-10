using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Projectiles.Explosive
{
    public class FlowerLogProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flower Log");

        }

        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 50;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            Projectile.timeLeft = 320;
            Projectile.aiStyle = 0;
            AIType = ProjectileID.MiniNukeRocketII;
            Projectile.CloneDefaults(ProjectileID.MiniNukeRocketII);
        }
        public override void Kill(int timeleft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.t_LivingWood, 0f, 0f, 0, default, 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Grass, 0f, 0f, 0, default, 2f);

            }

            Collision.AnyCollision(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.DD2_GoblinBomb);

        }
    }
}