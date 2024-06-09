using CardGames.OpenGLGameEngine.Enums;
using CardGames.OpenGLGameEngine.EventArgs;
using CardGames.OpenGLGameEngine.Scenes;
using CardGames.OpenGLGameEngine.Services;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Entities
{
    public class Component
    {
        public Entity Entity { get; set; } = null!;

        protected InputFlags ActiveInputFlags
        {
            get => InputFlagService.GetInstance().ActiveInputFlags;
            set => InputFlagService.GetInstance().ActiveInputFlags = value;
        }

        public double DeltaTime
        {
            get => TimeService.GetInstance().DeltaTime;
        }

        public EntityComponentManager EntityComponentManager
        {
            get => SceneManager.GetInstance().ActiveScene.EntityComponentManager;
        }

        public event EventHandler<ComponentEventArgs>? KeyInput;

        public virtual void Init()
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void UpdateInput(FrameEventArgs eventArgs, KeyboardState input, MouseState mouse, 
            ref bool isFirstMove, ref Vector2 lastPosition)
        {
            OnKeyInput(input);
        }

        protected void OnKeyInput(KeyboardState keyboardState)
        {
            KeyInput?.Invoke(this, new ComponentEventArgs(keyboardState));
        }
    }
}
