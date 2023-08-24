﻿using System.Text.Json;
using System.IO;
using System.Diagnostics;

namespace ValksTweaks;

public class CommandDebug : ModCommand
{
    public override string Command => "debug";
    public override CommandType Type => CommandType.World;

    public override void Action(CommandCaller caller, string input, string[] args)
    {
        Vector2I topLeft = Debug.TopLeft;
        Vector2I botRight = Debug.BottomRight;

        int diffX = botRight.X - topLeft.X;
        int diffY = botRight.Y - topLeft.Y;

        Schematic schematic = new()
        {
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

        Debug.Template = schematic;

        string json = JsonSerializer.Serialize(schematic, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        string savePath = Main.SavePath + "/ValksTweaks";

        Directory.CreateDirectory(savePath);

        File.WriteAllText(savePath + "/Template.json", json);

        // Open the folder containing the tile save data
        if (Directory.Exists(savePath))
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = savePath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        Main.NewText("Saved template");
    }
}
