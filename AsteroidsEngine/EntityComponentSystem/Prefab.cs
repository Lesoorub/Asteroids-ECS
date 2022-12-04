using System;

namespace EntityComponentSystem
{
    /// <summary>
    /// Шаблон объекта
    /// Фактически, полностью отвечает за предсоздание объекта
    /// </summary>
    public class Prefab
    {
        public delegate void BeforeOnAwake(GameObject gameObject);
        /// <summary>
        /// Событие возникающее перед событием OnAwake на компонентах объекта,
        /// служит для предварительной настройки объекта
        /// </summary>
        public event BeforeOnAwake onBeforeOnAwake;
        /// <summary>
        /// Список компонентов которые необходимо будет добавить на созданный объект
        /// </summary>
        public Type[] Components;
        public Prefab(BeforeOnAwake OnBeforeOnAwake, params Type[] Components)
        {
            onBeforeOnAwake += OnBeforeOnAwake;
            this.Components = Components ?? new Type[0];
        }
        /// <summary>
        /// Вызов события onBeforeOnAwake
        /// </summary>
        /// <param name="gameObject">Созданный объкет</param>
        public void onBeforeOnAwake_Invoke(GameObject gameObject)
        {
            onBeforeOnAwake?.Invoke(gameObject);
        }
    }
}
