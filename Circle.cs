using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    class Circle : Shape
    {
        public Circle(float radius, Vector position) : base(new Vertex[360])
        {
            for (var i = 0; i < vertices.Length; i++)
            {
                float theta = i * MathF.PI / radius;

                /*float theta = 2.0f * (float)Math.PI * i / radius;
                float x = radius * MathF.Cos(theta);
                float y = radius * MathF.Sin(theta);
                vertices[i] = new Vertex(new Vector(position.x * y, position.y * x), Color.Blue);*/
                vertices[i] = new Vertex(new Vector((position.x * MathF.Cos(theta))/8, (position.y * MathF.Sin(theta))/8), Color.Blue);
            }

            /*
             for (int i = 0; i < vertices.Length; i++)
            {
                var currentangle = Math.Atan2(vertices[i].position.y, vertices[i].position.x);
                var currentmagnitude = MathF.Sqrt(MathF.Pow(vertices[i].position.x, 2) + MathF.Pow(vertices[i].position.y, 2));
                var newX = MathF.Cos((float)currentangle + angle) * currentmagnitude;
                var newY = MathF.Sin((float)currentangle + angle) * currentmagnitude; 
                vertices[i].position = new Vector(newX, newY);                
            }
             * */

            // vertices[0] = new Vertex(new Vector(position.x - width / 2, position.y - height / 2), Color.Red);
            //vertices[1] = new Vertex(new Vector(position.x + width / 2, position.y - height / 2), Color.Pink);
            //vertices[2] = new Vertex(new Vector(position.x, position.y + height / 2), Color.Blue);
        }
    }
}
