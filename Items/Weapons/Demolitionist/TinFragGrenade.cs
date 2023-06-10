using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using RealmOne.Common.DamageClasses;
using RealmOne.Projectiles.Explosive;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Demolitionist
{
    public class TinFragGrenade : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tin Frag Grenade"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A commonly used hand thrown explosive, wrapped around in thick tin plating"
                + "\n'Best used combat situations. It's not something you want to juggle with"
            + "\nWith Frag Grenades, let the grenade cook by throwing it on the floor, just not on enemy impact, making the grenade penetrate and explode in a larger radius");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

        }

        public override void SetDefaults()
        {

            Item.damage = 15;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 10;
            Item.height = 24;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_GrenadeThrow");
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<TinFragGrenadeProj>();
            Item.shootSpeed = 5f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.maxStack = 99;
        }
        public override void HoldItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<HazardBuff>(), 2, true);

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "TinFragGrenade", "Demolition Stats:")
            {
                OverrideColor = new Color(220, 87, 24)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "TinFragGrenade", "Type: Frag Grenade")
            {
                OverrideColor = new Color(244, 202, 59)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "TinFragGrenade", "Explosion Radius: 10")
            {
                OverrideColor = new Color(239, 91, 110)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "TinFragGrenade", "Destroys Tiles: No")
            {
                OverrideColor = new Color(76, 156, 200)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "TinFragGrenade", "Functionality: Weak")
            {
                OverrideColor = new Color(108, 200, 98)

            };
            tooltips.Add(line);

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.TinBar, 12);
            recipe.AddIngredient(ItemID.Grenade, 5);

            recipe.AddRecipeGroup("IronBar", 8);

            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }
}