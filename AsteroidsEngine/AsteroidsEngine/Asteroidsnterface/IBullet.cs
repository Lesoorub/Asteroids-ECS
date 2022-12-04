namespace AsteroidsEngine
{
    public interface IBullet : IHasPosition, IHasSpeed
    {
        /// <summary>
        /// Текущий угол вращения игрока
        /// </summary>
        float Rotation { get; }
    }
}