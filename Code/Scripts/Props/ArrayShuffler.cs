using System.Collections.Generic;
using System.Linq;


/*
    A static class that shuffles the given array
    and return the shuffled array when done.

    Does not modfiy the original array.
*/
namespace CS576.Janitor.Process
{
    public static class ArrayShuffler
    {
        public static T[] Shuffle<T>(T[] array)
        {
            RandomNumberGenerator.InitializeRandomEnvironment();
            List<T> shuffledList = new List<T>(array.ToList());
            for (int i = 0; i < shuffledList.Count; i++)
            {
                T temp = shuffledList[i];
                int randomIndex = RandomNumberGenerator.GenerateBetween(
                    i,
                    shuffledList.Count
                );
                shuffledList[i] = shuffledList[randomIndex];
                shuffledList[randomIndex] = temp;
            }

            return shuffledList.ToArray();
        }
    }
}
