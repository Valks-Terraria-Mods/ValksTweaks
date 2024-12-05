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
    public int HorizontalWingSpeedMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1f, 3f)]
    public int HorizontalWingAccelerationMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1f, 1000f)]
    public int PickupRangeMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(5)]
    public int PlayerRespawnTimeSingleplayer;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(10)]
    public int PlayerRespawnTimeMultiplayer;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(true)]
    public bool ShouldPlayerRespawnWithFullHealth;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(20f)]
    [Range(1f, 20f)]
    public int ExtractinatorUseSpeedMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool DisablePlayerTombstones;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    public int PlayerRunAcceleration;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    public int PlayerMaxRunSpeed;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(10)]
    [Range(0, 10000)]
    public int PlayerMaxFallSpeed;
}