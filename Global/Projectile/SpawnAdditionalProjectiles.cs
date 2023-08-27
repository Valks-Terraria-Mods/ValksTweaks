namespace ValksTweaks;

public class SpawnAdditionalProjectiles
{
    class GlobalWepProjectile : GlobalProjectile
    {
        static bool spawningProjectiles;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            var config = Config.Instance;

            // Hook is blacklisted because it was duplicating the players grappling hooks
            // Falling tiles is blacklisted otherwise multiple sand tiles will fall
            // Falling stars is blacklisted otherwise multiple stars will fall at night
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

            AdditionalProjectile(
                requirement: projectile.owner == Main.myPlayer,
                projectile: projectile,
                source: source,
                numProjLeft: config.AdditionalPlayerProjectilesLeft,
                numProjRight: config.AdditionalPlayerProjectilesRight);

            AdditionalProjectile(
                requirement: projectile.owner != Main.myPlayer,
                projectile: projectile,
                source: source,
                numProjLeft: config.AdditionalEnemyProjectilesLeft,
                numProjRight: config.AdditionalEnemyProjectilesRight);
        }

        void AdditionalProjectile(
            bool requirement,
            Projectile projectile,
            IEntitySource source,
            int numProjLeft, 
            int numProjRight)
        {
            if (!requirement)
                return;

            // Projectile.NewProjectile invokes OnSpawn, so prevent infinite loop
            if (spawningProjectiles)
                return;

            spawningProjectiles = true;

            // Spawn the additional projectiles on right
            var config = Config.Instance;
            var posRight = projectile.position;

            for (int i = 0; i < numProjRight; i++)
            {
                posRight += new Vector2(config.AdditionalProjectileHorizontalSpacing, 0);
                SpawnProjectile(projectile, source, posRight);
            }

            // Spawn the additional projectiles on left
            var posLeft = projectile.position;

            for (int i = 0; i < numProjLeft; i++)
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
    }
}
