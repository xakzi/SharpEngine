using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Rectangle : Shape
    {
        public Rectangle(float width, float height, Vector position) : base(new Vertex[6])
        {
            vertices[0] = new Vertex(new Vector(position.x - width / 2, position.y - height / 2), Color.Aqua);
            vertices[1] = new Vertex(new Vector(position.x + width / 2, position.y - height / 2), Color.Pink);
            vertices[2] = new Vertex(new Vector(position.x - width / 2, position.y + height / 2), Color.Yellow);
            vertices[3] = new Vertex(new Vector(position.x + width / 2, position.y + height / 2), Color.Red);
            vertices[4] = new Vertex(new Vector(position.x - width / 2, position.y + height / 2), Color.Blue);
            vertices[5] = new Vertex(new Vector(position.x + width / 2, position.y - height / 2), Color.Ocean);
        }
    }
}
