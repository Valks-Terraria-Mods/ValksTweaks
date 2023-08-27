namespace ValksTweaks;

public class ProjectileGlobal : GlobalProjectile
{
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
