﻿namespace ValksTweaks;

public class VModPlayer : ModPlayer
{
    public override void PostUpdateRunSpeeds()
    {
        Config config = ModContent.GetInstance<Config>();

        float runAcc = config.PlayerRunAcceleration;
        float maxSpd = config.PlayerMaxRunSpeed;
        float maxFallSpd = config.PlayerMaxFallSpeed;

        if (runAcc != 0)
            Player.runAcceleration = runAcc;
        
        if (maxSpd != 0)
            Player.maxRunSpeed = maxSpd;

        if (maxFallSpd != 0)
            Player.maxFallSpeed = config.PlayerMaxFallSpeed;
    }

    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        int playerRespawnTime = ModContent.GetInstance<Config>().PlayerRespawnTime;
        Player.respawnTimer = playerRespawnTime;
    }

    public override void OnRespawn()
    {
        if (ModContent.GetInstance<Config>().ShouldPlayerRespawnWithFullHealth)
            Player.Heal(250);
    }
}
