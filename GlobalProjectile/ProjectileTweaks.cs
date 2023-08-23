namespace ValksTweaks;

internal class ProjectileTweaks
{
    class GlobalWepProjectile : GlobalProjectile
    {
        static bool spawningProjectiles;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            var blacklistedAIStyles = new short[]
        {
            ProjAIStyleID.Hook,
            ProjAIStyleID.FallingTile,
            ProjAIStyleID.FallingStar,
            ProjAIStyleID.FallingStarAnimation
        };

            foreach (var style in blacklistedAIStyles)
                if (projectile.aiStyle == style)
                    return;

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
}
