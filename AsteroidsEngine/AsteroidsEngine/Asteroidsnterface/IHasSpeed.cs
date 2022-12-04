namespace AsteroidsEngine
{
    /// <summary>
    /// Интерфейс скорости
    /// </summary>
    public interface IHasSpeed : IHasPosition
    {
        /// <summary>
        /// Мгновенная скорость по оси X
        /// </summary>
        float SpeedX { get; }
        /// <summary>
        /// Мгновенная скорость по оси Y
        /// </summary>
        float SpeedY { get; }
    }
}