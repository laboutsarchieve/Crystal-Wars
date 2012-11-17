using System;
using MapEditor.Application;

namespace MapEditor
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            using(PrototypeAppliction game = new PrototypeAppliction())
            {
                game.Run();
            }
        }
    }
#endif
}

