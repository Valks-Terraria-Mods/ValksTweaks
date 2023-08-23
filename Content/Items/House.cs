using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;

namespace ValksTweaks.Content.Items;

public class House : ModItem
{
    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.rare = ItemRarityID.LightPurple;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.useAnimation = 20;
        Item.useTime = 20;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shoot = ProjectileID.BoneArrow;
        Item.consumable = true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        /*Stream stream = ModContent.GetInstance<ValksTweaks>().GetFileStream("Template.json");
        using StreamReader reader = new(stream);
        string json = reader.ReadToEnd();
        Template template = JsonSerializer.Deserialize<Template>(json);*/

        Template template = Debug.Template;

        Vector2I size = template.Size;

        Vector2I startPos = new(
            (int)Main.MouseWorld.X / 16,
            (int)Main.MouseWorld.Y / 16 - size.Y + 1);

        int index = 0;

        int[] furnitureIds = { 
            TileID.WorkBenches,
            TileID.Torches,
            TileID.OpenDoor,
            TileID.ClosedDoor,
            TileID.Chairs
        };

        List<FurnitureTile> furnitureTiles = new();

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                TileInfo tile = template.Tiles[index++];

                int x = startPos.X + i;
                int y = startPos.Y + j;

                // Place walls
                WorldGen.PlaceWall(x, y, tile.WallType, false);

                // Do not place furniture tiles just yet
                if (furnitureIds.Contains(tile.TileType))
                {
                    furnitureTiles.Add(new FurnitureTile
                    {
                        Position = new Vector2I(x, y),
                        TileFrameX = tile.TileFrameX,
                        Id = tile.TileType
                    });
                    continue;
                }

                // Place tiles
                WorldGen.SlopeTile(x, y, tile.Slope);
                WorldGen.PlaceTile(x, y, tile.TileType, true, true);

                // Remove empty tiles
                if (!tile.HasTile)
                    Main.tile[x, y].ClearTile();
            }
        }

        // Place furniture tiles
        foreach (var tile in furnitureTiles)
        {
            //WorldGen.SlopeTile(tile.Position.X, tile.Position.Y, (int)SlopeType.Solid);

            // Open doors break surrounding tiles when placed in the world
            if (tile.Id == TileID.OpenDoor)
                tile.Id = TileID.ClosedDoor;

            Main.tile[tile.Position.X, tile.Position.Y].TileFrameX = (short)tile.TileFrameX;
            WorldGen.PlaceTile(tile.Position.X, tile.Position.Y, tile.Id, true, true);
        }

        return false;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CopperCoin)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}

public class FurnitureTile
{
    public Vector2I Position { get; set; }
    public int TileFrameX { get; set; }
    public int Id { get; set; }
}
