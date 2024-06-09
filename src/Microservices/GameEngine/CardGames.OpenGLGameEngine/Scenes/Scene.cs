using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Scenes
{
    public abstract class Scene
    {
        protected Dictionary<string, Shader> _shaders = new Dictionary<string, Shader>();

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = false;

        public bool IsLoaded { get; set; } = false;

        public EntityComponentManager EntityComponentManager { get; private set; } = new EntityComponentManager();

        public Scene(string name)
        {
            Name = name;
        }

        public void OnLoad()
        {
            if (IsLoaded)
            {
                return;
            }

            OnAwake();

            IsLoaded = true;
        }

        public abstract void OnAwake();
        
        public virtual void OnDraw()
        {
            EntityComponentManager.Draw();
        }
        
        public virtual void OnUpdate()
        {
            EntityComponentManager.Update();
        }
        
        public virtual void Refresh()
        {
            EntityComponentManager.Refresh();
        }
       
        public virtual void UpdateInput(FrameEventArgs eventArgs, KeyboardState input, MouseState mouse, 
            ref bool isFirstMove, ref Vector2 lastPosition)
        {
            EntityComponentManager.UpdateInput(eventArgs, input, mouse, ref isFirstMove, ref lastPosition);
        }
        
        public virtual void SetComponentReferencesWithAttribute(Attribute attribute, object value)
        {
            EntityComponentManager.SetComponentReferencesWithAttribute(attribute, value);
        }
    }
}
