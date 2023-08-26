namespace ValksTweaks;

public class VModPlayer : ModPlayer
{
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
    {
        int playerRespawnTime = ModContent.GetInstance<Config>().PlayerRespawnTime;
        Main.LocalPlayer.respawnTimer = playerRespawnTime;
    }
}
