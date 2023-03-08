using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using RealmOne;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Projectiles;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Audio;
using System.Diagnostics.Metrics;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.GameContent;
using RealmOne.Common;
using RealmOne.Common.Systems;
using static Terraria.ModLoader.ModContent;

namespace RealmOne.Dusts
{
    public class FlamerDust : ModDust
    {


        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.6f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = true; // Makes the dust have no gravity.
            dust.noLight = true; // Makes the dust emit no light.
            dust.scale *= 0.99f; // Multiplies the dust's initial scale by 1.5.
        }

        public override bool Update(Dust dust)
        { // Calls every frame the dust is active
            dust.position += dust.velocity;
            dust.scale *= 0.99f;

            float light = 0.3f * dust.scale;

            Lighting.AddLight(dust.position, light, light, light);

            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }

            return false; // Return false to prevent vanilla behavior.
        }

       
       
    }
}


