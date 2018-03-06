using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Laba2WPF
{
    public static class RabinMiller
    {
        public static bool IsPrime(int n, int round)
        {
            // если n < 2 или n четное - false, если 2 == 2 - true
            if ((n < 2) || (n % 2 == 0)) return (n == 2);

            // представим n − 1 в виде (2^s)·t, где t нечётно, это можно сделать последовательным делением n - 1 на 2
            int s = n - 1;
            while (s % 2 == 0) s >>= 1;

            Random r = new Random();

            // повторить round раз
            for (int i = 0; i < round; i++)
            {
                int a = r.Next(n - 1) + 1;
                int temp = s;
                long mod = 1;
                for (int j = 0; j < temp; ++j) mod = (mod * a) % n;

                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp *= 2;
                }

                if (mod != n - 1 && temp % 2 == 0) return false;
            }
            return true;
        }
    }

    public static class RabinMillerBig
    {
        public static bool IsPrime(this BigInteger source, int round)
        {
            // если n == 2 или n == 3 - эти числа простые, возвращаем true
            if (source == 2 || source == 3)
                return true;

            // если n < 2 или n четное - возвращаем false
            if (source < 2 || source % 2 == 0)
                return false;

            // представим n − 1 в виде (2^s)·t, где t нечётно, это можно сделать последовательным делением n - 1 на 2
            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s++;
            }

            // выберем случайное целое число a в отрезке [2, n − 2]
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            // повторить round раз
            for (int i = 0; i < round; i++)
            {
                do
                {

                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                // x ← a^t mod n, вычислим с помощью возведения в степень по модулю
                BigInteger x = BigInteger.ModPow(a, d, source);

                // если x == 1 или x == n − 1, то перейти на следующую итерацию цикла
                if (x == 1 || x == source - 1)
                    continue;

                // повторить s − 1 раз
                for (int r = 1; r < s; r++)
                {
                    // x ← x^2 mod n
                    x = BigInteger.ModPow(x, 2, source);
                    // если x == 1, то вернуть "составное"
                    if (x == 1)
                        return false;
                    // если x == n − 1, то перейти на следующую итерацию внешнего цикла
                    if (x == source - 1)
                        break;
                }
                if (x != source - 1)
                    return false;
            }
            // вернуть "вероятно простое"
            return true;
        }
    }

}
