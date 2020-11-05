using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Tree
    {
        Game game;
        

        Matrix world;
        Texture2D texture, texture2;
        VertexPositionTexture[] vertex;
        VertexBuffer buffer;
        short[] index;
        IndexBuffer iBuffer;
        Camera camera;
        Effect effect;
        Vector3 translation, rotation;
        float distance;
        BoundingBox collider;
        bool cai = true;

        public Tree(Game game, Camera camera)
        {

            this.game = game;
            this.camera = camera;



            this.world = Matrix.Identity;


            this.vertex = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-1,1,0), Vector2.Zero),
                new VertexPositionTexture(new Vector3( 1,1,0), Vector2.UnitX),
                new VertexPositionTexture(new Vector3(-1,-1,0), Vector2.UnitY),
                new VertexPositionTexture(new Vector3( 1,-1,0), Vector2.One),
            };
            this.buffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionTexture), this.vertex.Length, BufferUsage.None);
            this.buffer.SetData<VertexPositionTexture>(this.vertex);

            this.index = new short[]
            {
                0,1,2,
                1,3,2,
            };
            this.iBuffer = new IndexBuffer(game.GraphicsDevice, IndexElementSize.SixteenBits, this.index.Length, BufferUsage.None);
            this.iBuffer.SetData<short>(this.index);


            this.collider = new BoundingBox();
            this.UpdateBoundingBox();

        }

        public void SetTranslation(Vector3 translation)
        {
            this.translation = translation;
        }
        public void Movement(Camera camera)

        {
            this.rotation = camera.GetRotation();

            
            if (cai)
            {
              this.translation += new Vector3(0, -0.1f, 0);
            }
            this.world = Matrix.Identity;

            this.world *= Matrix.CreateRotationY(MathHelper.ToRadians(this.rotation.Y)) *
                          Matrix.CreateTranslation(this.translation);

            if (cai)
            {
                this.UpdateBoundingBox();
            }
        }

        public float GetDistance()
        {
            this.distance = Vector3.Distance(this.camera.GetPosition(), this.translation);

            return this.distance;
        }
        protected void UpdateBoundingBox()
        {
            this.collider.Min = this.translation - new Vector3(0.1f, 1f, 0.1f);
            this.collider.Max = this.translation + new Vector3(0.1f, 1f, 0.1f);


        }
        public BoundingBox GetBoundingBox()
        {
            return this.collider;
        }

        public void StopFalling()
        {
            cai = false;
        }

        public void Update()
        {
           
        }
        public void Draw()
        {
            texture = Game1.textures.GetTexture("tree");
            texture2 = Game1.textures.GetTexture("tree2");
            effect = Game1.textures.GetEffect("effect1");
            

            // TODO: Add your drawing code here
           

            game.GraphicsDevice.SetVertexBuffer(this.buffer);
            game.GraphicsDevice.Indices = this.iBuffer;
           
            effect.CurrentTechnique = effect.Techniques["Technique1"];
            effect.Parameters["World"].SetValue(this.world);
            effect.Parameters["View"].SetValue(camera.GetView());
            effect.Parameters["Projection"].SetValue(camera.GetProjection());
            effect.Parameters["colorTexture"].SetValue(this.texture);
            effect.Parameters["colorTexture2"].SetValue(this.texture2);
            effect.Parameters["time"].SetValue(Game1.fade);

            game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, this.vertex, 0, this.vertex.Length, this.index, 0, this.index.Length / 3);
            }


        }
    }
}

