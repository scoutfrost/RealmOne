using RealmOne.RealmPlayer;
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


        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3; // 
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
