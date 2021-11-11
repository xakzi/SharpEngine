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