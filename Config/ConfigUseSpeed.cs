namespace ValksTweaks;

internal partial class Config
{
    [BackgroundColor(0, 0, 80, 200)]
    [DefaultValue(1f)]
    [Range(1, 1000)]
    public int UseSpeedGlobalMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1, 1000)]
    public int UseSpeedMeleeMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1, 1000)]
    public int UseSpeedRangedMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1, 1000)]
    public int UseSpeedMagicMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1, 1000)]
    public int UseSpeedSummonMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1f)]
    [Range(1, 1000)]
    public int UseSpeedThrownMultiplier;
}
