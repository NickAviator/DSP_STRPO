using System;
using System.Threading.Tasks;

namespace DSP_STRPO
{
    class l1_UserInterface
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            l1_UserInterface obj = new l1_UserInterface();
            obj.ChooseAction();
        }

        /// <summary>
        /// Использовать действия - буквы/цифры:
        /// 1 - Вычислить МФУ - Пэли
        /// 2 - Расчитать спектральные коэффициенты Фурье
        /// 3 - Получить уравнение восстановленного сигнала
        /// 4 - Рассчитать АКФ сигнала
        /// ...
        /// обрабатывать в методе ниже вызов методов из Math_Base
        /// </summary>
        void ChooseAction()
        {
            int method;
            int num = 999;
            while (num != 0)
            {
                l2_DataRepresent obj_l2 = new l2_DataRepresent();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Вычислить МФУ - Пэли");
                Console.WriteLine("2 - Расчитать спектральные коэффициенты Фурье");
                Console.WriteLine("21 - Расчитать спектральные коэффициенты Фурье (параллельно)");
                Console.WriteLine("3 - Получить уравнение восстановленного сигнала");
                Console.WriteLine("31 - Получить уравнение восстановленного сигнала (параллельно)");
                Console.WriteLine("4 - Рассчитать АКФ сигнала");
                Console.WriteLine("99 - TEST");
                Console.WriteLine("0 - Выход");
                int.TryParse(Console.ReadLine(), out num);
                switch (num)
                {
                    case 1:                      
                        obj_l2.First();
                        break;
                    case 2:
                        method = 1; // стандартные вычисления
                        obj_l2.Second(method);
                        break;
                    case 21:
                        method = 2; // использование параллельных вычислений
                        obj_l2.Second(method);
                        break;
                    case 3:
                        method = 1; // стандартные вычисления 
                        obj_l2.Third(method);
                        break;
                    case 31:
                        method = 2; // использование параллельных вычислений
                        obj_l2.Third(method);
                        break;
                    case 4:
                        obj_l2.Fourth();
                        break;
                    case 99:
                        obj_l2.Test();
                        break;
                    default:
                        Console.WriteLine("Параметр не выбран");
                        break;
                }
            }
        }
    }
}
