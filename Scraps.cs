/*var walkDirection = new Vector();
                    walkDirection = WalkCalculations(window, fixedDeltaTime, triangle, walkDirection);

                    walkDirection = walkDirection.Normalize();
                    triangle.Transform.Position += walkDirection * (movementSpeed * fixedDeltaTime);


                    CircleChangeColor(triangle, circle);

                    RectangleChangeColor(triangle, rectangle);*/



/*private static Vector WalkCalculations(Window window, float fixedDeltaTime, Triangle triangle, Vector walkDirection)
        {
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

            return walkDirection;
        }

        private static void CircleChangeColor(Triangle triangle, Circle circle)
        {
            var angle = MathF.Acos(Vector.Dot((circle.GetCenter() - triangle.GetCenter()).Normalize(), triangle.Transform.Forward));
            float interpolate = angle / MathF.PI;
            circle.SetColor(new Color(interpolate, interpolate, interpolate, 1));
        }

        private static void RectangleChangeColor(Triangle triangle, Rectangle rectangle)
        {
            float direction = Vector.Dot((rectangle.GetCenter() - triangle.GetCenter()).Normalize(), triangle.Transform.Forward);
            
            if (direction < 0) // change color on triangles left and right side
            //if (Vector.Dot(rectangle.Transform.Forward, triangle.Transform.Forward) < 0) //change color on triangles right side
            {
                rectangle.SetColor(Color.Green);
            }
            else
            {
                rectangle.SetColor(Color.Red);
            }
        }*/

/*var circle = new Circle(material);
            circle.Transform.Position = Vector.Left;
            circle.velocity = Vector.Right * 0.5f;
            scene.Add(circle);

            var circle2 = new Circle(material);
            circle2.Transform.Position = Vector.Right;
            circle2.velocity = Vector.Left * 0.5f;
            scene.Add(circle2);

            var square = new Rectangle(material);
            square.Transform.Position = Vector.Left + Vector.Backward * 0.2f;
            square.linearForce = Vector.Right * 0.3f;
            square.Mass = 2f;
            scene.Add(square);

            var ground = new Rectangle(material);
            ground.Transform.CurrentScale = new Vector(20f, 1f, 1f);
            ground.Transform.Position = new Vector(0f, -1f);
            ground.gravityScale = 0f;
            scene.Add(ground);*/