using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    public class Textures
    {
        ContentManager content;
        public static Texture2D textGrass, textWood, textBrick, textMoinho, textTree, textMar, heightmap, textSnow, textWood2, textBrick2, textMoinho2, textTree2;
        public static Effect effect1, effect2;

        public Textures(ContentManager content)
        {
            this.content = content;
        }

        public void LoadTexture()
        {
            textGrass = content.Load<Texture2D>(@"Textures\grass");
            textWood = content.Load<Texture2D>(@"Textures\wood");
            textBrick = content.Load<Texture2D>(@"Textures\brick");
            textMoinho = content.Load<Texture2D>(@"Textures\moinho");
            textTree= content.Load<Texture2D>(@"Textures\tree");
            textSnow = content.Load<Texture2D>(@"Textures\snow");
            textWood2 = content.Load<Texture2D>(@"Textures\wood2");
            textBrick2 = content.Load<Texture2D>(@"Textures\brick2");
            textMoinho2 = content.Load<Texture2D>(@"Textures\moinho2");
            textTree2 = content.Load<Texture2D>(@"Textures\tree2");
            heightmap = content.Load<Texture2D>(@"Textures\mountain");
            textMar = content.Load<Texture2D>(@"Textures\mar");
        }
        public void LoadEffect()
        {
            effect1 = content.Load<Effect>(@"Effects\Effect1");
            effect2 = content.Load<Effect>(@"Effects\MarEffect");
        }
        public Texture2D GetTexture(string textureName)
        {
            switch (textureName)
            {
                case "grass":
                    return textGrass;

                case "mar":
                    return textMar;

                case "tree":
                    return textTree;



                case "wood":
                    return textWood;
                 

                case "brick":
                    return textBrick;
                 

                case "moinho":
                    return textMoinho;


                case "heightmap":
                    return heightmap;


                case "snow":
                    return textSnow;


                case "tree2":
                    return textTree2;



                case "wood2":
                    return textWood2;


                case "brick2":
                    return textBrick2;


                case "moinho2":
                    return textMoinho2;


                default:
                    return textGrass;

                  
            }

            
        }
        public Effect GetEffect(string effectName)
        {
            switch (effectName)
            {
                case "effect1":
                    return effect1;

                case "marEffect":
                    return effect2;

                default:
                    return effect1;


            }


        }
    }
}
