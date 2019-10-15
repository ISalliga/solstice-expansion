using Terraria;
using Terraria.ModLoader;
using Solstice;

namespace Solstice
{
	public class GuardStunned : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Stunned");
			Description.SetDefault("You cannot move.");
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
            npc.velocity.X /= 2;
            if (!npc.noGravity && npc.velocity.Y < 0) npc.velocity.Y = (npc.velocity.Y / 2) + 3;
            if (npc.noGravity) npc.velocity.Y /= 2;
		}
	}
}
