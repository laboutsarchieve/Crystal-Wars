using System;

namespace CrystalWars
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CrystalWarsApplication game = new CrystalWarsApplication())
            {
                game.Run();
            }
        }
    }
#endif
}

