using Terraria;
using Terraria.ModLoader;

namespace FasterDamages;

class FastWeapons : GlobalItem
{
    public override float UseSpeedMultiplier(Item item, Player player)
    {
        if (item.pick > 0 || item.axe > 0 || item.hammer > 0) 
            return 1f;

        if (item.DamageType == DamageClass.Magic || 
            item.DamageType == DamageClass.Ranged || 
            item.DamageType == DamageClass.Melee || 
            item.DamageType == DamageClass.Throwing || 
            item.DamageType == DamageClass.Summon)
        {
            if (item.damage > 0)
            {
                return FasterDamagesConfig.Instance.WeaponSpeed;
            }
        }

        return 1f;
    }
}
