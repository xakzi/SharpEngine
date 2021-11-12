using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Cone : Shape
    {
        public Cone(float radius, float angle, Vector position) : base(new Vertex[36])
        {
            //Added Circle calculations here, not sure how to do the Cone :D
            float theta = MathF.PI * 2 / (36-2);

            Color newColor;
            vertices[0].position = position;
            for (var i = 0; i < vertices.Length; i++)
            {
                if (i % 9 < 3) newColor = Color.Yellow;
                else if (i % 9 >= 3 && i % 9 < 6) newColor = Color.Red;
                else newColor = Color.Ocean;

                vertices[i] = new Vertex(new Vector(radius * MathF.Cos(theta * (i - 1)) + vertices[0].position.x, radius * MathF.Sin(theta * (i - 1)) + vertices[0].position.y), newColor);
            }
        }
    }
}
