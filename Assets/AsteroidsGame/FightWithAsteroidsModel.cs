using AsteroidsEngine;
using System.Collections.Generic;
using UnityEngine;

public class FightWithAsteroidsModel : MonoBehaviour
{
    public float Speed = 1;
    public AsteroidsGameSettings Settings;

    public int Score => scene.Score;
    public IPlayer Player => scene.Player;
    public ObservableList<IAsteroid> Asteroids => scene.Asteroids;
    public ObservableList<IUFO> UFOs => scene.UFOs;
    public ObservableList<IBullet> Bullets => scene.Bullets;


    public delegate void GameStartedArgs();
    public event GameStartedArgs OnGameStart;
    public delegate void GameEndArgs();
    public event GameStartedArgs OnGameEnd;
    public delegate void ScoreUpdateArgs(int newScore);
    public event ScoreUpdateArgs OnScoreUpdate;
    public delegate void SceneChangedArgs(AsteroidsGameScene scene);
    public event SceneChangedArgs OnSceneChanged;

    AsteroidsGameScene scene;
    Camera cam;


    private void Start()
    {
        cam = Camera.main;

        OnSceneChanged += SceneChanged;

        var verticalSize = cam.orthographicSize * 2f;
        var horizontalSize = verticalSize * ((float)Screen.width / Screen.height);

        var halfScreenInWorldPoint = -cam.ScreenToWorldPoint(
            new Vector3(
                verticalSize,
                horizontalSize) / 2f);

        float offset = 1;

        Settings = new AsteroidsGameSettings()
        {
            BulletSpeed = 10,
            BulletReload = 0.4f,
            BulletColliderRadius = 0.05f,

            PlayerColliderRadius = 0.3f,

            UFOColliderRadius = 0.5f,

            BigAsteroidSpeed = 2f,
            SmallAsteroidSpeed = 3f,
            BigAsteroidColliderRadius = 0.4f,
            SmallAsteroidColliderRadius = 0.2f,

            LaserWidth = 0.05f,

            UFOSpawnChance = 0.05f,
            Borders = new AsteroidsGameSettings.Border()
            {
                Left = -halfScreenInWorldPoint.x - offset,
                Right = halfScreenInWorldPoint.x + offset,
                Bottom = -halfScreenInWorldPoint.y - offset,
                Top = halfScreenInWorldPoint.y + offset,
            }
        };

        CreateNewScene();
    }

    void Update()
    {
        var player = scene.Player;
        if (player == null) return;
        if (Input.GetKey(KeyCode.W))
            player.AddSpeed(0, Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            player.AddSpeed(0, -Speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            player.AddSpeed(Speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.A))
            player.AddSpeed(-Speed * Time.deltaTime, 0);

        var mpos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (player.X != mpos.x && scene.Player.Y != mpos.y)
            player.Rotation = Mathf.Atan2(mpos.y - player.Y, mpos.x - player.X) * Mathf.Rad2Deg;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ShootLaser();
        }

        scene.Update(Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (scene == null) return;
        scene.OnGameStart -= Scene_OnGameStart;
        scene.OnGameEnd -= Scene_OnGameEnd;
        scene.Dispose();
    }
    private void Scene_OnScoreUpdate(int newScore)
    {
        OnScoreUpdate?.Invoke(newScore);
    }

    private void Scene_OnGameEnd()
    {
        OnGameEnd?.Invoke();
    }

    private void Scene_OnGameStart()
    {
        OnGameStart?.Invoke();
    }

    void CreateNewScene()
    {
        scene = new AsteroidsGameScene();
        OnSceneChanged?.Invoke(scene);
    }
    void SceneChanged(AsteroidsGameScene scene)
    {
        scene.Setup(Settings);

        scene.OnGameStart += Scene_OnGameStart;
        scene.OnGameEnd += Scene_OnGameEnd;
        scene.OnScoreUpdate += Scene_OnScoreUpdate;

        scene.Start();
    }
    public void Respawn()
    {
        scene.OnGameStart -= Scene_OnGameStart;
        scene.OnGameEnd -= Scene_OnGameEnd;
        scene.OnScoreUpdate -= Scene_OnScoreUpdate;

        CreateNewScene();
    }
}