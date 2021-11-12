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

        static Triangle triangle = new Triangle(0.15f, 0.15f, new Vector(-.5f, -.5f));
        static Rectangle rectangle = new Rectangle(0.15f, 0.15f, new Vector(.5f, .5f));
        static Circle circle = new Circle((float)180, new Vector(1f, 1f));

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

                rectangle.Scale(multiplier);
                if (rectangle.currentScale <= 0.5f) { multiplier = minScale; }
                if (rectangle.currentScale >= 1.5f) { multiplier = maxScale; }
                rectangle.Rotate();
                rectangle.Move(direction);
                if(rectangle.GetMaxBounds().x >= 1 && direction.x > 0 || rectangle.GetMinBounds().x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                }

                if (rectangle.GetMaxBounds().y >= 1 && direction.y > 0 || rectangle.GetMinBounds().y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                }

                triangle.Scale(multiplier);
                if (triangle.currentScale <= 0.5f) { multiplier = minScale; }
                if (triangle.currentScale >= 1.5f) { multiplier = maxScale; }
                triangle.Rotate();
                triangle.Move(direction);
                if (triangle.GetMaxBounds().x >= 1 && direction.x > 0 || triangle.GetMinBounds().x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                }

                if (triangle.GetMaxBounds().y >= 1 && direction.y > 0 || triangle.GetMinBounds().y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                }

                circle.Scale(multiplier);
                if(circle.currentScale <= 0.5f) { multiplier = minScale; }
                if(circle.currentScale >= 1.5f) { multiplier = minScale; }
                circle.Move(direction);

            }
        }

        private static void Render(Window window)
        {
            //glDrawArrays(GL_LINE_LOOP, 0, 3); //Lined Triangle
            rectangle.Render();
            triangle.Render();
            circle.Render();

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
