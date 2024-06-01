using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Text;

namespace CardGames.GameEngine.Rendering
{
    public class Shader
    {
        private readonly int _id;

        private readonly int _vertexShaderId;
        private readonly int _fragmentShaderId;

        public Shader(byte[] vertexShaderContent, byte[] fragmentShaderContent)
        {
            _vertexShaderId = CreateShader(ShaderType.VertexShader, vertexShaderContent);
            _fragmentShaderId = CreateShader(ShaderType.FragmentShader, fragmentShaderContent);

            _id = GL.CreateProgram();

            GL.AttachShader(_id, _vertexShaderId);
            GL.AttachShader(_id, _fragmentShaderId);

            GL.LinkProgram(_id);

            GL.GetProgram(_id, GetProgramParameterName.LinkStatus, out var linkStatusCode);

            if (linkStatusCode != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(_id);

                throw new Exception($"An error occurred while linking shader program {_id} \n\n {infoLog}");
            }

            DeleteShader(_vertexShaderId);
            DeleteShader(_fragmentShaderId);
        }

        public void Activate()
        {
            GL.UseProgram(_id);
        }

        public void Deactivate()
        {
            GL.UseProgram(0);
        }

        public void Delete()
        {
            GL.DeleteProgram(_id);
        }

        public int GetAttribProgram(string name)
        {
            return GL.GetAttribLocation(_id, name);
        }

        public void SetMatrix4(string name, Matrix4 matrix)
        {
            var location = GL.GetUniformLocation(_id, name);

            GL.UniformMatrix4(location, true, ref matrix);
        }

        public void SetUniform4(string name, Vector4 vector)
        {
            var location = GL.GetUniformLocation(_id, name);

            GL.Uniform4(location, vector);
        }

        private int CreateShader(ShaderType shaderType, byte[] shaderContent)
        {
            var id = GL.CreateShader(shaderType);

            GL.ShaderSource(id, Encoding.UTF8.GetString(shaderContent));

            GL.CompileShader(id);

            GL.GetShader(id, ShaderParameter.CompileStatus, out var compileStatusCode);

            if (compileStatusCode != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(id);

                throw new Exception($"An error occurred while compiling the shader {id} \n\n {infoLog}");
            }

            return id;
        }

        private void DeleteShader(int shader)
        {
            GL.DetachShader(_id, shader);

            GL.DeleteShader(shader);
        }
    }
}
