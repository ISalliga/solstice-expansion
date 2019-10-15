using Terraria.ID;
using Terraria.ModLoader;

namespace Solstice.Pyrostone
{
	public class Pyrostone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyrostone");
			Tooltip.SetDefault("'Comfortingly warm to the touch.'");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.value = 350;
			item.rare = 2;
			item.maxStack = 99;
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StoneBlock, 10);
            recipe.AddIngredient(ItemID.Torch, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
