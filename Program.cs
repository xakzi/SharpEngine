using System;
using GLFW;
using static OpenGL.Gl;
using System.IO;

namespace SharpEngine
{
    struct Vector
    {
        public float x, y, z;

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
        }

        // *
        public static Vector operator *(Vector v, float f)
        {
            return new Vector(v.x * f, v.y * f, v.z * f);
        }

        // +
        public static Vector operator +(Vector v, float f)
        {
            return new Vector(v.x + f, v.y + f, v.z + f);
        }
        // -
        public static Vector operator -(Vector v, float f)
        {
            return new Vector(v.x - f, v.y - f, v.z - f);
        }
        // /
        public static Vector operator /(Vector v, float f)
        {
            return new Vector(v.x / f, v.y / f, v.z / f);
        }
    }
    class Program
    {
        static Vector[] vertices = new Vector[] {
            new Vector(-.1f, -.1f),
            new Vector(.1f, -.1f),
            new Vector(0f, .1f),

            new Vector(.4f, .4f),
            new Vector(.6f, .4f),
            new Vector(.5f, .6f)
            };
        
        static float transformSpeed = 0.005f;
        static bool touchWall = false;
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

                //Bounces
                //TriangleBouncesRightAndLeftContinously();
                //TriangleBouncesUpAndDownContinously();
                //TriangleBouncesRightUpAndLeftDown();

                //Diagonal Lines
                //TriangleMoveDownLeftContinuously();
                //TriangleMoveToRightUpContinuously();

                //Stright Lines
                //TriangleMoveUpContinously();
                //TriangleMoveDownContinously();
                //TriangleMoveRightContinously();
                //TriangleMoveLeftContinously();

                //Shape
                //TriangleShrinkContinuously();
                //TriangleScaleUpContinously();

                //Rotation -- Not working properly yet.
                //TriangleRotateOnSpot();

                UpdateTriangleBuffer();
            }
        }

        private static void TriangleMoveLeftContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].x -= transformSpeed;
        }

        private static void TriangleMoveRightContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].x += transformSpeed;
        }

        private static void TriangleMoveDownContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].y -= transformSpeed;
        }

        private static void TriangleMoveUpContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].y += transformSpeed;
        }
        private static void TriangleRotateOnSpot()
        {
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i].x = (float)(vertices[i].x * Math.Cos(transformSpeed) + vertices[i].y * Math.Sin(transformSpeed));
                vertices[i].y = (float)(vertices[i].y * Math.Cos(transformSpeed) - vertices[i].x * Math.Sin(transformSpeed));  
            }
        }

        private static void TriangleBouncesUpAndDownContinously()
        {
            if (touchWall == false)
            {
                TriangleMoveUpContinously();
                if (vertices[2].y >= 1f)
                    touchWall = true;
            }
            else
            {
                TriangleMoveDownContinously();
                if (vertices[0].y <= -1f)
                    touchWall = false;
            }
        }

        private static void TriangleBouncesRightAndLeftContinously()
        {
            if (touchWall == false)
            {
                TriangleMoveRightContinously();
                if (vertices[1].x >= 1f)
                    touchWall = true;
            }
            else
            {
                TriangleMoveLeftContinously();
                if (vertices[0].x <= -1f)
                    touchWall = false;
            }
        }

        private static void TriangleMoveToRightContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
            {
                vertices[i].x += transformSpeed;
            }
        }

        private static void TriangleBouncesRightUpAndLeftDown()
        {
            if (touchWall == false)
            {
                TriangleMoveToRightUpContinuously();
                if (vertices[1].x >= 1f)
                    touchWall = true;
            }
            else
            {
                TriangleMoveDownLeftContinuously();
                if (vertices[0].x <= -1f)
                    touchWall = false;
            }
        }

        private static void Render(Window window)
        {
            //glDrawArrays(GL_LINE_LOOP, 0, 3); //Lined Triangle
            glDrawArrays(GL_TRIANGLES, 0, vertices.Length); //Filled Triangle
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
            for (var i = 0; i < vertices.Length/2; i++)
            {
                vertices[i] *= 1.005f;
            }
        }

        private static void TriangleShrinkContinuously()
        {
            for (var i = 0; i < vertices.Length/2; i++)
            {
                vertices[i] /= 1.001f;
            }
        }

        private static void TriangleMoveDownLeftContinuously()
        {
            for (var i = 0; i < vertices.Length/2; i++)
            {
                vertices[i] -= transformSpeed;
            }
        }

        private static void TriangleMoveToRightUpContinuously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
            {
                vertices[i] += transformSpeed;
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

            glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vector), NULL);

            glEnableVertexAttribArray(0);
        }

        static unsafe void UpdateTriangleBuffer()
        {
            fixed (Vector* vertex = &vertices[0])
            {
                glBufferData(GL_ARRAY_BUFFER, sizeof(Vector) * vertices.Length, vertex, GL_STATIC_DRAW);
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
