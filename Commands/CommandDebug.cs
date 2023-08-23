using System.Text.Json;
using System.Collections.Generic;
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

        Template template = new()
        {
            Size = new Vector2I(diffX - 1, diffY - 1)
        };

        for (int x = topLeft.X + 1; x < topLeft.X + diffX; x++)
        {
            for (int y = topLeft.Y + 1; y < topLeft.Y + diffY; y++)
            {
                if (Main.tile[x, y].HasTile)
                    template.TileTypes.Add(Main.tile[x, y].TileType);
                else
                    // Use -1 to represent empty tiles
                    template.TileTypes.Add(-1);
            }
        }

        Debug.Template = template;

        string json = JsonSerializer.Serialize(template, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        string savePath = Main.SavePath + "/ValksTweaks";

        Directory.CreateDirectory(savePath);

        File.WriteAllText(savePath + "/Template.json", json);

        //string readText = File.ReadAllText(savePath + "/Template.json");

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

public class Template
{
    public Vector2I Size { get; set; }
    public List<int> TileTypes { get; set; } = new();
}