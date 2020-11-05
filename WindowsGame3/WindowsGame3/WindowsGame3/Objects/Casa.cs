using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
 public class Casa
    { 

    protected GraphicsDevice device;
    protected Matrix world;
    VertexPositionTexture[] verts;
    VertexBuffer buffer;
    Effect effect;
    BoundingBox collider;
    Vector3 position;
    bool cai = true;
   
    public Casa(GraphicsDevice device, Vector3 position)
    {
        this.device = device;
        this.world = Matrix.Identity;
            this.position = position;

        this.verts = new VertexPositionTexture[]
        {
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0.5f),  new Vector2(0.25f,0.25f)),
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector2(0.25f,0.75f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector2(0.75f,0.25f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, 0.5f), new Vector2(0.75f,0.75f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector2(0.25f,0.75f)),
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0.5f), new Vector2(0.75f,0.25f)),
             new VertexPositionTexture(new Vector3(-0.5f, 0.5f, -0.5f), new Vector2(0.25f,0.25f)),
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0.5f), new Vector2(0.75f,0.75f)),
           new VertexPositionTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector2(0.25f,0.75f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, 0.5f), new Vector2(0.75f,0.25f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, -0.5f), new Vector2(0.25f,0.25f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, -0.5f), new Vector2(0.25f,0.75f)),
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, -0.5f), new Vector2(0.75f,0.25f)),
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, -0.5f),new Vector2(0.75f,0.75f)),
        };



            this.world *= Matrix.CreateTranslation(this.position);


        this.buffer = new VertexBuffer(this.device,
                                       typeof(VertexPositionTexture),
                                       this.verts.Length,
                                       BufferUsage.None);

        this.buffer.SetData<VertexPositionTexture>(this.verts);

        this.effect = new BasicEffect(this.device);

        this.collider = new BoundingBox();
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

        public void StopFalling()
        {
            cai = false;
        }

       public Vector3 GetPosition()
        {
            Vector3 center = (this.collider.Max + this.collider.Min) / 2;
            return center;
        }
        public void Update()
        {
            if (cai)
            {
                this.position += new Vector3(0, -0.1f, 0);
                this.world = Matrix.Identity;
                this.world *= Matrix.CreateTranslation(this.position);
                this.UpdateBoundingBox();
            }
            
        }
        public virtual void Draw(Camera camera)
    {
        this.device.SetVertexBuffer(this.buffer);

        this.effect = Game1.textures.GetEffect("effect1");


            effect.CurrentTechnique = effect.Techniques["Technique1"];
            effect.Parameters["World"].SetValue(this.world);
            effect.Parameters["View"].SetValue(camera.GetView());
            effect.Parameters["Projection"].SetValue(camera.GetProjection());
            effect.Parameters["colorTexture"].SetValue(Game1.textures.GetTexture("brick"));
            effect.Parameters["colorTexture2"].SetValue(Game1.textures.GetTexture("brick2"));
            effect.Parameters["time"].SetValue(Game1.fade);




            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
        {
            pass.Apply();

            this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip,
                                                                this.verts, 0, 12);
        }
    }
}
}
