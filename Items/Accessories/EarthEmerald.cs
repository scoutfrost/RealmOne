using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Accessories
{
	public class EarthEmerald : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earth Emerald"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("'If the Earth was shrunk into a gem'"
			  + "\n25% increased tool and weapon speed"
			  + "\nSpelunker, Night Owl, Shine buffs"
			  + "\nThese stats are better in the day");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		}

		public override void SetDefaults()
		{

			Item.width = 20;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.accessory = true;

		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{

			if (Main.dayTime)
			{
				player.GetAttackSpeed(DamageClass.Generic) += 0.5f;
				player.nightVision = true;
			}

			else
				player.GetDamage(DamageClass.Generic) += 0.3f;
			return;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 25);
			recipe.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe.AddIngredient(ItemID.Chain, 2);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			Recipe balls = CreateRecipe();
			balls.AddIngredient(ItemID.Bone, 25);
			balls.AddIngredient(ItemID.DemoniteBar, 10);
			balls.AddIngredient(ItemID.Chain, 2);
			balls.AddTile(TileID.Anvils);
			balls.Register();
		}
	}
}