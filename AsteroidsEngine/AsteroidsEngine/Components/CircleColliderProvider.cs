using EntityComponentSystem;
using System.Collections.Generic;

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

        public override void OnDestroyed()
        {
            allColliders.Clear();
        }
    }
}