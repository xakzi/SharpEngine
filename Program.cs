using System;
using GLFW;

namespace SharpEngine
{
    class Program
    {
        static float Lerp(float from, float to, float t)
        {
            return from + (to - from) * t;
        }

        static float GetRandomFloat(Random random, float min = 0, float max = 1)
        {
            return Lerp(min, max, (float)random.Next() / int.MaxValue);
        }

        static void Main(string[] args)
        {

            var window = new Window();
            var material = new Material("shaders/world-position-color.vert", "shaders/vertex-color.frag");
            var scene = new Scene();
            window.Load(scene);

            const int fixedStepNumberPerSecond = 30;
            const float fixedDeltaTime = 1.0f / fixedStepNumberPerSecond;
            double previousFixedStep = 0.0;
            const float movementSpeed = 1f;

            var triangle = new Triangle(material);
            scene.Add(triangle);

            var rectangle = new Rectangle(material);
            rectangle.Transform.Position = new Vector(-.5f, -.5f);
            scene.Add(rectangle);

            var circle = new Circle(material);
            circle.Transform.Position = new Vector(-.5f, .5f);
            scene.Add(circle);

            var ground1 = new Rectangle(material);
            ground1.Transform.CurrentScale = new Vector(20f, 1f, 1f);
            ground1.Transform.Position = new Vector(0f, -1f);
            scene.Add(ground1);

            // engine rendering loop
            while (window.IsOpen())
            {
                while (Glfw.Time > previousFixedStep + fixedDeltaTime)
                {
                    previousFixedStep += fixedDeltaTime;
                    var walkDirection = new Vector();

                    if (window.GetKey(Keys.W))
                    {
                        walkDirection += triangle.Transform.Forward;
                        //shape.Transform.Position += new Vector(0f, movementSpeed * fixedDeltaTime);
                    }
                    if (window.GetKey(Keys.S))
                    {
                        walkDirection += triangle.Transform.Backward;
                        //shape.Transform.Position += new Vector(0f, -movementSpeed * fixedDeltaTime);
                    }
                    if (window.GetKey(Keys.A))
                    {
                        //walkDirection += Vector.Left;
                        //shape.Transform.Position += new Vector(-movementSpeed * fixedDeltaTime, 0 );
                        var rotation = triangle.Transform.Rotation;
                        rotation.z += 2 * MathF.PI * fixedDeltaTime;
                        triangle.Transform.Rotation = rotation;
                    }
                    if (window.GetKey(Keys.D))
                    {
                        //walkDirection += Vector.Right;
                        //shape.Transform.Position += new Vector(movementSpeed * fixedDeltaTime, 0);
                        var rotation = triangle.Transform.Rotation;
                        rotation.z -= 2 * MathF.PI * fixedDeltaTime;
                        triangle.Transform.Rotation = rotation;
                    }



                    walkDirection = walkDirection.Normalize();
                    triangle.Transform.Position += walkDirection * (movementSpeed * fixedDeltaTime);
                    
                    
                    CircleChangeColor(triangle, circle);

                    RectangleChangeColor(triangle, rectangle);

                }
                window.Render();
            }
        }

        private static void CircleChangeColor(Triangle triangle, Circle circle)
        {
            var angle = MathF.Acos(Vector.Dot(circle.GetCenter() - triangle.GetCenter(), triangle.Transform.Forward));

            if (angle > 2)
            {
                //2+
                circle.SetColor(Color.Black);
                Console.WriteLine("the circle is now Black!");

            }
            else if (angle < 1)
            {
                //1 - 2
                circle.SetColor(Color.Red);
                Console.WriteLine("the circle is now Red!.. should be white and color the whole circle.");
            }
            else
            {
                //0 - 1
                circle.SetColor(Color.White);
                Console.WriteLine("the circle is now white!.. should only color half of the circle D:");
            }
        }

        private static void RectangleChangeColor(Triangle triangle, Rectangle rectangle)
        {
            if (Vector.Dot(rectangle.GetCenter() - triangle.GetCenter(), triangle.Transform.Forward) < 0) // change color on triangles left and right side
            //if (Vector.Dot(rectangle.Transform.Forward, triangle.Transform.Forward) < 0) //change color on triangles right side
            {
                rectangle.SetColor(Color.Green);
                Console.WriteLine("Rectangle is Green!");
            }
            else
            {
                rectangle.SetColor(Color.Red);
                Console.WriteLine("Rectangle is Red!");
            }
        }
    }
}
