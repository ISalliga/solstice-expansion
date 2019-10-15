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
	public class SolsticeItem : GlobalItem
	{
        static float bowCharge = 0;

        public override void HoldItem(Item item, Player player)
        {
            if (item.ranged)
            {
                if (item.useAmmo == AmmoID.Arrow)
                {
                    player.itemLocation = player.Center - new Vector2(item.width / 2, item.height / 2);
                    player.itemLocation -= player.DirectionTo(Main.MouseWorld) * 6;
                    player.itemLocation += player.DirectionTo(Main.MouseWorld) * (bowCharge / 10);
                    player.itemLocation -= new Vector2(2, 2);
                    player.itemRotation = player.AngleTo(Main.MouseWorld);
                    if (Main.MouseWorld.X > player.Center.X) player.direction = 1;
                    else player.direction = -1;
                    if (player.direction == -1) player.itemRotation -= MathHelper.ToRadians(180);

                    bowCharge++;
                    if (bowCharge >= 60) bowCharge = 60;

                    player.rangedDamage += bowCharge / 60f;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine guardAbility = new TooltipLine(mod, "TrueMelee", "True Melee - Right click to guard \nGuarding blocks projectiles and stuns enemies");
            int mana = (item.damage / 2) / (item.useAnimation / 5);
            TooltipLine projMelee = new TooltipLine(mod, "ProjMelee", "Projectile Melee - Projectiles cost mana \nThis weapon's mana cost is " + mana.ToString());
            if (item.useStyle == 3 || item.useStyle == 1)
            {
                if (item.melee && item.pick == 0 && item.axe == 0 && item.hammer == 0 && !item.noUseGraphic && item.createTile < 0)
                {
                    if (item.shoot == 0) tooltips.Add(guardAbility);
                    else tooltips.Add(projMelee);
                }
            }
        }

        public override void SetDefaults(Item item)
        {
            int epicity = Projectile.NewProjectile(new Vector2(40, 40), Vector2.Zero, item.shoot, 0, 0);
            if (Main.projectile[epicity].aiStyle == 19)
            {
                item.useTime /= 2;
                item.useAnimation /= 2;
                item.damage = item.damage * 3 / 2;
            }

            if (item.ranged)
            {
                if (item.useAmmo == AmmoID.Arrow)
                {
                    item.holdStyle = 1;
                    item.autoReuse = false;
                }
            }
            Main.projectile[epicity].active = false;
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int epicity = Projectile.NewProjectile(new Vector2(40, 40), Vector2.Zero, type, 0, 0);
            if (Main.projectile[epicity].aiStyle == 19)
            {
                Vector2 velAdd = player.DirectionTo(Main.MouseWorld) * 8f;
                velAdd.Y /= 3;
                player.velocity += velAdd;
                if (player.velocity.X > player.maxRunSpeed + player.accRunSpeed) player.velocity.X = player.maxRunSpeed + player.accRunSpeed;
                if (player.velocity.X <= -player.maxRunSpeed - player.accRunSpeed) player.velocity.X = -player.maxRunSpeed - player.accRunSpeed;
            }
            Main.projectile[epicity].active = false;

            if (item.melee && item.shoot != 0 && !item.noUseGraphic && item.createTile < 0)
            {
                int mana = (item.damage / 2) / (item.useAnimation / 5);
                if (player.statMana < mana) return false;
                else
                {
                    player.statMana -= mana;
                    player.manaRegen = 0;
                    player.manaRegenCount = 0;
                }
            }

            if (item.ranged)
            {
                if (item.useAmmo == AmmoID.Arrow)
                {
                    bowCharge = 0;
                }

                float dmg = (float)damage * player.rangedDamage;

                damage = (int)dmg;
            }
            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }

        public override bool UseItem(Item item, Player player)
        {
            return base.UseItem(item, player);
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.useStyle == 3 || item.useStyle == 1)
            {
                if (item.shoot == 0 && item.pick == 0 && item.axe == 0 && item.hammer == 0 && !item.noUseGraphic && item.createTile < 0)
                {
                    return player.GetModPlayer<SolsticePlayer>().guardCooldown <= 0;
                }
            }
            return false;
        }

        public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            player.GetModPlayer<SolsticePlayer>().bowFiring = true;

            if (item.useStyle == 3) 
            {
                player.itemRotation = player.AngleTo(Main.MouseWorld) + 2.35619f;
                if (player.direction == 1) player.itemRotation -= 1.5708f;
                player.bodyFrame.Y = 3 * player.bodyFrame.Height;
                int wh = item.width + item.height / 2;
                Vector2 hitPos = player.Center + player.DirectionTo(Main.MouseWorld) * wh;
                hitbox = new Rectangle((int)hitPos.X - (item.width/2), (int)hitPos.Y - (item.height/2), item.width, item.height);
                if (player.velocity.X == 0)
                {
                    if (Main.MouseWorld.X > player.Center.X) player.direction = 1;
                    else player.direction = -1;
                }
            }
            if (item.useStyle == 3 || item.useStyle == 1)
            {
                if (item.shoot == 0 && item.pick == 0 && item.axe == 0 && item.hammer == 0 && !item.noUseGraphic && item.createTile < 0)
                {
                    if (player.altFunctionUse == 2)
                    {
                        foreach (Projectile projectile in Main.projectile)
                        {
                            if (hitbox.Distance(projectile.Center) <= (projectile.width + projectile.height) / 2 && projectile.hostile)
                            {
                                float buffTime = item.useAnimation * 1.4f;
                                player.immuneTime = (int)buffTime;
                                player.immune = true;
                                projectile.Kill();
                            }
                        }
                        player.GetModPlayer<SolsticePlayer>().guardCooldown = item.useAnimation * 2;
                        item.autoReuse = false;
                    }
                    else
                    {

                    }
                }
            }
        }

        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (item.useStyle == 3)
            {
                target.AddBuff(BuffID.BrokenArmor, 90);
            }
            if (item.useStyle == 3 || item.useStyle == 1)
            {
                if (item.shoot == 0 && item.pick == 0 && item.axe == 0 && item.hammer == 0 && !item.noUseGraphic && item.createTile < 0)
                {
                    float buffTime = item.useAnimation * 1.4f;
                    if (player.altFunctionUse == 2)
                    {
                        player.immuneTime = (int)buffTime;
                        player.immune = true;
                        target.AddBuff(mod.BuffType("GuardStunned"), (int)buffTime);
                    }
                }
            }
            base.OnHitNPC(item, player, target, damage, knockBack, crit);
        }
    }
}