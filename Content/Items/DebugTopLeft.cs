namespace ValksTweaks.Content.Items;

public class DebugTopLeft : ModItem
{
    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.DebugTopLeft>());
        Item.rare = 2;
    }
}
