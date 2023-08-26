namespace ValksTweaks;

public class VModPlayer : ModPlayer
{
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
