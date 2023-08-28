namespace ValksTweaks;

public class TileTweaks : GlobalTile
{
    bool miningTiles;

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (!ModContent.GetInstance<Config>().VeinMiner || fail || miningTiles)
            return;

        if (IsOre(i, j))
        {
            // Prevent random 1 dirt block from spawning
            noItem = true;

            MineAdjacent(i, j);
        }
    }

    void MineAdjacent(int i, int j)
    {
        if (TileIsInWorld(i, j) && IsVeinMinable(i, j))
        {
            miningTiles = true;

            WorldGen.KillTile(i, j,
                fail: false,
                effectOnly: false,
                noItem: false);

            miningTiles = false;

            MineAdjacent(i + 1, j);
            MineAdjacent(i - 1, j);
            MineAdjacent(i, j + 1);
            MineAdjacent(i, j - 1);
        }
    }

    bool TileIsInWorld(int i, int j) =>
        i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY;

    bool IsVeinMinable(int i, int j)
    {
        // Would add silt here but freezes and crashes the game because
        // silt falls. The only solution to this is to mine the silt from
        // top to bottom and that would change the current code drastically
        // which I'm not really into doing right now. So only solid non-falling
        // tiles will be supported for now.

        //int type = Main.tile[i, j].TileType;

        return IsOre(i, j);
    }

    bool IsOre(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        ushort type = tile.TileType;
        bool isSolid = Main.tileSolid[type];
        short metalDetectorPriority = Main.tileOreFinderPriority[type];

        return (isSolid && metalDetectorPriority > 0 && tile.HasTile)
            || (TileID.Sets.Ore[type]);
    }
}
