using System;
using System.Runtime.InteropServices;
using OpenGL;
using GLFW;
using static OpenGL.Gl;

namespace SharpEngine
{
    public class Triangle
    {
        Vertex[] vertices;
        public float currentScale { get; private set; }
        public Triangle(Vertex[] vertices)
        {
            this.vertices = vertices;
            currentScale = 1f;
            LoadTriangleIntoBuffer();
        }

        public Vector GetMaxBounds()
        {
            var max = vertices[2].position;
            for (var i = 0; i < vertices.Length; i++)
                max = Vector.Max(max, vertices[i].position);

            return max;
        }

        public Vector GetCenter()
        {
            return (GetMinBounds() + GetMaxBounds()) / 2;
        }

        public Vector GetMinBounds()
        {
            var min = vertices[0].position;
            for (var i = 0; i < vertices.Length; i++)
                min = Vector.Min(min, vertices[i].position);

            return min;
        }

        public void Scale(float multiplier)
        {
            // We first move the triangle to the center, to avoid 
            // the triangle moving around while scaling
            // Then, we move it back again
            var center = GetCenter();

            Move(center*-1);

            for (var i = 0; i < vertices.Length; i++)
                vertices[i].position *= multiplier;

            Move(center);

            currentScale *= multiplier;
        }

        public void Rotate()
        {
            
            float angle = 0.003f;
            var center = GetCenter();
            Move(center * -1);
            for (int i = 0; i < vertices.Length; i++)
            {
                var currentangle = Math.Atan2(vertices[i].position.y, vertices[i].position.x);
                var currentmagnitude = MathF.Sqrt(MathF.Pow(vertices[i].position.x, 2) + MathF.Pow(vertices[i].position.y, 2));
                var newX = MathF.Cos((float)currentangle + angle) * currentmagnitude;
                var newY = MathF.Sin((float)currentangle + angle) * currentmagnitude; 
                vertices[i].position = new Vector(newX, newY);                
            }
            Move(center);
        }

        public void Move(Vector direction)
        {
            for (var i = 0; i < vertices.Length; i++)
                vertices[i].position += direction;
        }

        public unsafe void Render()
        {
            fixed (Vertex* vertex = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * vertices.Length, vertex, GL_STATIC_DRAW);
            }
            glDrawArrays(GL_TRIANGLES, 0, vertices.Length);
        }

        private static unsafe void LoadTriangleIntoBuffer()
        {
            // load the vertices into a buffer
            var vertexArray = glGenVertexArray();
            var vertexBuffer = glGenBuffer();

            glBindVertexArray(vertexArray);
            glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);


            glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vertex), Marshal.OffsetOf(typeof(Vertex), nameof(Vertex.position)));
            glVertexAttribPointer(1, 4, GL_FLOAT, false, sizeof(Vertex), (void*)(sizeof(Vector)));

            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
        }
    }
}
