using EntityComponentSystem;

namespace AsteroidsEngine
{
    public class Speed : Component
    {
        public float SpeedX;
        public float SpeedY;

        public float Drag = 0;

        Position position;
        public override void OnAwake()
        {
            position = gameObject.GetComponent<Position>();
        }
        public override void Tick()
        {
            float timeBetweenFrames = time.TimeBetweedFrames;

            position.X += SpeedX * timeBetweenFrames;
            position.Y += SpeedY * timeBetweenFrames;

            //Unity formula
            //velocity * ( 1 - deltaTime * drag);
            float coef = (1 - timeBetweenFrames * Drag);
            SpeedX *= coef;
            SpeedY *= coef;
        }
    }
}