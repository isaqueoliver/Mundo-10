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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       public static Textures textures;
        Screen screen;
        Camera camera;
      
       TransformMoinho moinho, moinho2;

        Casa casa;

        Plano chao;

        TreeList treeList;

        Mar mar;

        float time = 0;
        public static float fade;
        public Game1()
        {
           
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            this.screen = Screen.GetInstance();
            this.screen.SetWidth(graphics.PreferredBackBufferWidth);
            this.screen.SetHeight(graphics.PreferredBackBufferHeight);
            textures = new Textures(Content);
            textures.LoadTexture();
            textures.LoadEffect();
            this.camera = new Camera();
           
            this.moinho = new TransformMoinho(GraphicsDevice, 100, new Vector3(-3,0,-3),45);

            this.moinho2 = new TransformMoinho(GraphicsDevice, -50, new Vector3(3, 0, -3), -45);

            this.casa = new Casa(GraphicsDevice, new Vector3(0, 0, 0));
            

            this.chao = new Plano(this, this.camera, new Vector3(0, -10, 0));

            this.mar = new Mar(this, new Vector3(0, -40, -20), 0.2f);

            this.treeList = new TreeList(this, this.camera, new Vector3(-8, 1, -23), 5, 9);


            base.Initialize();

          

           
        }

        public void Collision()
        {
            #region Chao x Coisas

            for (int i = 0; i < chao.Length(); i++)
            {
                if (this.casa.GetBoundingBox().Intersects(this.chao.GetBoundingBox(i)))
                {
                    this.casa.StopFalling();
                }

                if (this.moinho.GetBoundingBox().Intersects(this.chao.GetBoundingBox(i)))
                {
                    this.moinho.StopFalling();
                }

                if (this.moinho2.GetBoundingBox().Intersects(this.chao.GetBoundingBox(i)))
                {
                    this.moinho2.StopFalling();
                }

                if (this.camera.GetBoundingBox().Intersects(this.chao.GetBoundingBox(i)))
                {
                    if(this.camera.GetPosition().Y - (Math.Abs(this.camera.GetSize().Y )/1.5f ) > this.chao.GetColliderCenter(i).Y)
                    {
                      this.camera.RestorePositionY();
                    }
                    else
                    {
                        this.camera.RestorePosition();
                    }
                   
                }

                for (int j =0; j < this.treeList.Length(); j++)
                {
                    if (this.treeList.GetBoundingBox(j).Intersects(this.chao.GetBoundingBox(i)))
                    {
                        this.treeList.StopFalling(j);
                    }
                }
            }
            #endregion

            #region Camera X Coisas

            if (this.camera.GetBoundingBox().Intersects(this.casa.GetBoundingBox()))
            {
                if (this.camera.GetPosition().Y - (Math.Abs(this.camera.GetSize().Y) / 1.5f) > this.casa.GetPosition().Y)
                {
                    this.camera.RestorePositionY();
                }
                else
                {
                    this.camera.RestorePosition();
                }

            }

            if (this.camera.GetBoundingBox().Intersects(this.moinho.GetBoundingBox()))
            {
                if (this.camera.GetPosition().Y - (Math.Abs(this.camera.GetSize().Y) / 1.5f) > this.moinho.GetPosition().Y)
                {
                    this.camera.RestorePositionY();
                }
                else
                {
                    this.camera.RestorePosition();
                }
            }

            if (this.camera.GetBoundingBox().Intersects(this.moinho2.GetBoundingBox()))
            {
                if (this.camera.GetPosition().Y - (Math.Abs(this.camera.GetSize().Y) / 1.5f) > this.moinho2.GetPosition().Y)
                {
                    this.camera.RestorePositionY();
                }
                else
                {
                    this.camera.RestorePosition();
                }
            }

            for(int i = 0; i < treeList.Length(); i++)
            {
                if (this.camera.GetBoundingBox().Intersects(this.treeList.GetBoundingBox(i)))
                {
                    if (this.camera.GetPosition().Y - (Math.Abs(this.camera.GetSize().Y) / 1.5f) > this.treeList.GetPosition(i).Y)
                    {
                        this.camera.RestorePositionY();
                    }
                    else
                    {
                        this.camera.RestorePosition();
                    }
                }
            }
           #endregion
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
           
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            this.moinho.Update(gameTime);
            this.moinho2.Update(gameTime);
            camera.Update(gameTime);
            this.mar.Update(gameTime);
            base.Update(gameTime);
            this.treeList.Update();
            this.casa.Update();
          
            this.Collision();

            time += gameTime.ElapsedGameTime.Milliseconds *0.0002f;
            fade = ((float)Math.Sin(time) * 0.5f) + 0.5f;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Lerp(Color.CornflowerBlue, Color.WhiteSmoke, fade));

            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

           
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            //rs.FillMode = FillMode.Solid;
            GraphicsDevice.RasterizerState = rs;
            this.chao.Draw();

            

          
            this.casa.Draw(this.camera);
            
            this.moinho.Draw(this.camera);
            this.moinho2.Draw(this.camera);
            this.mar.Draw(this.camera);
            
           
            this.treeList.Draw();
           

            base.Draw(gameTime);
            
        }
    }
}
