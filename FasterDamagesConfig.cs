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

    [DefaultValue(float.Epsilon)]
    [Range(float.Epsilon, 1f)]
    public float UseAnimationMultiplier;

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
    public int WeaponDamageMultiplier;

    [DefaultValue(1)]
    [Range(1, 100)]
    public int WeaponCritMultiplier;

    [DefaultValue(1)]
    [Range(1, 100)]
    public int WeaponKnockbackMultiplier;

    [DefaultValue(1)]
    [Range(1, 20f)]
    public float ItemScaleMultiplier;

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

    [DefaultValue(1f)]
    [Range(0, 1f)]
    public float GoblinReforgeCostMultiplier;

    [DefaultValue(1f)]
    [Range(1f, 3f)]
    public float HorizontalWingSpeedMultiplier;

    [DefaultValue(1f)]
    [Range(1f, 3f)]
    public float HorizontalWingAccelerationMultiplier;

    [DefaultValue(1f)]
    [Range(1f, 1000f)]
    public int PickupRangeMultiplier;
}