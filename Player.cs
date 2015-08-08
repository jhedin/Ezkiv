using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ezkiv
{
    //takes input/moves the player's sprite around
    //collisions done in main. it checks the name(this will be first in the list) then checks dist/rad with everything else.
    class Player : GameObject
    {
        Texture2D       spr;
        Texture2D       leftSpr;
        Texture2D       rightSpr;
        Texture2D       upSpr;
        Texture2D       downSpr;
        Vector2         locXY;
        Vector2         velXY;
        //TouchCollection touchCollection;
        GamePadDPad dpad;
        Rectangle       left;
        Rectangle       right;
        Rectangle       up;
        Rectangle       down;

        Vector2 centreOfDPad;

        string name;
        public Player(Texture2D playerSpr, Texture2D UpSpr, Texture2D RightSpr, Texture2D LeftSpr, Texture2D DownSpr, String objName)
        {
            left = new Rectangle(91, 10, 90, 30);
            right = new Rectangle(91, 70, 90, 30);
            up = new Rectangle(151, 10, 30, 90);
            down = new Rectangle(91, 10, 30, 90);
            float dx = (left.Center.X + right.Center.X + up.Center.X + down.Center.X) / 4;
            float dy = (left.Center.Y + right.Center.Y + up.Center.Y + down.Center.Y) / 4;
            centreOfDPad = new Vector2(dx,dy);
            spr = playerSpr;
            upSpr = UpSpr;
            downSpr = DownSpr;
            leftSpr = LeftSpr;
            rightSpr = RightSpr;
            locXY = new Vector2(100,150);
            velXY = new Vector2();
            name = objName;
        }
        public void setLocXY(Vector2 newLoc)
        {
            locXY = newLoc;
        }
        public string getName()
        {
            return name;
        }
        public Vector2 getLocXY()
        {
            return locXY;
        }
        public int collide(GameObject obj)
        {
            return 0;
        }
        public void update()
        {

            updateInput();
            // move
            locXY += velXY;
            // check off screen
            if (locXY.X < 0 || locXY.X > 210)
            {
                //velXY.X = -velXY.X;
                if (locXY.X < 0)
                {
                    locXY.X = 0;
                }
                else
                {
                    locXY.X = 210;
                }
            }

            if (locXY.Y < 0 || locXY.Y > 290)
            {
                //velXY.Y = -velXY.Y;
                if (locXY.Y < 0)
                {
                    locXY.Y = 0;
                }
                else
                {
                    locXY.Y = 290;
                }
            }
        }
        void updateInput()
        {
            //dpad = GamePad.GetCapabilities(PlayerIndex.One).
            //touchCollection = TouchPanel.GetState();
            velXY.X = 0;
            velXY.Y = 0;
            float distSqr;
            float dx;
            float dy;
            float dyx;
            /*foreach (TouchLocation touch in touchCollection)
            {
                dx = touch.Position.X - centreOfDPad.X;
                dy = touch.Position.Y - centreOfDPad.Y;
                distSqr = dx*dx + dy*dy;
                if (distSqr < 3600 && distSqr > 100)
                {
                    dyx = Math.Abs(dy/dx);
                    if (dyx > 2)
                    {
                        velXY.X = 0;
                        velXY.Y = Math.Sign(dy);
                    }
                    else if (dyx < 0.5)
                    {
                        velXY.X = Math.Sign(dx);
                        velXY.Y = 0;
                    }
                    else
                    {
                        velXY.X = touch.Position.X - centreOfDPad.X;
                        velXY.Y = touch.Position.Y - centreOfDPad.Y;
                    }
                }*/

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                    velXY.Y += 1;
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                    velXY.Y -= 1;
                if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
                    velXY.X += 1;
                if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
                    velXY.X -= 1;
            
            if (velXY.LengthSquared() != 0)
            {
                velXY.Normalize();
                velXY = velXY * 8;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRect = new Rectangle(0, 0, 30, 30);
            drawRect.X = (int)locXY.X;
            drawRect.Y = (int)locXY.Y;
            spriteBatch.Draw(spr, drawRect, Color.Turquoise);

            /*spriteBatch.Draw(leftSpr, left, Color.White);
            spriteBatch.Draw(upSpr, up, Color.White);
            spriteBatch.Draw(rightSpr, right, Color.White);
            spriteBatch.Draw(downSpr, down, Color.White);*/
        }
    }
}