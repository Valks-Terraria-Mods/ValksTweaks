All weapons (ranged, melee, mage, summon, thrown) operate 25% faster as shown below.

```cs
if (item.ranged || item.melee || item.ranged || item.thrown || item.summon)
{
    if (item.damage > 0)
    {
        return 1.25f;
    }
}
```
