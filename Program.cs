using System;
using GLFW;
using static OpenGL.Gl;
using System.IO;

namespace SharpEngine
{
    class Program
    {

        static Vertex[] vertices = new Vertex[] {
            new Vertex(new Vector(-.1f, -.1f), Color.Red),
            new Vertex(new Vector(.1f, -.1f), Color.Green),
            new Vertex(new Vector(0f, .1f), Color.Blue),

            new Vertex(new Vector(.4f, .2f), Color.Purple),
            new Vertex(new Vector(.6f, .2f), Color.Cyan),
            new Vertex(new Vector(.5f, .4f), Color.Yellow)
            };
        
        static float transformSpeed = 0.005f;
        static void Main(string[] args)
        {
            
            // initialize and configure
            Window window = CreateWindow();
            LoadTriangleIntoBuffer();
            CreateShaderProgram();
            var direction = new Vector(transformSpeed, transformSpeed);
            var multiplier = 0.998f;
            var scale = 1f;
            // engine rendering loop
            while (!Glfw.WindowShouldClose(window))
            {
                Glfw.PollEvents(); // react to window changes (position etc.)
                ClearScreen();
                Render(window);

                //Bounces
                direction = TriangleBounceOnWall(direction);

                //scaling
                TriangleScaleUpAndDownContinously(ref multiplier, ref scale);

                UpdateTriangleBuffer();
            }
        }

        private static Vector TriangleBounceOnWall(Vector direction)
        {
            // 3. Move the Triangle by its Direction
            for (var i = 0; i < vertices.Length; i++)
                vertices[i].position += direction;

            // 4. Check the X-Bounds of the Screen
            for (var i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].position.x >= 1 && direction.x > 0 || vertices[i].position.x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                    break;
                }
            }
            // 5. Check the Y-Bounds of the Screen
            for (var i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].position.y >= 1 && direction.y > 0 || vertices[i].position.y <= -1 && direction.y < 0)
                {
                    direction.y *= -1;
                    break;
                }
            }

            return direction;
        }

        private static void TriangleScaleUpAndDownContinously(ref float multiplier, ref float scale)
        {
            //move it to the center
            // - finding the center of the triangle
            var min = vertices[0].position;
            for (var i = 0; i < vertices.Length; i++)
                min = Vector.Min(min, vertices[i].position);

            var max = vertices[2].position;
            for (var i = 0; i < vertices.Length; i++)
                max = Vector.Max(max, vertices[i].position);

            var center = (min + max) / 2;

            // - moving it by the opposite vector
            for (var i = 0; i < vertices.Length; i++)
                vertices[i].position -= center;

            //scale the triangle
            for (var i = 0; i < vertices.Length; i++)
                vertices[i].position *= multiplier;

            //move it back to where it was
            for (var i = 0; i < vertices.Length; i++)
                vertices[i].position += center;

            scale *= multiplier;

            if (scale <= 0.5f)
                multiplier = 1.002f;
            if (scale >= 1f)
                multiplier = 0.998f;
        }

        private static void Render(Window window)
        {
            //glDrawArrays(GL_LINE_LOOP, 0, 3); //Lined Triangle
            glDrawArrays(GL_TRIANGLES, 0, vertices.Length); //Filled Triangle

            Glfw.SwapBuffers(window);
            //glFlush();
        }

        private static void ClearScreen()
        {
            glClearColor(0, 0, 0, 1);
            glClear(GL_COLOR_BUFFER_BIT);
        }

        private static unsafe void LoadTriangleIntoBuffer()
        { 
            // load the vertices into a buffer
            var vertexArray = glGenVertexArray();
            var vertexBuffer = glGenBuffer();

            glBindVertexArray(vertexArray);
            glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);

            UpdateTriangleBuffer();

            glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vertex), NULL);
            glVertexAttribPointer(1, 4, GL_FLOAT, false, sizeof(Vertex), (void*)(sizeof(Vector)));

            glEnableVertexAttribArray(0);
            glEnableVertexAttribArray(1);
        }

        static unsafe void UpdateTriangleBuffer()
        {
            fixed (Vertex* vertex = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * vertices.Length, vertex, GL_STATIC_DRAW);
            }
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
