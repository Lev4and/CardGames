﻿using CardGames.OpenGLGameEngine.Entities.Components;
using CardGames.OpenGLGameEngine.Entities;
using CardGames.OpenGLGameEngine.Models;
using OpenTK.Mathematics;
using CardGames.OpenGLGameEngine.Models.Shapes3D.Models;

namespace CardGames.OpenGLGameEngine.Assets.Scripts.Shared.Shapes
{
    public class Plane
    {
        private Shader _shader;
        private Entity _entity;

        public Vector3 Position;
        public Quaternion? Rotation;
        public Vector2? Scale;

        public Plane(Shader shader, Entity entity, Vector3 position, Quaternion? rotation = null, Vector2? scale = null)
        {
            _shader = shader;
            _entity = entity;

            Position = position;
            Rotation = rotation;

            Scale = scale;

            _entity.AddComponent(new ModelComponent(_shader, 
                new Model($"Assets/Models/Plane.dae"), 
                position, 
                rotation, 
                scale != null 
                    ? new Vector3(scale.Value.X, 0.01f, scale.Value.Y) 
                    : new Vector3(1.0f, 0.01f, 1.0f), 
                new Dictionary<string, Texture>
                {
                    {
                        "Material", new Texture("Assets/Textures/PlaneGray.png")
                    }
                }));

            _entity.AddComponent(new StaticRigidBodyComponent());
        }
    }
}
