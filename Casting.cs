using Terraria;
using Terraria.ModLoader;
using Solstice;

namespace Solstice
{
	public class Casting : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Critical Casting");
			Description.SetDefault("Magic damage increased by 9%.");
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.magicDamage += 0.09f;
		}
	}
}
