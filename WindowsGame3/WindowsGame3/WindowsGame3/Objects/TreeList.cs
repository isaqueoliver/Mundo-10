using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{

    public class TreeList
    {
        Tree[] trees;
        Game game;
        Vector3[] position;
        Camera camera;
        Tree treeSort;
        Vector3 positionSort;




        public TreeList(Game game, Camera camera, Vector3 position, int rows, int columns)
        {
            this.game = game;
            this.camera = camera;
            this.trees = new Tree[columns * rows];
            this.position = new Vector3[columns * rows];
            int x = 1 - columns;


            for (int j = 0; j < rows; j++)
            {
                x += columns - 1;
                for (int i = 0; i < columns; i++)
                {
                    this.position[i + j + x] = Vector3.Zero;
                    this.position[i + j + x].X = 2 * i;
                    this.position[i + j + x].Y = 0;
                    this.position[i + j + x].Z = 2 * j;
                    this.position[i + j + x] += position;

                    

                    this.trees[i + j + x] = new Tree(game,  this.camera);

                    this.trees[i + j + x].SetTranslation(this.position[i + j + x]);


                }

            }
            
        }
       public int Length()
        {
            return trees.Length;
        }
        public BoundingBox GetBoundingBox(int x)
        {
            return trees[x].GetBoundingBox();
        }
        public Vector3 GetPosition(int x)
        {
            
            Vector3 center = (trees[x].GetBoundingBox().Max + trees[x].GetBoundingBox().Min) / 2;
            return center;
        }
        public void StopFalling(int x)
        {
            trees[x].StopFalling();
        }
        public void Update()
        {

           for(int i = 0; i < this.trees.Length; i++)
            {
                this.trees[i].Movement(this.camera);
            }

            for (int a = 0; a < this.trees.Length; a++)
            {
                for (int b = 0; b < this.trees.Length - 1; b++)
                {

                    if (this.trees[b].GetDistance() < this.trees[b + 1].GetDistance())
                    {
                        this.treeSort = this.trees[b + 1];
                        this.positionSort = this.position[b + 1];
                        this.trees[b + 1] = this.trees[b];
                        this.position[b + 1] = this.position[b];
                        this.trees[b] = this.treeSort;
                        this.position[b] = this.positionSort;
                    }
                }
            }
        }

        public void Draw()
        {

            for (int i = 0; i < this.trees.Length; i++)
            {

                this.trees[i].Draw();
            }
        }

    }
}

