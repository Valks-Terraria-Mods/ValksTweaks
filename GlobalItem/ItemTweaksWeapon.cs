namespace ValksTweaks;

internal partial class ItemTweaks
{
    static bool IsMagicWeapon(Item item) => item.CountsAsClass(DamageClass.Magic);
    static bool IsMeleeWeapon(Item item) => item.CountsAsClass(DamageClass.Melee);
    static bool IsRangedWeapon(Item item) => item.CountsAsClass(DamageClass.Ranged);
    static bool IsSummonWeapon(Item item) => item.CountsAsClass(DamageClass.Summon);
    static bool IsThrownWeapon(Item item) => item.CountsAsClass(DamageClass.Throwing);

    static bool IsWeapon(Item item) => item.damage > 0;

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        var config = Config.Instance;
        var projectileType = config.ProjectileType;
        var maxProjectiles = ProjectileID.Count - 1;
        var randomProjectiles = new int[]
        {
            ProjectileID.BeeArrow,
            ProjectileID.BloodArrow,
            ProjectileID.BoneArrow,
            ProjectileID.ChlorophyteArrow,
            ProjectileID.CursedArrow,
            ProjectileID.FireArrow,
            ProjectileID.FlamingArrow,
            ProjectileID.FrostArrow,
            ProjectileID.FrostburnArrow,
            ProjectileID.HellfireArrow,
            ProjectileID.HolyArrow,
            ProjectileID.JestersArrow,
            ProjectileID.MoonlordArrow,
            ProjectileID.IchorArrow,
            ProjectileID.PhantasmArrow,
            ProjectileID.ShadowFlameArrow,
            ProjectileID.ShimmerArrow,
            ProjectileID.UnholyArrow,
            ProjectileID.VenomArrow,
            ProjectileID.WoodenArrowFriendly
        };

        if (config.RandomProjectileType)
        {
            type = randomProjectiles[new Random().Next(0, randomProjectiles.Length - 1)];
        }
        else if (projectileType != 0)
            type = type < maxProjectiles ? projectileType : maxProjectiles;

        velocity *= Config.Instance.ProjectileVelocityMultiplier;
    }

    public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
    {
        damage *= Config.Instance.WeaponDamageMultiplier;
    }

    public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
    {
        crit *= Config.Instance.WeaponCritMultiplier;
    }

    public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
    {
        knockback *= Config.Instance.WeaponKnockbackMultiplier;
    }

    public override void ModifyItemScale(Item item, Player player, ref float scale)
    {
        scale *= Config.Instance.ItemScaleMultiplier;
    }

    public override float UseTimeMultiplier(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.UseTimeMultiplier(item, player);

        var config = Config.Instance;
        var globalMultiplier = config.UseTimeGlobalMultiplier;

        if (IsMagicWeapon(item))
            return config.UseTimeMagicMultiplier * globalMultiplier;
        else if (IsMeleeWeapon(item))
            return config.UseTimeMeleeMultiplier * globalMultiplier;
        else if (IsRangedWeapon(item))
            return config.UseTimeRangedMultiplier * globalMultiplier;
        else if (IsSummonWeapon(item))
            return config.UseTimeSummonMultiplier * globalMultiplier;
        else if (IsThrownWeapon(item))
            return config.UseTimeThrownMultiplier * globalMultiplier;

        return base.UseTimeMultiplier(item, player);
    }

    // This seems to only affect how fast the swing art animation is played
    public override float UseSpeedMultiplier(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.UseAnimationMultiplier(item, player);

        var config = Config.Instance;
        var globalMultiplier = config.UseSpeedGlobalMultiplier;

        // A value of 4 seems good
        if (IsMagicWeapon(item))
            return config.UseSpeedMagicMultiplier * globalMultiplier;
        else if (IsMeleeWeapon(item))
            return config.UseSpeedMeleeMultiplier * globalMultiplier;
        else if (IsRangedWeapon(item))
            return config.UseSpeedRangedMultiplier * globalMultiplier;
        else if (IsSummonWeapon(item))
            return config.UseSpeedSummonMultiplier * globalMultiplier;
        else if (IsThrownWeapon(item))
            return config.UseSpeedThrownMultiplier * globalMultiplier;

        return base.UseSpeedMultiplier(item, player);
    }
}
