using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;

namespace ezkiv
{
    class Counter
    {
        public Vector2 origin;//actually, this is the lefthand/top corner
        public uint value;
        private int altVal;//for timed stuff.
        Texture2D font;
        Color fontColor;
        /*Rectangle srcRect;//oh wait... passing pointers, not copies!
        Rectangle destRect;*/

        public Counter( uint val, Vector2 org)
        {
            value = val;
            origin = org;
            altVal = 0;
        }
        public void init(Texture2D numbs, Color colour)
        {
            font = numbs;
            fontColor = colour;
        }
        public uint update()
        {
            altVal++;
            if (altVal % 30 == 0)
            {
                value++;
                altVal = 0;
            }
            return value;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //write the number, with the leftmost digit centered on the origin
            uint div = value;
            Stack<uint> digits = new Stack<uint>();
            int count = 0;//count*(7+1) describes the distance between images 7 = width, 1 = spacing
            if (div == 0)
            {
                digits.Push(0); 
            } 
            
            while (div % 10 != 0 || div / 10 != 0)
            {
                digits.Push(div % 10);//what is the current digit?
                div = div / 10;//steps to the left              
            }
            //so, now we have a stack of digits, now to draw in order!
            
            while (digits.Count != 0)
            {
                spriteBatch.Draw(font, new Rectangle((int)origin.X + count * 8, (int)origin.Y, 7, 14), new Rectangle((int)digits.Pop() * 7 ,0,7,14), fontColor);
                count++;
            }


        }
    }
}