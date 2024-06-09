using BepuPhysics;
using CardGames.OpenGLGameEngine.Enums;
using CardGames.OpenGLGameEngine.Models.Physics;
using CardGames.OpenGLGameEngine.Models;
using CardGames.OpenGLGameEngine.Services;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class PlayerComponent : Component
    {
        private readonly Vector3? _startingLocation;
        private readonly Shader _shader;

        private float _moveSpeed = 50.0f;
        private float _jumpForce = 25.0f;

        private BodyReference _bodyReference;
        private StaticHitHandler _hitHandler;
        
        private CameraComponent _camera = null!;
        private TransformComponent _transform = null!;
        private SpotLightComponent _flashlight = null!;
        private BoxRigidComponent _boxRigidComponent = null!;

        public MovementPresets ActiveMovementPreset { get; set; } = MovementPresets.Player;
        
        public PlayerComponent(Shader shader, Vector3? startingLocation = null)
        {
            _shader = shader;
            _startingLocation = startingLocation;
        }

        public override void Init()
        {
            _camera = Entity.AddComponent(new CameraComponent(_shader));

            _transform = Entity.AddComponent(new TransformComponent(_startingLocation));

            _flashlight = Entity.AddComponent(new SpotLightComponent(_shader, _transform.Position));

            _boxRigidComponent = Entity.AddComponent(new BoxRigidComponent(
                new BepuPhysics.Collidables.Box(1.0f, 2.0f, 1.0f), 3.0f));

            _bodyReference = PhysicsService.GetInstance().Simulation.Bodies[_boxRigidComponent.Handle];

            _hitHandler = new StaticHitHandler() 
            { 
                RayHit = new RayHit() 
                { 
                    Hit = false, 
                    T = 1.0f 
                } 
            };

        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();

            _hitHandler.RayHit = new RayHit() 
            { 
                Hit = false, 
                T = 1.0f 
            };

            PhysicsService.GetInstance().Simulation.RayCast(DataManipulationService
                .OpenTKVectorToSystemVector(_transform.Position), new System.Numerics.Vector3(0.0f, -1.0f, 0.0f), 100.0f, 
                    ref _hitHandler);
        }

        public override void UpdateInput(FrameEventArgs eventArgs, KeyboardState input, MouseState mouse, 
            ref bool isFirstMove, ref Vector2 lastPosition)
        {
            base.UpdateInput(eventArgs, input, mouse, ref isFirstMove, ref lastPosition);

            if ((ActiveInputFlags & InputFlags.Player) == 0)
            {
                return;
            }

            var cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (ActiveMovementPreset == MovementPresets.Spectator)
            {
                _bodyReference.Awake = false;

                if (input.IsKeyDown(Keys.W))
                {
                    _bodyReference.Pose.Position += DataManipulationService
                        .OpenTKVectorToSystemVector(_camera.Front * cameraSpeed * (float)eventArgs.Time);
                }

                if (input.IsKeyDown(Keys.S))
                {
                    _bodyReference.Pose.Position -= DataManipulationService
                        .OpenTKVectorToSystemVector(_camera.Front * cameraSpeed * (float)eventArgs.Time);
                }

                if (input.IsKeyDown(Keys.A))
                {
                    _bodyReference.Pose.Position -= DataManipulationService
                        .OpenTKVectorToSystemVector(_camera.Right * cameraSpeed * (float)eventArgs.Time);
                }

                if (input.IsKeyDown(Keys.D))
                {
                    _bodyReference.Pose.Position += DataManipulationService
                        .OpenTKVectorToSystemVector(_camera.Right * cameraSpeed * (float)eventArgs.Time);
                }

                if (input.IsKeyDown(Keys.Space))
                {
                    _bodyReference.Pose.Position += DataManipulationService
                        .OpenTKVectorToSystemVector(_camera.Up * cameraSpeed * (float)eventArgs.Time);
                }

                if (input.IsKeyDown(Keys.LeftShift))
                {
                    _bodyReference.Pose.Position -= DataManipulationService
                        .OpenTKVectorToSystemVector(_camera.Up * cameraSpeed * (float)eventArgs.Time);
                }
            }
            else if (ActiveMovementPreset == MovementPresets.Player)
            {
                _bodyReference.Awake = true;

                var viewMatrix = _camera.GetViewMatrix();
                var cameraRotation = Quaternion.FromMatrix(new Matrix3(viewMatrix));

                var forward = Vector3.Transform(-Vector3.UnitZ, cameraRotation);
                var right = Vector3.Transform(Vector3.UnitX, cameraRotation);


                var moveDirection = (forward * GetVerticalInput(input)) + (right * GetHorizontalInput(input));

                moveDirection.Y = 0f;

                if (moveDirection.X != 0 && moveDirection.Z != 0)
                {
                    _bodyReference.ApplyImpulse(DataManipulationService
                        .OpenTKVectorToSystemVector(moveDirection.Normalized() * _moveSpeed), 
                            DataManipulationService.OpenTKVectorToSystemVector(new Vector3(0.0f, 0.0f, 0.0f)));
                }

                _bodyReference.Velocity.Linear = new System.Numerics.Vector3(
                    _bodyReference.Velocity.Linear.X * 0.95f *  (float)TimeService.GetInstance().DeltaTime / 0.05f, 
                        _bodyReference.Velocity.Linear.Y, _bodyReference.Velocity.Linear.Z * 0.95f * (float)TimeService.GetInstance().DeltaTime / 0.05f);

                var flatVel = new Vector3(_bodyReference.Velocity.Linear.X, 0.0f, _bodyReference.Velocity.Linear.Z);
                var flatAng = new Vector3(_bodyReference.Velocity.Angular.X, 0.0f, _bodyReference.Velocity.Angular.Z);

                if (flatVel.Length > _moveSpeed)
                {
                    var limitedVel = flatVel.Normalized() * _moveSpeed;

                    _bodyReference.Velocity.Linear.X = limitedVel.X;
                    _bodyReference.Velocity.Linear.Z = limitedVel.Z;
                }

                if (flatAng.Length > _moveSpeed)
                {
                    var limitedAng = flatAng.Normalized() * _moveSpeed;

                    _bodyReference.Velocity.Angular.X = limitedAng.X;
                    _bodyReference.Velocity.Angular.Z = limitedAng.Z;
                }

                if (input.IsKeyDown(Keys.LeftShift))
                {
                    _moveSpeed = 100.0f;
                }
                else
                {
                    _moveSpeed = 50.0f;
                }

                if (input.IsKeyPressed(Keys.Space))
                {
                    Jump();
                }

            }

            if (input.IsKeyPressed(Keys.F))
            {
                _flashlight.ToggleLight();
            }

            if (isFirstMove)
            {
                lastPosition = new Vector2(mouse.X, mouse.Y);
                isFirstMove = false;
            }
            else
            {
                var deltaX = mouse.X - lastPosition.X;
                var deltaY = mouse.Y - lastPosition.Y;

                lastPosition = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }


        }
        private void Jump()
        {
            if (_hitHandler.RayHit.Hit)
            {
                _bodyReference.ApplyImpulse(new System.Numerics.Vector3(0.0f, 1.0f * _jumpForce, 0.0f), 
                    DataManipulationService.OpenTKVectorToSystemVector(new Vector3(0.0f, 0.0f, 0.0f)));
            }
        }

        public float GetVerticalInput(KeyboardState input)
        {
            var verticalInput = 0f;

            if (input.IsKeyDown(Keys.W))
            {
                verticalInput = 1f;
            }

            if (input.IsKeyReleased(Keys.W))
            {
                verticalInput = 0.0f;
            }

            if (input.IsKeyDown(Keys.S))
            {
                verticalInput = -1f;
            }

            if (input.IsKeyReleased(Keys.S))
            {
                verticalInput = 0.0f;
            }

            return verticalInput;
        }

        public float GetHorizontalInput(KeyboardState input)
        {
            var horizontalInput = 0f;

            if (input.IsKeyDown(Keys.D))
            {
                horizontalInput = 1f;
            }

            if (input.IsKeyReleased(Keys.D))
            {
                horizontalInput = 0.0f;
            }

            if (input.IsKeyDown(Keys.A))
            {
                horizontalInput = -1f;
            }

            if (input.IsKeyReleased(Keys.A))
            {
                horizontalInput = 0.0f;
            }

            return horizontalInput;
        }
    }
}
