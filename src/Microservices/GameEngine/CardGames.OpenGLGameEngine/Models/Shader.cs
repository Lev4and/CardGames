using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Reflection.Metadata;
using System.Text;

namespace CardGames.OpenGLGameEngine.Models
{
    public class Shader
    {
        private readonly int _id;

        private readonly int _vertexShaderId;
        private readonly int _fragmentShaderId;

        private readonly Dictionary<string, int> _uniformLocations;

        public Shader(string vertexShaderFilePath, string fragmentShaderFilePath)
        {
            _vertexShaderId = CompileShader(ShaderType.VertexShader, vertexShaderFilePath);
            _fragmentShaderId = CompileShader(ShaderType.FragmentShader, fragmentShaderFilePath);

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

            GL.GetProgram(_id, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(_id, i, out _, out _);
                var location = GL.GetUniformLocation(_id, key);

                _uniformLocations.Add(key, location);
            }
        }

        public void Activate()
        {
            GL.UseProgram(_id);
        }

        public void Deactivate()
        {
            GL.UseProgram(0);
        }

        public void DeleteProgram()
        {
            GL.DeleteProgram(_id);
        }

        public int GetAttribLocation(string name)
        {
            return GL.GetAttribLocation(_id, name);
        }

        public void SetInt(string name, int data)
        {
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetFloat(string name, float data)
        {
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.Uniform3(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, bool transpose, Matrix4 matrix)
        {
            GL.UniformMatrix4(_uniformLocations[name], transpose, ref matrix);
        }

        public void SetUniform4(string name, Vector4 vector)
        {
            GL.Uniform4(_uniformLocations[name], vector);
        }

        private int CompileShader(ShaderType shaderType, string shaderFilePath)
        {
            var id = GL.CreateShader(shaderType);

            var shaderFileContent = File.ReadAllText(shaderFilePath);

            GL.ShaderSource(id, shaderFileContent);

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
