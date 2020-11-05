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
    public class Plano
    {
        #region Variable
        Matrix world;
        VertexPositionTexture[] verts;
        VertexBuffer vBuffer;
        short[] indices;
        IndexBuffer iBuffer;
        Camera camera;
        Effect effect;
        Texture2D texture, texHM, texture2;
        Vector3 position;
        List<BoundingBox> collider;
        //List<Cubo> cubos;
      
      

        public static int row, column;
        Game game;
        #endregion
        
        public Plano(Game game, Camera camera, Vector3 position)
        {
            this.position = position;
            this.game = game;
            this.camera = camera;
            row = 50;
            column = 50;

            this.world = Matrix.Identity;
            this.world *= Matrix.CreateTranslation(this.position);


            texHM = Game1.textures.GetTexture("heightmap");

            Color[] auxHM = new Color[texHM.Width * texHM.Height];
            texHM.GetData<Color>(auxHM);

            this.verts = new VertexPositionTexture[row * column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    float _j = j / (float)(column - 1);
                    float _i = i / (float)(row - 1);

                    int auxTex_j = (int)(_j * (texHM.Width - 1));
                    int auxTex_i = (int)(_i * (texHM.Height - 1));

                    


                    int _index = auxTex_i * texHM.Width + auxTex_j;

                    verts[i * column + j] =
                        new VertexPositionTexture(new Vector3(j - ((column - 1) / 2f), auxHM[_index].B / 40f, i - ((row - 1) / 2f)),
                                                  new Vector2(_j, _i));
                    
                }
            }
            this.collider = new List<BoundingBox>();
            //this.cubos = new List<Cubo>();
       
            int d = 0;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < column - 1; j++)
                {
                    Vector3[] vertice = new Vector3[8];
                    vertice[0] = this.verts[(i + 1) * column + (j + 1)].Position;
                    vertice[1] = this.verts[(i + 1) * column + j].Position;
                    vertice[2] = new Vector3(this.verts[(i + 1) * column + j].Position.X, this.verts[(i + 1) * column + j].Position.Y - 1, this.verts[(i + 1) * column + j].Position.Z);
                    vertice[3] = new Vector3(this.verts[(i + 1) * column + (j + 1)].Position.X, this.verts[(i + 1) * column + (j + 1)].Position.Y - 1, this.verts[(i + 1) * column + (j + 1)].Position.Z);
                    vertice[4] = this.verts[i * column + j].Position;
                    vertice[5] = this.verts[i * column + (j + 1)].Position;
                    vertice[6] = new Vector3(this.verts[i * column + (j + 1)].Position.X, this.verts[i * column + (j + 1)].Position.Y - 1, this.verts[i * column + (j + 1)].Position.Z);
                    vertice[7] = new Vector3(this.verts[i * column + j].Position.X, this.verts[i * column + j].Position.Y - 1, this.verts[i * column + j].Position.Z);

                    List<Vector3> boxVerts = new List<Vector3>();
                    boxVerts.Add(vertice[4]);
                    boxVerts.Add(vertice[5]);
                    boxVerts.Add(vertice[0]);
                    boxVerts.Add(vertice[1]);
                    boxVerts.Add(vertice[7]);
                    boxVerts.Add(vertice[6]);
                    boxVerts.Add(vertice[3]);
                    boxVerts.Add(vertice[2]);
                   
                    //this.cubos.Add(new Cubo(game.GraphicsDevice, vertice, this.position)) ;

                     
                    this.collider.Add  (BoundingBox.CreateFromPoints(boxVerts));

                    
                }
            }
          

          

            this.vBuffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
            this.vBuffer.SetData<VertexPositionTexture>(this.verts);

            this.indices = new short[(row - 1) * (column - 1) * 2 * 3];

            int k = 0;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < column - 1; j++)
                {
                    indices[k++] = (short)(i * column + j); // 0
                    indices[k++] = (short)(i * column + (j + 1)); // 1
                    indices[k++] = (short)((i + 1) * column + j); // 10

                    indices[k++] = (short)(i * column + (j + 1)); // 1
                    indices[k++] = (short)((i + 1) * column + (j + 1)); // 11
                    indices[k++] = (short)((i + 1) * column + j); // 10
                }
            }

            
            this.iBuffer = new IndexBuffer(game.GraphicsDevice, IndexElementSize.SixteenBits, indices.Length, BufferUsage.None);
            this.iBuffer.SetData<short>(this.indices);
            UpdateBoundingBox();
           
        }
      
        public BoundingBox GetBoundingBox(int x)
        {
            return collider[x];
           
        }
        public Vector3 GetColliderCenter(int a)
        {
            Vector3[] points = new Vector3[8];
            points = collider[a].GetCorners();
            float x = (points[0].X + points[6].X)/2;
            float y = (points[0].Y + points[6].Y)/2;
            float z = (points[0].Z + points[6].Z)/2;


            return new Vector3(x, y, z);
        }
       public void UpdateBoundingBox()
        {
            List<BoundingBox> newcol = new List<BoundingBox>();

            foreach(BoundingBox c in collider)
            {
                List<Vector3> updatec = new List<Vector3>();
                foreach(Vector3 v in c.GetCorners())
                { 
                    updatec.Add(v + this.position);
                }

                
                newcol.Add(BoundingBox.CreateFromPoints(updatec));
            }

            collider = newcol;
            newcol = new List<BoundingBox>();
            Console.WriteLine(newcol.Count());
            Console.WriteLine(collider.Count());
        }
           
        public int Length()
        {
            return this.collider.Count;
        }
        public void Update(GameTime gameTime)
        {
          
        }

        public void Draw()
        {
            this.texture = Game1.textures.GetTexture("grass");
            this.texture2 = Game1.textures.GetTexture("snow");

            this.effect = Game1.textures.GetEffect("effect1");
            this.game.GraphicsDevice.SetVertexBuffer(this.vBuffer);
            this.game.GraphicsDevice.Indices = this.iBuffer;

            //RasterizerState rs = new RasterizerState();
            //rs.FillMode = FillMode.WireFrame;
            //this.game.GraphicsDevice.RasterizerState = rs;
           
            effect.CurrentTechnique = effect.Techniques["Technique1"];
            effect.Parameters["World"].SetValue(this.world);
            effect.Parameters["View"].SetValue(camera.GetView());
            effect.Parameters["Projection"].SetValue(camera.GetProjection());
            effect.Parameters["colorTexture"].SetValue(this.texture);
            effect.Parameters["colorTexture2"].SetValue(this.texture2);
            effect.Parameters["time"].SetValue(Game1.fade);

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


            //foreach(Cubo c in cubos)
            //{
            //    c.Draw(this.camera);
            //}
           
        }
    }
}
