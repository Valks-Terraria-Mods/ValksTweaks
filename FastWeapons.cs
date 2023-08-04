﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FasterDamages;

class GlobalWepItem : GlobalItem
{
    public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
    {
        if (FasterDamagesConfig.Instance.InfiniteAmmo)
            return false;

        return base.CanConsumeAmmo(weapon, ammo, player);
    }

    public override bool NeedsAmmo(Item item, Player player)
    {
        if (FasterDamagesConfig.Instance.InfiniteAmmo)
            return false;

        return base.NeedsAmmo(item, player);
    }

    public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        var config = FasterDamagesConfig.Instance;
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

        damage *= FasterDamagesConfig.Instance.ProjectileDamageMultiplier;
        velocity *= FasterDamagesConfig.Instance.ProjectileVelocityMultiplier;
    }

    public override bool? CanAutoReuseItem(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.CanAutoReuseItem(item, player);

        return FasterDamagesConfig.Instance.AllWeaponAutoReuse;
    }

    public override float UseTimeMultiplier(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.UseTimeMultiplier(item, player);

        return FasterDamagesConfig.Instance.UseTimeMultiplier;
    }

    // This seems to only affect how fast the swing art animation is played
    public override float UseSpeedMultiplier(Item item, Player player)
    {
        if (IsTool(item) || !IsWeapon(item))
            return base.UseAnimationMultiplier(item, player);

        // A value of 4 seems good
        return FasterDamagesConfig.Instance.UseSpeedMultiplier;
    }

    static bool IsWeapon(Item item) => item.damage > 0;

    static bool IsTool(Item item) => (item.pick > 0 || item.axe > 0 || item.hammer > 0)
        && !FasterDamagesConfig.Instance.AreToolsEffected;
}

class GlobalWepProjectile : GlobalProjectile
{
    public override bool PreAI(Projectile projectile)
    {
        if (!FasterDamagesConfig.Instance.AllProjectilesIgnoreCollision)
        {
            return base.PreAI(projectile);
        }
        else
        {
            if (projectile.owner == Main.myPlayer)
            {
                projectile.tileCollide = !FasterDamagesConfig.Instance.AllProjectilesIgnoreCollision;
            }
        }

        return base.PreAI(projectile);
    }
}
