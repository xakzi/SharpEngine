using static OpenGL.Gl;

namespace SharpEngine
{
    public class Triangle
    {
        Vertex[] vertices;
        public Triangle(Vertex[] vertices)
        {
            this.vertices = vertices;
            currentScale = 1f;
        }

        public float currentScale { get; private set; }

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
    }
}
