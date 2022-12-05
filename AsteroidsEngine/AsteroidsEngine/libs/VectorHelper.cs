using System;

namespace AsteroidsEngine
{
    public static class VectorHelper
    {
        /// <summary>
        /// Рассчет евклидового расстояния между позициями двух векторов
        /// </summary>
        /// <param name="x1">Позиция первого вектора по оси X</param>
        /// <param name="y1">Позиция первого вектора по оси Y</param>
        /// <param name="x2">Позиция второго вектора по оси X</param>
        /// <param name="y2">Позиция второго вектора по оси Y</param>
        /// <returns>Расстояние между объектами</returns>
        public static float Distance(float x1, float y1, float x2, float y2) =>
            (float)Math.Sqrt(DistanceSqr(x1, y1, x2, y2));
        /// <summary>
        /// Рассчет евклидового расстояния между позициями двух векторов в квадрате
        /// </summary>
        /// <param name="x1">Позиция первого вектора по оси X</param>
        /// <param name="y1">Позиция первого вектора по оси Y</param>
        /// <param name="x2">Позиция второго вектора по оси X</param>
        /// <param name="y2">Позиция второго вектора по оси Y</param>
        /// <returns>Расстояние между объектами</returns>
        public static float DistanceSqr(float x1, float y1, float x2, float y2) =>
            (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
        /// <summary>
        /// Сколярное произвидение двух векторов
        /// </summary>
        /// <param name="x1">Позиция первого вектора по оси X</param>
        /// <param name="y1">Позиция первого вектора по оси Y</param>
        /// <param name="x2">Позиция второго вектора по оси X</param>
        /// <param name="y2">Позиция второго вектора по оси Y</param>
        /// <returns></returns>
        public static float Dot(float x1, float y1, float x2, float y2)
        {
            return x1 * x2 + y1 * y2;
        }
        /// <summary>
        /// Проекция вектора на вектор
        /// </summary>
        /// <param name="x1">Позиция vec по оси X</param>
        /// <param name="y1">Позиция vec по оси Y</param>
        /// <param name="x2">Позиция onto по оси X</param>
        /// <param name="y2">Позиция onto по оси Y</param>
        /// <returns></returns>
        public static (float x, float y) Project(float vecX, float vecY, float ontoX, float ontoY)
        {
            float numerator = Dot(vecX, vecY, ontoX, ontoY);
            float denominator = Dot(ontoX, ontoY, ontoX, ontoY);
            if (denominator == 0)
                return (ontoX, ontoY);
            float coef = numerator / denominator;
            return (coef * ontoX, coef * ontoY);
        }
        /// <summary>
        /// Длина вектора в квадрате
        /// </summary>
        /// <param name="x">Позиция вектра по оси X</param>
        /// <param name="y">Позиция вектра по оси Y</param>
        /// <returns></returns>
        public static float MagnitudeSqr(float x, float y)
        {
            return x * x + y * y;
        }
        /// <summary>
        /// Длина вектора
        /// </summary>
        /// <param name="x">Позиция вектра по оси X</param>
        /// <param name="y">Позиция вектра по оси Y</param>
        /// <returns></returns>
        public static float Magnitude(float x, float y)
        {
            return (float)Math.Sqrt(MagnitudeSqr(x, y));
        }
    }
}
