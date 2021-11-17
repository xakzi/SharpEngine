using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using OpenGL;

namespace SharpEngine
{
    class Physics
    {
        readonly Scene scene;
        public Physics(Scene scene)
        {
            this.scene = scene;
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < this.scene.shapes.Count; i++)
            {
                Shape shape = this.scene.shapes[i];
                // linear velocity
                shape.Transform.Position = shape.Transform.Position + shape.velocity * deltaTime;
                // a = F/m (another version of F = m * a
                var acceleration = shape.linearForce / shape.mass;
                // linear acceleration:
                shape.Transform.Position = shape.Transform.Position + acceleration * deltaTime * deltaTime / 2;
                shape.velocity = shape.velocity + acceleration * deltaTime;
            }
        }
    }
}
