using EntityComponentSystem;
using System.Collections.Generic;
using System.Net;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// Хранилище всех коллайдеров на сцене
    /// </summary>
    public class CircleColliderProvider : Component
    {
        /// <summary>
        /// Матрица столкновений
        /// </summary>
        public bool[,] CollisionMatrix = new bool[5, 5]
        {
            //Строка - что перескается.
            //Столбец - с чем пересекается.
            //Пересечение - пересекаются ли слой в строке со слоем в столбце.
            /*             Default Bullet Player Asteroid UFO   */
            /*Default  */{ true,   false, false, false,   false },
            /*Bullet   */{ false,  false, false, true,    true  },
            /*Player   */{ false,  false, false, true,    true  },
            /*Asteroid */{ false,  false, false, false,   false },
            /*UFO      */{ false,  false, true,  false,   false }
        };

        /// <summary>
        /// Словарь коллайдеров
        /// </summary>
        public Dictionary<CollisionLayers, List<CircleCollider>> allColliders =
            new Dictionary<CollisionLayers, List<CircleCollider>>();

        public bool RaycastAll(
            float x, float y, 
            float dirX, float dirY, 
            CollisionLayers layer, 
            float width, float distance, 
            out List<CircleCollider> result)
        {
            return RaycastAll(x, y, dirX, dirY, new CollisionLayers[] { layer }, width, distance, out result);
        }
        public bool RaycastAll(
            float x, float y,
            float dirX, float dirY,
            CollisionLayers[] layers,
            float width, float distance,
            out List<CircleCollider> result)
        {
            result = new List<CircleCollider>();
            float distanceSqr = distance * distance;
            foreach (var layer in layers)
            {
                if (!allColliders.TryGetValue(layer, out List<CircleCollider> colliders))
                    continue;
                foreach (var collider in colliders)
                {
                    var deltaX = collider.X - x;
                    var deltaY = collider.Y - y;

                    var (projectX, projectY) = VectorHelper.Project(deltaX, deltaY, dirX, dirY);

                    //Проверка на дальность
                    float distanceToProjectionSqr = VectorHelper.MagnitudeSqr(projectX, projectY);
                    if (distanceToProjectionSqr > distanceSqr)
                        continue;

                    //Проверка на то что объект находится по направлению луча, а не позади
                    //Сумма дистанций от начала луча до проекции и от проекции до конца луча
                    var sumDistance = distanceToProjectionSqr +
                        VectorHelper.DistanceSqr(projectX, projectY, dirX * distance, dirY * distance);
                    const float epsilon = 1e-6f;//Введем эпсилон чтобы не попасть на случайную неточность
                    if (sumDistance - epsilon <= distanceSqr &&
                        sumDistance + epsilon >= distanceSqr)
                        continue;

                    //Рассчет дистанции от точки проекции до объекта
                    var distanceSqrFromProjectionToCollider = VectorHelper.DistanceSqr(projectX, projectY, deltaX, deltaY);
                    if (distanceSqrFromProjectionToCollider > width + collider.Radius)
                        continue;
                    
                    result.Add(collider);
                }
            }
            return result.Count > 0;
        }

        public override void OnDestroyed()
        {
            allColliders.Clear();
        }
    }
}