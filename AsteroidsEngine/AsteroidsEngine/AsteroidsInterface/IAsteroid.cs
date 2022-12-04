namespace AsteroidsEngine
{
    /// <summary>
    /// Интерфейс информации о астероидн
    /// </summary>
    public interface IAsteroid : IHasPosition, IHasSpeed
    {
        /// <summary>
        /// Размер астероида
        /// </summary>
        AsteroidSize Size { get; }
        /// <summary>
        /// Текущий угол вращения игрока
        /// </summary>
        float Rotation { get; }
    }
}