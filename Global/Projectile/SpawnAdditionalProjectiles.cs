namespace ValksTweaks;

class AdditionalProjNPC : GlobalNPC
{
    static bool spawningProj;

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        if (!NPCID.Sets.ProjectileNPC[npc.type] || spawningProj)
            return;

        var config = Config.Instance;

        int numProjRight = config.AdditionalEnemyProjectilesRight;
        int numProjLeft = config.AdditionalEnemyProjectilesLeft;
        int horzSpacing = config.AdditionalProjectileHorizontalSpacing;

        if (numProjRight != 0)
        {
            spawningProj = true;

            // Spawn the additional projectiles on right
            var posRight = npc.position;

            for (int i = 0; i < numProjRight; i++)
            {
                posRight += new Vector2(horzSpacing, 0);
                SpawnNPC(npc, source, (int)posRight.X, (int)posRight.Y);
            }

            spawningProj = false;
        }

        if (numProjLeft != 0)
        {
            spawningProj = true;

            // Spawn the additional projectiles on right
            var posLeft = npc.position;

            for (int i = 0; i < numProjLeft; i++)
            {
                posLeft -= new Vector2(horzSpacing, 0);
                SpawnNPC(npc, source, (int)posLeft.X, (int)posLeft.Y);
            }

            spawningProj = false;
        }
    }

    void SpawnNPC(NPC npc, IEntitySource source, int x, int y)
    {
        NPC.NewNPC(
            source,
            x,
            y,
            npc.type,
            npc.whoAmI,
            npc.ai[0],
            npc.ai[1],
            npc.ai[2],
            npc.ai[3],
            npc.target
            );
    }
}
class AdditionalProj : GlobalProjectile
{
    static bool spawningAdditionalProjPlayer;
    static bool spawningAdditionalProjEnemy;

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        var config = Config.Instance;

        // Projectile.NewProjectile invokes OnSpawn, so prevent infinite loop
        if (spawningAdditionalProjPlayer ||
            spawningAdditionalProjEnemy)
            return;

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
            requirement: projectile.owner == Main.myPlayer && projectile.friendly,
            projectile: projectile,
            source: source,
            numProjLeft: config.AdditionalPlayerProjectilesLeft,
            numProjRight: config.AdditionalPlayerProjectilesRight,
            ref spawningAdditionalProjPlayer);

        AdditionalProjectile(
            requirement: !projectile.friendly,
            projectile: projectile,
            source: source,
            numProjLeft: config.AdditionalEnemyProjectilesLeft,
            numProjRight: config.AdditionalEnemyProjectilesRight,
            ref spawningAdditionalProjEnemy);
    }

    void AdditionalProjectile(
        bool requirement,
        Projectile projectile,
        IEntitySource source,
        int numProjLeft,
        int numProjRight,
        ref bool spawningAdditionalProjectiles)
    {
        if (!requirement)
            return;

        // Spawn the additional projectiles on right
        var config = Config.Instance;

        if (numProjRight != 0)
        {
            spawningAdditionalProjectiles = true;

            var posRight = projectile.position;

            for (int i = 0; i < numProjRight; i++)
            {
                posRight += new Vector2(config.AdditionalProjectileHorizontalSpacing, 0);
                SpawnProjectile(projectile, source, posRight);
            }

            spawningAdditionalProjectiles = false;
        }

        // Spawn the additional projectiles on left
        if (numProjLeft != 0)
        {
            spawningAdditionalProjectiles = true;

            var posLeft = projectile.position;

            for (int i = 0; i < numProjLeft; i++)
            {
                posLeft -= new Vector2(config.AdditionalProjectileHorizontalSpacing, 0);
                SpawnProjectile(projectile, source, posLeft);
            }

            spawningAdditionalProjectiles = false;
        }
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
