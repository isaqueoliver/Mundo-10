using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    public class TransformHelice : Helice
    {
        private float angle;
        private float speed;
        private float positionZ;
        Matrix w;


        public void SetWorld(Matrix w)
        {
            this.w = w;
        }
        public TransformHelice(GraphicsDevice device, float positionZ, float speed) :
            base(device)
        {

            this.angle = 0;
            this.speed = speed;
            this.positionZ = positionZ;
            this.w = world;

        }

        public void Update(GameTime gameTime)
        {
            this.angle += this.speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;

        

            this.world = Matrix.Identity;
          this.world *= Matrix.CreateScale(0.5f, 0.5f, 0.5f);
            this.world *= Matrix.CreateTranslation(new Vector3(0, 0f, 0.65f));
            this.world *= Matrix.CreateRotationZ(MathHelper.ToRadians(this.angle) + positionZ) * w;
             this.world *= Matrix.CreateTranslation(new Vector3(0, 0.7f, 0));
           
            




        }

        public override void Draw(Camera camera)
        {
           
            
            

            base.Draw(camera);
        }
    }
}
