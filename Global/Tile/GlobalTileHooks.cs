namespace ValksTweaks;

public class GlobalTileHooks : GlobalTile
{
    public override void PlaceInWorld(int i, int j, int type, Item item)
    {
        if (type == ModContent.TileType<Content.Tiles.DebugTopLeft>())
        {
            ModContent.GetInstance<CmdSave>().TopLeft = 
                new Vector2I(i, j);

            Main.NewText("Set top left position");
        }

        if (type == ModContent.TileType<Content.Tiles.DebugBottomRight>())
        {
            ModContent.GetInstance<CmdSave>().BottomRight = 
                new Vector2I(i, j);

            Main.NewText("Set bottom right position");
        }
    }
}
