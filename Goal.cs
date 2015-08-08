using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ezkiv
{
    class Goal : GameObject
    {
        string          name;
        Texture2D       spr;
        Vector2         locXY;
        Rectangle       limits;
        Random          rand;
        float           rad;

        public Goal(Rectangle Limits, Texture2D objSpr, String objName)
        {
            limits = Limits;
            spr = objSpr;
            name = objName;
            rand = new Random();
            locXY = new Vector2();
            rad = 7.5f;
            resetLoc();
        }
        public Vector2 getLocXY()
        {
            return locXY;
        }
        void resetLoc()
        {
            locXY.X = rand.Next(limits.Width) + limits.X;
            locXY.Y = rand.Next(limits.Height) + limits.Y;
        }

        public void setLocXY(Vector2 newLoc/*dummy*/)
        {
            resetLoc();
        }

        public int collide(GameObject obj)
        {
            Vector2 temp = obj.getLocXY();
            temp = locXY + new Vector2(rad,rad) - temp - new Vector2(15,15);
            if (temp.Length() <= rad + 15f)
            {
                resetLoc();
                return 1;
            }
            return 0;
        }

        public string getName()
        {
            return name;
        }

        public void update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRect = new Rectangle(0, 0, 15, 15);
            drawRect.X = (int)locXY.X;
            drawRect.Y = (int)locXY.Y;
            spriteBatch.Draw(spr, drawRect, Color.Yellow);
        }
    }
}