using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using AsteroidsEngine;

public class FightWithAsteroidsView : MonoBehaviour
{
    [Header("Instances")]
    public TMP_TextArgumentExtention EndGameScore;
    public UnityEvent OnEndGame;
    public UnityEvent OnStartGame;
    public PlayerController playerController;
    public InfoController InfoController;

    [Header("Prefabs")]
    public GameObject BulletPrefab;
    public GameObject UfoPrefab;
    public GameObject AsteroidPrefab;
    Dictionary<IAsteroid, GameObject> Asteroids = new Dictionary<IAsteroid, GameObject>();
    Dictionary<IBullet, GameObject> Bullets = new Dictionary<IBullet, GameObject>();
    Dictionary<IUFO, GameObject> UFOs = new Dictionary<IUFO, GameObject>();

    public void SetScore(int score)
    {
        InfoController.SetScore(score);
        EndGameScore.SetArguments(("Score", score));
    }
    public void PlayerSpawn(IPlayer player, AsteroidsGameSettings Settings)
    {
        playerController.PlayerSpawn(player, Settings);
    }
    public void PlayerDie()
    {
        playerController.PlayerDie();
    }

    public void ResetView()
    {
        foreach (var pair in Asteroids)
            Destroy(pair.Value);
        foreach (var pair in Bullets)
            Destroy(pair.Value);
        foreach (var pair in UFOs)
            Destroy(pair.Value);
    }

    public void SpawnBullet(IBullet bullet)
    {
        var bulletObj = Instantiate(
            BulletPrefab,
            new Vector3(bullet.X, bullet.Y),
            Quaternion.AngleAxis(bullet.Rotation, Vector3.forward),
            null);
        if (bulletObj.TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = new Vector2(bullet.SpeedX, bullet.SpeedY);

        Bullets.Add(bullet, bulletObj);
    }
    public void UpdateBullet(IBullet bullet)
    {
        if (Bullets.TryGetValue(bullet, out var bulletObj))
        {
            bulletObj.transform.position = new Vector3(bullet.X, bullet.Y);
            bulletObj.transform.rotation = Quaternion.AngleAxis(bullet.Rotation, Vector3.forward);
            if (bulletObj.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = new Vector2(bullet.SpeedX, bullet.SpeedY);
        }
    }
    public void DespawnBullet(IBullet bullet)
    {
        if (Bullets.TryGetValue(bullet, out var bulletObj))
        {
            Destroy(bulletObj);
            Bullets.Remove(bullet);
        }
    }


    public void SpawnAsteroid(IAsteroid asteroid)
    {
        var asteroidObj = Instantiate(
            AsteroidPrefab,
            new Vector3(asteroid.X, asteroid.Y),
            Quaternion.AngleAxis(asteroid.Rotation, Vector3.forward),
            null);
        if (asteroidObj.TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = new Vector2(asteroid.SpeedX, asteroid.SpeedY);

        switch (asteroid.Size)
        {
            case AsteroidSize.Big:
                break;
            case AsteroidSize.Small:
                asteroidObj.transform.localScale /= 2;
                break;
        }

        Asteroids.Add(asteroid, asteroidObj);
    }
    public void UpdateAsteroid(IAsteroid asteroid)
    {
        if (Asteroids.TryGetValue(asteroid, out var asteroidObj))
        {
            asteroidObj.transform.position = new Vector3(asteroid.X, asteroid.Y);
            asteroidObj.transform.rotation = Quaternion.AngleAxis(asteroid.Rotation, Vector3.forward);
            if (asteroidObj.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = new Vector2(asteroid.SpeedX, asteroid.SpeedY);
        }
    }
    public void DespawnAsteroid(IAsteroid asteroid)
    {
        if (Asteroids.TryGetValue(asteroid, out var asteroidObj))
        {
            Destroy(asteroidObj);
            Asteroids.Remove(asteroid);
        }
    }


    public void SpawnUFO(IUFO ufo)
    {
        var asteroidObj = Instantiate(
            UfoPrefab,
            new Vector3(ufo.X, ufo.Y),
            Quaternion.identity,
            null);
        if (asteroidObj.TryGetComponent<Rigidbody2D>(out var rb))
            rb.velocity = new Vector2(ufo.SpeedX, ufo.SpeedY);

        UFOs.Add(ufo, asteroidObj);
    }
    public void UpdateUFO(IUFO ufo)
    {
        if (UFOs.TryGetValue(ufo, out var asteroidObj))
        {
            asteroidObj.transform.position = new Vector3(ufo.X, ufo.Y);
            if (asteroidObj.TryGetComponent<Rigidbody2D>(out var rb))
                rb.velocity = new Vector2(ufo.SpeedX, ufo.SpeedY);
        }
    }
    public void DespawnUFO(IUFO ufo)
    {
        if (UFOs.TryGetValue(ufo, out var ufoObj))
        {
            Destroy(ufoObj);
            UFOs.Remove(ufo);
        }
    }
}
