using AsteroidsEngine;
using EntityComponentSystem;
using System;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// НЛО
    /// </summary>
    public class UFO : Component, IUFO
    {
        public float X => position.X;
        public float Y => position.Y;
        public float SpeedX => speed.SpeedX;
        public float SpeedY => speed.SpeedY;

        Position position;
        Speed speed;
        IPlayer target;
        float maxSpeed;

        public override void OnAwake()
        {
            var ast_scene = (scene as AsteroidsGameScene);
            position = gameObject.GetComponent<Position>();
            speed = gameObject.GetComponent<Speed>();
            target = ast_scene.Player;
            maxSpeed = ast_scene.settings.UFOSpeed;
        }

        public override void Tick()
        {
            var dirX = target.X - position.X;
            var dirY = target.Y - position.Y;

            if (dirX != 0 || dirY != 0)
            {
                var dir_len = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                dirX /= dir_len;
                dirY /= dir_len;

                speed.SpeedX = dirX * maxSpeed;
                speed.SpeedY = dirY * maxSpeed;
            }

        }

        public override void OnDestroyed()
        {
            var scene = this.scene as AsteroidsGameScene;
            scene.AddScore(scene.settings.AddScoreByDestroyUFO);
        }

        public static GameObject Spawn(IScene scene, float x, float y)
        {
            return scene.Instantiate(new Prefab(
                OnBeforeOnAwake: (gameobject) =>
                {
                    var positionComponent = gameobject.GetComponent<Position>();
                    positionComponent.X = x;
                    positionComponent.Y = y;

                    var circleCollider = gameobject.GetComponent<CircleCollider>();
                    circleCollider.Layer = CollisionLayers.UFO;
                },
                typeof(Position),
                typeof(Speed),
                typeof(UFO),
                typeof(CircleCollider)
                ));
        }
    }
}