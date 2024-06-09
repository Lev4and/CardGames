using CardGames.OpenGLGameEngine.Enums;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Entities
{
    public class EntityComponentManager
    {
        private List<Entity> _entities;

        public EntityComponentManager() 
        {
            _entities = new List<Entity>();
        }

        public void Draw()
        {
            foreach (var entity in _entities.Where(entity => entity.IsVisible))
            {
                entity.Draw();
                
                foreach (Entity childEntity in entity.ChildEntities)
                {
                    childEntity.Draw();
                }
            }
        }

        public void Update()
        {
            foreach (var entity in _entities)
            {
                entity.Update();

                foreach (Entity childEntity in entity.ChildEntities)
                {
                    childEntity.Update();
                }
            }
        }

        public void Refresh()
        {
            foreach (var entity in _entities)
            {
                if (!entity.IsActive)
                {
                    entity.Destroy();

                    foreach (Entity childEntity in entity.ChildEntities)
                    {
                        childEntity.Destroy();
                    }
                }
            }
        }

        public void UpdateInput(FrameEventArgs eventArgs, KeyboardState input, MouseState mouse, 
            ref bool isFirstMove, ref Vector2 lastPosition)
        {
            foreach (var entity in _entities)
            {
                entity.UpdateInput(eventArgs, input, mouse, 
                    ref isFirstMove, ref lastPosition);

                foreach (Entity childEntity in entity.ChildEntities)
                {
                    childEntity.UpdateInput(eventArgs, input, mouse, 
                        ref isFirstMove, ref lastPosition);
                }
            }
        }


        public void UpdateEntityLayer(Entity entity, Layer layer)
        {
            entity.Layer = layer;
        }

        public Entity AddEntity(Layer layer = Layer.None)
        {
            var entity = new Entity();

            entity.Layer = layer;

            _entities.Add(entity);

            return entity;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return _entities;
        }

        public IEnumerable<Entity> GetEntitiesWithType<T>() 
            where T : Component
        {
            return _entities.Where(entity => entity.HasComponent<T>());
        }

        public void SetComponentReferencesWithAttribute(Attribute attribute, object value)
        {
            foreach (var entity in _entities)
            {
                entity.SetPropertyReferenceWithAttribute(attribute, value);
            }
        }
    }
}
