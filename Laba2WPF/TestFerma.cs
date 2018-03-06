using System;
using System.Numerics;

namespace Laba2WPF
{
    static class TestFerma
    {
        static Random rand = new Random();

        public static bool IsPrime(decimal x)
        {
            if (x == 2)
                return true;
            rand.Next();
            for (int i = 0; i < 100; i++)
            {
                decimal a = (rand.Next() % (x - 2)) + 2;
                if (Nod(a, x) != 1)
                    return false;
                if (Pows(a, x - 1, x) != 1)
                    return false;
            }
            return true;
        }

        static decimal Nod(decimal a, decimal b) //Алгоритмом Евклида:
        {
            if (b == 0)
                return a;
            return Nod(b, a % b);
        }

        static decimal Mul(decimal a, decimal b, decimal m) //Быстрое возведение в степень по модулю
        {
            if (b == 1)
                return a;
            if (b % 2 == 0)
            {
                decimal t = Mul(a, b / 2, m);
                return (2 * t) % m;
            }
            return (Mul(a, b - 1, m) + a) % m;
        }

        static decimal Pows(decimal a, decimal b, decimal m)
        {
            if (b == 0)
                return 1;
            if (b % 2 == 0)
            {
                decimal t = Pows(a, b / 2, m);
                return Mul(t, t, m) % m;
            }
            return (Mul(Pows(a, b - 1, m), a, m)) % m;
        }

    }
}
