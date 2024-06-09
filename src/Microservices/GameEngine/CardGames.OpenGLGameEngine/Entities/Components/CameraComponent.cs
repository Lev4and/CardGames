using CardGames.OpenGLGameEngine.Attributes;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class CameraComponent : Component
    {
        private readonly Shader _shader;

        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;
        private float _fov = MathHelper.PiOver2;

        [OnResize]
        public Vector2 ScreenSize { get; set; }

        public Vector3 Front
        {
            get
            {
                return _front;
            }
        }

        public Vector3 Up
        {
            get
            {
                return _up;
            }
        }

        public Vector3 Right
        {
            get
            {
                return _right;
            }
        }

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);

                _pitch = MathHelper.DegreesToRadians(angle);

                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);

                UpdateVectors();
            }
        }

        [OnMouseWheel]
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);

                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public TransformComponent Transform = null!;

        public CameraComponent(Shader shader)
        {
            _shader = shader;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Transform.Position, Transform.Position + _front, _up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, ScreenSize.X / ScreenSize.Y, 0.01f, 100f);
        }

        public override void Init()
        {
            base.Init();

            Transform = Entity.AddComponent(new TransformComponent(Vector3.UnitZ * 3));
        }
        public override void Draw()
        {
            base.Draw();

            _shader.SetMatrix4("view", true, GetViewMatrix());
            _shader.SetMatrix4("projection", true, GetProjectionMatrix());

            _shader.SetVector3("viewPos", Transform.Position);

        }
        public override void Update()
        {
            base.Update();
        }

        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);
            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
    }
}
