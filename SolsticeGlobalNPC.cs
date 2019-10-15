using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using System.IO;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Solstice;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;

namespace Solstice
{
    public class SolsticeGlobalNPC : GlobalNPC
    {
        public bool crushed = false;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            float dmg = damage * 12/10;
            if (crushed) damage = (int)dmg;
        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            float dmg = damage * 1.16f;
            if (crushed) damage = (int)dmg;
        }

        public override void AI(NPC npc)
        {
            if (crushed)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 1, 0f, 0f, 0, new Color(255, 255, 255), 1.513158f)];
                dust.noGravity = true;
            }
        }
    }
}