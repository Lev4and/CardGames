using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace CardGames.OpenGLGameEngine.Models
{
    public class Texture
    {
        private readonly int _id;

        public Texture(string textureFilePath)
        {
            _id = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);

            Bind();

            StbImage.stbi_set_flip_vertically_on_load(1);

            var textureFileContent = File.ReadAllBytes(textureFilePath);

            var image = ImageResult.FromMemory(textureFileContent, ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width,
                image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Unbind();
        }

        public Texture(IEnumerable<string> files)
        {
            _id = GL.GenTexture();

            Bind(TextureTarget.TextureCubeMap);

            StbImage.stbi_set_flip_vertically_on_load(0);

            for (var i = 0; i < files.Count(); i++)
            {
                var textureFileContent = File.ReadAllBytes(files.ElementAt(i));

                var result = ImageResult.FromMemory(textureFileContent, ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(TextureTarget.TextureCubeMapPositiveX + i, 0, PixelInternalFormat.Rgba, 
                    result.Width, result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, result.Data);
            }

            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            Unbind();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _id);
        }

        public void Bind(TextureTarget textureTarget)
        {
            GL.BindTexture(textureTarget, _id);
        }

        public void Bind(TextureUnit unit)
        {
            GL.ActiveTexture(unit);

            GL.BindTexture(TextureTarget.Texture2D, _id);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Unbind(TextureUnit unit)
        {
            GL.ActiveTexture(unit);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Delete()
        {
            GL.DeleteTexture(_id);
        }
    }
}
