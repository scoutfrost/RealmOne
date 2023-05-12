using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using RealmOne.Common.Systems;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using ReLogic.Content;

namespace RealmOne.Items.Weapons.PreHM.Forest
{
    public class RustedCenturionSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusted Centurion Sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A common weapon used from the Centurions"
             + "\nThe sword is still sharp but very rusty, causing the sword to deal 2+ armour penetration to enemies.");


            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }


        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Melee;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(silver: 88, copper: 3);
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.crit = 1;
            Item.UseSound = new SoundStyle($"{nameof(RealmOne)}/Assets/Soundss/SFX_MetalSwing");
            Item.autoReuse = true;
            Item.ArmorPenetration = 2;
            Item.scale = 1.5f;
        }





        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Iron, 1f, 1f, 0, default, 0.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1.5f;

        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Collision.AnyCollision(Item.position + Item.velocity, Item.velocity, Item.width, Item.height);
            SoundEngine.PlaySound(rorAudio.SFX_Porce);
            target.AddBuff(BuffID.BrokenArmor, 200);
            for (int i = 0; i < 10; i++)
            {

                Vector2 speed = Main.rand.NextVector2Square(-1f, 1f);

                Dust d = Dust.NewDustPerfect(target.position, DustID.Iron, speed * 5, Scale: 1f); ;
                d.noGravity = true;

            }




        }



        public override Vector2? HoldoutOffset()
        {
            Vector2 offset = new Vector2(6, 0);
            return offset;
        }





    }
}