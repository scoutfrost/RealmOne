using Microsoft.Xna.Framework;
using RealmOne.Common.DamageClasses;
using RealmOne.Projectiles.Explosive;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Demolitionist
{
	public class TheGardenGlove : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Garden Glove"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("An Essential Gardening tool!"
				+ "\nThrow a worm filled mud ball!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{

			Item.damage = 14;
			Item.DamageType = ModContent.GetInstance<DemolitionClass>();
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 38;
			Item.useAnimation = 38;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SquirmoMudBubbleBlow");
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<WormBall>();
			Item.shootSpeed = 10f;
			Item.noMelee = true;
			Item.scale = 0.6f;

		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
			var line = new TooltipLine(Mod, "", "");

			line = new TooltipLine(Mod, "TheGardenGlove", "'COMPOST N' GET LOST, FREAKS!!'")
			{
				OverrideColor = new Color(195, 118, 155)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "TheGardenGlove", "Demolition Stats:")
			{
				OverrideColor = new Color(220, 87, 24)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "TheGardenGlove", "Type: Spawns Projectile Via Impact")
			{
				OverrideColor = new Color(244, 202, 59)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "TheGardenGlove", "Explosion Radius: 4")
			{
				OverrideColor = new Color(239, 91, 110)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "TheGardenGlove", "Destroys Tiles: No")
			{
				OverrideColor = new Color(76, 156, 200)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "TheGardenGlove", "Functionality: Throws a worm ball that splits into homing Squirms, great overall")
			{
				OverrideColor = new Color(108, 200, 98)

			};
			tooltips.Add(line);

		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6f, 4f);
		}
	}
}