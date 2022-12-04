using EntityComponentSystem;
using System;
using static AsteroidsEngine.AsteroidsGameSettings;

namespace AsteroidsEngine.Components
{
    /// <summary>
    /// Генератор враждебных игровых объектов
    /// </summary>
    public class EnemyObjectsGenerator : Component
    {
        float objectSpawnDelay;
        float nextSpawnTime;
        Random rnd;
        float ufoSpawnChance;
        Border border;
        public override void Init()
        {
            rnd = new Random();

            var ast_scene = scene as AsteroidsGameScene;
            var settings = ast_scene.settings;
            objectSpawnDelay = settings.ObjectSpawnDelay;
            nextSpawnTime = time.ElapsedTimeSinceTheStartOfTheScene + objectSpawnDelay;
            border = settings.Borders;
            ufoSpawnChance = settings.UFOSpawnChance;
        }

        public override void Tick()
        {
            if (time.ElapsedTimeSinceTheStartOfTheScene >= nextSpawnTime)
            {
                nextSpawnTime = time.ElapsedTimeSinceTheStartOfTheScene + objectSpawnDelay;
                SpawnSomeObject();
            }
        }

        /// <summary>
        /// Спавн некоторого объекта
        /// </summary>
        public void SpawnSomeObject()
        {
            var (x, y) = GetRandomPositionOnBorder();
            if (rnd.NextDouble() < ufoSpawnChance)
            {
                //Spawn ufo
                UFO.Spawn(scene, x, y);
            }
            else
            {
                //Spawn asteroid
                scene.Instantiate(Asteroid.GetPrefab(scene, x, y));
            }
        }

        /// <summary>
        /// Получение случайной позиции на границе мира
        /// </summary>
        /// <returns></returns>
        (float x, float y) GetRandomPositionOnBorder()
        {
            if (rnd.NextDouble() > 0.5f)
            {
                return (
                    (float)rnd.NextDouble() * (border.Right - border.Left) + border.Left,
                    rnd.NextDouble() > 0.5f ? border.Top : border.Bottom
                );
            }
            else
            {
                return (
                    rnd.NextDouble() > 0.5f ? border.Right : border.Left,
                    (float)rnd.NextDouble() * (border.Top - border.Bottom) + border.Bottom
                );
            }
        }
    }
}
