using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria;

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

        List<FurnitureTile> workbenches = new();
        List<FurnitureTile> chairs = new();
        List<FurnitureTile> doors = new();
        List<FurnitureTile> torches = new();
        List<FurnitureTile> tables = new();

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
                if (tile.TileType is TileID.Chairs)
                {
                    AddFurnitureTiles(ref chairs, x, y, tile);
                    continue;
                }

                if (tile.TileType is TileID.WorkBenches)
                {
                    AddFurnitureTiles(ref workbenches, x, y, tile);
                    continue;
                }

                if (tile.TileType is TileID.OpenDoor or TileID.ClosedDoor)
                {
                    AddFurnitureTiles(ref doors, x, y, tile);
                    continue;
                }

                if (tile.TileType is TileID.Torches)
                {
                    AddFurnitureTiles(ref torches, x, y, tile);
                    continue;
                }

                if (tile.TileType is TileID.Tables)
                {
                    AddFurnitureTiles(ref tables, x, y, tile);
                    continue;
                }

                // Place tiles
                WorldGen.PlaceTile(x, y, tile.TileType, true, true);
                WorldGen.SlopeTile(x, y, tile.Slope);

                // WorldGen.PlaceTile(...) overwrites the TileFrameX so that is why
                // this is set after
                Main.tile[x, y].TileFrameX = (short)tile.TileFrameX;
                Main.tile[x, y].TileFrameY = (short)tile.TileFrameY;

                // Remove empty tiles
                if (!tile.HasTile)
                    Main.tile[x, y].ClearTile();
            }
        }

        // Otherwise chairs will not be placed properly
        chairs.Reverse();

        PlaceFurnitureTiles(chairs);
        PlaceFurnitureTiles(workbenches);
        PlaceFurnitureTiles(doors);
        PlaceFurnitureTiles(torches);
        PlaceFurnitureTiles(tables);

        return false;
    }

    void AddFurnitureTiles(ref List<FurnitureTile> furniture, int x, int y, TileInfo tile)
    {
        furniture.Add(new FurnitureTile
        {
            Position = new Vector2I(x, y),
            TileFrameX = tile.TileFrameX,
            TileFrameY = tile.TileFrameY,
            Id = tile.TileType
        });
    }

    void PlaceFurnitureTiles(List<FurnitureTile> furniture)
    {
        foreach (var tile in furniture)
        {
            // Open doors break surrounding tiles when placed in the world
            if (tile.Id == TileID.OpenDoor)
                tile.Id = TileID.ClosedDoor;

            int x = tile.Position.X;
            int y = tile.Position.Y;

            WorldGen.PlaceTile(x, y, tile.Id, mute: true, forced: true);

            // WorldGen.PlaceTile(...) overwrites the TileFrameX so that is why
            // this is set after
            Main.tile[x, y].TileFrameX = (short)tile.TileFrameX;
            Main.tile[x, y].TileFrameY = (short)tile.TileFrameY;
        }
    }

    /*public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Wood, 100)
            .AddTile(TileID.WorkBenches)
            .Register();
    }*/
}

public class FurnitureTile
{
    public Vector2I Position { get; set; }
    public int TileFrameX { get; set; }
    public int TileFrameY { get; set; }
    public int Id { get; set; }
}
