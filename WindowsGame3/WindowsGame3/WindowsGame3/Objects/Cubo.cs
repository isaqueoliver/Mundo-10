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
using WindowsGame3;

namespace WindowsGame3
{
    class Cubo
    {
        #region variables
        GraphicsDevice graphicsDevice;

        VertexPositionColor[] verts;
        VertexBuffer vertexBuffer;

        short[] index;
        IndexBuffer indexBuffer;

        protected Matrix world;

        Vector3 position;

        BasicEffect effect;


        #endregion

        public Cubo(GraphicsDevice graphics, Vector3[] vertice,  Vector3 position)
        {
            this.graphicsDevice = graphics;
            this.position = position;
            this.verts = new VertexPositionColor[]
            {
                new VertexPositionColor(vertice[0], Color.Red), // 0
                new VertexPositionColor(vertice[1], Color.Blue), // 1
                new VertexPositionColor(vertice[2], Color.Green), // 2
                new VertexPositionColor(vertice[3], Color.Yellow), // 3
                new VertexPositionColor(vertice[4], Color.Red), // 4
                new VertexPositionColor(vertice[5], Color.Blue), // 5
                new VertexPositionColor(vertice[6], Color.Green), // 6
                new VertexPositionColor(vertice[7], Color.Yellow), // 7
            };

            this.vertexBuffer = new VertexBuffer(this.graphicsDevice,
                                                 typeof(VertexPositionColor),
                                                 this.verts.Length,
                                                 BufferUsage.None);
            this.vertexBuffer.SetData<VertexPositionColor>(this.verts);

            this.index = new short[]
            {
                0, 1, 2, // front
                0, 2, 3,
                1, 5, 6, // rigth
                1, 6, 2,
                5, 4, 7, // back
                5, 7, 6,
                4, 0, 3, // left
                4, 3, 7,
                4, 5, 1, // up
                4, 1, 0,
                3, 2, 6, // down
                3, 6, 7,
            };

            this.indexBuffer = new IndexBuffer(this.graphicsDevice,
                                               IndexElementSize.SixteenBits,
                                               this.index.Length,
                                               BufferUsage.None);
            this.indexBuffer.SetData<short>(this.index);

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateTranslation(this.position);

            this.effect = new BasicEffect(this.graphicsDevice);


            




        }

       
        public virtual void Draw(Camera camera)
        {
            this.graphicsDevice.SetVertexBuffer(this.vertexBuffer);
            this.graphicsDevice.Indices = this.indexBuffer;

            RasterizerState rs = new RasterizerState();

           
            
                rs.CullMode = CullMode.None;
                rs.FillMode = FillMode.WireFrame;
            
           

            this.graphicsDevice.RasterizerState = rs;

            effect.World = this.world;
            effect.View =camera.GetView();
            effect.Projection = camera.GetProjection();
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,
                    this.verts, 0, this.verts.Length, this.index, 0, this.index.Length / 3);
            }

           


           
        }

     

      
       

      
    }
}
