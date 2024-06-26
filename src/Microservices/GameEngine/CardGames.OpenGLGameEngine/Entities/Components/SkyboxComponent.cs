﻿using CardGames.OpenGLGameEngine.Models.Skybox;
using CardGames.OpenGLGameEngine.Models;

namespace CardGames.OpenGLGameEngine.Entities.Components
{
    public class SkyboxComponent : Component
    {
        private readonly Shader _shader;
        private readonly Skybox _skybox;

        public SkyboxComponent(Shader shader, Skybox skybox)
        {
            _shader = shader;
            _skybox = skybox;
        }

        public override void Init()
        {
            base.Init();

            _skybox.BindAndBuffer(_shader);
        }

        public override void Draw()
        {
            base.Draw();

            _skybox.Draw(_shader, new TransformComponent());
        }
    }
}
