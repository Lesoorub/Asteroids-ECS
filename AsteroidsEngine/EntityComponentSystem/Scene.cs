using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

namespace EntityComponentSystem
{
    /// <summary>
    /// Базовая сцена
    /// </summary>
    public class Scene : IScene, IDisposable
    {
        /// <summary>
        /// Словарь игровых объектов
        /// </summary>
        private ConcurrentDictionary<int, GameObject> GameObjects = new ConcurrentDictionary<int, GameObject>();
        /// <summary>
        /// Очередь "свеже" созданных объектов, у которых еще не был вызван OnInit
        /// </summary>
        private Queue<GameObject> FreshInstantiated = new Queue<GameObject>();
        public Time time { get; private set; }
        /// <summary>
        /// Максимальное количество FPS при запуске в асинхронном режиме
        /// </summary>
        public int MaxFPS = 20;

        public delegate void InstantiateArgs(GameObject gameObject);
        public event InstantiateArgs OnInstantiate;

        public delegate void DestroyArgs(GameObject gameObject);
        public event DestroyArgs OnDestroy;

        public void Destroy(GameObject gameObject)
        {
            if (gameObject == null || gameObject.isDestroyed) return;
            gameObject.isDestroyed = true;
            GameObjects.TryRemove(gameObject.Id, out _);
            OnDestroy?.Invoke(gameObject);
            gameObject.IsActive = false;
            gameObject.OnDestoryed();
            gameObject.Dispose();
        }

        /// <summary>
        /// Шаблон объекта создаваемого по умолчанию
        /// </summary>
        readonly static Prefab empty = new Prefab(null);

        public GameObject Instantiate()
        {
            return Instantiate(
                prefab: empty
                );
        }
        public GameObject Instantiate(Prefab prefab)
        {
            GameObject gameObject = GameObject.Create(this);

            if (prefab.Components.Length > 0)
            {
                foreach (var componenet in prefab.Components)
                    gameObject.GetOrAddComponent(componenet);
                gameObject.IsActive = true;
            }

            GameObjects.TryAdd(gameObject.Id, gameObject);

            (prefab ?? empty).onBeforeOnAwake_Invoke(gameObject);

            gameObject.OnAwake();
            OnInstantiate?.Invoke(gameObject);
            FreshInstantiated.Enqueue(gameObject);
            return gameObject;
        }

        public GameObject[] FindGameObjectsWithType<T>() where T : Component
        {
            var type = typeof(T);
            return GameObjects.Where(x => x.Value.GetComponent(type) != null).Select(x => x.Value).ToArray();
        }
        public IEnumerable<T> FindComponentsByType<T>() where T : Component
        {
            var type = typeof(T);
            return GameObjects.Where(x => x.Value.GetComponent(type) != null).Select(x => x.Value.GetComponent<T>());
        }
        public IEnumerable<object> FindComponentsByType(Type type)
        {
            return GameObjects.Where(x => x.Value.GetComponent(type) != null).Select(x => x.Value.GetComponent(type));
        }

        /// <summary>
        /// Запуск сцены
        /// </summary>
        public void Start()
        {
            time = new Time();
            OnStart();
            foreach (var gameObject in GameObjects.Values)
                gameObject.Init();
        }
        /// <summary>
        /// Обновление сцены, т.е обработка тика
        /// </summary>
        /// <param name="timeBetweedFrames"></param>
        public void Update(float timeBetweedFrames)
        {
            time.TimeBetweedFrames = timeBetweedFrames;
            OnTickBegin();

            while (FreshInstantiated.Count > 0)
            {
                var obj = FreshInstantiated.Dequeue();
                obj.Init();
            }

            foreach (var gameObject in GameObjects.Values)
                gameObject.Tick();
            OnTickEnded();
        }

        /// <summary>
        /// Синхронный запуск сцены
        /// </summary>
        public void StartSync()
        {
            using (var cancelTokenSource = new CancellationTokenSource())
            {
                StartAsync(cancelTokenSource.Token).GetAwaiter().GetResult();
            }
        }
        /// <summary>
        /// Асинхронный запуск сцены
        /// </summary>
        /// <param name="token">Токин отмены</param>
        /// <returns>Задача являющаяся процессом выполнения сцены</returns>
        public Task StartAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                Start();
                Stopwatch tickTimer = new Stopwatch();

                float targetTickTime = 1f / MaxFPS;
                while (true)
                {
                    tickTimer.Restart();

                    Update(time.TimeBetweedFrames);

                    var TickTime = (float)tickTimer.ElapsedTicks / TimeSpan.TicksPerSecond;
                    if (TickTime < targetTickTime)
                    {
                        await Task.Delay((int)Math.Floor(targetTickTime - TickTime));
                    }

                    if (token.IsCancellationRequested)
                        break;

                    time.TimeBetweedFrames = (float)tickTimer.ElapsedTicks / TimeSpan.TicksPerSecond;
                }
            }, token);
        }

        /// <summary>
        /// Событие возникающее при запуске сцены
        /// </summary>
        public virtual void OnStart() { }
        /// <summary>
        /// Событие перед кадром кадра
        /// </summary>
        public virtual void OnTickBegin() { }
        /// <summary>
        /// Событие после кадром кадра
        /// </summary>
        public virtual void OnTickEnded() { }

        /// <summary>
        /// Освобождение всех ресурсов занятых игровыми объектами
        /// </summary>
        public void Dispose()
        {
            foreach (var gameObject in GameObjects.Values)
            {
                gameObject.isDestroyed = true;
                gameObject.Dispose();
            }
            GameObjects.Clear();
        }
    }
}
