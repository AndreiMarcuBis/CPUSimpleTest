using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace CPUSimpleTest
{
    class Program
    {
        private static long duration;

        private static void thread_main()
        {
            Stopwatch w = new Stopwatch();
            w.Start();


            float[] f = new float[1024];
            int[] i = new int[1024];
            int fi = 0;
            int ii = 0;

            Random r = new Random();
            for (int k = 0; k < f.Length; ++k)
                f[k] = (float)r.NextDouble();

            for (int k = 0; k < i.Length; ++k)
                i[k] = r.Next();

            while (w.ElapsedMilliseconds < duration)
            {
                int k = (int)(f[fi]) % f.Length;
                if (k < 0)
                    k *= -1;
                f[fi] += f[k];
                f[fi] /= 42;

                k = (int)(i[ii]) % i.Length;
                if (k < 0)
                    k *= -1;
                i[ii] += i[k];
                i[ii] /= 7;

                fi += 1;
                if (fi >= f.Length)
                    fi = 0;

                ii += 1;
                if (ii >= i.Length)
                    ii = 0;
            }

            w.Stop();
        }

        public static void Main(string[] args)
        {
            duration = 30000;

            List<Thread> pool = new List<Thread>();

            for (int i = 0; i < 8; ++i)
            {
                Thread t = new Thread(thread_main);
                pool.Add(t);
                t.Start();
            }
        }
    }
}
