using static AsteroidsEngine.Components.Player;

namespace AsteroidsEngine
{
    /// <summary>
    /// Интерфейс содержащий все данные игрока
    /// </summary>
    public interface IPlayer : IHasPosition, IHasSpeed
    {
        /// <summary>
        /// Текущий угол вращения игрока
        /// </summary>
        float Rotation { get; set; }
        /// <summary>
        /// Количество жизней в запасе у игрока
        /// </summary>
        int Lives { get; set; }
        /// <summary>
        /// Физическое сопротивление игрока со средой
        /// </summary>
        float Drag { get; set; }
        /// <summary>
        /// Кол-во зарядов лазера
        /// </summary>
        int LaserCharges { get; }
        /// <summary>
        /// Влючен ли сейчас лазер
        /// </summary>
        bool LaserIsShooting { get; }
        /// <summary>
        /// Время, в секундах, до завершения перезарядки лазера
        /// </summary>
        float LaserReaload { get; }
        /// <summary>
        /// Является ли игрок неуязвимым
        /// </summary>
        bool IsInvincibility { get; }
        /// <summary>
        /// Событие уменьшения жизней у игрока
        /// </summary>
        event LiveConsumed OnLiveConsumed;

        /// <summary>
        /// Добавить мгновенную скорость игроку
        /// </summary>
        /// <param name="speedX">Добавочная мгновенная скорость по оси X</param>
        /// <param name="speedY">Добавочная мгновенная скорость по оси Y</param>
        void AddSpeed(float speedX, float speedY);
        /// <summary>
        /// Обычный одиночный выстрел, при попадании в астероид или нло
        /// уничтожается и уничтожает объект с которым столкнулся
        /// </summary>
        void Shoot();
        /// <summary>
        /// Выстрел лазером, действующим несколько секунд, уничтожающий все что пересечет
        /// </summary>
        void ShootLaser();
    }
}