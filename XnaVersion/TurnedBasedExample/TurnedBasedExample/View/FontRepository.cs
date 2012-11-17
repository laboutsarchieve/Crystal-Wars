using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TurnedBasedExample.View
{
    static class FontRepository
    {
        public static SpriteFont debugFont;

        public static void Initalize(ContentManager content)
        {
            debugFont = content.Load<SpriteFont>(@"font/DebugFont");
        }
    }
}
