using CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Entities;
using CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Shapes;
using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using static CardGames.OpenGLGameEngine.Models.Constants;

namespace CardGames.OpenGLGameEngine.Scenes
{
    public class BaseDebugScene : Scene
    {
        public BaseDebugScene(string name) : base(name)
        {

        }

        public override void OnAwake()
        {
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Multisample);
            
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _shaders = new Dictionary<string, Shader>()
            {
                { 
                    ShaderConstants.TextShader, 
                    new Shader(ShaderRoutes.BaseTextVertexShader, ShaderRoutes.BaseTextFragmentShader)
                },
                { 
                    ShaderConstants.LightShader, 
                    new Shader(ShaderRoutes.BaseVertexShader, ShaderRoutes.BaseFragmentShader)
                },
                { 
                    ShaderConstants.SkyboxShader, 
                    new Shader(ShaderRoutes.SkyboxVertexShader, ShaderRoutes.SkyboxFragmentShader)
                },
                { 
                    ShaderConstants.TextureShader, 
                    new Shader(ShaderRoutes.BaseVertexShader, ShaderRoutes.BaseLightingShader)
                }
            };

            var skyboxTextureFilePaths = new List<string>()
            {
                "Assets/Backgrounds/DefaultBackground.jpg",
                "Assets/Backgrounds/DefaultBackground.jpg",
                "Assets/Backgrounds/DefaultBackground.jpg",
                "Assets/Backgrounds/DefaultBackground.jpg",
                "Assets/Backgrounds/DefaultBackground.jpg",
                "Assets/Backgrounds/DefaultBackground.jpg"
            };

            var skybox = EntityComponentManager.AddEntity();

            skybox.AddComponent(new SkyboxComponent(_shaders[ShaderConstants.SkyboxShader], 
                new Models.Skybox.Skybox(skyboxTextureFilePaths)));

            //var plane = new Plane(_shaders[ShaderConstants.TextureShader],
            //    EntityComponentManager.AddEntity(Enums.Layer.Ground), new Vector3(0.0f, 0.0f, 0.0f),
            //        scale: new Vector2(15.0f, 15.0f));

            var player = new Player(_shaders[ShaderConstants.TextureShader], 
                EntityComponentManager.AddEntity(Enums.Layer.Player));
        }
    }
}
