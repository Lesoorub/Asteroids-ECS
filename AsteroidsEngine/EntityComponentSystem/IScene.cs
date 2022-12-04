using System;
using System.Collections.Generic;
using static EntityComponentSystem.Scene;

namespace EntityComponentSystem
{
    public interface IScene
    {
        /// <summary>
        /// Объект времени
        /// </summary>
        Time time { get; }

        /// <summary>
        /// Событие создания объекта на сцене
        /// </summary>
        event InstantiateArgs OnInstantiate;
        /// <summary>
        /// Событие уничтожения объекта на сцене
        /// </summary>
        event DestroyArgs OnDestroy;
        /// <summary>
        /// Уничтожение объекта
        /// </summary>
        /// <param name="gameObject">Уничтожаемый объект</param>
        void Destroy(GameObject gameObject);
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <returns>Созданный объект</returns>
        GameObject Instantiate();
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="prefab">Шаблон объекта</param>
        /// <returns>Созданный объект</returns>
        GameObject Instantiate(Prefab prefab);
        /// <summary>
        /// Найти все компоненты передаемого типа во всей сцене
        /// </summary>
        /// <param name="type">Тип компонента по которому будет производиться поиск</param>
        /// <returns>Последовательность найденных компонентов</returns>
        IEnumerable<object> FindComponentsByType(Type type);
        /// <summary>
        /// Найти все компоненты передаемого типа во всей сцене
        /// </summary>
        /// <typeparam name="T">Тип компонента по которому будет производиться поиск</typeparam>
        /// <returns>Последовательность найденных компонентов</returns>
        IEnumerable<T> FindComponentsByType<T>() where T : Component;
        /// <summary>
        /// Найти все игровые объекты которые имеют искомый компонент
        /// </summary>
        /// <typeparam name="T">Тип искомого компонента</typeparam>
        /// <returns>Массив игровых объектов</returns>
        GameObject[] FindGameObjectsWithType<T>() where T : Component;
    }
}
