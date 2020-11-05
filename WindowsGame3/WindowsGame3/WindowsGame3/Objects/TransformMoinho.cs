using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    public class TransformMoinho : Moinho
    {
        float s;
        Vector3 position;
        float angleY;
        TransformHelice h1, h2, h3, h4;
        bool cai = true;
        BoundingBox collider;
        public TransformMoinho(GraphicsDevice device, float speed, Vector3 position, float angleY) :
            base(device)
        {

            this.s = speed;
            this.position = position;
            this.angleY = angleY;
            
            this.h1 = new TransformHelice(device, 0, s);
            this.h2 = new TransformHelice(device, 67.5f, s);
            this.h3 = new TransformHelice(device, 202.5f, s);
            this.h4 = new TransformHelice(device, 135, s);

            this.collider = new BoundingBox();
            this.UpdateBoundingBox();
        }

        public void Update(GameTime gameTime)
        {
            this.h1.SetWorld(this.world);
            this.h2.SetWorld(this.world);
            this.h3.SetWorld(this.world);
            this.h4.SetWorld(this.world);

            this.h1.Update(gameTime);
            this.h2.Update(gameTime);
            this.h3.Update(gameTime);
            this.h4.Update(gameTime);

            
            this.world = Matrix.Identity;
          
          
            this.world *= Matrix.CreateRotationY(angleY)* Matrix.CreateTranslation(this.position) ;
            this.world *= Matrix.CreateTranslation(new Vector3(0, 0.51f, 0));


            if (cai)
            {
                this.position += new Vector3(0, -0.1f, 0);
                this.world = Matrix.Identity;
                this.world *= Matrix.CreateRotationY(angleY) * Matrix.CreateTranslation(this.position);
               
                this.UpdateBoundingBox();
            }
            
        }

        protected void UpdateBoundingBox()
        {
            this.collider.Min = this.position - new Vector3(0.7f, 0, 0.7f);
            this.collider.Max = this.position + new Vector3(0.7f, 1.4f, 0.7f);


        }

        public Vector3 GetPosition()
        {

            Vector3 center = (this.collider.Min + this.collider.Max)/2;

            return center;
        }
        public BoundingBox GetBoundingBox()
        {
            return this.collider;
        }

        public void StopFalling()
        {
            cai = false;
        }

     
        public override void Draw(Camera camera)
        {
            


            base.Draw(camera);
            this.h1.Draw(camera);
            this.h2.Draw(camera);
            this.h3.Draw(camera);
            this.h4.Draw(camera);
        }
    }
}
