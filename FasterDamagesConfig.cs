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

    [DefaultValue(1000f)]
    [Range(0.5f, 1000f)]
    public float UseSpeedMultiplier;

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
    public float ProjectileVelocityMultiplier;
}