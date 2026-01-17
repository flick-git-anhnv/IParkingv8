using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace IParkingv8.Cash
{
    class RandomNumber
    {
        public const ulong MAX_RANDOM_INTEGER = 2147483648;
        public const ulong MAX_PRIME_NUMBER = 2147483648;
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        enum primality { COMPOSITE, PSEUDOPRIME };

        public ulong GenerateRandomNumber()
        {
            ulong rnd = 0;
            byte i;
            byte[] tmp = new byte[8];

            rngCsp.GetBytes(tmp);

            for (i = 0; i < 8; i++)
            {
                rnd += (ulong)tmp[i] << 8 * i;
            }

            return rnd;
        }

        public ulong GeneratePrime()
        {
            ulong tmp = 0;

            tmp = GenerateRandomNumber();
            tmp %= MAX_PRIME_NUMBER;

            /*  ensure it is an odd number	*/
            if ((tmp & 1) == 0)
                tmp += 1;

            /*  increment until prime  */
            while (MillerRabin(tmp, 5) == primality.COMPOSITE)
            {
                tmp += 2;
            }
            return tmp;
        }

        primality MillerRabin(ulong n, ushort trials)
        {
            ushort i;
            for (i = 0; i < trials; i++)
            {
                ulong nm3 = n, a = 0;
                nm3 -= 3;
                /* gets random value in [0,UINTMAX] */
                a = GenerateRandomNumber();
                /* gets random value in [2,N-1] */
                a %= nm3;
                a += 2;
                if (SingleMillerRabin(n, a) == primality.COMPOSITE) return primality.COMPOSITE;
            }
            return primality.PSEUDOPRIME; /* n probably prime */
        }

        primality SingleMillerRabin(ulong n, ulong a)
        {
            /* First step -- express (N-1) = 2^s * d, where d is odd. */
            ushort s = 0, r = 0;
            ulong d = n - 1;
            while ((d & 0x1) == 0)
            {
                ++s;
                d >>= 1;
            }

            if (s == 0) return primality.COMPOSITE;	// Can't happen for odd n

            {
                /* So now N-1 = 2^s * d */
                ulong x = XpowYmodN(a, d, n);
                if (x == 1) return primality.PSEUDOPRIME;
                if (x == n - 1) return primality.PSEUDOPRIME;
                for (r = 1; r < s; ++r)
                {
                    x = x * x % n;
                    if (x == 1)
                    {
                        return primality.COMPOSITE;
                    }
                    if (x == n - 1) return primality.PSEUDOPRIME;
                }
            }
            return primality.COMPOSITE;
        }

        public ulong XpowYmodN(ulong x, ulong y, ulong N)
        {
            ulong rptsq = x,							// rptsq = x^{2^0} = x initially
                result = 1;
            while (y != 0)
            {
                if ((y & 0x1) != 0) result = result * rptsq % N;
                rptsq = rptsq * rptsq;			// at ith iteration, rptsq_i = (rptsq_{i-1})^2
                rptsq %= N;
                y >>= 1;
            }
            return result;
        }

    }

}
