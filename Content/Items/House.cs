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
            TileID.ClosedDoor,
            TileID.Chairs
        };

        List<FurnitureTile> furnitureTiles = new();

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                int tileId = template.TileTypes[index++];

                if (furnitureIds.Contains(tileId))
                {
                    furnitureTiles.Add(new FurnitureTile
                    {
                        Position = new Vector2I(startPos.X + x, startPos.Y + y),
                        Id = tileId
                    });
                    continue;
                }

                // Empty tile
                if (tileId == -1)
                {
                    Main.tile[startPos.X + x, startPos.Y + y].ClearTile();
                }
                else
                {
                    WorldGen.SlopeTile(startPos.X + x, startPos.Y + y, (int)SlopeType.Solid);
                    WorldGen.PlaceTile(startPos.X + x, startPos.Y + y, tileId, true, true);
                }
            }
        }

        foreach (var tile in furnitureTiles)
        {
            WorldGen.SlopeTile(tile.Position.X, tile.Position.Y, (int)SlopeType.Solid);
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
    public int Id { get; set; }
}
