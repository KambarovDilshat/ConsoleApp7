using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Lesson1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main()
        {
            await FetchNonexistentResource();
            ProcessArray();
            HandleNestedException();
            InvokeExceptionPropagation();

            Console.WriteLine("Нажмите Enter, чтобы завершить...");
            Console.ReadLine();

            // Перехватить исключение запроса к несуществующему веб ресурсу и вывести сообщение о том,
            // что произошла ошибка. Программа должна завершиться без ошибок.
            async System.Threading.Tasks.Task FetchNonexistentResource()
            {
                var httpClient = new HttpClient();

                try
                {
                    var response = await httpClient.GetStringAsync("http://nonexistentwebsite.com");
                    Console.WriteLine(response);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Ошибка при запросе к веб ресурсу: " + e.Message);
                }
                finally
                {
                    httpClient.Dispose();
                }
                Console.WriteLine("----------------");
            }

            // Написать программу, которая обращается к элементам массива по индексу и выходит за его пределы.
            // После обработки исключения вывести в финальном
            // блоке сообщение о завершении обработки массива.
            void ProcessArray()
            {
                int[] array = { 1, 2, 3, 4, 5 };

                try
                {
                    Console.WriteLine(array[7]);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("Ошибка при обращении к элементу массива: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Завершение обработки массива");
                }
                Console.WriteLine("----------------");
            }

            // Реализовать несколько методов или классов с методами и вызвать один метод из другого.
            // В вызываемом методе сгенерировать исключение и «поднять» его в вызывающий метод.
            void HandleNestedException()
            {
                try
                {
                    CallerMethod();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка, перехваченная в HandleNestedException: {e.Message}");
                }
                Console.WriteLine("----------------");
            }

            void CallerMethod()
            {
                try
                {
                    ExceptionGenerator();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка, перехваченная в CallerMethod: {e.Message}");
                    throw;  // "Поднимаем" исключение дальше
                }
            }

            void ExceptionGenerator()
            {
                throw new InvalidOperationException("Произошла ошибка в ExceptionGenerator");
            }

            //Реализовать несколько методов или классов с методами и вызвать один метод из другого.
            //В вызываемом методе сгенерировать исключение и «поднять» его в вызывающий метод
            void InvokeExceptionPropagation()
            {
                var exampleClass = new ExampleClass();
                try
                {
                    exampleClass.InvokeMethod();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка, перехваченная после InvokeMethod: {e.Message}");
                }
            }
        }

        public class ExampleClass
        {
            public void InvokeMethod()
            {
                try
                {
                    GenerateExceptionMethod();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка, перехваченная в InvokeMethod класса ExampleClass: {e.Message}");
                    throw;  // "Поднимаем" исключение дальше
                }
            }

            private void GenerateExceptionMethod()
            {
                throw new InvalidOperationException("Ошибка, сгенерированная в GenerateExceptionMethod класса ExampleClass");
            }
        }
    }
}