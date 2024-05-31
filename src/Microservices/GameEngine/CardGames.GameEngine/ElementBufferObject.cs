﻿using OpenTK.Graphics.OpenGL;

namespace CardGames.GameEngine
{
    public class ElementBufferObject
    {
        private readonly int _id;

        public ElementBufferObject(IEnumerable<uint> data)
        {
            _id = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _id);

            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count() * sizeof(uint), 
                data.ToArray(), BufferUsageHint.StaticDraw);
        }
        
        public void Bind() 
        { 
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _id); 
        }
        
        public void Unbind() 
        { 
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0); 
        }
        
        public void Delete() 
        { 
            GL.DeleteBuffer(_id); 
        }
    }
}
