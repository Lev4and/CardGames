namespace CardGames.OpenGLGameEngine.Models
{
    public class Constants
    {
        public static class ShaderRoutes
        {
            public const string BaseVertexShader = "Assets/Shaders/Shader.vert";
            public const string BaseFragmentShader = "Assets/Shaders/Shader.frag";
            public const string BaseLightingShader = "Assets/Shaders/LightingShader.frag";
            public const string BaseTextVertexShader = "Assets/Shaders/TextShader.vert";
            public const string BaseTextFragmentShader = "Assets/Shaders/TextShader.frag";
            public const string SkyboxVertexShader = "Assets/Shaders/SkyboxShader.vert";
            public const string SkyboxFragmentShader = "Assets/Shaders/SkyboxShader.frag";
        }

        public static class ShaderConstants
        {
            public const string TextShader = "TextShader";
            public const string LightShader = "LightShader";
            public const string SkyboxShader = "SkyboxShader";
            public const string TextureShader = "TextureShader";
        }
    }
}
