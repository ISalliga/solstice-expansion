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
	public class FlamingPunch : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyrostone Gauntlet");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Orange;
        }
        public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.magic = true;
			projectile.timeLeft = 240;
			projectile.width = 24;
			projectile.height = 26;
			projectile.ignoreWater = false;
			projectile.maxPenetrate = 0;
			projectile.alpha = 50;
		}
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust;
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position - new Vector2((projectile.width / 2) + 10, (projectile.height / 2) + 10), projectile.width + 20, projectile.height + 20, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
                dust.noGravity = true;
            }
            Main.PlaySound(SoundID.Item62, projectile.Center);
        }
        public override void AI()
		{
            projectile.ai[0]++;
            if (projectile.ai[0] > 30) projectile.velocity.Y += 0.12f;

            Vector2 target = projectile.Center + projectile.DirectionTo(projectile.Center + projectile.velocity);

            projectile.rotation = projectile.AngleTo(projectile.Center + projectile.DirectionTo(projectile.Center + projectile.velocity).RotatedBy(MathHelper.ToRadians(90)));

            Dust dust;
            Vector2 position = projectile.Center;
            dust = Main.dust[Terraria.Dust.NewDust(position + (projectile.DirectionTo(target) * (projectile.height / 2)), 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
            dust.noGravity = true;
            dust.velocity = (projectile.velocity.RotatedBy(MathHelper.ToRadians(90)) * 0.2f) + projectile.velocity;
            Dust dust2;
            dust2 = Main.dust[Terraria.Dust.NewDust(position + (projectile.DirectionTo(target) * (projectile.height / 2)), 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
            dust2.noGravity = true;
            dust2.velocity = (projectile.velocity.RotatedBy(MathHelper.ToRadians(-90)) * 0.2f) + projectile.velocity;
            Dust dust3;
            dust3 = Main.dust[Terraria.Dust.NewDust(position - (projectile.DirectionTo(target) * (projectile.height / 2)), 0, 0, 6, 0f, 0f, 0, new Color(255, 255, 255), 2.565789f)];
            dust3.noGravity = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }
    }
}