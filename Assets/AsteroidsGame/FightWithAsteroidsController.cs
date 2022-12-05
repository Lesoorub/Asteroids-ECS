using AsteroidsEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FightWithAsteroidsController : MonoBehaviour
{
    public FightWithAsteroidsView view;
    public FightWithAsteroidsModel model;

    private void OnEnable()
    {
        model.OnGameStart += Scene_OnGameStart;
        model.OnGameEnd += Scene_OnGameEnd;
    }
    private void OnDisable()
    {
        model.OnGameStart -= Scene_OnGameStart;
        model.OnGameEnd -= Scene_OnGameEnd;
    }

    private void Update()
    {
        view.UpdatePlayerPosition(model.Player);
        view.UpdateLaserCharges(model.Player.LaserCharges);
        view.UpdateLaserReload(model.Player.LaserReaload, model.Settings.LaserChargeReloadTime);

        foreach (IBullet bullet in model.Bullets)
            view.UpdateBullet(bullet);
        foreach (IAsteroid asteroid in model.Asteroids)
            view.UpdateAsteroid(asteroid);
        foreach (IUFO uFO in model.UFOs)
            view.UpdateUFO(uFO);
    }

    public void _Respawn()
    {
        model.Respawn();
    }

    private void Scene_OnGameStart()
    {
        view.ResetView();
        view.SetLives(model.Player.Lives);
        view.SetScore(model.Score);

        view.OnStartGame?.Invoke();

        //new
        view.SetPlayer(model.Player);
        //

        model.Bullets.OnAdded += Bullets_OnAdded;
        model.Bullets.OnRemoved += Bullets_OnRemoved;

        model.Asteroids.OnAdded += Asteroids_OnAdded;
        model.Asteroids.OnRemoved += Asteroids_OnRemoved;

        model.UFOs.OnAdded += UFOs_OnAdded;
        model.UFOs.OnRemoved += UFOs_OnRemoved;

        model.OnScoreUpdate += Model_OnScoreUpdate;

        model.Player.OnLiveConsumed += Player_OnLiveConsumed;
    }

    private void Player_OnLiveConsumed(int newLives)
    {
        view.SetLives(newLives);
    }

    private void Scene_OnGameEnd()
    {
        view.DestroyPlayer();
        view.OnEndGame?.Invoke();

        model.Bullets.OnAdded -= Bullets_OnAdded;
        model.Bullets.OnRemoved -= Bullets_OnRemoved;

        model.Asteroids.OnAdded -= Asteroids_OnAdded;
        model.Asteroids.OnRemoved -= Asteroids_OnRemoved;

        model.UFOs.OnAdded -= UFOs_OnAdded;
        model.UFOs.OnRemoved -= UFOs_OnRemoved;

        model.OnScoreUpdate -= Model_OnScoreUpdate;

        model.Player.OnLiveConsumed -= Player_OnLiveConsumed;
    }

    private void UFOs_OnRemoved(IUFO obj)
    {
        view.DespawnUFO(obj);
    }

    private void UFOs_OnAdded(IUFO obj)
    {
        view.SpawnUFO(obj);
    }

    private void Asteroids_OnRemoved(IAsteroid obj)
    {
        view.DespawnAsteroid(obj);
    }

    private void Asteroids_OnAdded(IAsteroid obj)
    {
        view.SpawnAsteroid(obj);
    }

    private void Bullets_OnRemoved(IBullet obj)
    {
        view.DespawnBullet(obj);
    }

    private void Bullets_OnAdded(IBullet obj)
    {
        view.SpawnBullet(obj);
    }

    private void Model_OnScoreUpdate(int newScore)
    {
        view.SetScore(newScore);
    }
}