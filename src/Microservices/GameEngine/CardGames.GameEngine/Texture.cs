using OpenTK.Graphics.OpenGL;
using StbImageSharp;

namespace CardGames.GameEngine
{
    public class Texture
    {
        private readonly int _id;

        public Texture(byte[] fileContent)
        {
            _id = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);

            Bind();

            StbImage.stbi_set_flip_vertically_on_load(1);

            var image = ImageResult.FromMemory(fileContent, ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, 
                image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Unbind();
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _id);
        }

        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Delete()
        {
            GL.DeleteTexture(_id);
        }
    }
}
