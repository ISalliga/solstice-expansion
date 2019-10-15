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
	public class AestheticPlayer : ModPlayer
	{
        public bool ZonePurity = false;

        public override void UpdateBiomes()
        {
            ZonePurity = AestheticWorld.purityTiles > 50;
        }

        public override void PostUpdate()
        {
            if (player.statMana == 0)
            {
                if (Main.rand.NextFloat() < 0.6f)
                {
                    Dust dust;
                    Vector2 position = player.position;
                    dust = Main.dust[Terraria.Dust.NewDust(position, player.width, player.height, 27, 0f, 0f, 0, new Color(255, 0, 0), 1f)];
                    dust.noGravity = true;
                }
            }
        }

        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("Solstice:Day", Main.dayTime && player.ZoneOverworldHeight);
            player.ManageSpecialBiomeVisuals("Solstice:Night", !Main.dayTime && player.ZoneOverworldHeight);
            player.ManageSpecialBiomeVisuals("Solstice:Snow", player.ZoneSnow);
            player.ManageSpecialBiomeVisuals("Solstice:Jungle", player.ZoneJungle);
            player.ManageSpecialBiomeVisuals("Solstice:Corruption", player.ZoneCorrupt);
            player.ManageSpecialBiomeVisuals("Solstice:Crimson", player.ZoneCrimson);
        }
    }
}