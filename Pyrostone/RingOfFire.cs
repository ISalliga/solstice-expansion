using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Solstice;

namespace Solstice.Pyrostone
{
	public class RingOfFire : ModBuff
	{
        int timer = 0;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ring of Fire");
			Description.SetDefault("Enemies within the ring will be burned");
		}

		public override void Update(Player player, ref int buffIndex)
		{
            timer++;
            if (timer % 3 == 0)
            {
                Vector2 vel = new Vector2(0, 240);
                for (int i = 0; i <= 90; i++)
                {
                    if (Main.rand.NextBool(3))
                    {
                        int dust = Dust.NewDust(player.Center + vel, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f);
                        Main.dust[dust].velocity = player.DirectionFrom(player.Center + vel) + (player.velocity);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                    }
                    vel = vel.RotatedBy(MathHelper.ToRadians(360 / 90));
                }
            }

            if (timer % 6 == 0)
            {
                Vector2 vel = new Vector2(0, 240);
                for (int i = 0; i <= 90; i++)
                {
                    int dust = Dust.NewDust(player.Center + vel, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f);
                    Main.dust[dust].velocity = player.DirectionFrom(player.Center + vel) + (player.velocity);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    vel = vel.RotatedBy(MathHelper.ToRadians(360 / 90));
                }
            }
            player.GetModPlayer<SolsticePlayer>().infernoRing = true;
        }
	}
}
