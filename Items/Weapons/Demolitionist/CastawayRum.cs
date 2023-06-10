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
    public class CastawayRum : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Castaway Rum"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A washed up old bottle of strong pineapple, vanilla, coffee beans and paprika"
                + "\n'The bottle explodes into explosive and sharp pieces of glass'"
                + "\nRight click to drink the rum, granting the Salty Swig buff, an exlusive buff to Drink-Related Items"
                + "\nSalty Swig buff grants 8% increased weapon speed and movement speed but you have -5 defence"
                + "\n'Surprised there's no fish or sand in the bottle!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

        }

        public override void SetDefaults()
        {

            Item.damage = 18;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_GrenadeThrow");
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CastawayRumProj>();
            Item.shootSpeed = 6f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.maxStack = 99;
            Item.scale = 0.7f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "CastawayRum", "Demolition Stats:")
            {
                OverrideColor = new Color(220, 87, 24)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "CastawayRum", "Type: Molotov Flame Impact")
            {
                OverrideColor = new Color(244, 202, 59)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "CastawayRum", "Explosion Radius: 6")
            {
                OverrideColor = new Color(239, 91, 110)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "CastawayRum", "Destroys Tiles: No")
            {
                OverrideColor = new Color(76, 156, 200)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "CastawayRum", "Functionality: Good for Crowd Control for weaker enemies")
            {
                OverrideColor = new Color(108, 200, 98)

            };
            tooltips.Add(line);

        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {

                Item.useStyle = ItemUseStyleID.DrinkLiquid;
                Item.useTime = 50;
                Item.useAnimation = 50;

                Item.width = 20;
                Item.height = 20;
                Item.shoot = ProjectileID.None;
                Item.maxStack = 99;

                Item.value = 500;
                Item.rare = ItemRarityID.Green;
                Item.consumable = true;
                Item.buffType = ModContent.BuffType<SaltySwig>();
                Item.buffTime = 900;
                Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/LightbulbShine");

            }

            else
            {

                Item.damage = 19;
                Item.DamageType = ModContent.GetInstance<DemolitionClass>();
                Item.width = 24;
                Item.height = 24;
                Item.useTime = 40;
                Item.useAnimation = 40;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.knockBack = 5;
                Item.value = 10000;
                Item.rare = ItemRarityID.Green;
                Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_GrenadeThrow");
                Item.autoReuse = true;
                Item.shoot = ModContent.ProjectileType<CastawayRumProj>();
                Item.shootSpeed = 6f;
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.consumable = true;
                Item.maxStack = 99;
                Item.scale = 0.7f;
            }

            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.TinBar, 12);
            recipe.AddIngredient(ItemID.Grenade, 50);

            recipe.AddRecipeGroup("IronBar", 12);

            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();

        }
    }
}