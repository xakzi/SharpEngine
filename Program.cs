using System;
using GLFW;
using static OpenGL.Gl;
using System.IO;

namespace SharpEngine
{

    public struct Vertex
    {
        public Vector position;

        public Vertex(Vector position)
        {
            this.position = position;
        }
    }
    class Program
    {

        static Vertex[] vertices = new Vertex[] {
            new Vertex(new Vector(-.1f, -.1f)),
            new Vertex(new Vector(.1f, -.1f)),
            new Vertex(new Vector(0f, .1f)),

            new Vertex(new Vector(.4f, .2f)),
            new Vertex(new Vector(.6f, .2f)),
            new Vertex(new Vector(.5f, .4f))
            };
        
        static float transformSpeed = 0.005f;
        static bool touchWall = false;
        static bool touchWall2 = false;
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
            for (var i = 3; i < vertices.Length; i++)
            {
                vertices[i].position += direction;
            }
            // 4. Check the X-Bounds of the Screen
            for (var i = 3; i < vertices.Length; i++)
            {
                if (vertices[i].position.x >= 1 && direction.x > 0 || vertices[i].position.x <= -1 && direction.x < 0)
                {
                    direction.x *= -1;
                    break;
                }
            }
            // 5. Check the Y-Bounds of the Screen
            for (var i = 3; i < vertices.Length; i++)
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
            var min = vertices[3].position;
            for (var i = 3; i < vertices.Length; i++)
            {
                min = Vector.Min(min, vertices[i].position);
            }

            var max = vertices[5].position;
            for (var i = 3; i < vertices.Length; i++)
            {
                max = Vector.Max(max, vertices[i].position);
            }

            var center = (min + max) / 2;

            // - moving it by the opposite vector
            for (var i = 3; i < vertices.Length; i++)
            {
                vertices[i].position -= center;
            }
            //scale the triangle
            for (var i = 3; i < vertices.Length; i++)
            {
                vertices[i].position *= multiplier;
            }

            //move it back to where it was
            for (var i = 3; i < vertices.Length; i++)
            {
                vertices[i].position += center;
            }

            scale *= multiplier;

            if (scale <= 0.5f)
            {
                multiplier = 1.002f;
            }
            if (scale >= 1f)
            {
                multiplier = 0.998f;
            }
        }

       /* private static void TriangleMoveLeftContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].position -= new Vector(transformSpeed, 0f);
        }
        private static void TriangleMoveLeftContinously2()
        {
            for (var i = 3; i < vertices.Length; i++)
                vertices[i].position -= new Vector(transformSpeed, 0f);
        }

        private static void TriangleMoveRightContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].position += new Vector(transformSpeed, 0f);
        }
        private static void TriangleMoveRightContinously2()
        {
            for (var i = 3; i < vertices.Length; i++)
                vertices[i].position.x += transformSpeed;
        }

        private static void TriangleMoveDownContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].position -= new Vector(0f, transformSpeed);
        }

        private static void TriangleMoveDownContinously2()
        {
            for (var i = 3; i < vertices.Length; i++)
                vertices[i].position -= new Vector(0f, transformSpeed);
        }

        private static void TriangleMoveUpContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].position += new Vector(0f, transformSpeed);
        }
        private static void TriangleMoveUpContinously2()
        {
            for (var i = 3; i < vertices.Length; i++)
                vertices[i].position += new Vector(0f, transformSpeed);
        }
        private static void TriangleRotateOnSpotTriangle1()
        {
            float centerX = (vertices[0].position.x + vertices[1].position.x + vertices[2].position.x) / 3;
            float centerY = (vertices[0].position.y + vertices[1].position.y + vertices[2].position.y) / 3;
            for (var i = 0; i < vertices.Length/2; i++)
            {
                //vertices[i].x = (float)(vertices[i].x * Math.Cos(transformSpeed) + vertices[i].y * Math.Sin(transformSpeed));
                //vertices[i].y = (float)(vertices[i].y * Math.Cos(transformSpeed) - vertices[i].x * Math.Sin(transformSpeed));
                vertices[i].position.x = (float)(Math.Cos(transformSpeed) * (vertices[i].position.x - centerX) + Math.Sin(transformSpeed) * (vertices[i].position.y - centerY) + centerX);
                vertices[i].position.y = (float)(Math.Cos(transformSpeed) * (vertices[i].position.y - centerY) - Math.Sin(transformSpeed) * (vertices[i].position.x - centerX) + centerY);

            }
        }
        private static void TriangleRotateOnSpotTriangle2()
        {
            float centerX = (vertices[3].position.x + vertices[4].position.x + vertices[5].position.x) / 3;
            float centerY = (vertices[3].position.y + vertices[4].position.y + vertices[5].position.y) / 3;
            for (var i = 3; i < vertices.Length; i++)
            {
                //vertices[i].x = (float)(vertices[i].x * Math.Cos(transformSpeed) + vertices[i].y * Math.Sin(transformSpeed));
                //vertices[i].y = (float)(vertices[i].y * Math.Cos(transformSpeed) - vertices[i].x * Math.Sin(transformSpeed));
                vertices[i].position.x = (float)(Math.Cos(transformSpeed) * (vertices[i].position.x - centerX) + Math.Sin(transformSpeed) * (vertices[i].position.y - centerY) + centerX);
                vertices[i].position.y = (float)(Math.Cos(transformSpeed) * (vertices[i].position.y - centerY) - Math.Sin(transformSpeed) * (vertices[i].position.x - centerX) + centerY);

            }
        }


        private static void TriangleBouncesUpAndDownContinously()
        {
            if (touchWall == false)
            {
                TriangleMoveUpContinously();
                for (var i = 0; i < vertices.Length / 2; i++)
                    if (vertices[i].position.y >= 1f)
                    touchWall = true;
            }
            else
            {
                TriangleMoveDownContinously();
                for (var i = 0; i < vertices.Length / 2; i++)
                    if (vertices[i].position.y <= -1f)
                    touchWall = false;
            }
        }

        private static void TriangleBouncesUpAndDownContinously2()
        {
            if (touchWall2 == false)
            {
                TriangleMoveUpContinously2();
                for (var i = 3; i < vertices.Length; i++)
                    if (vertices[i].position.y >= 1f)
                        touchWall2 = true;
            }
            else
            {
                TriangleMoveDownContinously2();
                for (var i = 3; i < vertices.Length; i++)
                    if (vertices[i].position.y <= -1f)
                        touchWall2 = false;
            }
        }

        private static void TriangleBouncesRightAndLeftContinously()
        {
            if (touchWall == false)
            {
                TriangleMoveRightContinously();
                for (var i = 0; i < vertices.Length / 2; i++)
                    if (vertices[i].position.x >= 1f)
                    touchWall = true;
            }
            else
            {
                TriangleMoveLeftContinously();
                for (var i = 0; i < vertices.Length / 2; i++)
                    if (vertices[i].position.x <= -1f)
                    touchWall = false;
            }
        }

        private static void TriangleBouncesRightAndLeftContinously2()
        {
            if (touchWall2 == false)
            {
                TriangleMoveRightContinously2();
                for (var i = 3; i < vertices.Length; i++)
                    if (vertices[i].position.x >= 1f)
                        touchWall2 = true;
            }
            else
            {
                TriangleMoveLeftContinously2();
                for (var i = 3; i < vertices.Length; i++)
                    if (vertices[i].position.x <= -1f)
                        touchWall2 = false;
            }
        }

        private static void TriangleMoveToRightContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
            {
                vertices[i].position += new Vector(transformSpeed, 0f);
            }
        }

        private static void TriangleScaleUpContinously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
            {
                vertices[i].position *= 1.005f;
            }
        }

        private static void TriangleShrinkContinuously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].position /= 1.0001f;
        }
        private static void TriangleShrinkContinuously2()
        {
            for (var i = 3; i < vertices.Length; i++)
                vertices[i].position /= 1.0001f;
        }

        private static void TriangleMoveDownLeftContinuously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
                vertices[i].position -= new Vector(transformSpeed, transformSpeed);
        }
        private static void TriangleMoveDownLeftContinuously2()
        {
            for (var i = 3; i < vertices.Length; i++)
                vertices[i].position -= new Vector(transformSpeed, transformSpeed);
        }

        private static void TriangleMoveToRightUpContinuously()
        {
            for (var i = 0; i < vertices.Length / 2; i++)
            {
                vertices[i].position += new Vector(transformSpeed, transformSpeed);
            }
        }
        private static void TriangleMoveToRightUpContinuously2()
        {
            for (var i = 3; i < vertices.Length; i++)
            {
                vertices[i].position += new Vector(transformSpeed, transformSpeed);
            }
        }*/

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
            fixed (Vector* vertex = &vertices[0].position)
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
