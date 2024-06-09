using BepuPhysics.Collidables;
using BepuPhysics;
using CardGames.OpenGLGameEngine.Services;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class BoxRigidComponent : RigidBodyComponent
    {
        public Box Box { get; private set; }

        public BoxRigidComponent(Box box, float mass) : base(mass, true)
        {
            Box = box;
        }

        public override void Init()
        {
            base.Init();
            
            var inertia = Box.ComputeInertia(Mass);
            
            Handle = PhysicsService.GetInstance().Simulation.Bodies.Add(BodyDescription.CreateDynamic(
                DataManipulationService.OpenTKVectorToSystemVector(TransformComponent.Position), inertia, 
                    PhysicsService.GetInstance().Simulation.Shapes.Add(Box), 0.01f));
            
            PhysicsService.GetInstance().CollidableMaterials.Allocate(Handle).FrictionCoefficient = 0.5f;
            PhysicsService.GetInstance().CollidableMaterials.Allocate(Handle).MaximumRecoveryVelocity = 2.0f;

            PhysicsService.GetInstance().CollidableMaterials.Allocate(Handle).SpringSettings = 
                new BepuPhysics.Constraints.SpringSettings(30, 1);

        }
    }
}
