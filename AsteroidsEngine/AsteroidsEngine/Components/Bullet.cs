using EntityComponentSystem;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// Пуля выпущенная игроком
    /// </summary>
    public class Bullet : Component, IBullet
    {
        public float X => position.X;
        public float Y => position.Y;
        public float SpeedX => speed.SpeedX;
        public float SpeedY => speed.SpeedY;
        public float Rotation { get; private set; }
        Position position;
        Speed speed;
        float destroyTime;

        AsteroidsGameSettings settings;

        public override void OnAwake()
        {
            position = gameObject.GetComponent<Position>();
            speed = gameObject.GetComponent<Speed>();
            settings = (scene as AsteroidsGameScene).settings;
            var circleCollider = gameObject.GetComponent<CircleCollider>();
            circleCollider.Radius = settings.BulletColliderRadius;
            circleCollider.OnCollision += CircleCollider_OnCollision;
            destroyTime = time.ElapsedTimeSinceTheStartOfTheScene + settings.BulletLifetime;
        }

        public override void Tick()
        {
            if (time.ElapsedTimeSinceTheStartOfTheScene >= destroyTime)
            {
                Destroy();
                return;
            }
            settings.Borders.CorrectPosition(ref position.X, ref position.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        /// <param name="rotation">Ship rotation in degrees</param>
        /// <returns></returns>
        public static GameObject Spawn(IScene scene, float x, float y, float speed, float rotation, float distanceFromPlayer)
        {
            return scene.Instantiate(new Prefab(
                OnBeforeOnAwake: (gameObject) =>
                {
                    var positionComponent = gameObject.GetComponent<Position>();

                    var (dirX, dirY) = AngleHelper.DegreesToDirection(rotation);

                    positionComponent.X = x + distanceFromPlayer * dirX;
                    positionComponent.Y = y + distanceFromPlayer * dirY;


                    var speedComponent = gameObject.GetComponent<Speed>();

                    speedComponent.SpeedX = dirX * speed;
                    speedComponent.SpeedY = dirY * speed;

                    var bulletComponent = gameObject.GetComponent<Bullet>();
                    bulletComponent.Rotation = rotation + 90;

                    var circleCollider = gameObject.GetComponent<CircleCollider>();
                    circleCollider.Layer = CollisionLayers.Bullet;
                },
                typeof(Position),
                typeof(Speed),
                typeof(Bullet),
                typeof(CircleCollider)
            ));
        }

        private void CircleCollider_OnCollision(CircleCollider anotherCollider)
        {
            anotherCollider.Destroy();
            Destroy();
        }
    }
}
