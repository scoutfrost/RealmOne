
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Items.Placeables;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using System.Collections.Generic;
using RealmOne.NPCs.Critters;
using RealmOne.Items.Food;
using RealmOne.NPCs.Enemies.Forest;
using RealmOne.Tiles.Blocks;
using RealmOne.Items.Misc.Plants;

namespace RealmOne.Biomes.Farm
{
	public class FarmTree : ModTree
	{
		
		public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings {
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 11f / 72f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};

        public override void SetStaticDefaults() => GrowsOnTileId = new int[] { ModContent.TileType<FarmSoil>() };

        public override int CreateDust() => DustID.WoodFurniture;
		public override int TreeLeaf() => GoreID.TreeLeaf_Palm;
        public override int DropWood() => ModContent.ItemType<TatteredWood>();

        public override Asset<Texture2D> GetTexture() => ModContent.Request<Texture2D>("RealmOne/Biomes/Farm/FarmTree", AssetRequestMode.ImmediateLoad);
        public override Asset<Texture2D> GetTopTextures() => ModContent.Request<Texture2D>("RealmOne/Biomes/Farm/FarmTree_Tops", AssetRequestMode.ImmediateLoad);
		public override Asset<Texture2D> GetBranchTextures() => ModContent.Request<Texture2D>("RealmOne/Biomes/Farm/FarmTree_Branches", AssetRequestMode.ImmediateLoad);
      

		public override int SaplingGrowthType(ref int style) {
			style = 0;
			return ModContent.TileType<FarmSapling>();
		}

        public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
            topTextureFrameWidth = 138;
            topTextureFrameHeight = 84;
        }
        // Branch Textures
        public override bool Shake(int x, int y, ref bool createLeaves)
        {

            WeightedRandom<FarmTreeEnum> options = new WeightedRandom<FarmTreeEnum>();
            options.Add(FarmTreeEnum.None, 0.8f);
            options.Add(FarmTreeEnum.Acorn, 0.8f);
            options.Add(FarmTreeEnum.Wood, 0.8f);
            options.Add(FarmTreeEnum.Critter, 0.6f);
            options.Add(FarmTreeEnum.Fruit, 0.6);

            FarmTreeEnum effect = options;

//# A small helper from Spirit Mod. All credits go to them for this lil tree helper.
            if (effect == FarmTreeEnum.Acorn)
            {
                Vector2 offset = this.GetRandomTreePosition(Main.tile[x, y]);
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, ModContent.ItemType<Wattle>(), Main.rand.Next(1, 2));
            }
            else if (effect == FarmTreeEnum.Wood)
            {
                Vector2 offset = this.GetRandomTreePosition(Main.tile[x, y]);
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, ModContent.ItemType<TatteredWood>(), Main.rand.Next(5, 10));
            }
            else if (effect == FarmTreeEnum.Critter)
            {
                int repeats = Main.rand.Next(1, 3);

                for (int i = 0; i < repeats; ++i)
                {
                    Vector2 offset = this.GetRandomTreePosition(Main.tile[x, y]);
                    Vector2 pos = new Vector2(x * 16, y * 16) + offset;
                    int npc = NPC.NewNPC(WorldGen.GetItemSource_FromTreeShake(x, y), (int)pos.X, (int)pos.Y, ModContent.NPCType<MagpieNPC>(), ModContent.NPCType<MangoBat>());
                    Main.npc[npc].velocity = new Vector2(Main.rand.NextFloat(1, 3), 0).RotatedByRandom(MathHelper.TwoPi);
                }
            }
            else if (effect == FarmTreeEnum.Fruit)
            {
                Vector2 offset = this.GetRandomTreePosition(Main.tile[x, y]);
                Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2(x, y) * 16 + offset, Main.rand.NextBool() ? ModContent.ItemType<ToastedNutBar>() : ModContent.ItemType<VegeToast>(),ModContent.ItemType<JamToast>(), 1);
            }
            return false;
        }


       
        public enum FarmTreeEnum
        {
            None = 0,
            Acorn,
            Wood,
            Critter,
            Gore,
            Fruit,
        }


    }

}