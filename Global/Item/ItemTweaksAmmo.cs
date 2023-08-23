namespace ValksTweaks;

internal partial class ItemTweaks : GlobalItem
{
    public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
    {
        if (Config.Instance.InfiniteAmmo)
            return false;

        return base.CanConsumeAmmo(weapon, ammo, player);
    }

    public override bool NeedsAmmo(Item item, Player player)
    {
        if (Config.Instance.InfiniteAmmo)
            return false;

        return base.NeedsAmmo(item, player);
    }

    public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
    {
        if (Config.Instance.InfiniteAmmo)
            reduce = 0;
    }

    public override bool ConsumeItem(Item item, Player player)
    {
        if (Config.Instance.InfiniteAmmo)
            return false;

        return base.ConsumeItem(item, player);
    }

    static bool IsTool(Item item) => (item.pick > 0 || item.axe > 0 || item.hammer > 0)
        && !Config.Instance.AreToolsEffected;
}
