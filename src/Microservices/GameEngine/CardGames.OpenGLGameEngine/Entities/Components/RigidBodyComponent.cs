using BepuPhysics;
using CardGames.OpenGLGameEngine.Services;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class RigidBodyComponent : Component
    {
        private bool _isKinematic;

        protected float Mass;
        protected TransformComponent TransformComponent = null!;

        public BodyHandle Handle;
        
        public RigidBodyComponent(float mass, bool isKinematic)
        {
            _isKinematic = isKinematic;
            
            Mass = mass;
        }

        public override void Init()
        {
            base.Init();

            TransformComponent = Entity.AddComponent(new TransformComponent());

        }

        public override void Update()
        {
            base.Update();

            if (_isKinematic)
            {
                TransformComponent.Position = DataManipulationService.SystemVectorToOpenTKVector(
                    PhysicsService.GetInstance().Simulation.Bodies[Handle].Pose.Position);

                TransformComponent.Rotation = DataManipulationService.SystemQuaternionToOpenTKQuaternion(
                    PhysicsService.GetInstance().Simulation.Bodies[Handle].Pose.Orientation);
            }
        }
    }
}
