using System;
using System.Text;

namespace EntityComponentSystem
{
    /// <summary>
    /// Объек концентрирующий всю информацию о времени сцены
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Время в секундах прошедшее с момента запуска сцены
        /// </summary>
        public float ElapsedTimeSinceTheStartOfTheScene => (float)(DateTime.Now - startScene).TotalSeconds;
        /// <summary>
        /// Время между кадрами
        /// </summary>
        public float TimeBetweedFrames;
        /// <summary>
        /// Время запуска сцены
        /// </summary>
        DateTime startScene;

        public Time()
        {
            startScene = DateTime.Now;
        }
    }
}
