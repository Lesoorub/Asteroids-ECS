using EntityComponentSystem;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// Круглый коллайдер (триггер)
    /// </summary>
    public class CircleCollider : Component
    {
        /// <summary>
        /// Радиус
        /// </summary>
        public float Radius = 0.5f;
        /// <summary>
        /// Слой
        /// </summary>
        public CollisionLayers Layer = CollisionLayers.Default;
        Position position;
        CircleColliderProvider provider;

        public delegate void CollidedArgs(CircleCollider anotherCollider);
        /// <summary>
        /// Событие столкновения с другим коллайдером
        /// </summary>
        public event CollidedArgs OnCollision;

        public override void OnAwake()
        {
            position = gameObject.GetComponent<Position>();
            provider = scene.FindComponentsByType<CircleColliderProvider>().FirstOrDefault();

            if (provider.allColliders.TryGetValue(Layer, out var list))
                list.Add(this);
            else
                provider.allColliders.Add(Layer, new List<CircleCollider>() { this });
        }
        public override void Tick()
        {
            int layerNum = (int)Layer;
            int maxtrixLen = provider.CollisionMatrix.GetLength(1);
            for (int k = 0; k < maxtrixLen; k++)
            {
                if (!provider.CollisionMatrix[layerNum, k] ||
                    !provider.allColliders.TryGetValue((CollisionLayers)k, out var collisionList))
                    continue;

                foreach (var anotherCollider in collisionList.ToArray())
                {
                    if (!anotherCollider.IsEnabled ||
                        !anotherCollider.gameObject.IsActive ||
                        anotherCollider.gameObject.Id == gameObject.Id ||
                        !isIntersects(this, anotherCollider))
                        continue;

                    OnCollision?.Invoke(anotherCollider);
                    anotherCollider?.OnCollision?.Invoke(this);
                }
            }
        }

        public override void OnDestroyed()
        {
            if (this.provider == null) return;
            if (this.provider.allColliders.TryGetValue(Layer, out var list))
                list.Remove(this);
            else
                this.provider.allColliders.Remove(Layer);
        }

        /// <summary>
        /// Рассчет пересечения коллайдеров
        /// </summary>
        /// <param name="a">Коллайдер a</param>
        /// <param name="b">Коллайдер b</param>
        /// <returns>Пересекаются ли коллайдеры a и b ?</returns>
        bool isIntersects(CircleCollider a, CircleCollider b)
        {
            if (a == null || b == null) return false;

            float minDistanceSqr = (a.Radius + b.Radius) * (a.Radius + b.Radius);
            return distanceSqr(a.position.X, a.position.Y, b.position.X, b.position.Y) <= minDistanceSqr;
        }

        /// <summary>
        /// Рассчет евклидового расстояния между позициями двух объектов
        /// </summary>
        /// <param name="x1">Позиция первого объекта по оси X</param>
        /// <param name="y1">Позиция первого объекта по оси Y</param>
        /// <param name="x2">Позиция второго объекта по оси X</param>
        /// <param name="y2">Позиция второго объекта по оси Y</param>
        /// <returns>Расстояние между объектами</returns>
        float distanceSqr(float x1, float y1, float x2, float y2) =>
            (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
    }
}