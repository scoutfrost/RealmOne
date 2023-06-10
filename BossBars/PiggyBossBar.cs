using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Bosses;
using RealmOne.NPCs.Enemies.MiniBoss;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

namespace RealmOne.BossBars
{
   
    public class PiggyBossBar : ModBossBar
    {




        public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)/* tModPorter Note: life and shield current and max values are now separate to allow for hp/shield number text draw */
        {
            

            NPC npc = Main.npc[info.npcIndexToAimAt];
            if (!npc.active)
                return false;


            life = npc.life;
            lifeMax = npc.lifeMax;

            if (npc.ModNPC is PossessedPiggy body)
            {
                shield = body.MinionHealthTotal;
                shieldMax = body.MinionMaxHealthTotal;
            }


            return true;
        }
    }
}