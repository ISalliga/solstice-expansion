using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Solstice.Pyrostone
{
	public class PyrostoneGauntlet : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Pyrostone Gauntlet");
			Tooltip.SetDefault("Throws a flaming combustible punch");
		}

		public override void SetDefaults()
		{
			item.damage = 15;
			item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = true;
			item.width = 40;           
			item.height = 40;           //Weapon's texture's height
			item.useTime = 28;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 28;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			item.useStyle = 5;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
			item.rare = 0;              //The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item1;      //The sound when the weapon is using
            item.shoot = mod.ProjectileType("FlamingPunch");
            item.shootSpeed = 10f;
			item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Pyrostone", 7);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
