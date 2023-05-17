using RealmOne.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Others
{
	public class OtherWorldly : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("OtherWorldly Stabiliser"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Toggles Transcendental Entropy Mode."
				+ "\n'He always knew there was someone stupid enough to use this'"
				+ "\n'Only useable for the one that desires utter control of the universe'"
				+ "\nOnly available in Master Mode"
				+ "\n'Changes the way you play Terraria'"
				+ "\nMakes enemies way more stronger (No AI Changes)"
				+ "\n'Bosses are now as strong as Cthulhu originally deemed them to be'"
				+ "\nHealing potions take longer to re consume them"
				+ "\nBetter loot and exclusive powerful drops"
				+ "\nShop prices are as valuable as Lay's Chips LUL"
				+ "\nRealms are now more difficult and the enemies of each realm are harder"
				+ "\nTraps and fall damage now do ridiculous amount of damage"
				+ "\nLife Crystals only heal for 10 health now LMFAO HEEHEEHAW"
				+ "\nLovecraftian Bosses are universally destructive"
				+ "\n'I've always wondered how far you will go, pathetic!'"
			   + "\n**TEST ITEM!! DOES NOT FULLY WORK**");

		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 10;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = 90000;
			Item.rare = ItemRarityID.Master;
			Item.UseSound = SoundID.DD2_EtherianPortalOpen;
			Item.autoReuse = false;
			Item.masterOnly = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<EmptyLocket>(), 1);
			recipe.Register();
		}
	}
}