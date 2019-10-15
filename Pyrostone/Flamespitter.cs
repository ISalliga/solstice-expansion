using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Solstice.Pyrostone
{
	public class Flamespitter : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Flamespitter");
			Tooltip.SetDefault("Creates inaccurate volleys of flame");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
            item.noMelee = true;
            item.useAmmo = AmmoID.Gel;
			item.ranged = true;          
			item.width = 40;           
			item.height = 40;           //Weapon's texture's height
			item.useTime = 16;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 16;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
			item.rare = 0;              //The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item34;      //The sound when the weapon is using
            item.shoot = mod.ProjectileType("FlamespitterProj");
            item.shootSpeed = 14f;
			item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyrostone", 8);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Collision.CanHit(player.Center, 10, 10, player.Center + player.DirectionTo(Main.MouseWorld) * 46, 10, 10))
                position = player.Center + player.DirectionTo(Main.MouseWorld) * 46;
            speedX += Main.rand.Next(-2, 3);
            speedY += Main.rand.Next(-2, 3);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
