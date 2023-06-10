using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace RealmOne.Armor;

[AutoloadEquip(EquipType.Head)]
public class AquaBlossomHead : ModItem
{
    private int Watertimer;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Aqua Blossom Lilyhat");
        Tooltip.SetDefault("5% increased magic crit chance");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.rare = 2;
        Item.defense = 2;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Magic) += 0.5f;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        if (body.type == ModContent.ItemType<AquaBlossomBody>())
        {
            return legs.type == ModContent.ItemType<AquaBlossomLegs>();
        }
        return false;
    }

    public override void UpdateArmorSet(Player player)
    {

        player.setBonus = "Shine effect. Even larger while underwater\nGills effect and flipper underwater\n2+ mana regen.";
        player.gills = true;
        Lighting.AddLight(player.position, 0.08f, 0.4f, 0.8f);
        Lighting.Brightness(2, 2);
        Watertimer++;
        if (Watertimer == 7)
        {
            int d = Dust.NewDust(player.position, player.width, player.height, 100, 0f, 0f, 0, default(Color), 1f);
            Main.dust[d].scale = 1.5f;
            Dust obj = Main.dust[d];
            obj.velocity *= 0.6f;
            Main.dust[d].noLight = false;
            Watertimer = 0;
        }
        player.manaRegen += 2;
    }
}
