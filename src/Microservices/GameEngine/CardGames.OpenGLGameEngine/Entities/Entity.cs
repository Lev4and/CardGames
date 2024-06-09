using CardGames.OpenGLGameEngine.Enums;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Entities
{
    public class Entity
    {
        private List<Component> _components;

        public Layer Layer { get; set; }

        public bool IsVisible { get; set; } = true;

        public bool IsActive { get; private set; } = true;

        public List<Entity> ChildEntities { get; set; } = new List<Entity>();

        public Entity()
        {
            _components = new List<Component>();
        }

        public void Draw()
        {
            foreach (var component in _components)
            {
                component.Draw();
            }
        }

        public void Update()
        {
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        public void UpdateInput(FrameEventArgs eventArgs, KeyboardState input, MouseState mouse, 
            ref bool isFirstMove, ref Vector2 lastPosition)
        {
            foreach (var component in _components)
            {
                component.UpdateInput(eventArgs, input, mouse, ref isFirstMove, ref lastPosition);
            }
        }

        public Entity AddChildEntity(Entity entity)
        {
            ChildEntities.Add(entity);

            return entity;
        }

        public T AddComponent<T>(T newComponent) 
            where T : Component
        {
            if (HasComponent<T>())
            {
                return GetComponent<T>();
            }

            newComponent.Entity = this;
            newComponent.Init();

            _components.Add(newComponent);

            return newComponent;
        }

        public bool RemoveComponent<T>() 
            where T : Component
        {
            if (HasComponent<T>())
            {
                _components.Add(GetComponent<T>());

                return true;
            }

            return false;
        }

        public void SetPropertyReferenceWithAttribute(Attribute attribute, object value)
        {
            foreach (var component in _components)
            {
                var properties = component.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (property.GetCustomAttributes(true).Contains(attribute))
                    {
                        property.SetValue(component, value);
                    }
                }
            }
        }
        public T GetComponent<T>() 
            where T : Component
        {
            return _components.First(component => component.GetType() == typeof(T)) as T
                ?? throw new InvalidCastException($"Could not Find any Components of Type {typeof(T).Name}");
        }
        
        public bool HasComponent<T>() 
            where T : Component
        {
            return _components.Any(component => component.GetType() == typeof(T));
        }
        
        public void Destroy()
        {
            IsActive = false;
        }
    }
}
