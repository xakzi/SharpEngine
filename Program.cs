using System;
using GLFW;
using static OpenGL.Gl;
using System.IO;

namespace SharpEngine
{
    class Program
    {
        static float[] vertices = new float[] {
            // vertex 1 x,y,z   
            -.5f, -.5f, 0f,
            // vertex 2 x,y,z
                .5f, -.5f, 0f,
            //vertex 3 x,y,z
                0f, .5f, 0f,

           /* //use this to fit both first and second triangle
            // vertex 1 x,y,z   
            -1f, -.5f, 0f,
            // vertex 2 x,y,z
                0f, -.5f, 0f,
            //vertex 3 x,y,z
                -.5f, .5f, 0f,
            
            // vertex 1 x,y,z   
            1f, -.5f, 0f,
            // vertex 2 x,y,z
                0f, -.5f, 0f,
            //vertex 3 x,y,z
                0.5f, .5f, 0f*/
            };

        static float[] verticesTempCenter = new float[]
        {
            0f,0f,0f,
            0f,0f,0f,
            0f,0f,0f
        };

        
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
                glClearColor(0, 0, 0, 1);
                glClear(GL_COLOR_BUFFER_BIT);
                //glDrawArrays(GL_LINE_LOOP, 0, 3); //Lined Triangle
                glDrawArrays(GL_TRIANGLES, 0, 3); //Filled Triangle
                //glDrawArrays(GL_TRIANGLES, 0, 6); //use this to get second triangle
                //Glfw.SwapBuffers(window); //Don't need this, uses glFlush() Instead.
                glFlush();

                //TriangleMoveToRightContinuously();
                //TriangleMoveDownContinuously();
                //TriangleShrinkContinuously();
                //TriangleScaleUpContinously();

                UpdateTriangleBuffer();
            }
        }

        private static void TriangleScaleUpContinously()
        {
            vertices[0] -= transformSpeed;
            vertices[1] -= transformSpeed;
            vertices[3] += transformSpeed;
            vertices[4] -= transformSpeed;
            vertices[7] += transformSpeed;
        }

        private static void TriangleShrinkContinuously()
        {
            vertices[0] += transformSpeed;
            vertices[1] += transformSpeed;
            vertices[3] -= transformSpeed;
            vertices[4] += transformSpeed;
            vertices[7] -= transformSpeed;
        }

        private static void TriangleMoveDownContinuously()
        {
            vertices[1] -= transformSpeed;
            vertices[4] -= transformSpeed;
            vertices[7] -= transformSpeed;
        }

        private static void TriangleMoveToRightContinuously()
        {
            vertices[0] += transformSpeed;
            vertices[3] += transformSpeed;
            vertices[6] += transformSpeed;
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
            glShaderSource(vertexShader, File.ReadAllText("shaders/red-triangle.vert"));
            glCompileShader(vertexShader);

            //create fragment shader
            var fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fragmentShader, File.ReadAllText("shaders/red-triangle.frag"));
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
            Glfw.WindowHint(Hint.Doublebuffer, Constants.False);

            // create and launch a window
            var window = Glfw.CreateWindow(800, 600, "MyWindow", Monitor.None, Window.None);
            Glfw.MakeContextCurrent(window);
            Import(Glfw.GetProcAddress);
            return window;
        }
    }
}
