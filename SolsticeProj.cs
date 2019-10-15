using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Input;

namespace Solstice
{
    public class SolsticeProj : GlobalProjectile
    {
        bool goButFaster = true;
        int time = 0;

        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            if (goButFaster && projectile.aiStyle == 19)
            {
                goButFaster = false;
                projectile.AI();
            }

            return true;
        }

        public override void AI(Projectile projectile)
        {
            time++;
            if (projectile.aiStyle == 19)
            {
                if (time > Main.player[projectile.owner].itemAnimationMax * 2) projectile.active = false;
            }
        }

        public override void PostAI(Projectile projectile)
        {
            goButFaster = true;
        }
    }
}