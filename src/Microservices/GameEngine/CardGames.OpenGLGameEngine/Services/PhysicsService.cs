using BepuPhysics;
using BepuUtilities.Memory;
using BepuUtilities;
using static CardGames.OpenGLGameEngine.Models.Physics.NarrowPhaseCallbacks;

namespace CardGames.OpenGLGameEngine.Services
{
    public class PhysicsService
    {
        private static PhysicsService? _instance = null;

        public Simulation Simulation { get; set; } = null!;

        public ThreadDispatcher ThreadDispatcher { get; set; } = null!;

        public BufferPool BufferPool { get; set; } = null!;

        public CollidableProperty<SimpleMaterial> CollidableMaterials { get; set; } = null!;

        private PhysicsService()
        {

        }

        public static PhysicsService GetInstance()
        {
            if (_instance is null)
            {
                _instance = new PhysicsService();
            }

            return _instance;
        }
    }
}
