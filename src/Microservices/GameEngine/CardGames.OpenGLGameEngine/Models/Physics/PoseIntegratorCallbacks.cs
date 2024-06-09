using BepuPhysics;
using BepuUtilities;
using System.Numerics;

namespace CardGames.OpenGLGameEngine.Models.Physics
{
    public struct PoseIntegratorCallbacks : IPoseIntegratorCallbacks
    {
        private Vector3Wide _gravityWideDt;

        public readonly AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;

        public readonly bool AllowSubstepsForUnconstrainedBodies => false;

        public readonly bool IntegrateVelocityForKinematics => false;

        public Vector3 Gravity { get; set; }

        public PoseIntegratorCallbacks(Vector3 gravity) : this()
        {
            Gravity = gravity;
        }

        public void Initialize(Simulation simulation)
        {
            
        }

        public void IntegrateVelocity(Vector<int> bodyIndices, Vector3Wide position, QuaternionWide orientation, 
            BodyInertiaWide localInertia, Vector<int> integrationMask, int workerIndex, Vector<float> dt, 
                ref BodyVelocityWide velocity)
        {
            velocity.Linear += _gravityWideDt;
        }

        public void PrepareForIntegration(float dt)
        {
            _gravityWideDt = Vector3Wide.Broadcast(Gravity * dt);
        }
    }
}
