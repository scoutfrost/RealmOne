using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Bosses;
using RealmOne.Common.Systems;
using RealmOne.Items.BossSummons;
using RealmOne.Items.PaperUI;
using RealmOne.Items.Placeables.Furniture.BossThing;
using RealmOne.Items.Weapons.PreHM.BossDrops.SquirmoDrops;
using RealmOne.Items.Weapons.Summoner;
using RealmOne.Tiles;
using RealmOne.Vanities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using RealmOne;

namespace RealmOne
{
    public class Integrations
    {
        private void DoBossChecklistIntegration()
        {
            // The mods homepage links to its own wiki where the calls are explained: https://github.com/JavidPack/BossChecklist/wiki/Support-using-Mod-Call
            // If we navigate the wiki, we can find the "AddBoss" method, which we want in this case

            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
            {
                return;
            }

            // For some messages, mods might not have them at release, so we need to verify when the last iteration of the method variation was first added to the mod, in this case 1.3.1
            // Usually mods either provide that information themselves in some way, or it's found on the github through commit history/blame
          

            // The "AddBoss" method requires many parameters, defined separately below:

            // The name used for the title of the page
            string bossName = "Squirmo";

            // The NPC type of the boss
            int bossType = ModContent.NPCType<SquirmoHead>();

            // Value inferred from boss progression, see the wiki for details
            float weight = 0.7f;

            // Used for tracking checklist progress
            Func<bool> downed = () => DownedBossSystem.downedSquirmo;

            // If the boss should show up on the checklist in the first place and when (here, always)
            Func<bool> available = () => true;

            // "collectibles" like relic, trophy, mask, pet
            List<int> collection = new List<int>()
            {
                 ModContent.ItemType<GlobGun>(),
                        ModContent.ItemType<SquirmStaff>(),
                        ModContent.ItemType<SquirYo>(),
                        ModContent.ItemType<SquirmoLorePageOne>(),
                        ModContent.ItemType<TwinklingTwig>(),
                        ModContent.ItemType<SquirmoRelicItem>(),
                        ModContent.ItemType <SquirmoTrophyItem>(),
                        ModContent.ItemType<SquirmoMask>(),

            };

            // The item used to summon the boss with (if available)
            int summonItem = ModContent.ItemType<SquirmoSummon>();

            // Information for the player so he knows how to encounter the boss
            string spawnInfo = "Even from the past dread of worm adaptation, they havent really caused global damage. But for Squirmo, ever seeking revenge on human inhabitants is still a current warning for people.  Adhere the relief of the soil by defeating Squirmo!!";


            // The boss does not have a custom despawn message, so we omit it
            string despawnInfo = null;

            // By default, it draws the first frame of the boss, omit if you don't need custom drawing
            // But we want to draw the bestiary texture instead, so we create the code for that to draw centered on the intended location
            var customBossPortrait = (SpriteBatch sb, Rectangle rect, Color color) => {
                Texture2D texture = ModContent.Request<Texture2D>("RealmOne/Assets/Textures/SquirmoTexture").Value;
                Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                sb.Draw(texture, centered, color);
            };

            bossChecklistMod.Call(
                "AddBoss",
                
                bossName,
                bossType,
                weight,
                downed,
                available,
                collection,
                summonItem,
                spawnInfo,
                despawnInfo,
                customBossPortrait
            );

            // Other bosses or additional Mod.Call can be made here.
        }
    }
}
