using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Solstice;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;

namespace Solstice
{
	public class SolsticePlayer : ModPlayer
	{
        public int guardCooldown = 0;

        public Vector2 weaponTarget = Vector2.Zero;
        public bool spearHit = false;

        public bool infernoRing = false;

        public bool bowFiring = false;

        public override void PreUpdate()
        {
            if (infernoRing)
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.Distance(player.Center) <= 240)
                    {
                        if (!npc.friendly)
                        {
                            npc.AddBuff(BuffID.OnFire, 30);
                            npc.AddBuff(BuffID.Ichor, 30);
                        }
                    }
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (crit)
            {
                if (proj.melee)
                {
                    target.AddBuff(mod.BuffType("Crushed"), 240);
                }
                if (proj.ranged)
                {
                    target.AddBuff(mod.BuffType("Crippled"), 420);
                }
                if (proj.magic)
                {
                    Main.player[proj.owner].AddBuff(mod.BuffType("Casting"), 420);
                }
            }

            if (proj.aiStyle == 19)
            {
                player.velocity = player.DirectionFrom(target.Center) * 3f;
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (crit)
            {
                target.AddBuff(mod.BuffType("Crushed"), 240);
            }
        }

        public override void ResetEffects()
        {
            guardCooldown--;
            if (guardCooldown >= 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Dust dust;
                    Vector2 position = Main.LocalPlayer.Center + new Vector2(-13, player.height / 2);
                    dust = Main.dust[Terraria.Dust.NewDust(position, 26, 0, 271, 0f, 0f, 0, new Color(255, 255, 255), 0.7894737f)];
                    dust.noGravity = true;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                }
            }

            infernoRing = false;

            bowFiring = false;
        }
    }
}