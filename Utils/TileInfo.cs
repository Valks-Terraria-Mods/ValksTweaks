namespace ValksTweaks;

public class TileInfo
{
    public int WallType { get; set; }
    public int TileType { get; set; }
    public int TileFrameX { get; set; }
    public int TileFrameY { get; set; }
    public int Slope { get; set; }
    public bool HasTile { get; set; }

    // Note: This property is not used when serialized
    public Vector2I Position { get; set; }
}
