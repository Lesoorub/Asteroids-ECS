using AsteroidsEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel model;
    public PlayerView view;

    IPlayer player;
    AsteroidsGameSettings settings;

    public void Update()
    {
        if (player == null) return;
        view.SetPlayerTransform(player.X, player.Y, player.Rotation);
        view.SetPlayerInfo(player.X, player.Y, player.SpeedX, player.SpeedY);
        view.SetLaserReload(player.LaserChargeReload, settings.LaserChargeReloadTime);
    }

    public void PlayerSpawn(IPlayer player, AsteroidsGameSettings Settings)
    {
        view.SpawnPlayer();
        view.SetLaserCharges(player.LaserCharges);
        view.SetLives(player.Lives);

        this.player = player;
        this.settings = Settings;

        if (this.player == null) return;
        this.player.OnLiveConsumed += Player_OnLiveConsumed;
        this.player.OnLaserChargesChanged += Player_OnLaserChargesChanged;
    }

    public void PlayerDie()
    {
        view.PlayerDie();

        if (this.player == null) return;
        this.player.OnLiveConsumed -= Player_OnLiveConsumed;
        this.player.OnLaserChargesChanged -= Player_OnLaserChargesChanged;

        this.player = null;
    }

    private void Player_OnLaserChargesChanged(int newLaserChargesCount)
    {
        view.SetLaserCharges(newLaserChargesCount);
    }

    private void Player_OnLiveConsumed(int newLives)
    {
        view.SetLives(newLives);
    }
}
