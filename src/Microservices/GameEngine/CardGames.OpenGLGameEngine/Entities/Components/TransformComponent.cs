using CardGames.OpenGLGameEngine.Enums;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class TransformComponent : Component
    {
        private Vector3 _previousScale;
        private Vector3 _previousParentPosition;

        private Quaternion _previousRotation;

        public Vector3 Scale { get; set; }

        public Vector3 Position { get; set; }
        
        public Quaternion Rotation { get; set; }
        
        public SyncedTransforms SyncedTransforms { get; set; }
        
        public TransformComponent(Vector3? postion = null, Quaternion? rotation = null, Vector3? scale = null)
        {
            Scale = scale ?? Vector3.One;
            Position = postion ?? Vector3.Zero;
            Rotation = rotation ?? Quaternion.Identity;
            SyncedTransforms = SyncedTransforms.None;
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Update()
        {
            base.Update();

            foreach (var childEntity in Entity.ChildEntities)
            {
                var childTransform = childEntity.GetComponent<TransformComponent>();
                
                if ((childTransform.SyncedTransforms & SyncedTransforms.Position) != 0)
                {
                    var offset = childTransform.Position - _previousParentPosition;
                    childTransform.Position = Position + offset;
                }

                if ((childTransform.SyncedTransforms & SyncedTransforms.Rotation) != 0)
                {
                    var offset = childTransform.Rotation - _previousRotation;
                    childTransform.Rotation = Rotation + offset;
                }

                if ((childTransform.SyncedTransforms & SyncedTransforms.Scale) != 0)
                {
                    var offset = childTransform.Scale - _previousScale;
                    childTransform.Scale = Scale + offset;
                }
            }

            _previousParentPosition = Position;
            _previousRotation = Rotation;
            _previousScale = Scale;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public void RotateTo(Quaternion rotation)
        {
            Rotation = rotation;
        }
    }
}
