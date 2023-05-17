using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmOne;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using RealmOne.Projectiles;
using Terraria.Localization;
using Terraria.Audio;
using RealmOne.Common.Systems;
using RealmOne.Items.Ammo;
using RealmOne.Items;
using RealmOne.Armor;

namespace RealmOne.Armor
{
    [AutoloadEquip(EquipType.Head)]

    public class PiggyHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Piggy Patroller Mask");
            Tooltip.SetDefault("5% increased damage but 5% decreased acceleration"
                + "\n'This is so uncomfortable, but it does the job'");

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
            Item.defense = 4; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.runAcceleration -= 0.05f;

        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PiggyBody>() && legs.type == ModContent.ItemType<PiggyLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "4% increased armour penetration to all weapons" + "\nGreatly increases coin pickup"; // This is the setbonus tooltip
            player.GetArmorPenetration(DamageClass.Generic) += 4f;
            player.goldRing = true;
        }



        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(Mod, "PiggyPorcelain", 5)
            .AddTile(TileID.Furnaces)
            .Register();


        }
    }
}
