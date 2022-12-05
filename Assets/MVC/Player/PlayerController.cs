using AsteroidsEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel model;
    public PlayerView view;

    IPlayer player;

    public void SetPlayer(IPlayer player)
    {
        if (this.player != null)
        {
            player.OnLiveConsumed -= Player_OnLiveConsumed;
        }
        this.player = player;
        player.OnLiveConsumed += Player_OnLiveConsumed;
    }

    private void Player_OnLiveConsumed(int newLives)
    {
        view.SetLives(model.Lives);
    }

    public void Update()
    {
        if (player == null) return;
        view.SetPlayerTransform(player.X, player.Y, player.Rotation);
    }
}
