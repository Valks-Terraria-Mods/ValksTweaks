﻿using System.Text.Json;
using System.IO;
using System.Collections.Generic;

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
        Stream stream = ModContent.GetInstance<ValksTweaks>().GetFileStream("Template.json");
        using StreamReader reader = new(stream);
        string json = reader.ReadToEnd();
        Schematic schematic = JsonSerializer.Deserialize<Schematic>(json);

        //Schematic schematic = Debug.Template;

        if (schematic == null)
        {
            Main.NewText("Template not set");
            return false;
        }

        Vector2I size = schematic.Size;

        Vector2I startPos = new(
            (int)Main.MouseWorld.X / 16,
            (int)Main.MouseWorld.Y / 16 - size.Y + 1);

        int index = 0;

        Dictionary<int, List<TileInfo>> furniture = new();

        // Prepare the furniture dictionary
        for (int i = 0; i < size.X * size.Y; i++)
        {
            TileInfo tileInfo = schematic.Tiles[i];
            int tileId = tileInfo.TileType;

            if (IsFurnitureTile(tileId) && !furniture.ContainsKey(tileId))
                furniture[tileInfo.TileType] = new();
        }

        for (int i = 0; i < size.X; i++)
        {
            for (int j = 0; j < size.Y; j++)
            {
                TileInfo tileInfo = schematic.Tiles[index++];

                int x = startPos.X + i;
                int y = startPos.Y + j;

                // Place walls
                WorldGen.PlaceWall(x, y, tileInfo.WallType, false);

                // Do not add furniture tiles right now
                if (IsFurnitureTile(tileInfo.TileType))
                {
                    // Pass over the position
                    tileInfo.Position = new Vector2I(x, y);

                    // Keep track of the furniture tile to be added later
                    furniture[tileInfo.TileType].Add(tileInfo);

                    // This is a furniture tile so skip it
                    continue;
                }

                // Place solid tiles
                PlaceTile(x, y, tileInfo);
            }
        }

        AddFurnitureTiles(furniture);

        return false;
    }

    /*public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Wood, 100)
            .AddTile(TileID.WorkBenches)
            .Register();
    }*/

    void AddFurnitureTiles(Dictionary<int, List<TileInfo>> furniture)
    {
        // Otherwise chairs will not be placed properly
        furniture[TileID.Chairs].Reverse();

        foreach (List<TileInfo> furnitureList in furniture.Values)
            foreach (TileInfo tileInfo in furnitureList)
                AddFurnitureTile(tileInfo);
    }

    void AddFurnitureTile(TileInfo tileInfo)
    {
        // Open doors break surrounding tiles when placed in the world
        ReplaceTile(tileInfo, TileID.OpenDoor, TileID.ClosedDoor);
        ReplaceTile(tileInfo, TileID.TallGateOpen, TileID.TallGateClosed);
        ReplaceTile(tileInfo, TileID.TrapdoorOpen, TileID.TrapdoorClosed);

        int x = tileInfo.Position.X;
        int y = tileInfo.Position.Y;

        PlaceTile(x, y, tileInfo);
    }

    void ReplaceTile(TileInfo tileInfo, int oldTile, int newTile)
    {
        if (tileInfo.TileType == oldTile)
            tileInfo.TileType = newTile;
    }

    void PlaceTile(int x, int y, TileInfo tileInfo)
    {
        WorldGen.PlaceTile(x, y, tileInfo.TileType,
            mute: true,
            forced: true);

        WorldGen.SlopeTile(x, y, tileInfo.Slope);

        // WorldGen.PlaceTile(...) overwrites the TileFrameX so that is why
        // this is set after
        Main.tile[x, y].TileFrameX = (short)tileInfo.TileFrameX;
        Main.tile[x, y].TileFrameY = (short)tileInfo.TileFrameY;

        // Remove empty tiles
        if (!tileInfo.HasTile)
            Main.tile[x, y].ClearTile();
    }

    bool IsFurnitureTile(int id) => id switch
    {
        TileID.WorkBenches or
        TileID.OpenDoor or
        TileID.ClosedDoor or
        TileID.TallGateOpen or 
        TileID.TallGateClosed or
        TileID.TrapdoorOpen or
        TileID.TrapdoorClosed or
        TileID.Beds or
        TileID.Tables or 
        TileID.Tables2 or 
        TileID.Torches or 
        TileID.Chairs or 
        TileID.Toilets => true,
        _ => false
    };
}

public class FurnitureTile
{
    public Vector2I Position { get; set; }
    public int TileFrameX { get; set; }
    public int TileFrameY { get; set; }
    public int Slope { get; set; }
    public int Id { get; set; }
}
