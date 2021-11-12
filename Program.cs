using System;
using GLFW;
using static OpenGL.Gl;
using System.IO;
using System.Runtime.InteropServices;

namespace SharpEngine
{

    class Program
    {
        /*static Shape triangle = new Shape
        (
            new Vertex[]
            {
                new Vertex(new Vector(-.1f, -.1f), Color.Pink),
                new Vertex(new Vector(.1f, -.1f), Color.Aqua),
                new Vertex(new Vector(0f, .1f), Color.Ocean)
            }
        );*/

        static Triangle triangle = new Triangle(.15f, .15f, new Vector(.2f, .2f));
        static Rectangle rectangle = new Rectangle(.15f, .15f, new Vector(-.2f, -.2f));
        static Circle circle = new Circle(.1f, new Vector(.2f, -.2f));
        static Cone cone = new Cone(.1f, .1f, new Vector(-.2f, .2f));

        static float transformSpeed = 0.005f;
        static void Main(string[] args)
        {
            // initialize and configure
            Window window = CreateWindow();
            
            CreateShaderProgram();
            var direction = new Vector(transformSpeed, transformSpeed);
            var multiplier = 0.998f;
            var maxScale = 0.998f;
            var minScale = 1.002f;
            // engine rendering loop
            while (!Glfw.WindowShouldClose(window))
            {
                Glfw.PollEvents(); // react to window changes (position etc.)
                ClearScreen();
                Render(window);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                rectangle.Scale(multiplier);
                if (rectangle.currentScale <= 0.5f) { multiplier = minScale; }
                if (rectangle.currentScale >= 1.5f) { multiplier = maxScale; }
                rectangle.Move(direction); //works as inteded.

                //Actually only needs half of this to work, since Triangle is touching the other side -- fix this in the future :)
                //need to create a direction change in Shape.cs and put it in a method
                //where it gives "this.direction" so the direction changes shape by shape
                if (rectangle.GetMaxBounds().x >= 1 && direction.x > 0 || rectangle.GetMinBounds().x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                }

                if (rectangle.GetMaxBounds().y >= 1 && direction.y > 0 || rectangle.GetMinBounds().y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                }
                rectangle.Rotate(); //works as intended.

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                triangle.Scale(multiplier);
                if (triangle.currentScale <= 0.5f) { multiplier = minScale; }
                if (triangle.currentScale >= 1.5f) { multiplier = maxScale; }
                triangle.Move(direction); //is not really needed as move, moves all shapes that exists lol --Fix this in the future :)

                //Actually only needs half of this to work, since rectangle is touching the other side -- fix this in the future :)
                if (triangle.GetMaxBounds().x >= 1 && direction.x > 0 || triangle.GetMinBounds().x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                }

                if (triangle.GetMaxBounds().y >= 1 && direction.y > 0 || triangle.GetMinBounds().y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                }
                triangle.Rotate(); //works as intended.

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                circle.Scale(multiplier);
                if(circle.currentScale <= 0.5f) { multiplier = minScale; }
                if(circle.currentScale >= 1.5f) { multiplier = minScale; }
                
                circle.Move(direction);  //is not really needed as move, moves all shapes that exists lol --Fix this in the future :)

                //This check is not needed for circle as it moves with the other shapens and only rectangle & triangle is touching the walls -- fix this in the future :)
                if (circle.GetMaxBounds().x >= 1 && direction.x > 0 || circle.GetMinBounds().x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                }

                if (circle.GetMaxBounds().y >= 1 && direction.y > 0 || circle.GetMinBounds().y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                }
                circle.Rotate(); //works as intended.

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                cone.Scale(multiplier);
                if (cone.currentScale <= 0.5f) { multiplier = minScale; }
                if (cone.currentScale >= 1.5f) { multiplier = minScale; }

                cone.Move(direction);  //is not really needed as move, moves all shapes that exists lol --Fix this in the future :)

                //This check is not needed for circle as it moves with the other shapens and only rectangle & triangle is touching the walls -- fix this in the future :)
                if (cone.GetMaxBounds().x >= 1 && direction.x > 0 || cone.GetMinBounds().x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                }

                if (cone.GetMaxBounds().y >= 1 && direction.y > 0 || cone.GetMinBounds().y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                }
                cone.Rotate(); //works as intended.

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }

        private static void Render(Window window)
        {
            //glDrawArrays(GL_LINE_LOOP, 0, 3); //Lined Triangle
            rectangle.Render();
            triangle.Render();
            circle.Render();
            cone.Render();

            Glfw.SwapBuffers(window);
            //glFlush();
        }

        private static void ClearScreen()
        {
            glClearColor(0, 0, 0, 1);
            glClear(GL_COLOR_BUFFER_BIT);
        }

        private static void CreateShaderProgram()
        {
            // create vertex shader
            var vertexShader = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vertexShader, File.ReadAllText("shaders/position-color.vert"));
            glCompileShader(vertexShader);

            //create fragment shader
            var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fragmentShader, File.ReadAllText("shaders/vertex-color.frag"));
            glCompileShader(fragmentShader);

            // create shader program - rendering pipeline
            var program = glCreateProgram();
            glAttachShader(program, vertexShader);
            glAttachShader(program, fragmentShader);
            glLinkProgram(program);
            glUseProgram(program);
        }

        private static Window CreateWindow()
        {
            // initialize and configure
            Glfw.Init();
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, Constants.True);
            Glfw.WindowHint(Hint.Doublebuffer, Constants.True);

            // create and launch a window
            var window = Glfw.CreateWindow(800, 600, "MyWindow", Monitor.None, Window.None);
            Glfw.MakeContextCurrent(window);
            Import(Glfw.GetProcAddress);
            return window;
        }
    }
}
