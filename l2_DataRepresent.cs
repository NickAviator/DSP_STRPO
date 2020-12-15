using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DSP_STRPO
{
    class l2_DataRepresent
    {
        /// <summary>
        /// Вычисление МФУ-Пэли
        /// </summary>
        public void First()
        {
            int n = 0, base_num = 0;
            Console.WriteLine("Введите размерность матрицы:");
            int.TryParse(Console.ReadLine(), out n);
            Console.WriteLine("Выберете основание системы счисления (2, 4):");
            int.TryParse(Console.ReadLine(), out base_num);
            l3_MathBase obj_l3 = new l3_MathBase();
            try
            {
                int[,] arr_out = obj_l3.MatrixGen(n, base_num);
                Console.WriteLine("Матрица значений МФУ-Пэли:");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(arr_out[i, j] + " ");
                        // сохранить в файл
                    }


                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                //if any error - put here
            }
            Console.WriteLine("\t");
        }

        /// <summary>
        /// Рассчет спектральных коэффициентов Фурье
        /// </summary>
        public void Second(int method)
        {
            Console.WriteLine("Called Math Base method");
            // (int n, int base_num, double T, int type)
            int n = 0, base_num = 0, type = 1;
            double T = 0;
            Console.WriteLine("Введите размерность матрицы:");
            int.TryParse(Console.ReadLine(), out n);
            Console.WriteLine("Выберете основание системы счисления (2, 4):");
            int.TryParse(Console.ReadLine(), out base_num);
            Console.WriteLine("Введите длительность сигнала (мс):");
            double.TryParse(Console.ReadLine(), out T);
            Console.WriteLine("Выберете тип сигнала: \n 1 - физический белый шум \n 2 - сигнал с экспоненциальной ФСПМ"); // тип ФСПМ
            int.TryParse(Console.ReadLine(), out type);

            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch         
            try
            {
                if (method == 1)
                {
                    l3_MathBase obj_l3 = new l3_MathBase();
                    double[,] X_out = obj_l3.CalcCore(n, base_num, T, type);
                    Console.WriteLine("Матрица спектральных коэффициентов Фурье:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write(X_out[i, j] + " ");
                            // сохранить в файл
                        }
                        Console.WriteLine();
                    }                 
                }
                if (method == 2)
                {
                    // переопределение и вывод объекта
                    l3_Math_Modified obj_l3M = new l3_Math_Modified();
                    double[,] X_out = obj_l3M.CalcCore(n, base_num, T, type);
                    Console.WriteLine("Матрица спектральных коэффициентов Фурье:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write(X_out[i, j] + " ");
                            // сохранить в файл
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("Выполнено параллельно");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                //if any error - put here
            }
            Console.WriteLine("\t");
            stopwatch.Stop();
            Console.WriteLine("Время выполнения вычислений (мс): " + stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Получение уравнения восстановленного сигнала
        /// </summary>
        public void Third(int method)
        {
            int n, base_num, type, ask;
            double T;
            Console.WriteLine("Введите размерность матрицы:");
            int.TryParse(Console.ReadLine(), out n);
            Console.WriteLine("Выберете основание системы счисления (2, 4):");
            int.TryParse(Console.ReadLine(), out base_num);
            Console.WriteLine("Введите длительность сигнала (мс):");
            double.TryParse(Console.ReadLine(), out T);
            Console.WriteLine("Выберете тип сигнала: \n 1 - физический белый шум \n 2 - сигнал с экспоненциальной ФСПМ"); // тип ФСПМ
            int.TryParse(Console.ReadLine(), out type);

            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch         
            if (method == 1)
            {
                l3_MathBase obj_l3 = new l3_MathBase();
                for (int i = 0; i < n - 1; i++) //+i = 0.01
                    obj_l3.GetFunction(i, n, base_num, T, type);
            }
            if (method == 2)
            {
                l3_Math_Modified obj_l3M = new l3_Math_Modified();
                for (int i = 0; i < n - 1; i++) //+i = 0.01
                    obj_l3M.GetFunction(i, n, base_num, T, type);
            }
            stopwatch.Stop();
            Console.WriteLine("Время выполнения вычислений (мс): " + stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Успешно! Вывести результат? 1 - да, 0 - нет");
            int.TryParse(Console.ReadLine(), out ask);
            if (ask == 1)
            {
                using (var readtext = new StreamReader(@"C:\Users\Nikita\Desktop\Log.txt"))
                {
                    while (!readtext.EndOfStream)
                    {
                        string currentLine = readtext.ReadLine();
                        Console.WriteLine(currentLine);
                    }
                }
            }
            Console.WriteLine("\t");
        }

        /// <summary>
        /// Рассчет автокорреляционной функции
        /// </summary>
        public void Fourth()
        {
            int n = 0, base_num = 0, type = 1, ask;
            double T = 0;
            Console.WriteLine("Введите длительность сигнала (мс):");
            double.TryParse(Console.ReadLine(), out T);
            Console.WriteLine("Выберете тип сигнала: \n 1 - физический белый шум \n 2 - сигнал с экспоненциальной ФСПМ"); // тип ФСПМ
            int.TryParse(Console.ReadLine(), out type);
            l3_MathBase obj_l3 = new l3_MathBase();
            for (int i = 0; i < T * 6; i++) //T * 6 - масштабирование графика
                obj_l3.GetAKF(i, T, type);
            Console.WriteLine("Успешно! Вывести результат? 1 - да, 0 - нет");
            int.TryParse(Console.ReadLine(), out ask);
            if (ask == 1)
            {
                using (var readtext = new StreamReader(@"C:\Users\Nikita\Desktop\LogAKF.txt"))
                {
                    while (!readtext.EndOfStream)
                    {
                        string currentLine = readtext.ReadLine();
                        Console.WriteLine(currentLine);
                    }
                }
            }
            Console.WriteLine("\t");
        }

        public void Test()
        {
            Parallel.Invoke(
                () =>
                    {
                        double i = Math.Pow(1024, 8);
                        Thread.Sleep(5000);
                        Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                        Console.WriteLine(i);

                    },
                () =>
                    {
                        Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                        Console.WriteLine("2nd");
                        Thread.Sleep(5000);
                        //Thread.Sleep(300);
                    }
            );
        }
    }
}
