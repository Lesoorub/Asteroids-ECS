using AsteroidsEngine.Components;
using EntityComponentSystem;
using SimpleLogger;

namespace AsteroidsEngine
{
    /// <summary>
    /// Основная сцена игры Астероиды
    /// </summary>
    public class AsteroidsGameScene : Scene, IAsteroidsEngine
    {
        /// <summary>
        /// Настройки игры
        /// </summary>
        public AsteroidsGameSettings settings { get; private set; } = new AsteroidsGameSettings();

        public IPlayer Player => player;

        private int score = 0;
        public int Score
        {
            get => score;
            private set
            {
                score = value;
                OnScoreUpdate?.Invoke(score);
            }
        }
        public ObservableList<IAsteroid> Asteroids { get; private set; }
        public ObservableList<IUFO> UFOs { get; private set; }
        public ObservableList<IBullet> Bullets { get; private set; }

        public delegate void GameStartArgs();
        public event GameStartArgs OnGameStart;
        public delegate void GameEndArgs();
        public event GameEndArgs OnGameEnd;
        public delegate void ScoreUpdateArgs(int newScore);
        public event ScoreUpdateArgs OnScoreUpdate;

        /// <summary>
        /// Игрок
        /// </summary>
        Player player;
        /// <summary>
        /// Флаг начала игры, нужен для блокировки обновлний сцены после конца игры
        /// </summary>
        bool isGameStarted;

        public void Setup(AsteroidsGameSettings settings)
        {
            this.settings = settings;
        }
        /// <summary>
        /// Инициализация игровых объектов и игры
        /// </summary>
        private void InitGame()
        {
            //Инициализация игровых объектов
            //Хранилище коллизий
            Instantiate(new Prefab(OnBeforeOnAwake: null, typeof(CircleColliderProvider)));
            //Игрок
            player = Components.Player.Spawn(this, 0, 0)
                .GetComponent<Player>();
            //Списки объектов
            Asteroids = new ObservableList<IAsteroid>();
            UFOs = new ObservableList<IUFO>();
            Bullets = new ObservableList<IBullet>();
            //Генератор метеоритов и астероидов
            Instantiate(new Prefab(OnBeforeOnAwake: null, typeof(EnemyObjectsGenerator)));

            //Вызов обновления счета
            Score = 0;

            isGameStarted = true;
            OnGameStart?.Invoke();
        }

        public override void OnStart()
        {
            InitGame();
            OnInstantiate += AsteroidsGameScene_OnInstantiate;
            OnDestroy += AsteroidsGameScene_OnDestroy;
        }

        /// <summary>
        /// Обработчик создаваемых объектов
        /// </summary>
        /// <param name="gameObject"></param>
        private void AsteroidsGameScene_OnInstantiate(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<Bullet>(out var bullet))
                Bullets.Add(bullet);
            else if (gameObject.TryGetComponent<Asteroid>(out var asteroid))
                Asteroids.Add(asteroid);
            else if (gameObject.TryGetComponent<UFO>(out var ufo))
                UFOs.Add(ufo);
        }
        /// <summary>
        /// Обработчик уничтожения объектов на сцене
        /// </summary>
        /// <param name="gameObject">Удаляемый объект</param>
        private void AsteroidsGameScene_OnDestroy(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<Bullet>(out var bullet))
                Bullets.Remove(bullet);
            else if (gameObject.TryGetComponent<Asteroid>(out var asteroid))
                Asteroids.Remove(asteroid);
            else if (gameObject.TryGetComponent<UFO>(out var ufo))
                UFOs.Remove(ufo);
        }
        /// <summary>
        /// Обновление сцены
        /// </summary>
        /// <param name="timeBetweedFrames"></param>
        public new void Update(float timeBetweedFrames)
        {
            if (isGameStarted)//Прерывание кадров
                base.Update(timeBetweedFrames);
        }
        /// <summary>
        /// Увеличение счета на определенное количество очков
        /// </summary>
        /// <param name="value"></param>
        internal void AddScore(int value)
        {
            Score += value;
        }
        /// <summary>
        /// Вызов конца игры
        /// </summary>
        internal void EndGame()
        {
            if (!isGameStarted) return;
            isGameStarted = false;
            OnGameEnd?.Invoke();
        }
    }
}