using System;

namespace AsteroidsEngine
{
    public static class AngleHelper
    {
        /// <summary>
        /// Коеффициент перевода угла из градусов в радины
        /// </summary>
        public const float Deg2Rad = (2f * (float)Math.PI) / 360f;
        /// <summary>
        /// Коеффициент перевода угла из радинов в градусы
        /// </summary>
        public const float Rad2Deg = 360f / (2f * (float)Math.PI);
        /// <summary>
        /// Преобразование угола в радианах в единичный вектор
        /// </summary>
        /// <param name="angle">Угол в радианах</param>
        /// <returns>Единичный вектор</returns>
        public static (float dirX, float dirY) RadiansToDirection(float angle)
        {
            float dirX = (float)Math.Cos(angle);
            float dirY = (float)Math.Sin(angle);
            return (dirX, dirY);
        }
        /// <summary>
        /// Преобразование угола в градусах в единичный вектор
        /// </summary>
        /// <param name="angle">Угол в градусах</param>
        /// <returns>Единичный вектор</returns>
        public static (float dirX, float dirY) DegreesToDirection(float angle)
        {
            return RadiansToDirection(angle * Deg2Rad);
        }
    }
}
