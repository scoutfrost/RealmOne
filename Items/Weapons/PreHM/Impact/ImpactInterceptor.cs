using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Common.Systems;
using RealmOne.Projectiles.Magic;
using RealmOne.RealmPlayer;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Items.Weapons.PreHM.Impact
{

	public class ImpactInterceptor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Impact Interceptor");
			Tooltip.SetDefault("Calls down randomly positioned pulse lasers that spark into electricity when homed onto an enemy"
				+ "\nThe lasers depend on the mouse position when firing"
				+ "\n'No WIFI password required'");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemGlowy.AddItemGlowMask(Item.type, "RealmOne/Items/Weapons/PreHM/Impact/ImpactInterceptor_Glow");

		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;

			Item.autoReuse = true;
			Item.useTurn = true;
			Item.mana = 8;
			Item.damage = 6;
			Item.DamageType = DamageClass.Magic;
			Item.knockBack = 2f;
			Item.noMelee = true;
			Item.rare = ItemRarityID.Blue;
			Item.shootSpeed = 48f;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(silver: 90);
			Item.shoot = ProjectileType<PulseProj>();
			Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Sonar");
			Item.scale = 0.7f;
			Item.reuseDelay = 32;

		}

		public override Vector2? HoldoutOffset()
		{
			var offset = new Vector2(3, 0);
			return offset;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.X;

			if (ceilingLimit > player.Center.X + 200f)
				ceilingLimit = player.Center.Y + 200f;

			for (int i = 0; i < 6; i++)
			{
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				position.X += 100 * i;
				Vector2 heading = target - position;

				Vector2 speed = Main.rand.NextVector2CircularEdge(3f, 3f);
				var d = Dust.NewDustPerfect(Main.LocalPlayer.Center, DustID.Flare_Blue, speed * 5, Scale: 2.5f);
				;
				d.noGravity = true;

				if (heading.X < 4f)
					heading.X *= 4f;

				if (heading.Y < 40f)
					heading.Y = 40f;

				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, heading, ProjectileType<PulseProj>(), damage, knockback, player.whoAmI, 0f, ceilingLimit);
			}

			return false;
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = Request<Texture2D>("RealmOne/Items/Weapons/PreHM/Impact/ImpactInterceptor_Glow", AssetRequestMode.ImmediateLoad).Value;
			spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
					Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),

				Color.LightCyan,
				rotation,
				texture.Size() * 0.5f,
				scale,
				SpriteEffects.None,
				0f
			);
		}

		public override bool OnPickup(Player player)
		{
			SoundEngine.PlaySound(rorAudio.PulsaPickup);

			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1)
			.AddIngredient(Mod, "ImpactTech", 12)

			.AddTile(TileID.Anvils)
			.Register();

		}
	}
}