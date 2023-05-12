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
    [AutoloadEquip(EquipType.Body)]

    public class BrassBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Brass Chestplate");
            Tooltip.SetDefault("8% knockback and weapon speed"
                +"\n'A knight in rusty armour!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public override void SetDefaults()
        { 
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 5; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {

            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
            player.GetKnockback(DamageClass.Generic) += 0.08f;
        }

        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect


        // UpdateArmorSet allows you to give set bonuses to the armor.



    }
}
