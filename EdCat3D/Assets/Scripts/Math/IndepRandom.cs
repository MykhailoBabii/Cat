using System;

namespace BO.Common
{
    //[Serializable]
    //public class NetRandom
    //{
    //    // Fields
    //    private int inext;
    //    private int inextp;
    //    private const int MBIG = 2147483647;
    //    private const int MSEED = 161803398;
    //    private const int MZ = 0;
    //    private int[] SeedArray;

    //    // Methods
    //    public NetRandom()
    //        : this((int)(System.Diagnostics.Stopwatch.GetTimestamp() % int.MaxValue))
    //    {
    //    }

    //    public NetRandom(int Seed)
    //    {
    //        this.SeedArray = new int[56];
    //        int num4 = (Seed == -2147483648) ? MBIG : System.Math.Abs(Seed);
    //        int num2 = MSEED - num4;
    //        this.SeedArray[55] = num2;
    //        int num3 = 1;
    //        for (int i = 1; i < 55; i++)
    //        {
    //            int index = (21 * i) % 55;
    //            this.SeedArray[index] = num3;
    //            num3 = num2 - num3;
    //            if (num3 < 0)
    //            {
    //                num3 += MBIG;
    //            }
    //            num2 = this.SeedArray[index];
    //        }
    //        for (int j = 1; j < 5; j++)
    //        {
    //            for (int k = 1; k < 56; k++)
    //            {
    //                this.SeedArray[k] -= this.SeedArray[1 + ((k + 30) % 55)];
    //                if (this.SeedArray[k] < 0)
    //                {
    //                    this.SeedArray[k] += MBIG;
    //                }
    //            }
    //        }
    //        this.inext = 0;
    //        this.inextp = 21;
    //        Seed = 1;
    //    }

    //    private double GetSampleForLargeRange()
    //    {
    //        int num = this.InternalSample();
    //        if ((this.InternalSample() % 2) == 0)
    //        {
    //            num = -num;
    //        }
    //        double num2 = num;
    //        num2 += 2147483646.0;
    //        return (num2 / 4294967293);
    //    }

    //    private int InternalSample()
    //    {
    //        int inext = this.inext;
    //        int inextp = this.inextp;
    //        if (++inext >= 56)
    //        {
    //            inext = 1;
    //        }
    //        if (++inextp >= 56)
    //        {
    //            inextp = 1;
    //        }
    //        int num = this.SeedArray[inext] - this.SeedArray[inextp];
    //        if (num == MBIG)
    //        {
    //            num--;
    //        }
    //        if (num < 0)
    //        {
    //            num += MBIG;
    //        }
    //        this.SeedArray[inext] = num;
    //        this.inext = inext;
    //        this.inextp = inextp;
    //        return num;
    //    }

    //    public virtual int Next()
    //    {
    //        return this.InternalSample();
    //    }

    //    public virtual int Next(int maxValue)
    //    {
    //        if (maxValue < 0)
    //        {
    //            throw new ArgumentOutOfRangeException("maxValue", string.Format("ArgumentOutOfRange_MustBePositive", new object[] { "maxValue" }));
    //        }
    //        return (int)(this.Sample() * maxValue);
    //    }

    //    public virtual int Next(int minValue, int maxValue)
    //    {
    //        if (minValue > maxValue)
    //        {
    //            throw new ArgumentOutOfRangeException("minValue", string.Format("Argument_MinMaxValue", new object[] { "minValue", "maxValue" }));
    //        }
    //        long num = maxValue - minValue;
    //        if (num <= 2147483647L)
    //        {
    //            return (((int)(this.Sample() * num)) + minValue);
    //        }
    //        return (((int)((long)(this.GetSampleForLargeRange() * num))) + minValue);
    //    }

    //    public virtual void NextBytes(byte[] buffer)
    //    {
    //        if (buffer == null)
    //        {
    //            throw new ArgumentNullException("buffer");
    //        }
    //        for (int i = 0; i < buffer.Length; i++)
    //        {
    //            buffer[i] = (byte)(this.InternalSample() % 256);
    //        }
    //    }

    //    public virtual double NextDouble()
    //    {
    //        return this.Sample();
    //    }

    //    protected virtual double Sample()
    //    {
    //        return (this.InternalSample() * 4.6566128752457969E-10);
    //    }
    //}

    // Depricated! Use NRandom.
    public class IndepRandom
    {
        public IndepRandom(int seed)
        {
            Current = seed;// *151;
        }

        public IndepRandom Clone()
        {
            return (IndepRandom)this.MemberwiseClone();
        }

        public int Current { get; private set; }

        public int NextInt()
        {
            Current = (7027 + Current * 9973 / 3767) & (int)UInt16.MaxValue;
            return Current;
        }


        public float NextFloat()
        {
            return (float)((double)NextInt() / (double)UInt16.MaxValue);
        }


        public bool NextBool(float trueProbability)
        {
            if (trueProbability == 0)
                return false;

            return NextFloat() <= trueProbability;
        }


        public float NextFloat(float maxValue)
        {
            return NextFloat() * maxValue;
        }

        public float NextFloat(float minValue, float maxValue)
        {
            return Math.Lerp(minValue, maxValue, NextFloat());
        }

        public int NextInt(int maxValue)
        {
            return (int)(NextInt() % ((uint)(maxValue + 1)));
        }

        public int NextInt(int minValue, int maxValue)
        {
            return (int)(NextInt() % ((uint)(maxValue - minValue + 1))) + minValue;
        }
    }


    class NRandomSeed
    {
        public uint x { get; set; }
        public uint y { get; set; }
        public uint z { get; set; }
        public uint w { get; set; }

        public NRandomSeed()
        {
            x = 123456789;
            y = 362436069;
            z = 521288629;
            w = 88675123;
        }

        public NRandomSeed(uint seed)
        {
            x = seed;
            y = 362436069;
            z = 521288629;
            w = 88675123;
        }
    };

    // This class mimics the standard Random class both in terms of methods syntax and values ranges.
    // It makes easy to replace the standard class with this one. It does not mimics the inheritance
    // related behavior. So there is a room for improvement.

    public class NRandom
    {
        private NRandomSeed Seed { get; set; }

        public NRandom()
        {
            Seed = new NRandomSeed();
        }

        public NRandom(int seed)
        {
            Seed = new NRandomSeed((uint)seed);
        }

        private int NextInternal()
        {
            uint t = (Seed.x ^ (Seed.x << 11));
            Seed.x = Seed.y;
            Seed.y = Seed.z;
            Seed.z = Seed.w;
            Seed.w = (Seed.w ^ (Seed.w >> 19)) ^ (t ^ (t >> 8));
            return (int)Seed.w;
        }

        public virtual int Next()
        {
            return NextInternal() & 0x7fffffff;
        }

        public virtual int Next(int maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue");

            if (maxValue == 0)
                return 0;

            return (int)(Next() % (uint)maxValue);
        }

        public virtual int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue and maxValue");

            if (minValue == maxValue)
                return minValue;

            return (int)((uint)NextInternal() % ((uint)(maxValue - minValue))) + minValue;
        }

        public virtual void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)Next(Byte.MaxValue + 1);
            }
        }

        public virtual double NextDouble()
        {
            return (double)(uint)Next() / (double)((uint)Int32.MaxValue + 1);
        }

        protected virtual double Sample()
        {
            return NextDouble();
        }
    }
}