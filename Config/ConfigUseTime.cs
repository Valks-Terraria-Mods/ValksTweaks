namespace ValksTweaks;

internal partial class Config
{
    [BackgroundColor(0, 0, 80, 200)]
    [DefaultValue(1f)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeGlobalMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeMeleeMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeRangedMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeMagicMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeSummonMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeThrownMultiplier;
}
