using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
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
			player.setBonus = "2+ defense" + "\nDouble tap down to gain 'Brass's Might which increases the players defense by 10+ but 15% decreased movement & running speed " + "\n10 second cooldown"; // This is the setbonus tooltip

			Watertimer++;

			if (Watertimer == 8)
			{
				int d = Dust.NewDust(player.position, player.width, player.height, DustID.CopperCoin);
				Main.dust[d].scale = 1f;
				Main.dust[d].velocity *= 0.5f;
				Main.dust[d].noLight = false;

				Watertimer = 0;
			}

			player.statDefense += 2;
		}
	}
}
