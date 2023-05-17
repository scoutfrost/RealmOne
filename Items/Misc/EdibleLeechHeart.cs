using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealmOne.Items.Misc
{
	public class EdibleLeechHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Edible Leech Heart"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("'Permanently increases maximum life by 25'");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

			ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
				new Color(240, 50, 200),
				new Color(240, 80, 220),
				new Color(250, 140, 240)
			};
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 20000;
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 999;
			Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/LeechHeartEat");
			Item.consumable = true;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.damage = 0;
			Item.DamageType = DamageClass.Melee;
			Item.scale = 0.7f;
			Item.useStyle = ItemUseStyleID.EatFood;

		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood, 0f, 0f, 35, default, 3f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].velocity *= 2f;

		}

		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Texture2D texture = TextureAssets.Item[Item.type].Value;

			Rectangle frame;

			if (Main.itemAnimations[Item.type] != null)
				frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
			else
				frame = texture.Frame();

			Vector2 frameOrigin = frame.Size() / 2f;
			var offset = new Vector2(Item.width / 2 - frameOrigin.X, Item.height - frame.Height);
			Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

			float time = Main.GlobalTimeWrappedHourly;
			float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

			time %= 4f;
			time /= 2f;

			if (time >= 1f)
				time = 2f - time;

			time = time * 0.5f + 0.5f;

			for (float i = 0f; i < 1f; i += 0.25f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;
				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(210, 50, 120, 70), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			for (float i = 0f; i < 1f; i += 0.34f)
			{
				float radians = (i + timer) * MathHelper.TwoPi;
				spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(196, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
			}

			return true;
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
			var line = new TooltipLine(Mod, "", "");

			line = new TooltipLine(Mod, "EdibleLeechHeart", "'The heart of the hellborn protector of the world'")
			{
				OverrideColor = new Color(254, 40, 220)

			};
			tooltips.Add(line);

			line = new TooltipLine(Mod, "EdibleLeechHeart", "'Gives off a sense of biological immortality or either eadable cardiac arrest'")
			{
				OverrideColor = new Color(200, 50, 120)

			};
			tooltips.Add(line);
		}
	}
}