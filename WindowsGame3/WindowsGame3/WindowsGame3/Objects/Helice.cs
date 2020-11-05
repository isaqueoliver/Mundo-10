using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    public class Helice 
    {
        protected GraphicsDevice device;
        protected Matrix world;
        
        VertexPositionTexture[] verts;
        VertexBuffer buffer;
        Effect effect;


      
        public Helice(GraphicsDevice device)
        {
            this.device = device;
            this.world = Matrix.Identity;

            this.verts = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3( 0, 0,0),new Vector2(0.3f,1)),  
                new VertexPositionTexture(new Vector3( -0.1f,-0.1f,0),new Vector2(0.2f,0.9f)),
                new VertexPositionTexture(new Vector3(0.1f,-0.1f,0),new Vector2(0.4f,0.9f)), 
                new VertexPositionTexture(new Vector3(-0.3f, -1.5f,0),new Vector2(0,0)),   
                new VertexPositionTexture(new Vector3( 0.3f,-1.5f,0),new Vector2(0.6f,0)), 
           
               
            };

            this.buffer = new VertexBuffer(this.device,
                                           typeof(VertexPositionTexture),
                                           this.verts.Length,
                                           BufferUsage.None);
            this.buffer.SetData<VertexPositionTexture>(this.verts);

            this.effect = new BasicEffect(this.device);
        }

        public virtual void Draw(Camera camera)
        {
            this.device.SetVertexBuffer(this.buffer);

            effect = Game1.textures.GetEffect("effect1");

            effect.CurrentTechnique = effect.Techniques["Technique1"];
            effect.Parameters["World"].SetValue(this.world);
            effect.Parameters["View"].SetValue(camera.GetView());
            effect.Parameters["Projection"].SetValue(camera.GetProjection());
            effect.Parameters["colorTexture"].SetValue(Game1.textures.GetTexture("wood"));
            effect.Parameters["colorTexture2"].SetValue(Game1.textures.GetTexture("wood2"));
            effect.Parameters["time"].SetValue(Game1.fade);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip,
                                                                    this.verts, 0, 3);
            }
        }
    }
}
