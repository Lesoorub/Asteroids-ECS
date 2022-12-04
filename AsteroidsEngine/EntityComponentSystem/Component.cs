namespace EntityComponentSystem
{
    /// <summary>
    /// Абстракный компонент для игрового обекта
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// 
        /// </summary>
        public IScene scene => gameObject.scene;
        /// <summary>
        /// Сокращение до объекта времени
        /// </summary>
        public Time time => gameObject.scene.time;
        /// <summary>
        /// Игровой объект в котором распологается компонент
        /// </summary>
        public GameObject gameObject { get; private set; }
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Событие возникающее при создании gameObject
        /// </summary>
        public virtual void OnAwake() { }

        /// <summary>
        /// Событие возникающее при включении компонента
        /// </summary>
        public virtual void OnEnable() { }

        /// <summary>
        /// Событие возникающее при появлении на сцене
        /// </summary>
        public virtual void Init() { }

        /// <summary>
        /// Событие возникающее при каждом обновление сцены
        /// </summary>
        public virtual void Tick() { }

        /// <summary>
        /// Событие возникающее при выключении компонента
        /// </summary>
        public virtual void OnDisable() { }

        /// <summary>
        /// Событие возникающее при уничтожении объекта
        /// </summary>
        public virtual void OnDestroyed() { }


        /// <summary>
        /// Инициализация компонента игровым объектом
        /// </summary>
        /// <param name="obj">Игровой объект</param>
        public void InitGameObject(GameObject obj)
        {
            if (this.gameObject != null) return;
            this.gameObject = obj;
        }

        /// <summary>
        /// Сокращение до метода Destroy у игрового объекта
        /// </summary>
        public void Destroy()
        {
            gameObject.Destroy();
        }

        /// <summary>
        /// Сокращение до метода Instantiate у игрового объекта
        /// </summary>
        public GameObject Instantiate()
        {
            return gameObject.Instantiate();
        }
    }
}
