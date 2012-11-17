using System;

namespace TurnedBasedExample
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TurnBasedApplication game = new TurnBasedApplication())
            {
                game.Run();
            }
        }
    }
#endif
}

