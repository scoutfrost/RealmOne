using RealmOne.RealmPlayer;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Armor
{
	[AutoloadEquip(EquipType.Head)]

	public class BrassHead : ModItem
	{
		public override void SetStaticDefaults()
		{

			DisplayName.SetDefault("Brass Helmet");
			Tooltip.SetDefault("6% increased melee damage ");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			// If your head equipment should draw hair while drawn, use one of the following:
			// ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
			// ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
			// ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
			// ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
			// ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
		}

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Melee) += 0.5f;

		}

		// IsArmorSet determines what armor pieces are needed for the setbonus to take effect
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BrassBody>() && legs.type == ModContent.ItemType<BrassLegs>();
		}

		int Watertimer = 0;

		public override void UpdateArmorSet(Player player)
		{
      //      string tapDir = Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN");
            player.setBonus = "Double tap UP to gain Brass Might which increases the players defense by 10+ but 14% decreased movement & running speed\n10 second cooldown";
            Watertimer++;
            player.GetModPlayer<RealmModPlayer>().brassSet = true;


            if (Watertimer == 9)
			{
				int d = Dust.NewDust(player.position, player.width, player.height, DustID.CopperCoin);
				Main.dust[d].scale = 1f;
				Main.dust[d].velocity *= 0.5f;
				Main.dust[d].noLight = false;

				Watertimer = 0;
			}

			player.statDefense += 2;
		}

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "BrassIngot", 4)
            .AddTile(TileID.Furnaces)
            .Register();

        }
    }
}
