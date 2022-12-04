namespace AsteroidsEngine
{
    /// <summary>
    /// Интерфейс позиции
    /// </summary>
    public interface IHasPosition
    {
        /// <summary>
        /// Положение игрока в пространстве по оси X
        /// </summary>
        float X { get; }
        /// <summary>
        /// Положение игрока в пространстве по оси Y
        /// </summary>
        float Y { get; }
    }
}