using BepuPhysics.Collidables;
using BepuPhysics;
using CardGames.OpenGLGameEngine.Services;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class StaticRigidBodyComponent : RigidBodyComponent
    {
        public StaticRigidBodyComponent() : base(0.0f, false)
        {

        }

        public override void Init()
        {
            base.Init();

            PhysicsService.GetInstance().Simulation.Statics.Add(new StaticDescription(new System.Numerics.Vector3(0, 0, 0), 
                PhysicsService.GetInstance().Simulation.Shapes.Add(new Box(TransformComponent.Scale.X * 2500, 1.0f, 
                    TransformComponent.Scale.Y * 2500))));
            
            PhysicsService.GetInstance().CollidableMaterials.Allocate(Handle).FrictionCoefficient = 0.5f;
            PhysicsService.GetInstance().CollidableMaterials.Allocate(Handle).MaximumRecoveryVelocity = 2.0f;

            PhysicsService.GetInstance().CollidableMaterials.Allocate(Handle).SpringSettings = 
                new BepuPhysics.Constraints.SpringSettings(30, 1);
        }
    }
}
