using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSP_STRPO
{
    class l3_Math_Modified
    {
        public double Xf = 1;
        /// <summary>
        /// Рассчет ядра Фурье
        /// </summary>
        /// <param name="n"></param>
        /// <param name="base_num"></param>
        /// <param name="FFT"></param>
        /// <param name="T"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public double[,] CalcCore(int n, int base_num, double T, int type)
        {
            double[,] X = new double[n, n];
            int[,] arr_buf = MatrixGen(n, base_num);

            Parallel.For(0, n, k =>
            {
                for (int i = 0; i < n; i++)
                { 
                    X[k, i] = Math.Round((1 / (double)n) * arr_buf[k, i] * CalcXf(T, type, k), 2);
                }
                Thread.Sleep(1);
            });

            return X;
        }

        public int[,] MatrixGen(int n, int base_num)
        {
            //int base_num = 2; //by default
            //n = 4;
            int[,] arr = new int[n, n];

            Parallel.For(0, n, i =>
            {
                for (int j = 0; j < n; j++)
                {
                    //check as fill with 0
                    int Sum = GetExtent(i, j, base_num);
                    // получаем функции Уолша (классические)
                    if (base_num == 2)
                    {
                        arr[i, j] = (int)(Math.Pow((-1), Sum));
                    }
                    // получаем функции Уолша (модифицированные)
                    if (base_num == 4)
                    {
                        arr[i, j] = (int)(Math.Sqrt(2) * Math.Cos((Math.PI) / 2 * Sum) + Math.Sqrt(2) * Math.Sin((Math.PI) / 2 * Sum));
                    }
                }
            });
            return arr;
        }

        // В качестве Parallel.Invoke
        private int GetExtent(int k, int i, int base_n)
        {
            string a_k = "", a_i = "";
            int sum = 0;
            //количество разрядов у обоих чилел должно быть одинаковым
            Parallel.Invoke(
                () =>
                    {
                        a_k = Int32ToString(k, base_n);
                        Thread.Sleep(1);
                    },
                () =>
                    {
                        a_i = Int32ToString(i, base_n);
                        Thread.Sleep(1);
                    }
            );
            while (a_k.Length != a_i.Length)
            {
                if (a_k.Length < a_i.Length)
                    a_k = "0" + a_k;
                if (a_k.Length > a_i.Length)
                    a_i = "0" + a_i;
            }

            int n = a_k.Length;
            //сравнить с формулой (2.5), что делать с разрядами? 
            for (int lmb = 0; lmb < n; lmb++)
            {
                //нужно a_k[n+1-lmb], а пока это Адамар
                sum += Convert.ToInt32(a_k[lmb]) * Convert.ToInt32(a_i[lmb]);
            }

            return sum;
        }

        private string Int32ToString(int value, int toBase)
        {
            string result = string.Empty;
            do
            {
                result = "0123456789ABCDEF"[value % toBase] + result;
                value /= toBase;
            }
            while (value > 0);

            //result = "0" + result; 

            return result;
        }

        private double CalcXf(double T, int type, int k)
        {
            double w = 2 * Math.PI / T;
            double N_up = T; //the same as (w * T) / 2 * Math.PI;
            //double Xf = 1;
            if (type == 1)
                Xf = Math.Abs(k / Math.Sqrt(4 * N_up));
            if (type == 2)
                Xf = Math.Abs(k / Math.Sqrt(4 * N_up * Math.PI));
            return Xf;
        }

        public double GetFunction(double i, int n, int base_num, double T, int type)
        {
            double Y = 0, add = 0;
            double Sum = 0;
            double[,] arr_out_F = CalcCore(n, base_num, T, type);

            Random r = new Random();

            using (var sw = new System.IO.StreamWriter(@"C:\Users\Nikita\Desktop\Log.txt"))

                for (int k = 0; k < n; k++)
                {
                    Sum += arr_out_F[k, (int)i] * Math.Pow(-1, r.Next(0, 4));
                    // Запись пар х - у в файл
                    sw.WriteLine(k.ToString() + ". Sum: " + Math.Round(Sum, 2).ToString() + "X[" + k + "]:" + Math.Round(arr_out_F[k, (int)i], 2).ToString());
                }

            if (i % 2 == 0)
                add = Math.Round(Sum, 2);
            else
                add = Math.Round(Sum, 2);
            Y += add;
            return Y;
        }
    }
}
