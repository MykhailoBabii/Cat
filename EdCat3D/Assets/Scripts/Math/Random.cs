#if DETERMINISTIC_LOGIC
using InternalRandom = BO.Common.NRandom;
#else
using InternalRandom = System.Random;
#endif


namespace BO.Common
{
    public class Random
    {
        private InternalRandom _random;

        private InternalRandom NewRandom()
        {
            return new InternalRandom();
        }

        private InternalRandom NewRandom(int seed)
        {
            return new InternalRandom(seed);
        }

        public int Seed { get; private set; }


        //----------------------------------------------------------------------------------
        public Random()
        {
            Seed = NewRandom().Next();

            _random = NewRandom(Seed);
        }

        
        //----------------------------------------------------------------------------------
        public Random(int seed)
        {
            _random = NewRandom(seed);

            Seed = seed;
        }


        //----------------------------------------------------------------------------------
        public float NextFloat()                               { return (float)_random.NextDouble(); }
        public float NextFloat(float minValue, float maxValue) { return Math.Lerp(minValue, maxValue, NextFloat()); }
        public int   NextInt()                                 { return _random.Next(); }
        public int   NextInt(int minValue, int maxValue)       { return _random.Next(minValue, maxValue); }
    }


    public class StaticRandom
    {
        private static InternalRandom _random = new InternalRandom();

        //----------------------------------------------------------------------------------
        public static float NextFloat()                               { return (float)_random.NextDouble(); }
        public static float NextFloat(float minValue, float maxValue) { return Math.Lerp(minValue, maxValue, NextFloat()); }
        public static int   NextInt()                                 { return _random.Next(); }
        public static int   NextInt(int minValue, int maxValue)       { return _random.Next(minValue, maxValue); }
        public static bool  TestProbability(double probability)       { return probability > 0.0 && probability >= 1.0 || _random.NextDouble() - probability < .0; }
    }
}