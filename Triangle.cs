using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Triangle : Shape
    {
        public Triangle(float width, float height, Vector position) : base(new Vertex[3])
        {
            vertices[0] = new Vertex(new Vector(position.x - width / 2, position.y - height / 2), Color.Red);
            vertices[1] = new Vertex(new Vector(position.x + width / 2, position.y - height / 2), Color.Pink);
            vertices[2] = new Vertex(new Vector(position.x, position.y + height / 2), Color.Blue);
        }
    }
}
