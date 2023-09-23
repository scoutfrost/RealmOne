using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using RealmOne.Projectiles;
using Microsoft.Xna.Framework.Graphics;

namespace RealmOne.Items.Weapons.PreHM.BloodMoon
{
    public class Crimclub : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 66;
            Item.crit = 45;
            Item.damage = 34;
            Item.knockBack = 10f;

            Item.useAnimation = 46;
            Item.useTime = 46;
            Item.noUseGraphic = true;
            Item.autoReuse = false;
            Item.noMelee = true;


            Item.DamageType = DamageClass.Melee;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<CrimclubSwing>();
        }
    }

    public class CrimclubSwing : ModProjectile
    {
        public override string Texture => "RealmOne/Items/Weapons/PreHM/BloodMoon/Crimclub";

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCsAndTiles.Add(index);
        }


    }


}
