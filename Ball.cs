using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ezkiv 
{
    class Ball : GameObject
    {
        public Vector2 locXY; // cartesian position
        public Vector2 velXY; // cartesian velocity
        public bool fire;
        string name;
        Texture2D spr;
        float rad;

        public Ball(int X, int Y, int vX, int vY, Texture2D objspr, string objName)
        {
            locXY = new Vector2(X, Y);
            velXY = new Vector2(vX, vY);
            fire = true;
            spr = objspr;
            name = objName;
            rad = 5f;
        }
        public Vector2 getLocXY()
        {
            return locXY;
        }
        public string getName()
        {
            return name;
        }

        public void setLocXY(Vector2 newLoc)
        {
            locXY = newLoc;
        }

        public int collide(GameObject obj)
        {
            Vector2 temp = obj.getLocXY();
            temp = locXY + new Vector2(rad, rad) - temp - new Vector2(15, 15);
            if (temp.Length() <= rad + 15f)
                return 2;
            return 0;
        }

        public void update()
        {
            // move
            locXY += velXY;
            // check off screen
            if (locXY.X < 0 || locXY.X > 230)
            {
                velXY.X = -velXY.X;
                if (locXY.X < 0)
                {
                    locXY.X = 0;
                }
                else
                {
                    locXY.X = 230;
                }
            }          
            
            if (locXY.Y < 0 || locXY.Y > 310)
            {
                velXY.Y = -velXY.Y;
                if (locXY.Y < 0)
                {
                    locXY.Y = 0;
                }
                else
                {
                    locXY.Y = 310;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRect = new Rectangle(0, 0, 10, 10);
            Color drawCol = Color.White;         
            drawRect.X = (int)locXY.X;
            drawRect.Y = (int)locXY.Y;
            if (fire)
            {
                drawCol = Color.Red; // gives a redish tint.
            }
            else
            {
                drawCol = Color.White;
            }
            spriteBatch.Draw(spr, drawRect, drawCol);
        }
    }
}