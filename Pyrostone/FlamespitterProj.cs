using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Solstice.Pyrostone
{
	public class FlamespitterProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flamespitter");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.timeLeft = 12;
			projectile.width = 14;
			projectile.height = 28;
			projectile.ignoreWater = true;
			projectile.maxPenetrate = 0;
			projectile.alpha = 50;
		}
		public override void AI()
		{
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center;
            dust = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
            dust.noGravity = true;
            dust.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(90)) * 0.1f;
            Dust dust2;
            dust2 = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
            dust2.noGravity = true;
            dust2.velocity = projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)) * 0.1f;
            for (int i = 0; i < 2; i++)
            {
                Dust dust3;
                dust3 = Main.dust[Terraria.Dust.NewDust(position, 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
                dust3.noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }
    }
}