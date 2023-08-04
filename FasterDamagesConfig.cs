using System;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FasterDamages;

public class FasterDamagesConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    public static FasterDamagesConfig Instance => ModContent.GetInstance<FasterDamagesConfig>();

    [DefaultValue(4f)]
    [Range(1, 1000)]
    public int UseSpeedMultiplier;

    [DefaultValue(float.Epsilon)]
    [Range(float.Epsilon, 1f)]
    public float UseTimeMultiplier;

    [DefaultValue(true)]
    public bool AllWeaponAutoReuse;

    [DefaultValue(false)]
    public bool AreToolsEffected;

    [DefaultValue(false)]
    public bool InfiniteAmmo;

    [DefaultValue(false)]
    public bool AllProjectilesIgnoreCollision;

    [DefaultValue(false)]
    public bool RandomProjectileType;

    [DefaultValue(0)]
    public int ProjectileType;

    [DefaultValue(1)]
    [Range(1, 100)]
    public int ProjectileDamageMultiplier;

    [DefaultValue(1)]
    [Range(1, 100f)]
    public int ProjectileVelocityMultiplier;

    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalProjectilesLeft;

    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalProjectilesRight;

    [DefaultValue(30)]
    [Range(1, 1000)]
    public int AdditionalProjectileHorizontalSpacing;
}