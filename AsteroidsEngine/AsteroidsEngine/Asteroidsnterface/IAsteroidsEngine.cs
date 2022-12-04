using static AsteroidsEngine.AsteroidsGameScene;
using static EntityComponentSystem.Scene;

namespace AsteroidsEngine
{
    public interface IAsteroidsEngine
    {
        /// <summary>
        /// Игровой счет игрока
        /// </summary>
        int Score { get; }

        /// <summary>
        /// Игрок
        /// </summary>
        IPlayer Player { get; }
        /// <summary>
        /// Список астероидов
        /// </summary>
        ObservableList<IAsteroid> Asteroids { get; }
        /// <summary>
        /// Список НЛО
        /// </summary>
        ObservableList<IUFO> UFOs { get; }
        /// <summary>
        /// Список пуль
        /// </summary>
        ObservableList<IBullet> Bullets { get; }

        /// <summary>
        /// Событие создания объекта на сцене
        /// </summary>
        event InstantiateArgs OnInstantiate;
        /// <summary>
        /// Событие уничтожения объекта на сцене
        /// </summary>
        event DestroyArgs OnDestroy;

        /// <summary>
        /// Событие начала игры
        /// </summary>
        event GameStartArgs OnGameStart;
        /// <summary>
        /// Событие конца игры
        /// </summary>
        event GameEndArgs OnGameEnd;
        /// <summary>
        /// Событие возникающиее при обновлении счета
        /// </summary>
        event ScoreUpdateArgs OnScoreUpdate;

        /// <summary>
        /// Настройка сцены
        /// </summary>
        /// <param name="settings">Объект настроек</param>
        void Setup(AsteroidsGameSettings settings);
        /// <summary>
        /// Обновление сцены
        /// </summary>
        /// <param name="timeBetweedFrames"></param>
        void Update(float timeBetweedFrames);
    }
}