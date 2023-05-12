using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RealmOne.Common.Systems
{
    public class DownedBossSystem : ModSystem
    {
        public static bool downedPiggy;

        public static bool downedSquirmo;
        public static bool downedOutcropOutcast;


        public override void OnWorldLoad()
        {
            downedPiggy = false;

            downedSquirmo = false;
            downedOutcropOutcast = false;
        }

        public override void OnWorldUnload()
        {
            downedPiggy = false;

            downedSquirmo = false;
            downedOutcropOutcast = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {

            if (downedPiggy)
            {
                tag.Set("downedPiggy", (object)true);
            }
            if (downedSquirmo)
            {
                tag.Set("downedSquirmo", (object)true);
            }

            if (downedOutcropOutcast)
            {
                tag.Set("downedOutcropOutcast", (object)true);
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedPiggy = tag.ContainsKey("downedPiggy");

            downedSquirmo = tag.ContainsKey("downedSquirmo");
            downedOutcropOutcast = tag.ContainsKey("downedOutcropOutcast");

        }


    }
}
