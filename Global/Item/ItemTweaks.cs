namespace ValksTweaks;

internal class Test : GlobalTile
{
    public override void RightClick(int i, int j, int type)
    {
        //Main.NewText(type);
    }
}

internal partial class ItemTweaks
{
    public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
    {
        speed *= Config.Instance.HorizontalWingSpeedMultiplier;
        acceleration *= Config.Instance.HorizontalWingAccelerationMultiplier;
    }

    public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
    {
        reforgePrice *= (int)Config.Instance.GoblinReforgeCostMultiplier;

        return base.ReforgePrice(item, ref reforgePrice, ref canApplyDiscount);
    }

    public override void GrabRange(Item item, Player player, ref int grabRange)
    {
        grabRange *= Config.Instance.PickupRangeMultiplier;
    }

    public override bool? CanAutoReuseItem(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.CanAutoReuseItem(item, player);

        return Config.Instance.AllWeaponAutoReuse;
    }
}
