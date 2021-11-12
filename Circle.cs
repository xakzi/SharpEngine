using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Circle : Shape
    {
        public Circle(float radius, Vector position) : base(new Vertex[48])
        {

            float theta = MathF.PI * 2 / (48 - 2);

                Color newColor;
                vertices[0].position = position;
                for (var i = 0; i < vertices.Length; i++)
                {
                    if (i % 9 < 3) newColor = Color.Red;
                    else if (i % 9 >= 3 && i % 9 < 6) newColor = Color.Pink;
                    else newColor = Color.Aqua;

                    vertices[i] = new Vertex(new Vector(radius * MathF.Cos(theta * (i - 1)) + vertices[0].position.x, radius * MathF.Sin(theta * (i - 1)) + vertices[0].position.y), newColor);
                }
        }
    }
}
