using System;

namespace Test
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a positive integer.
        /// </summary>
        /// <param name="exclusiveMaximum">
        /// Exclusive upper bound.
        /// </param>
        public static int NextPositive(this Random random, int exclusiveMaximum = int.MaxValue - 1000)
        {
            return random.Next(1, exclusiveMaximum);
        }

        /// <summary>
        /// Returns a zero or negative integer.
        /// </summary>
        /// <param name="exclusiveMinimum">
        /// Exclusive lower bound.
        /// </param>
        public static int NextZeroOrNegative(this Random random, int exclusiveMinimum = int.MinValue + 1000)
        {
            return -1 * random.Next(0, -1 * exclusiveMinimum);
        }

        /// <summary>
        /// Returns true or false.
        /// </summary>
        public static bool NextBool(this Random random)
        {
            int zeroOrOne = random.Next(0, 2);
            return zeroOrOne == 1;
        }

        /// <summary>
        /// Returns a randomly generated string.
        /// </summary>
        public static string NextString(this Random random)
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns a random date in the future.
        /// </summary>
        public static DateTime NextDateTimeInFuture(this Random random)
        {
            return DateTime.Now.AddDays(random.Next(1, 3651));
        }

    }
}