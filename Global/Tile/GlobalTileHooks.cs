namespace ValksTweaks;

public class GlobalTileHooks : GlobalTile
{
    public override void PlaceInWorld(int i, int j, int type, Item item)
    {
        if (type == ModContent.TileType<Content.Tiles.DebugTopLeft>())
        {
            Main.NewText("Set top left position");
            Debug.TopLeft = new Vector2I(i, j);
        }

        if (type == ModContent.TileType<Content.Tiles.DebugBottomRight>())
        {
            Main.NewText("Set bottom right position");
            Debug.BottomRight = new Vector2I(i, j);
        }
    }

    public override void RightClick(int i, int j, int type)
    {
        Tile tile = Main.tile[i, j];

        Main.NewText(tile.TileFrameX);
    }
}
