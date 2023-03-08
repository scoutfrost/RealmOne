using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.Audio;
using System;
using System.Collections.Generic;
using RealmOne.Common.DamageClasses;
using RealmOne.Buffs;
using RealmOne.RealmPlayer;
using RealmOne.Projectiles.Explosive;

namespace RealmOne.Items.Weapons.Demolitionist
{
    public class BattleboatBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Battleboat Bomb"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'Without a Cannon what's the use of this explosive'"
            + "\n'It has an extremely fast detonation time, so its either you die or you die'"
            + "\nSelf Explode yourself, dealing huge damage to enemies and you"
             + "\n'Would this classify as Euthanism?'");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 54;
            Item.useAnimation = 54;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 10f;
            Item.value = 30000;
            Item.rare = 8;
            Item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<BattleboatBombProj>();
            Item.crit = 8;



        }
        public override bool OnPickup(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item61);
            return true;
        }


        public override void HoldItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<HazardBuff>(), 2, true);

        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<Screenshake>().SmallScreenshake = true;

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "BattleboatBomb", "Demolition Stats:")
            {
                OverrideColor = new Color(220, 87, 24)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BattleboatBomb", "Type: Self-Explosion")
            {
                OverrideColor = new Color(244, 202, 59)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BattleboatBomb", "Explosion Radius: 35")
            {
                OverrideColor = new Color(239, 91, 110)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BattleboatBomb", "Destroys Tiles: No")
            {
                OverrideColor = new Color(76, 156, 200)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BattleboatBomb", "Functionality: MASSIVE explosion radius, damages user. Great for crowds and large health enemies")
            {
                OverrideColor = new Color(108, 200, 98)

            };
            tooltips.Add(line);

        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Bomb, 50)
           .AddIngredient(Mod, "GoopyGrass", 7)
            .AddIngredient(ItemID.ExplosivePowder, 25)
            .AddIngredient(ItemID.Cannonball, 5)
            .AddIngredient(ItemID.Fireblossom, 8)
            .AddTile(TileID.Mythril)
            .Register();

        }




        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(6, 0);
            return offset;
        }

    }
}