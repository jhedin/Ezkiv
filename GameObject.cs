using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ezkiv
{
    interface GameObject
    {
        String getName();
        Vector2 getLocXY();
        void setLocXY(Vector2 newLoc);
        void update();
        void Draw(SpriteBatch spriteBatch);
        int collide(GameObject obj);//returns 0(n/a),1(goal) or 2(death)
    }
}
