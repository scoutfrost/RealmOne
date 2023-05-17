using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.UI;
using Terraria.Audio;
using Microsoft.Xna.Framework.Audio;
using RealmOne.Common.Systems;
using RealmOne.Items.Placeables;

namespace RealmOne.Items.Others
{
    public class LoreScroll1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lore Scroll (Squirmo)"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Open up a scroll to reveal the secrets of the soil" + "\nAppears in the chat log");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;


        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 20000;
            Item.rare = ItemRarityID.Master;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_Scroll");
            Item.maxStack = 1;




        }

        public override bool CanUseItem(Player player)
        {


            if (Main.netMode != NetmodeID.Server)
                Main.NewText(Language.GetTextValue("Years have passed and the worms act like nothing happened and kept swarming the wet and earthy layer. The population of worms were so vast that they even adapted to surface rocks and logs and hid in them for seasons. Even for the past dread of worm adaptation, they haven’t really caused global damage. But for Squirmo, ever seeking revenge on human inhabitants is still a current warning for people")

                , 241, byte.MaxValue, 180);



            return true;

        }
        /*  public override void OnConsumeItem(Player player)
          {
              if (Main.netMode != 2)
              {
                  Main.NewText(Language.GetTextValue("Years have passed and the worms act like nothing happened and kept swarming the wet and earthy layer. The population of worms were so vast that they even adapted to surface rocks and logs and hid in them for seasons.Even for the past dread of worm adaptation, they haven’t really caused global damage. But for Squirmo, ever seeking revenge on human inhabitants is still a current warning for people")

                  , (byte)241, byte.MaxValue, (byte)180);
              }

          }*/
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Parchment", 5);
            recipe.AddIngredient(ItemID.Worm, 5);

            recipe.AddTile(Mod, "SquirmoRelic");
            recipe.Register();
        }
    }
}