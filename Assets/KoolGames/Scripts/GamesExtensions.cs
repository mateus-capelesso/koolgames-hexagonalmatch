using System;
namespace KoolGames.Scripts
{
    public static class GamesExtensions
    {
        public static void Shuffle<T> (this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }
    }
}
