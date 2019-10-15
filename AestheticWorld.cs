using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Linq;
using Terraria.Localization;

namespace Solstice
{
    public class AestheticWorld : ModWorld
    {
        public static int purityTiles = 0;

        public override void TileCountsAvailable(int[] tileCounts)
        {
            purityTiles = tileCounts[TileID.Grass];
        }
    }
}