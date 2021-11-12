using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Circle : Shape
    {
        public Circle(float radius, Vector position) : base(new Vertex[36])
        {
            float theta = MathF.PI * 2 / 36;

            Color newColor = Color.Aqua;
            vertices[0].position = position;
            //vertices[0].color = Color.Blue;
            for (var i = 0; i < vertices.Length; i++)
            {
                if (i % 9 < 3) newColor = Color.Red;
                else if (i % 9 >= 3 && i % 9 < 6) newColor = Color.Pink;
                else newColor = Color.Blue;

                vertices[i] = new Vertex(new Vector(radius * MathF.Cos(theta * (i - 1))+position.x, radius * MathF.Sin(theta * (i - 1))+position.y), newColor);
            }
        }
    }
}
