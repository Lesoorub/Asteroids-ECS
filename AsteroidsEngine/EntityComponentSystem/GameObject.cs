using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityComponentSystem
{
    /// <summary>
    /// Игровой объект
    /// </summary>
    public class GameObject : IDisposable
    {
        /// <summary>
        /// Уникальный идентификатор объекта
        /// </summary>
        public int Id { get; private set; } = globalID++;
        /// <summary>
        /// Глобальный идентификатор, он будет присвоен новому объекту
        /// </summary>
        private static int globalID = 1;

        private bool isActive = false;
        /// <summary>
        /// Уничтожен ли объект
        /// </summary>
        public bool isDestroyed = false;
        /// <summary>
        /// Активирован ли объект
        /// </summary>
        public bool IsActive 
        { 
            get => isActive;
            set 
            {
                if (isActive == value) return;
                isActive = value;
                if (value) 
                    OnEnable(); 
                else 
                    OnDisable(); 
            } 
        }
        /// <summary>
        /// Все компоненты объекта
        /// </summary>
        private List<Component> components = new List<Component>();
        /// <summary>
        /// Сцена в которой распологается объект
        /// </summary>
        public IScene scene { get; private set; }
        private GameObject(IScene scene)
        {
            this.scene = scene;
        }
        /// <summary>
        /// Уничтожение данного объекта
        /// </summary>
        public void Destroy()
        {
            if (isDestroyed)
                return;
            scene.Destroy(this);
        }
        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <returns>Новый объект</returns>
        public GameObject Instantiate()
        {
            return scene.Instantiate();
        }
        /// <summary>
        /// Создание нового объекта по шаблону
        /// </summary>
        /// <returns>Новый объект</returns>
        public GameObject Instantiate(Prefab prefab)
        {
            return scene.Instantiate(prefab);
        }

        /// <summary>
        /// Получить компонент, либо создать его и вернуть если его не существует
        /// </summary>
        /// <param name="type">Тип искомого компонента</param>
        /// <returns>Найденный компонент</returns>
        public object GetOrAddComponent(Type type)
        {
            var component = GetComponent(type);
            if (component == null)
                return AddComponent(type);
            return component;
        }
        /// <summary>
        /// Получить компонент, либо создать его и вернуть если его не существует
        /// </summary>
        /// <typeparam name="T">Тип искомого компонента</typeparam>
        /// <returns>Найденный компонент</returns>
        public T GetOrAddComponent<T>() where T : Component, new()
        {
            var component = GetComponent<T>();
            if (component == null)
                return AddComponent<T>();
            return component;
        }
        /// <summary>
        /// Создать компонент
        /// </summary>
        /// <param name="type">Тип создоваемого компонента</param>
        /// <returns>Созданный компонент</returns>
        public object AddComponent(Type type)
        {
            if (!typeof(Component).IsAssignableFrom(type))
                return null;
            var t = Activator.CreateInstance(type) as Component;
            t.InitGameObject(this);
            components.Add(t);
            return t;
        }
        /// <summary>
        /// Создать компонент
        /// </summary>
        /// <typeparam name="T">Тип создоваемого компонента</typeparam>
        /// <returns>Созданный компонент</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            var t = new T();
            t.InitGameObject(this);
            components.Add(t);
            return t;
        }
        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <param name="type">Тип искомого компонента</param>
        /// <returns>Найденный компонент</returns>
        public object GetComponent(Type type)
        {
            if (!typeof(Component).IsAssignableFrom(type))
                return null;
            return components.FirstOrDefault(x => x != null && x.GetType().IsAssignableFrom(type));
        }
        /// <summary>
        /// Получить компонент
        /// </summary>
        /// <typeparam name="T">Тип искомого компонента</typeparam>
        /// <returns>Найденный компонент</returns>
        public T GetComponent<T>() where T : Component
        {
            return components.FirstOrDefault(x => x as T != null) as T;
        }
        /// <summary>
        /// Попытаться получить компонент
        /// </summary>
        /// <param name="type">Тип искомого компонента</param>
        /// <param name="component">Найденный компонент</param>
        /// <returns>Успешно ли был получен компонент</returns>
        public bool TryGetComponent(Type type, out Component component)
        {
            component = GetComponent(type) as Component;
            return component != null;
        }
        /// <summary>
        /// Попытаться получить компонент
        /// </summary>
        /// <typeparam name="T">Тип искомого компонента</typeparam>
        /// <param name="component">Найденный компонент</param>
        /// <returns>Успешно ли был получен компонент</returns>
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = GetComponent<T>();
            return component != null;
        }

        /// <summary>
        /// Создать игровой объект на сцене
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static GameObject Create(IScene scene)
        {
            if (scene == null) return null;
            return new GameObject(scene);
        }

        /// <summary>
        /// Вызвать OnAwake на всех компонентах
        /// </summary>
        public void OnAwake()
        {
            foreach (var component in components)
                if (component.IsEnabled)
                    component.OnAwake();
        }
        /// <summary>
        /// Вызвать Init на всех компонентах
        /// </summary>
        public void Init()
        {
            foreach (var component in components)
                if (component.IsEnabled)
                    component.Init();
        }
        /// <summary>
        /// Вызвать Tick на всех компонентах
        /// </summary>
        public void Tick()
        {
            foreach (var component in components)
                if (component.IsEnabled)
                    component.Tick();
        }
        /// <summary>
        /// Вызвать OnDisable на всех компонентах
        /// </summary>
        public void OnDisable()
        {
            foreach (var component in components)
                component.OnDisable();
        }
        /// <summary>
        /// Вызвать OnEnable на всех компонентах
        /// </summary>
        public void OnEnable()
        {
            foreach (var component in components)
                component.OnEnable();
        }
        /// <summary>
        /// Вызвать OnDestoryed на всех компонентах
        /// </summary>
        public void OnDestoryed()
        {
            foreach (var component in components)
                if (component.IsEnabled)
                    component.OnDestroyed();
        }
        /// <summary>
        /// Особождение ресурсов занятых компонентами
        /// </summary>
        public void Dispose()
        {
            if (!isDestroyed)
                Destroy();
            foreach (var component in components)
                if (component is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}
