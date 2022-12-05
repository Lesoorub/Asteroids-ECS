using EntityComponentSystem;
using System;
using static AsteroidsEngine.AsteroidsGameSettings;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// Компонент игрока
    /// </summary>
    public class Player : Component, IPlayer
    {
        public int Lives { get; set; }
        public float X => position.X;
        public float Y => position.Y;
        public float SpeedX => speed.SpeedX;
        public float SpeedY => speed.SpeedY;
        public float Rotation { get; set; }
        public float Drag { get => speed.Drag; set => speed.Drag = value; }
        public int LaserCharges { get; private set; }
        public bool LaserIsShooting { get; private set; }
        public float LaserChargeReload => Math.Max(timeWhenLaserCharge - time.ElapsedTimeSinceTheStartOfTheScene, 0);
        public bool IsInvincibility => time.ElapsedTimeSinceTheStartOfTheScene < invincibilityEndTime;

        public delegate void LiveConsumed(int newLives);
        public event LiveConsumed OnLiveConsumed;

        public delegate void LaserChargesChangedArgs(int newLaserChargesCount);
        public event LaserChargesChangedArgs OnLaserChargesChanged;

        //Хэшированные компоненты
        Position position;
        Speed speed;
        CircleCollider circleCollider;

        /// <summary>
        /// Время окончания перезарядки лазера
        /// </summary>
        float timeWhenLaserCharge;
        /// <summary>
        /// Время окончания выстрела лазера.
        /// На протяжении всего выстрела лазер уничтожает объекты
        /// </summary>
        float laserShootEndTime;
        /// <summary>
        /// Время готовности основного орудия к выстрелу
        /// </summary>
        float shotReadyTime;
        /// <summary>
        /// Время окончания неуязвимости
        /// </summary>
        float invincibilityEndTime;

        Border border;
        AsteroidsGameSettings settings;
        
        //Перегрузки методов
        public override void OnAwake()
        {
            position = gameObject.GetComponent<Position>();
            speed = gameObject.GetComponent<Speed>();
            circleCollider = gameObject.GetComponent<CircleCollider>();
            settings = (scene as AsteroidsGameScene).settings;

            circleCollider.Radius = settings.PlayerColliderRadius;
            circleCollider.OnCollision += CircleCollider_OnCollision;

            speed.Drag = settings.PlayerDrag;
            border = settings.Borders;
            Drag = settings.PlayerDrag;
            timeWhenLaserCharge = time.ElapsedTimeSinceTheStartOfTheScene + settings.LaserChargeReloadTime;
            Lives = settings.MaxPlayerHealth;
        }

        public override void Tick()
        {
            if (LaserCharges < settings.MaxLasetCharges &&
                time.ElapsedTimeSinceTheStartOfTheScene >= timeWhenLaserCharge)
            {
                LaserCharges++;
                timeWhenLaserCharge = time.ElapsedTimeSinceTheStartOfTheScene + settings.LaserChargeReloadTime;
                OnLaserChargesChanged?.Invoke(LaserCharges);
            }
            if (LaserIsShooting)
            {
                if (time.ElapsedTimeSinceTheStartOfTheScene >= laserShootEndTime)
                {
                    LaserIsShooting = false;
                }
                else
                {
                    //TODO
                    //laser logic
                    //foreach (var asteroid in (scene as IAsteroidsEngine).Asteroids)
                }
            }

            if (position.X < border.Left)
                position.X = border.Right;
            if (position.X > border.Right)
                position.X = border.Left;

            if (position.Y < border.Bottom)
                position.Y = border.Top;
            if (position.Y > border.Top)
                position.Y = border.Bottom;
        }
        public override void OnDestroyed()
        {
            circleCollider.OnCollision -= CircleCollider_OnCollision;
        }

        //Обработчики событий
        /// <summary>
        /// Оброботчик столкновения
        /// </summary>
        /// <param name="anotherCollider">Коллайдер с которым сталкивается объект</param>
        private void CircleCollider_OnCollision(CircleCollider anotherCollider)
        {
            if (time.ElapsedTimeSinceTheStartOfTheScene >= invincibilityEndTime)
                ConsumeLive();
        }

        //Методы
        public void AddSpeed(float speedX, float speedY)
        {
            speed.SpeedX += speedX;
            speed.SpeedY += speedY;
        }

        /// <summary>
        /// Уменьшить количество жизней игрока на 1
        /// Если жизни кончались вызвать окончание игры
        /// </summary>
        void ConsumeLive()
        {
            if (Lives <= 0)
            {
                (scene as AsteroidsGameScene)?.EndGame();
                return;
            }
            Lives--;
            invincibilityEndTime = time.ElapsedTimeSinceTheStartOfTheScene + settings.InvincibilityTime;
            OnLiveConsumed?.Invoke(Lives);
        }

        public void Shoot()
        {
            if (time.ElapsedTimeSinceTheStartOfTheScene >= shotReadyTime)
            {
                shotReadyTime = time.ElapsedTimeSinceTheStartOfTheScene + settings.BulletReload;
                Bullet.Spawn(
                    scene,
                    X,
                    Y,
                    settings.BulletSpeed,
                    Rotation,
                    settings.BulletSpawnDistanceFromPlayer);
            }
        }
        
        public void ShootLaser()
        {
            if (LaserCharges <= 0) return;
            LaserCharges--;
            laserShootEndTime = time.ElapsedTimeSinceTheStartOfTheScene + settings.LaserShootDuration;
            LaserIsShooting = true;
            OnLaserChargesChanged?.Invoke(LaserCharges);
        }

        //Статические методы
        /// <summary>
        /// Спавн игрока на сцене по координатам
        /// </summary>
        /// <param name="scene">Сцена на которой будет заспавнен игрок</param>
        /// <param name="x">Позиция по оси X</param>
        /// <param name="y">Позиция по оси Y</param>
        /// <returns></returns>
        public static GameObject Spawn(IScene scene, float x, float y)
        {
            return scene.Instantiate(new Prefab(
                (gameobject) =>
                {
                    var position = gameobject.GetComponent<Position>();
                    position.X = x;
                    position.Y = y;

                    var circleCollider = gameobject.GetComponent<CircleCollider>();
                    circleCollider.Layer = CollisionLayers.Player;
                },
                typeof(Position),
                typeof(Speed),
                typeof(CircleCollider),
                typeof(Player)
            ));
        }
    }
}
