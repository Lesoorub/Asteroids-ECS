namespace AsteroidsEngine
{
    /// <summary>
    /// Настройки игры Астероиды
    /// </summary>
    public class AsteroidsGameSettings
    {
        //Generation
        /// <summary>
        /// Время между спавном опасных объектов на сцене в секундах
        /// </summary>
        public float ObjectSpawnDelay = 2f;

        // Score
        /// <summary>
        /// Очки доваемые за уничтожение малого астероида
        /// </summary>
        public int AddScoreByDestroySmallAsteroid = 10;
        /// <summary>
        /// Очки доваемые за уничтожение большого астероида
        /// </summary>
        public int AddScoreByDestroyBigAsteroid = 10;
        /// <summary>
        /// Очки доваемые за уничтожение НЛО
        /// </summary>
        public int AddScoreByDestroyUFO = 50;

        // Player
        /// <summary>
        /// Максимальное число зарядов лазера
        /// </summary>
        public int MaxLasetCharges = 3;
        /// <summary>
        /// Длительность работы лазера в секундах
        /// </summary>
        public int LaserShootDuration = 1;
        /// <summary>
        /// Время перезарядки заряда лазера в секундах
        /// </summary>
        public float LaserChargeReloadTime = 10;
        /// <summary>
        /// Максимальное количество жизней игрока
        /// </summary>
        public int MaxPlayerHealth = 3;
        /// <summary>
        /// Сопротивление движению в пространстве
        /// </summary>
        public float PlayerDrag = 1;
        /// <summary>
        /// Длительность неуязвимости игрока в секундах
        /// </summary>
        public float InvincibilityTime = 1;

        // UFO
        /// <summary>
        /// Сокрость НЛО
        /// </summary>
        public float UFOSpeed = 1;
        /// <summary>
        /// Шанс спавна НЛо
        /// </summary>
        public float UFOSpawnChance = 0.05f;
        // Asteroid
        /// <summary>
        /// Скорость большого астероида
        /// </summary>
        public float BigAsteroidSpeed = 5;
        /// <summary>
        /// Сокрость малого астероида
        /// </summary>
        public float SmallAsteroidSpeed = 3;
        /// <summary>
        /// Количество создаваемых осколков астероида при его уничтожении
        /// </summary>
        public int SpawnChunksOnBigAsteroidHasDestroyed = 3;
        //Bullet
        /// <summary>
        /// Расстояние спавна пули от игрока
        /// </summary>
        public float BulletSpawnDistanceFromPlayer = 0.5f;
        /// <summary>
        /// Время перезарядки основной атаки у игрока в секундах
        /// </summary>
        public float BulletReload = 0.5f;
        /// <summary>
        /// Скорость полета пули
        /// </summary>
        public float BulletSpeed = 5;
        /// <summary>
        /// Длительность жизни пули в секундах
        /// </summary>
        public float BulletLifetime = 3;
        /// <summary>
        /// Границы мира, в пределах которых должны быть зациклены все игровые объекты
        /// </summary>
        public Border Borders = new Border()
        {
            Left = -5,
            Right = 5,
            Top = 3,
            Bottom = -3,
        };

        /// <summary>
        /// Объект границы мира
        /// </summary>
        public class Border
        {
            /// <summary>
            /// Левый край
            /// </summary>
            public float Left;
            /// <summary>
            /// Правый край
            /// </summary>
            public float Right;
            /// <summary>
            /// Верхний край
            /// </summary>
            public float Top;
            /// <summary>
            /// Нижний край
            /// </summary>
            public float Bottom;

            /// <summary>
            /// Размер по оси X
            /// </summary>
            public float SizeX => Right - Left;
            /// <summary>
            /// Размер по оси Y
            /// </summary>
            public float SizeY => Top - Bottom;

            /// <summary>
            /// Зациклить переданные координаты
            /// </summary>
            /// <param name="x">координата по оси X</param>
            /// <param name="y">координата по оси Y</param>
            public void CorrectPosition(ref float x, ref float y)
            {
                if (x < Left)
                    x = Right;
                if (x > Right)
                    x = Left;

                if (y < Bottom)
                    y = Top;
                if (y > Top)
                    y = Bottom;
            }
        }
    }
}