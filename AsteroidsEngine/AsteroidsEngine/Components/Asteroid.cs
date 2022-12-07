using EntityComponentSystem;
using System;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// Астероид
    /// </summary>
    public class Asteroid : Component, IAsteroid
    {
        public float X => position.X;
        public float Y => position.Y;
        public float SpeedX => speed.SpeedX;
        public float SpeedY => speed.SpeedY;
        public float Rotation { get; private set; }
        public AsteroidSize Size { get; private set; } = AsteroidSize.Big;

        Position position;
        Speed speed;
        AsteroidsGameSettings settings;
        public override void OnAwake()
        {
            position = gameObject.GetComponent<Position>();
            speed = gameObject.GetComponent<Speed>();
            settings = (scene as AsteroidsGameScene).settings;
            var collider = gameObject.GetComponent<CircleCollider>();

            collider.OnCollision += Collider_OnCollision;

            var (dirX, dirY) = GetRandomDirectory();
            float magnitude = 1;
            switch (Size)
            {
                case AsteroidSize.Big:
                    magnitude = settings.BigAsteroidSpeed;
                    collider.Radius = settings.BigAsteroidColliderRadius;
                    break;
                case AsteroidSize.Small:
                    magnitude = settings.SmallAsteroidSpeed;
                    collider.Radius = settings.SmallAsteroidColliderRadius;
                    break;
            }
            speed.SpeedX = dirX * magnitude;
            speed.SpeedY = dirY * magnitude;
        }
        public override void OnDestroyed()
        {
            var scene = this.scene as AsteroidsGameScene;
            switch (Size)
            {
                case AsteroidSize.Big:
                    DivideByChunks();
                    scene.AddScore(settings.AddScoreByDestroyBigAsteroid);
                    break;
                case AsteroidSize.Small:
                    scene.AddScore(settings.AddScoreByDestroySmallAsteroid);
                    break;
            }
        }

        private void Collider_OnCollision(CircleCollider anotherCollider)
        {
            //Уничтожаем астероид при столкновении
            Destroy();
        }

        public override void Tick()
        {
            settings.Borders.CorrectPosition(ref position.X, ref position.Y);
        }
        private void DivideByChunks()
        {
            var asteroidPrefab = GetPrefab(scene, X, Y);
            asteroidPrefab.onBeforeOnAwake += (gameobject) =>
            {
                var asteroidComponent = gameobject.GetComponent<Asteroid>();
                asteroidComponent.Size = AsteroidSize.Small;

            };
            Random rnd = new Random();
            float offsetAngle = (float)rnd.NextDouble();
            for (int k = 0; k < settings.SpawnChunksOnBigAsteroidHasDestroyed; k++)
            {
                var asteroid = scene.Instantiate(asteroidPrefab);

                var speedComponent = asteroid.GetComponent<Speed>();
                float angle = ((float)k / settings.SpawnChunksOnBigAsteroidHasDestroyed + offsetAngle) * (float)Math.PI * 2;
                speedComponent.SpeedX = (float)Math.Sin(angle) * settings.SmallAsteroidSpeed;
                speedComponent.SpeedY = (float)Math.Cos(angle) * settings.SmallAsteroidSpeed;

            }
        }

        private static Random rnd = new Random();
        public static Prefab GetPrefab(IScene scene, float x, float y)
        {
            return new Prefab(
                OnBeforeOnAwake: (gameObject) =>
                {
                    var positionComponent = gameObject.GetComponent<Position>();
                    positionComponent.X = x;
                    positionComponent.Y = y;

                    var circleCollider = gameObject.GetComponent<CircleCollider>();
                    circleCollider.Layer = CollisionLayers.Asteroid;
                },
                typeof(Position),
                typeof(Speed),
                typeof(Asteroid),
                typeof(CircleCollider)
                );
        }
        private static (float x, float y) GetRandomDirectory()
        {
            float angle = (float)(rnd.NextDouble() * Math.PI * 2);
            return ((float)Math.Sin(angle), (float)Math.Cos(angle));
        }
    }
}
