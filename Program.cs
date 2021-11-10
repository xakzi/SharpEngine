using System;
using GLFW;
using static OpenGL.Gl;
using System.IO;

namespace SharpEngine
{
    class Program
    {
        static float[] vertices = new float[] {
           // vertex 1 x, y, z
            -.1f, -.1f, 0f,
            // vertex 2 x, y, z
            .1f, -.1f, 0f,
            // vertex 3 x, y, z
            0f, .1f, 0f,
            // vertex 4 x, y, z
            .4f, .4f, 0f,
            // vertex 5 x, y, z
            .6f, .4f, 0f,
            // vertex 6 x, y, z
            .5f, .6f, 0f
            };

        const int vertexX = 0;
        const int vertexY = 1;
        const int vertexSize = 3;

        
        static float transformSpeed = 0.00005f;
        static void Main(string[] args)
        {
            // initialize and configure
            Window window = CreateWindow();
            LoadTriangleIntoBuffer();
            CreateShaderProgram();

            // engine rendering loop
            while (!Glfw.WindowShouldClose(window))
            {
                Glfw.PollEvents(); // react to window changes (position etc.)
                ClearScreen();
                Render(window);

                //TriangleMoveToRightContinuously();
                //TriangleMoveDownContinuously();
                //TriangleShrinkContinuously();
                //TriangleScaleUpContinously();
                //TriangleRotateOnSpot();

                UpdateTriangleBuffer();
            }
        }

        private static void TriangleRotateOnSpot()
        {
            float angle = 0.01f;
            float tmp = vertices[0];
            vertices[0] = (float)(vertices[0] * Math.Cos(angle) + vertices[1] * Math.Sin(angle));
            vertices[1] = (float)(vertices[1] * Math.Cos(angle) - tmp * Math.Sin(angle));

            tmp = vertices[3];
            vertices[3] = (float)(vertices[3] * Math.Cos(angle) + vertices[4] * Math.Sin(angle));
            vertices[4] = (float)(vertices[4] * Math.Cos(angle) - tmp * Math.Sin(angle));

            tmp = vertices[6];
            vertices[6] = (float)(vertices[6] * Math.Cos(angle) + vertices[7] * Math.Sin(angle));
            vertices[7] = (float)(vertices[7] * Math.Cos(angle) - tmp * Math.Sin(angle));
        }

        private static void Render(Window window)
        {
            //glDrawArrays(GL_LINE_LOOP, 0, 3); //Lined Triangle
            glDrawArrays(GL_TRIANGLES, 0, vertices.Length/vertexSize); //Filled Triangle
                                                                       //glDrawArrays(GL_TRIANGLES, 0, 6); //use this to get second triangle
                                                                       //Glfw.SwapBuffers(window); //Don't need this, uses glFlush() Instead.
            Glfw.SwapBuffers(window);
            //glFlush();
        }

        private static void ClearScreen()
        {
            glClearColor(0, 0, 0, 1);
            glClear(GL_COLOR_BUFFER_BIT);
        }

        private static void TriangleScaleUpContinously()
        {
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= 1.00009f;
            }
        }

        private static void TriangleShrinkContinuously()
        {
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= 0.9999f;
            }
        }

        private static void TriangleMoveDownContinuously()
        {
            for (var i = vertexY; i < vertices.Length; i += vertexSize)
            {
                vertices[i] -= transformSpeed;
            }
        }

        private static void TriangleMoveToRightContinuously()
        {
            for (var i = 0; i < vertices.Length; i++)
            {
                if (i % 3 == 0) vertices[i] += transformSpeed;
            }

        }

        private static unsafe void LoadTriangleIntoBuffer()
        { 
            // load the vertices into a buffer
            var vertexArray = glGenVertexArray();
            var vertexBuffer = glGenBuffer();
            glBindVertexArray(vertexArray);
            glBindBuffer(GL_ARRAY_BUFFER, vertexBuffer);

            UpdateTriangleBuffer();

            glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), NULL);

            glEnableVertexAttribArray(0);
        }

        static unsafe void UpdateTriangleBuffer()
        {
            fixed (float* vertex = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, vertex, GL_STATIC_DRAW);
            }
        }

        private static void CreateShaderProgram()
        {
            // create vertex shader
            var vertexShader = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vertexShader, File.ReadAllText("shaders/screen-coordinates.vert"));
            glCompileShader(vertexShader);

            //create fragment shader
            var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fragmentShader, File.ReadAllText("shaders/green.frag"));
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
