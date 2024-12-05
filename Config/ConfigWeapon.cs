namespace ValksTweaks;

internal partial class Config
{
    /*[BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(true)]
    public bool AllWeaponAutoReuse;*/

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int WeaponDamageMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(1, 100)]
    public int WeaponCritMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int WeaponKnockbackMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(1, 20f)]
    public float ItemScaleMultiplier;
}
