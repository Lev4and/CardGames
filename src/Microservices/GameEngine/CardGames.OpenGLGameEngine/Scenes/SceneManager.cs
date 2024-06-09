using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace CardGames.OpenGLGameEngine.Scenes
{
    public class SceneManager
    {
        private static SceneManager? _instance = null;

        private HashSet<Scene> _scenes;

        public Scene ActiveScene
        {
            get
            {
                return _scenes.First(scene => scene.IsActive);
            }
        }

        private SceneManager()
        {
            _scenes = new HashSet<Scene>();
        }

        public void AddScene(Scene scene)
        {
            if (_scenes.Any(scene => scene.Name == scene.Name))
            {
                throw new InvalidOperationException($"Scene {scene.Name} already exists");
            }

            scene.Id = _scenes.Any() 
                ? _scenes.Max(scene => scene.Id) + 1 
                : 0;

            _scenes.Add(scene);
        }

        public void RemoveScene(string name)
        {
            if (!_scenes.Any(scene => scene.Name == name))
            {
                return;
            }

            _scenes.RemoveWhere(scene => scene.Name == name);
        }

        public void SwapScene(int id)
        {
            if (!_scenes.Any(scene => scene.Id == id))
            {
                throw new InvalidOperationException($"Cannot Find Scene with Id {id}");
            }

            if (_scenes.Any(scene => scene.IsActive)) 
            { 
                _scenes.First(scene => scene.IsActive).IsActive = false; 
            }

            _scenes.First(scene => scene.Id == id).IsActive = true;

        }

        public void SwapScene(string name)
        {
            if (!_scenes.Any(scene => scene.Name == name))
            {
                throw new InvalidOperationException($"Cannot find scene with name {name}");
            }

            if (_scenes.Any(scene => scene.IsActive)) 
            { 
                _scenes.First(scene => scene.IsActive).IsActive = false; 
            }

            _scenes.First(scene => scene.Name == name).IsActive = true;
        }

        public void LoadAllScenes()
        {
            foreach (Scene scene in _scenes)
            {
                scene.OnLoad();
            }
        }

        public void LoadScene(int id)
        {
            if (!_scenes.Any(scene => scene.Id == id))
            {
                throw new InvalidOperationException($"Cannot find scene with id {id}");
            }

            _scenes.First(scene => scene.Id == id).OnLoad();
        }

        public void LoadScene(string name)
        {
            if (!_scenes.Any(scene => scene.Name == name))
            {
                throw new InvalidOperationException($"Cannot find scene with name {name}");
            }

            _scenes.First(scene => scene.Name == name).OnLoad();
        }

        public void DrawActiveScene()
        {
            if (_scenes.Any(scene => scene.IsActive))
            {
                _scenes.First(scene => scene.IsActive).OnDraw();
            }
        }

        public void UpdateActiveScene()
        {
            if (_scenes.Any(scene => scene.IsActive))
            {
                _scenes.First(scene => scene.IsActive).OnUpdate();
            }
        }

        public void RefreshActiveScene()
        {
            if (_scenes.Any(scene => scene.IsActive))
            {
                _scenes.First(scene => scene.IsActive).Refresh();
            }
        }

        public void UpdateActiveInput(FrameEventArgs eventArgs, KeyboardState input, MouseState mouse, 
            ref bool isFirstMove, ref Vector2 lastPosition)
        {
            if (_scenes.Any(scene => scene.IsActive))
            {
                _scenes.First(scene => scene.IsActive)
                    .UpdateInput(eventArgs, input, mouse, ref isFirstMove, ref lastPosition);
            }
        }

        public void SetActiveComponentReferences(Attribute attribute, object value)
        {
            if (_scenes.Any(scene => scene.IsActive))
            {
                _scenes.First(scene => scene.IsActive)
                    .SetComponentReferencesWithAttribute(attribute, value);
            }
        }

        public static SceneManager GetInstance()
        {
            if (_instance is null)
            {
                _instance = new SceneManager();
            }

            return _instance;
        }
    }
}
