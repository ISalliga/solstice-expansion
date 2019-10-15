using Terraria;
using Terraria.ModLoader;
using Solstice;

namespace Solstice
{
	public class Crushed : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Crushed");
			Description.SetDefault("Damage taken increased by 16%.");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.GetGlobalNPC<SolsticeGlobalNPC>().crushed = true;
        }
	}
}
