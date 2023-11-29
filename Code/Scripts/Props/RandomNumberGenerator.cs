using System;


namespace CS576.Janitor.Process
{
    public static class RandomNumberGenerator
    {
        /*
            I learned to write this function on stack overflow a
            while ago now I cannot seem to be able to find its source.
        */
        public static void InitializeRandomEnvironment(string seed = "seed")
        {
            int seedValue = seed.GetHashCode();
            long ticks = DateTime.Now.Ticks;
            int lowerTicks = (int)(ticks & 0xffffffff);
            int combinedSeed = seedValue ^ lowerTicks;

            UnityEngine.Random.InitState(combinedSeed);
        }

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
