using System;
using MapEditor.Application;

namespace MapEditor
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using(PrototypeApp game = new PrototypeApp())
            {
                game.Run();
            }
        }
    }
#endif
}

