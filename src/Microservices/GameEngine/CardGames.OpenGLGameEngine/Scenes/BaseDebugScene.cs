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
                EntityComponentManager.AddEntity(Enums.Layer.Ground), new Vector3(0.0f, -3.925f, 0.0f),
                    scale: new Vector2(100.0f, 100.0f));

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

            var table5 = new Table(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                new Vector3(0f, 0f, 0f));

            var cardCollectionFactory = new CardCollectionFactory(new CardIdRange(CardConstants.MinId, CardConstants.MaxId));
            var deckFactory = new DeckFactory(cardCollectionFactory);
            var deck = deckFactory.Create();

            new Chip(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                "Red", "White", new Vector3(5.5f + table5.Position.X - (1f * 3), 
                    0.1f, 2.9f + table5.Position.Z));

            new Chip(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                "Green", "White", new Vector3(5.5f + table5.Position.X - (1f * 4),
                    0.1f, 2.9f + table5.Position.Z));

            new Chip(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                "Blue", "White", new Vector3(5.5f + table5.Position.X - (1f * 5),
                    0.1f, 2.9f + table5.Position.Z));

            new Chip(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                "Yellow", "White", new Vector3(5.5f + table5.Position.X - (1f * 6),
                    0.1f, 2.9f + table5.Position.Z));

            new Chip(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                "Purple", "White", new Vector3(5.5f + table5.Position.X - (1f * 7),
                    0.1f, 2.9f + table5.Position.Z));

            new Chip(_shaders[ShaderConstants.TextureShader],
                EntityComponentManager.AddEntity(Enums.Layer.Ground),
                "Aqua", "White", new Vector3(5.5f + table5.Position.X - (1f * 8),
                    0.1f, 2.9f + table5.Position.Z));

            for (var i = 0; i < deck.Count(); i += 1)
            {
                new Assets.Scripts.Shared.Shapes.Card(_shaders[ShaderConstants.TextureShader],
                    EntityComponentManager.AddEntity(Enums.Layer.Ground),
                    deck.ElementAt(i), "Second",
                    new Vector3(5.5f + table5.Position.X - (1f * (i % 12)), 0.1f, 1.9f + table5.Position.Z - (1f * (i / 12))),
                    new Quaternion(MathHelper.DegreesToRadians(180f), MathHelper.DegreesToRadians(180f),
                        MathHelper.DegreesToRadians(0f)));
            }
        }
    }
}
