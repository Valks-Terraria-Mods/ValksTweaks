using System.Text.Json;
using System.IO;
using System.Linq;

namespace ValksTweaks;

public class CmdSave : ModCommand
{
    public override string Command => "save";
    public override CommandType Type => CommandType.World;

    public Schematic Schematic { get; set; }
    public Vector2I TopLeft { get; set; }
    public Vector2I BottomRight { get; set; }

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        // Assuming top left will never be (0, 0)
        if (TopLeft == Vector2I.Zero)
        {
            Main.NewText("Top left position not set");
            return;
        }

        // Assuming bottom right will never be (0, 0)
        if (BottomRight == Vector2I.Zero)
        {
            Main.NewText("Bottom right position not set");
            return;
        }

        if (args.Length == 0)
        {
            Main.NewText("Usage: /save <name>");
            return;
        }

        if (!args[0].All(char.IsLetterOrDigit))
        {
            Main.NewText("File name may only contain letters and numbers");
            return;
        }

        int diffX = BottomRight.X - TopLeft.X;
        int diffY = BottomRight.Y - TopLeft.Y;

        Schematic schematic = CreateSchematic(TopLeft, diffX, diffY);

        // Temporary code
        Schematic = schematic;

        string savePath = Main.SavePath + "/ValksTweaks";

        SaveSchematic(schematic, savePath, 
            fileName: args[0]);

        Main.NewText("Saved schematic");

        Utils.OpenFolder(savePath);
    }

    Schematic CreateSchematic(Vector2I topLeft, int diffX, int diffY)
    {
        Schematic schematic = new() {
            Size = new Vector2I(diffX - 1, diffY - 1)
        };

        for (int x = topLeft.X + 1; x < topLeft.X + diffX; x++)
        {
            for (int y = topLeft.Y + 1; y < topLeft.Y + diffY; y++)
            {
                Tile tile = Main.tile[x, y];

                schematic.Tiles.Add(new TileInfo
                {
                    WallType = tile.WallType,
                    TileType = tile.TileType,
                    TileFrameX = tile.TileFrameX,
                    TileFrameY = tile.TileFrameY,
                    Slope = (int)tile.Slope,
                    HasTile = tile.HasTile
                });
            }
        }

        return schematic;
    }

    void SaveSchematic(Schematic schematic, string savePath, string fileName)
    {
        string json = JsonSerializer.Serialize(schematic, new JsonSerializerOptions {
            WriteIndented = true
        });

        Directory.CreateDirectory(savePath);
        File.WriteAllText($"{savePath}/{fileName}.json", json);
    }
}
