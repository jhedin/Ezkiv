using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ezkiv
{
    class Display8Seg//4 digit, 8 seg display
    {
        Vector2 locXY;
        Texture2D digits;
        Texture2D letters;
        bool numb;//t-digits, f-letters
        int valDigit;
        string valLetter;
        public Display8Seg(Vector2 loc, Texture2D digs, Texture2D lets)
        {
            locXY = loc;
            digits = digs;
            letters = lets;
            setDigit(0);
        }
        public void setDigit(int number)
        {
            valDigit = number;
            numb = true;
        }
        public void setLetter(String letters)
        {
            valLetter = letters;
            numb = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destRect = new Rectangle((int)locXY.X,0,27,15);
            Rectangle srcRect = new Rectangle(0, 0, 27, 15);
            Texture2D src;
            Stack<int> temp = new Stack<int>();
            if (numb)
            {
                src = digits;
                int div = valDigit;
                int rem = 0;
                while (div > 0)
                {
                    rem = div % 10;
                    div = div / 10;
                    temp.Push(rem);
                }
                while (temp.Count() < 4)
                {
                    temp.Push(0);
                }
                //if there happens to be overflow...
                while (temp.Count() > 4)
                {
                    temp.Pop();
                }
            }
            else
            {
                src = letters;
                for(int i = 0; i < 4; i++)
                {
                    temp.Push((int)valLetter[4 - i - 1] - (int)'a');
                }

            }
            for (int i = 0; i < 4; i++)
            {
                destRect.Y = (int)locXY.Y + i * 19;
                srcRect.Y = temp.Pop() * 15;
                spriteBatch.Draw(src, destRect, srcRect, Color.White);
            }

        }
    }
}