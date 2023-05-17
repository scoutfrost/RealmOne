using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
	public class IronGunBarrel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Gun Barrel"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Crafts metal based guns with a single barrel"
				+ "\n'The barrel is sturdy, secure but quite heavy'"
				+ "\n'Common use of it is typically used for traditional guns with strong or medium firepower");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

		}

		public override void SetDefaults()
		{
			Item.material = true;
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 99;
			Item.value = Item.buyPrice(gold: 2, silver: 2);

		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.IronBar, 16)
			.AddTile(TileID.SharpeningStation)
			.Register();

		}
	}
}