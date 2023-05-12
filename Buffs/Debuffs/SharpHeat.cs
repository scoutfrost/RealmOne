using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace RealmOne.Buffs.Debuffs
{
    public class SharpHeat : ModBuff
    {
        public override string Texture => "RealmOne/Buffs/HellfireVodkaBuff";

        private readonly Color Blue = new Color(0, 114, 201);
        private readonly Color Orange = new Color(255, 194, 89);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharp Heat");
            Description.SetDefault("Burns the enemy with sharp heat, burns faster if their health is under 4");

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Terraria.ID.BuffID.Sets.LongerExpertDebuff[Type] = true;
        }


        public override void Update(NPC npc, ref int buffIndex)
        {

            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldFlame);
                Main.dust[dust].velocity.X *= 0.1f;
                Main.dust[dust].velocity.Y *= 0.4f;
            }
            if (!npc.friendly)
            {
                if (npc.defense < 5)
                    npc.lifeRegen -= 12;

                
                else
                    npc.lifeRegen -= 4;

                
            }
        }
    }
}
