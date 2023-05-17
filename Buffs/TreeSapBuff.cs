using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealmOne.Buffs;
using RealmOne.Items;

namespace RealmOne.Buffs
{
    public class TreeSapBuff : ModBuff
    {
        public override void SetStaticDefaults()


        {
            DisplayName.SetDefault("Tree Sap Buff");
            Description.SetDefault("Calming effect and 5% increased movement speed while in the forest");


        }


        public override void Update(Player player, ref int buffIndex)
        {

            player.calmed = true;
            player.moveSpeed += 0.05f;


        }




    }
}