namespace ValksTweaks;

public class DisableTombstones : GlobalProjectile
{
    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        var config = Config.Instance;

        if (config.DisablePlayerTombstones &&
            tombstoneProjectileIds.Contains(projectile.type))
        {
            projectile.active = false;
            return;
        }
    }

    static readonly HashSet<int> tombstoneProjectileIds = new()
    {
        ProjectileID.Tombstone,
        ProjectileID.GraveMarker,
        ProjectileID.CrossGraveMarker,
        ProjectileID.Headstone,
        ProjectileID.Gravestone,
        ProjectileID.Obelisk,
        ProjectileID.RichGravestone1,
        ProjectileID.RichGravestone2,
        ProjectileID.RichGravestone3,
        ProjectileID.RichGravestone4,
        ProjectileID.RichGravestone5
    };
}
