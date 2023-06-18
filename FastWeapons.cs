﻿using Terraria;
using Terraria.ModLoader;

namespace FasterDamages;

class FastWeapons : GlobalItem
{
    public override float UseTimeMultiplier(Item item, Player player)
    {
        if (item.pick > 0 || item.axe > 0 || item.hammer > 0) return 1f;
        if (item.magic || item.ranged || item.melee || item.ranged || item.thrown || item.summon)
        {
            if (item.damage > 0)
            {
                return FasterDamagesConfig.Instance.WeaponSpeed;
            }
        }
        return 1f;
    }
}
