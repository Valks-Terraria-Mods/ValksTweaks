using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Config = FasterDamages.FasterDamagesConfig;

namespace FasterDamages;

class GlobalWepItem : GlobalItem
{
    public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
    {
        if (Config.Instance.InfiniteAmmo)
            return false;

        return base.CanConsumeAmmo(weapon, ammo, player);
    }

    public override bool NeedsAmmo(Item item, Player player)
    {
        if (Config.Instance.InfiniteAmmo)
            return false;

        return base.NeedsAmmo(item, player);
    }

    public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
    {
        if (Config.Instance.InfiniteAmmo)
            reduce = 0;
    }

    public override bool ConsumeItem(Item item, Player player)
    {
        if (Config.Instance.InfiniteAmmo)
            return false;

        return base.ConsumeItem(item, player);
    }

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
        damage.Flat *= Config.Instance.WeaponDamageMultiplier;
    }

    public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
    {
        crit *= Config.Instance.WeaponCritMultiplier;
    }

    public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
    {
        knockback.Flat *= Config.Instance.WeaponKnockbackMultiplier;
    }

    public override void ModifyItemScale(Item item, Player player, ref float scale)
    {
        scale *= Config.Instance.ItemScaleMultiplier;
    }

    public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
    {
        speed *= Config.Instance.HorizontalWingSpeedMultiplier;
        acceleration *= Config.Instance.HorizontalWingAccelerationMultiplier;
    }

    public override bool ReforgePrice(Item item, ref int reforgePrice, ref bool canApplyDiscount)
    {
        reforgePrice *= (int)Config.Instance.GoblinReforgeCostMultiplier;

        return base.ReforgePrice(item, ref reforgePrice, ref canApplyDiscount);
    }

    public override void GrabRange(Item item, Player player, ref int grabRange)
    {
        grabRange *= Config.Instance.PickupRangeMultiplier;
    }

    public override bool? CanAutoReuseItem(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.CanAutoReuseItem(item, player);

        return Config.Instance.AllWeaponAutoReuse;
    }

    public override float UseTimeMultiplier(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.UseTimeMultiplier(item, player);

        return Config.Instance.UseTimeMultiplier;
    }

    // This seems to only affect how fast the swing art animation is played
    public override float UseSpeedMultiplier(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.UseAnimationMultiplier(item, player);

        // A value of 4 seems good
        return Config.Instance.UseSpeedMultiplier;
    }

    public override float UseAnimationMultiplier(Item item, Player player)
    {
        var config = Config.Instance;

        if (config.UseAnimationMultiplier == 1)
            return base.UseAnimationMultiplier(item, player);

        return config.UseAnimationMultiplier;
    }

    static bool IsWeapon(Item item) => item.damage > 0;

    static bool IsTool(Item item) => (item.pick > 0 || item.axe > 0 || item.hammer > 0)
        && !Config.Instance.AreToolsEffected;
}

class GlobalWepProjectile : GlobalProjectile
{
    static bool spawningProjectiles;

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        if (projectile.owner != Main.myPlayer)
            return;

        // Projectile.NewProjectile invokes OnSpawn, so prevent infinite loop
        if (spawningProjectiles)
            return;

        spawningProjectiles = true;

        var config = Config.Instance;

        // Spawn the additional projectiles on right
        var posRight = projectile.position;

        for (int i = 0; i < config.AdditionalProjectilesRight; i++)
        {
            posRight += new Vector2(config.AdditionalProjectileHorizontalSpacing, 0);
            SpawnProjectile(projectile, source, posRight);
        }

        // Spawn the additional projectiles on left
        var posLeft = projectile.position;

        for (int i = 0; i < config.AdditionalProjectilesLeft; i++)
        {
            posLeft -= new Vector2(config.AdditionalProjectileHorizontalSpacing, 0);
            SpawnProjectile(projectile, source, posLeft);
        }

        spawningProjectiles = false;
    }

    void SpawnProjectile(Projectile projectile, IEntitySource source, Vector2 pos) =>
        Projectile.NewProjectile(
            source, 
            pos, 
            projectile.velocity, 
            projectile.type, 
            projectile.damage, 
            projectile.knockBack, 
            projectile.owner, 
            projectile.ai[0], 
            projectile.ai[1], 
            projectile.ai[2]);

    public override bool PreAI(Projectile projectile)
    {
        if (!Config.Instance.AllProjectilesIgnoreCollision)
        {
            return base.PreAI(projectile);
        }
        else
        {
            if (projectile.owner == Main.myPlayer)
            {
                projectile.tileCollide = !Config.Instance.AllProjectilesIgnoreCollision;
            }
        }

        return base.PreAI(projectile);
    }
}
