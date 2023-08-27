namespace ValksTweaks;

internal partial class Config
{
    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool InfiniteAmmo;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool AllProjectilesIgnoreCollision;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool RandomProjectileType;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    public int ProjectileType;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(1, 100f)]
    public int ProjectileVelocityMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalPlayerProjectilesLeft;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalPlayerProjectilesRight;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalEnemyProjectilesLeft;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalEnemyProjectilesRight;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(30)]
    [Range(1, 1000)]
    public int AdditionalProjectileHorizontalSpacing;
}
