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
    public class Type24StickNade : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Type24 Stick Grenade");
            Tooltip.SetDefault("Type 24 hand held stick grenade, used in long and precautious situations."
                + "\n'Best used in long distance relationships"
            + "\nBest use of the Stick Grenade is to throw it high in the air, then catch it and let it blow up in your face :)");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

        }

        public override void SetDefaults()
        {

            Item.damage = 14;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 10;
            Item.height = 26;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_GrenadeThrow");
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Type24StickNadeProj>();
            Item.shootSpeed = 14f;
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

            line = new TooltipLine(Mod, "Type24StickNade", "Demolition Stats:")
            {
                OverrideColor = new Color(220, 87, 24)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Type24StickNade", "Type: Stick Grenade")
            {
                OverrideColor = new Color(244, 202, 59)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Type24StickNade", "Explosion Radius: 9")
            {
                OverrideColor = new Color(239, 91, 110)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Type24StickNade", "Destroys Tiles: No")
            {
                OverrideColor = new Color(76, 156, 200)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Type24StickNade", "Functionality: Can throw further at higher speeds")
            {
                OverrideColor = new Color(108, 200, 98)

            };
            tooltips.Add(line);

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.Wood, 12);
            recipe.AddIngredient(ItemID.Grenade, 50);

            recipe.AddRecipeGroup("IronBar", 5);

            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();

        }
    }
}