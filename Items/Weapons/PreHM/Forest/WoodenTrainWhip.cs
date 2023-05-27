using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using RealmOne.Rarities;
using RealmOne.Projectiles.Whip;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class WoodenTrainWhip : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<WoodenTrainProjectile>(), 10, 3, 4);
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.rare = ModContent.RarityType<ModRarities>();
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
        }


        public override bool MeleePrefix()
        {
            return true;
        }
    }
}
