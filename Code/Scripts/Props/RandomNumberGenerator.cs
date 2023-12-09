using System;


namespace CS576.Janitor.Process
{
    public static class RandomNumberGenerator
    {
        /*
            This is a random function I have been using for my developments.
            It takes in a seed value and convert it to hashcode,
            then it gets the current time tick and combine the hashed seed
            value with the tick value. 

            Initialize the random state with the combined seed.
        */
        public static void InitializeRandomEnvironment(string seed = "seed")
        {
            int seedValue = seed.GetHashCode();
            long ticks = DateTime.Now.Ticks;
            int lowerTicks = (int)(ticks & 0xffffffff);
            int combinedSeed = seedValue ^ lowerTicks;

            UnityEngine.Random.InitState(combinedSeed);
        }

        /*
            Generate a random number between the lower bound and
            upper bound (inclusive)

            Usually works with the above function
        */
        public static int GenerateBetween(int lowerbound, int upperbound, int rollTime = 10)
        {
            int randomNumber = 0;
            for (int i = 0; i < rollTime; i++)
            {
                randomNumber = UnityEngine.Random.Range(lowerbound, upperbound);
            }

            return randomNumber;
        }
    }
}
