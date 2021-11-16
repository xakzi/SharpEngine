using System;
using GLFW;

namespace SharpEngine
{
    class Program {
        static float Lerp(float from, float to, float t) {
            return from + (to - from) * t;
        }

        static float GetRandomFloat(Random random, float min = 0, float max = 1) {
            return Lerp(min, max, (float)random.Next() / int.MaxValue);
        }
        
        static void Main(string[] args) {
            
            var window = new Window();
            var material = new Material("shaders/world-position-color.vert", "shaders/vertex-color.frag");
            var scene = new Scene();
            window.Load(scene);

            var shape = new Triangle(material);
            //shape.Transform.CurrentScale = new Vector(0.5f, 1f, 1f);
            scene.Add(shape);

            var ground1 = new Rectangle(material);
            ground1.Transform.CurrentScale = new Vector(20f, 1f, 1f);
            ground1.Transform.Position = new Vector(0f, -1f);
            scene.Add(ground1);

            var ground2 = new Rectangle(material);
            ground2.Transform.CurrentScale = new Vector(20f, 1f, 1f);
            ground2.Transform.Position = new Vector(0f, 1f);
            scene.Add(ground2);

            var wall1 = new Rectangle(material);
            wall1.Transform.CurrentScale = new Vector(1f, 20f, 1f);
            wall1.Transform.Position = new Vector(-1f, 0f);
            scene.Add(wall1);

            var wall2 = new Rectangle(material);
            wall2.Transform.CurrentScale = new Vector(1f, 20f, 1f);
            wall2.Transform.Position = new Vector(1f, 0f);
            scene.Add(wall2);

            // engine rendering loop
            const int fixedStepNumberPerSecond = 30;
            const float fixedDeltaTime = 1.0f / fixedStepNumberPerSecond;
            double previousFixedStep = 0.0;
            const float movementSpeed = 1f;
            while (window.IsOpen()) {
                while (Glfw.Time > previousFixedStep + fixedDeltaTime) {
                    previousFixedStep += fixedDeltaTime;
                    var walkDirection = new Vector();
                    if (window.GetKey(Keys.W))
                    {
                        walkDirection += shape.Transform.Forward;
                        //shape.Transform.Position += new Vector(0f, movementSpeed * fixedDeltaTime);
                    }
                    if (window.GetKey(Keys.S))
                    {
                        walkDirection += shape.Transform.Backward;
                        //shape.Transform.Position += new Vector(0f, -movementSpeed * fixedDeltaTime);
                    }
                    if (window.GetKey(Keys.A))
                    {
                        //walkDirection += Vector.Left;
                        //shape.Transform.Position += new Vector(-movementSpeed * fixedDeltaTime, 0 );
                        var rotation = shape.Transform.Rotation;
                        rotation.z += 2 * MathF.PI * fixedDeltaTime;
                        shape.Transform.Rotation = rotation;
                    }
                    if (window.GetKey(Keys.D))
                    {
                        //walkDirection += Vector.Right;
                        //shape.Transform.Position += new Vector(movementSpeed * fixedDeltaTime, 0);
                        var rotation = shape.Transform.Rotation;
                        rotation.z -= 2 * MathF.PI * fixedDeltaTime;
                        shape.Transform.Rotation = rotation;
                    }
                    if(window.GetKey(Keys.Q))
                    {
                        var rotation = shape.Transform.Rotation;
                        rotation.z += 2 * MathF.PI * fixedDeltaTime;
                        shape.Transform.Rotation = rotation;
                    }
                    if (window.GetKey(Keys.E))
                    {
                        var rotation = shape.Transform.Rotation;
                        rotation.z -= 2 * MathF.PI * fixedDeltaTime;
                        shape.Transform.Rotation = rotation;
                    }

                    walkDirection = walkDirection.Normalize();
                    shape.Transform.Position += walkDirection * (movementSpeed * fixedDeltaTime);

                }
                window.Render();
            }
        }
    }
}
