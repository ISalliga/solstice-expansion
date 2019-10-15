using Terraria.ModLoader;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System;

namespace Solstice
{
	public class Solstice : Mod
	{
		public Solstice()
		{
		}

        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {

        }

        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(
                        buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }
        public override void Load()
        {
            if (!Main.dedServ)
            {
                if (WorldGen.crimson == true)
                {
                    Main.itemTexture[521] = GetTexture("SoulOfNightRed");
                }
            }
            if (!Main.dedServ)
            {
                Filters.Scene["Solstice:Day"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(4f, 3f, 1f).UseOpacity(0.08f), EffectPriority.Low);
                Filters.Scene["Solstice:Night"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(2f, 1f, 5f).UseOpacity(0.12f), EffectPriority.Low);
                Filters.Scene["Solstice:Corruption"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(4f, 0f, 5f).UseOpacity(0.12f), EffectPriority.Medium);
                Filters.Scene["Solstice:Crimson"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(8f, 1f, 0f).UseOpacity(0.12f), EffectPriority.Medium);
                Filters.Scene["Solstice:Snow"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(0f, 1f, 7f).UseOpacity(0.05f), EffectPriority.Low);
                Filters.Scene["Solstice:Jungle"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(0f, 6f, 2f).UseOpacity(0.05f), EffectPriority.Low);
            }
            if (!Main.dedServ)
            {
                PremultiplyTexture(GetTexture("CorruptionVignette"));
                PremultiplyTexture(GetTexture("CrimsonVignette"));
            }
        }
    }
}