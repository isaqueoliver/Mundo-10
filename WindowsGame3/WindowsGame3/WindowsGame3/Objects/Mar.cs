using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    public class Mar
    {
        Game game;
        
        Matrix world;
        int row, column;
        VertexPositionTexture[] verts;
        VertexBuffer vBuffer;
        short[] indices;
        IndexBuffer iBuffer;
        Effect effect;
        Texture2D texture;
        float time, size;
        float speed = 3;
        Vector3 position;


        public Mar(Game game, Vector3 position, float size)
        {
            this.game = game;
            this.position = position;
            this.size = size;
            this.world = Matrix.Identity;

            this.world *= Matrix.CreateTranslation(this.position);
            this.world *= Matrix.CreateScale(size);

            this.row = 100;
            this.column = 100;

            this.verts = new VertexPositionTexture[this.row * this.column];

            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.column; j++)
                {
                    this.verts[i * this.column + j] = new VertexPositionTexture(new Vector3((j - this.column / 2f) * 10 ,0, (-i + this.row / 2f) *10),
                                                                                new Vector2(j / (float)(this.column - 1), i / (float)(this.row - 1)));
                }
            }

            this.vBuffer = new VertexBuffer(this.game.GraphicsDevice,
                                           typeof(VertexPositionTexture),
                                           this.verts.Length,
                                           BufferUsage.None);
            this.vBuffer.SetData<VertexPositionTexture>(this.verts);

            this.indices = new short[(this.row - 1) * (this.column - 1) * 2 * 3];

            int k = 0;
            for (int i = 0; i < this.row - 1; i++)
            {
                for (int j = 0; j < this.column - 1; j++)
                {
                    this.indices[k++] = (short)(i * this.column + j);      // v0
                    this.indices[k++] = (short)(i * this.column + (j + 1)); // v1
                    this.indices[k++] = (short)((i + 1) * this.column + j);      // v2

                    this.indices[k++] = (short)(i * this.column + (j + 1)); // v1
                    this.indices[k++] = (short)((i + 1) * this.column + (j + 1)); // v3
                    this.indices[k++] = (short)((i + 1) * this.column + j);      // v2
                }
            }

            this.iBuffer = new IndexBuffer(this.game.GraphicsDevice,
                                           IndexElementSize.SixteenBits,
                                           this.indices.Length,
                                           BufferUsage.None);
            this.iBuffer.SetData<short>(this.indices);

          

        

        }

        public void Update(GameTime gameTime)
        {
            this.time += gameTime.ElapsedGameTime.Milliseconds / 1000f * this.speed;
        }

        public virtual void Draw(Camera camera)
        {

            this.effect = Game1.textures.GetEffect("marEffect");
            this.texture = Game1.textures.GetTexture("mar");
            this.game.GraphicsDevice.SetVertexBuffer(this.vBuffer);

            this.effect.CurrentTechnique = this.effect.Techniques["Technique1"];
            this.effect.Parameters["World"].SetValue(this.world);
            this.effect.Parameters["View"].SetValue(camera.GetView());
            this.effect.Parameters["Projection"].SetValue(camera.GetProjection());
            this.effect.Parameters["colorTexture"].SetValue(this.texture);
            this.effect.Parameters["time"].SetValue(this.time);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                             this.verts,
                                                                             0,
                                                                             this.verts.Length,
                                                                             this.indices,
                                                                             0,
                                                                             this.indices.Length / 3);
            }
        }
    }
}