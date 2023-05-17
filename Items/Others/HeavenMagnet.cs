using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RealmOne.Projectiles.Other;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RealmOne.Items.Others
{
	public class HeavenMagnet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heaven's Magnet"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Magnet in random loot from the heavens!!"
				+ "\nHas a cooldown of 5 minutes");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;

		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 60000;
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.UseSound = SoundID.DD2_DarkMageHealImpact;
			Item.useTime = 70;
			Item.useAnimation = 70;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shoot = ModContent.ProjectileType<LootBalloon>();
			Item.shootSpeed = 4f;
			Item.reuseDelay = 200;

		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 300f)
				ceilingLimit = player.Center.Y - 300f;
			// Loop these functions 3 times.
			for (int i = 0; i < 1; i++)
			{
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				position.Y -= 100 * i;
				Vector2 heading = target - position;

				if (heading.Y < 0f)
					heading.Y *= -1f;

				if (heading.Y < 20f)
					heading.Y = 20f;

				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(1, 1) * 0.01f;
				Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<LootBalloon>(), damage * 0, knockback, player.whoAmI, 0f, ceilingLimit);
			}

			return false;

		}
		public override bool PreDrawInInventory(SpriteBatch sB, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			for (int i = 0; i < 1; i++)
			{
				int num7 = 16;
				float num8 = (float)(Math.Cos(Main.GlobalTimeWrappedHourly % 2.4 / 2.4 * MathHelper.TwoPi) / 5 + 0.4);
				SpriteEffects spriteEffects = SpriteEffects.None;
				Texture2D texture = TextureAssets.Item[Item.type].Value;
				var vector2_3 = new Vector2(TextureAssets.Item[Item.type].Value.Width / 2, TextureAssets.Item[Item.type].Value.Height / 1 / 2);
				var color2 = new Color(249, 254, 159, 140);
				Rectangle r = TextureAssets.Item[Item.type].Value.Frame(1, 1, 0, 0);
				for (int index2 = 0; index2 < num7; ++index2)
				{
					Color color3 = Item.GetAlpha(color2) * (0.65f - num8);
					Main.spriteBatch.Draw(texture, position + new Vector2(7, 8), new Microsoft.Xna.Framework.Rectangle?(r), color3, 0f, vector2_3, Item.scale * .30f + num8, spriteEffects, 0.0f);
				}
			}

			return true;
		}
		private int cooldown = 1200; // 20 minutes in ticks
		private int timer = 0;
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
			var line = new TooltipLine(Mod, "", "");

			line = new TooltipLine(Mod, "HeavenMagnet", "'Some would even say this is a gift from God!'")
			{
				OverrideColor = new Color(220, 230, 149)

			};
			tooltips.Add(line);

		}

		public override bool? UseItem(Player player)
		{
			if (player.HasBuff(ModContent.BuffType<MagnetBuff>()))
				;
			{
				if (Main.netMode != NetmodeID.Server)
					Main.NewText(Language.GetTextValue("You are already under the effect of the Heaven's Magnet Gift")

					, 150, 243, 244);
			}

			player.AddBuff(ModContent.BuffType<MagnetBuff>(), 5 * 60 * 60);

			if (Main.netMode != NetmodeID.Server)
				Main.NewText(Language.GetTextValue($"\n[i:{ItemID.FallenStar}]The heaven has accepeted your wish, you have been granted an item![i:{ItemID.FallenStar}]"), 150, 243, 244);

			return true;
		}
	}

	public class MagnetBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heaven's Magnet Buff");
			Description.SetDefault("Gift from God!");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = false;
		}
		public override void Update(Player player, ref int buffIndex)
		{
			// Check if the buff has been active for at least 20 minutes
			if (player.buffTime[buffIndex] % (5 * 60 * 60) == 0)
				// Give the player a random item
				if (Main.netMode != NetmodeID.Server)
					Main.NewText(Language.GetTextValue($"\n[i:{ItemID.FallenStar}]The heaven has accepeted your wish, you have been granted an item![i:{ItemID.FallenStar}]"), 150, 243, 244);
		}
	}
}