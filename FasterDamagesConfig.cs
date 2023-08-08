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

    [BackgroundColor(0, 0, 80, 200)]
    [DefaultValue(4f)]
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

    [BackgroundColor(0, 0, 80, 200)]
    [DefaultValue(float.Epsilon)]
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

    /*[BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(float.Epsilon)]
    [Range(float.Epsilon, 1f)]
    public float UseAnimationMultiplier;*/

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(true)]
    public bool AllWeaponAutoReuse;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(false)]
    public bool AreToolsEffected;

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

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(1)]
    [Range(1, 100f)]
    public int ProjectileVelocityMultiplier;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalProjectilesLeft;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(0)]
    [Range(0, 100)]
    public int AdditionalProjectilesRight;

    [BackgroundColor(0, 0, 0, 200)]
    [DefaultValue(30)]
    [Range(1, 1000)]
    public int AdditionalProjectileHorizontalSpacing;

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
}