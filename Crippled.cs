using Terraria;
using Terraria.ModLoader;
using Solstice;

namespace Solstice
{
	public class Crippled : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Crippled");
			Description.SetDefault("Movement speed decreased.");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.velocity *= 0.85f;
		}
	}
}
