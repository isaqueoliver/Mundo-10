using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    public class Camera
    {
        private Matrix view;
        private Matrix projection;

        private Vector3 position;
        private Vector3 target;
        private Vector3 up;

        BoundingBox collider;

        Vector3 oldPosistion;

      

        float speed = 10;

        float angleY = 0;
        float angleX = 0;
        float speedRotation = 100;


        public Camera()
        {
            this.position = new Vector3(0,25,0);
            this.oldPosistion = this.position;
            this.target = Vector3.Zero;
            this.up = Vector3.Up;
            this.SetupView(this.position, this.target, this.up);

            this.SetupProjection();
        }

        public void SetupView(Vector3 position, Vector3 target, Vector3 up)
        {
            this.view = Matrix.CreateLookAt(position, target, up);
        }

        public void SetupProjection()
        {
            Screen screen = Screen.GetInstance();

            this.projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                  screen.GetWidth() / (float)screen.GetHeight(),
                                                                  0.0001f,
                                                                  1000);
        }

       
        public Matrix GetView()
        {
            return this.view;
        }

        public Matrix GetProjection()
        {
            return this.projection;
        }

        public Vector3 GetRotation()
        {
            return new Vector3(angleX, angleY, 0);
        }

        public Vector3 GetSize()
        {
            Vector3 size = this.collider.Max - this.collider.Min;
            return size;
        }
        public Vector3 GetPosition()
        {
            return this.position;
        }
        public void Update(GameTime gameTime)
        {
           

            this.oldPosistion = this.position;
            this.Rotation(gameTime);
            this.Translation(gameTime);
           
            this.position += new Vector3(0, -0.1f, 0);
              
            
            this.view = Matrix.Identity;
            this.view *= Matrix.CreateRotationX(MathHelper.ToRadians(this.angleX));
            this.view *= Matrix.CreateRotationY(MathHelper.ToRadians(this.angleY));
           
            this.view *= Matrix.CreateTranslation(this.position);
            this.view = Matrix.Invert(this.view);

           
            
            this.UpdateBoundingBox();
           
        }

        protected void UpdateBoundingBox()
        {
            this.collider.Min = this.position - new Vector3(0.5f, 0.5f, 0.5f);
            this.collider.Max = this.position + new Vector3(0.5f, 0.5f, 0.5f);


        }
        public BoundingBox GetBoundingBox()
        {
            return this.collider;
        }

        public void RestorePositionY()
        {
            this.position = new Vector3(this.position.X, this.oldPosistion.Y, this.position.Z);
        }
        public void RestorePosition()
        {
            this.position = this.oldPosistion;
        }
        private void Rotation(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.angleY += this.speedRotation * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.angleY -= this.speedRotation * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.angleX += this.speedRotation * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.angleX -= this.speedRotation * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }
        }

        
        private void Translation(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.position.X -= (float)Math.Sin(MathHelper.ToRadians(this.angleY)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                this.position.Z -= (float)Math.Cos(MathHelper.ToRadians(this.angleY)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                //this.position.Y += (float)Math.Sin(MathHelper.ToRadians(this.angleX)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                //  this.position.Y -= (float)Math.Sin(MathHelper.ToRadians(this.angleX)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY + 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY + 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.position.X += (float)Math.Sin(MathHelper.ToRadians(this.angleY - 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                this.position.Z += (float)Math.Cos(MathHelper.ToRadians(this.angleY - 90)) * gameTime.ElapsedGameTime.Milliseconds * 0.001f * this.speed;
                
            }
        }
    }
}