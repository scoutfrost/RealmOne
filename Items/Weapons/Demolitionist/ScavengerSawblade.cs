using Microsoft.Xna.Framework;
using RealmOne.Common.DamageClasses;
using RealmOne.Projectiles.Explosive;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Demolitionist
{
	public class ScavengerSawblade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scavenger Sawblade"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Throws a unstable and explosive sawblade"
				+ "\nRight click to detonate the sawblade into unstable sparks of electricity"
				+ "\n'Recommended to hold the sawblade in the middle not the blades!'"
				+ "\n'I'm not even sure how this is safe'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{
			Item.damage = 25;
			Item.DamageType = ModContent.GetInstance<DemolitionClass>();
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.DrinkLiquid;
			Item.holdStyle = ItemHoldStyleID.HoldGuitar;
			Item.crit = 4;
			Item.axe = 30;
			Item.scale = 0.8f;

			Item.shoot = ModContent.ProjectileType<ScavengerProjectile>();
			Item.shootSpeed = 8f;
			Item.noMelee = true;
			Item.channel = true;

		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
			var line = new TooltipLine(Mod, "", "");

			line = new TooltipLine(Mod, "ScavengerSawblade", "Demolition Stats:")
			{
				OverrideColor = new Color(220, 87, 24)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "ScavengerSawblade", "Type: Rolling Static")
			{
				OverrideColor = new Color(244, 202, 59)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "ScavengerSawblade", "Explosion Radius: NIL (Right Click: 6)")
			{
				OverrideColor = new Color(239, 91, 110)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "ScavengerSawblade", "Destroys Tiles: No")
			{
				OverrideColor = new Color(76, 156, 200)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "ScavengerSawblade", "Functionality: Infinitely pierces, stays on the ground, excellent for events")
			{
				OverrideColor = new Color(108, 200, 98)

			};
			tooltips.Add(line);

		}

		public override bool CanUseItem(Player player)
		{

			return player.ownedProjectileCounts[Item.shoot] < 4;

		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, "ScavengerSteel", 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

		}
		public override Vector2? HoldoutOffset()
		{
			var offset = new Vector2(6, 0);
			return offset;
		}
	}
}