namespace ValksTweaks;

internal partial class Config : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    public static Config Instance => ModContent.GetInstance<Config>();

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool AreToolsEffected;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(0, 1f)]
    public float GoblinReforgeCostMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1f, 3f)]
    public float HorizontalWingSpeedMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1f, 3f)]
    public float HorizontalWingAccelerationMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1f, 1000f)]
    public int PickupRangeMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool VeinMiner;
}