using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System;
using RealmOne.Common.Systems;
using RealmOne.Common;
using System.Collections.Generic;
using RealmOne.Common.DamageClasses;
using RealmOne.Projectiles.Explosive;

namespace RealmOne.Items.Weapons.Demolitionist
{
    public class C4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("C4"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Throws a sticky explosive that can be detonated on right click"
                + "\nMaximum of 5 C4 can be placed on the ground"
                + "\nThe C4 will eventually explode after 20 seconds");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = 1;
            Item.knockBack = 2f;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.crit = 4;
            
            Item.scale = 0.9f;

            Item.shoot = ModContent.ProjectileType<C5Proj>();
            Item.shootSpeed = 6f;
            Item.noMelee = true;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.consumable = true;
            Item.maxStack = 99;

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "C4", "Demolition Stats:")
            {
                OverrideColor = new Color(220, 87, 24)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "C4", "Type: Sticky Explosive")
            {
                OverrideColor = new Color(244, 202, 59)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "C4", "Explosion Radius: 10")
            {
                OverrideColor = new Color(239, 91, 110)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "C4", "Destroys Tiles: No")
            {
                OverrideColor = new Color(76, 156, 200)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "C4", "Functionality: Sticks to tiles, explodes via right click")
            {
                OverrideColor = new Color(108, 200, 98)

            };
            tooltips.Add(line);

        }


        public override bool CanUseItem(Player player)
        {

            return player.ownedProjectileCounts[Item.shoot] < 5;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "ScavengerSteel", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(6, 0);
            return offset;
        }

    }
}