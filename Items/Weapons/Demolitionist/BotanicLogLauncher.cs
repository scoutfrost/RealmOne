using Microsoft.Xna.Framework;
using RealmOne.Buffs;
using RealmOne.Common.DamageClasses;
using RealmOne.Projectiles.HeldProj;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Weapons.Demolitionist
{

    public class BotanicLogLauncher : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Botanic Log Launcher"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Hold out a heavy and floral cannon that shoots oversized bouncing flower logs!"
                + "\nTheres a family of worms living in it!"
                + "\nMust reload the launcher after each shot");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = ModContent.GetInstance<DemolitionClass>();
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<BotanicLogLauncherH>();
            Item.crit = 6;
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.value = Item.buyPrice(gold: 2, silver: 3);
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.UseSound = SoundID.DD2_GoblinBomberThrow;

        }
        public override void HoldItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<HazardBuff>(), 2, true);

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "", "");

            line = new TooltipLine(Mod, "BotanicLogLauncher", "Demolition Stats:")
            {
                OverrideColor = new Color(220, 87, 24)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BotanicLogLauncher", "Type: Charged Rocket Launcher")
            {
                OverrideColor = new Color(244, 202, 59)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BotanicLogLauncher", "Rocket Explosion Radius: 13")
            {
                OverrideColor = new Color(239, 91, 110)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BotanicLogLauncher", "Destroys Tiles: No")
            {
                OverrideColor = new Color(76, 156, 200)

            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "BotanicLogLauncher", "Functionality: Bouncing rockets make good work of large groups")
            {
                OverrideColor = new Color(108, 200, 98)

            };
            tooltips.Add(line);

        }
    }
}