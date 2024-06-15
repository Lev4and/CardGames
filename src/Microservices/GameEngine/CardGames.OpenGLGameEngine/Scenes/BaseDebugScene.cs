using CardGames.GameLogic;
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

            var plane = new Plane(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground), new Vector3(0.0f, 0.0f, 0.0f),
                    scale: new Vector2(15.0f, 15.0f));

            var player = new Player(_shaders[ShaderConstants.TextureShader], 
                EntityComponentManager.AddEntity(Enums.Layer.Player));

            var pointLight1 = EntityComponentManager.AddEntity();
            var pointLight2 = EntityComponentManager.AddEntity();
            var pointLight3 = EntityComponentManager.AddEntity();
            var pointLight4 = EntityComponentManager.AddEntity();

            pointLight1.AddComponent(new PointLightComponent(_shaders[ShaderConstants.TextureShader], 
                new Vector3(0.7f, 0.2f, 2.0f)));
            pointLight2.AddComponent(new PointLightComponent(_shaders[ShaderConstants.TextureShader], 
                new Vector3(2.3f, -3.3f, -4.0f)));
            pointLight3.AddComponent(new PointLightComponent(_shaders[ShaderConstants.TextureShader], 
                new Vector3(-4.0f, 2.0f, -12.0f)));
            pointLight4.AddComponent(new PointLightComponent(_shaders[ShaderConstants.TextureShader], 
                new Vector3(0.0f, 0.0f, -3.0f)));

            var directionalLightComponent = EntityComponentManager.AddEntity();

            directionalLightComponent.AddComponent(new DirectionalLightComponent(
                _shaders[ShaderConstants.TextureShader], new Vector3(-0.2f, -1.0f, -0.3f)));

            var materialComponent = EntityComponentManager.AddEntity();

            materialComponent.AddComponent(new MaterialComponent(
                _shaders[ShaderConstants.TextureShader]));

            var cardCollectionFactory = new CardCollectionFactory(new CardIdRange(CardConstants.MinId, CardConstants.MaxId));
            var cardCollectionFactoryDecorator = new ShuffledCardCollectionFactoryDecorator(cardCollectionFactory);
            var cardCollection = cardCollectionFactoryDecorator.Create();

            for (var i = 0; i < cardCollection.Count(); i += 1)
            {
                new Assets.Scripts.Shared.Shapes.Card(_shaders[ShaderConstants.TextureShader],
                    EntityComponentManager.AddEntity(Enums.Layer.Ground), $"Assets/Decks/Second/SecondDeck{cardCollection.ElementAt(i).ToString()}.png",
                        new Vector3(1.0f + (i * 0.15f), 1.0f + (i * 0.005f), 1.0f), new Quaternion(MathHelper.DegreesToRadians(90f), MathHelper.DegreesToRadians(0f), MathHelper.DegreesToRadians(180f)));
            }
        }
    }
}
