namespace ValksTweaks.Content.Tiles;

public class DebugTopLeft : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileSolid[Type] = true;
    }
}
