using System;
using System.Collections.Generic;

namespace _5Asd1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Console.WriteLine("Оберіть дію: " +
                              "\n1 - Напишіть щоб випадкого згенерувати матрицю" +
                              "\n2 - щоб вивести контрольний приклад роботи програми" +
                              "\n3 - щоб ввести свою матрицю" +
                              "\n4 - зупинити програму\n\n");
            string input_choise = "";
            new Program().user_interface(input_choise);
        }
        int[] merge_sort(int[] array)
        {
            if (array.Length > 1)
            {
                int[]
                arr1 = new int[array.Length / 2],
                arr2 = new int[array.Length - arr1.Length];

                int i = 0;
                for (; i < arr1.Length; i++)
                {
                    arr1[i] = array[i];
                }
                for (; i < array.Length; i++)
                {
                    arr2[i - arr1.Length] = array[i];
                }
                arr1 = merge_sort(arr1);
                arr2 = merge_sort(arr2);
                array = outMerge(arr1, arr2);
            }

            return array;
        }

        int[] outMerge(int[] arr1, int[] arr2)
        {

            int[] res_arr = new int[arr1.Length + arr2.Length];

            int t1 = 0, t2 = 0, t3 = 0;

            while ((t1 != arr1.Length) && (t2 != arr2.Length))
            {
                if (arr1[t1] > arr2[t2])
                {

                    res_arr[t3] = arr1[t1];
                    t1++; t3++;
                }
                else
                {
                    res_arr[t3] = arr2[t2];
                    t2++; t3++;
                }
            }
            try
            {
                while (t1 != arr1.Length)
                {
                    res_arr[t3] = arr1[t1];
                    t1++; t3++;
                }
            } catch { }
            try
            {
                while (t2 != arr2.Length)
                {
                    res_arr[t3] = arr2[t2];
                    t2++; t3++;
                }
            } catch { }

            return res_arr;
        }

        int[,] create_random_arr(int N, int M)
        {
            int[,] nArr = new int[N, M];
            Random rnd = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    nArr[i, j] = rnd.Next(10, 99);
                    Console.Write(nArr[i, j] + " ");
                }
                Console.WriteLine();
            }
            return nArr;
        }

        int[,] get_example_array()
        {
            N = 3; M = 3;
            int[,] nArr = { {0,0,6},
                            {0,4,0 },
                            {8,0,9 }, };
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(nArr[i, j] + " ");
                }
                Console.WriteLine();
            }
            return nArr;
        }

        int[,] get_user_matrix(int N, int M)
        {
            int[,] nArr = new int[N, M];
            for (int i = 0; i < N; i++)
            {
                string[] tmp = Console.ReadLine().Split(' ');
                for (int j = 0; j < M; j++)
                {
                    nArr[i, j] = int.Parse(tmp[j]);
                }
            }
            return nArr;
        }
        int N = 0, M = 0;
        void get_sorted_matrix(int[,] matrix)
        {
            List<int> temp_list = new List<int>();

            for (int i = 0; i < Math.Min(M, N); i++)
            {
                int elem_to_add;
                if (M - N > 0)
                {
                    elem_to_add = M - N + i;
                }
                else elem_to_add = i;

                temp_list.Add(matrix[N - i - 1, elem_to_add]);
            }
            int lenght = temp_list.Count;

            for (int i = 0; i < lenght / 2; i++)
            {
                temp_list.Add(matrix[N - i - 1, matrix.Length / N - i - 1]);
            }
            int[] sort_arr = merge_sort(temp_list.ToArray());
            int k = 0;
            for (int i = 0; i < Math.Min(matrix.Length / N, N); i++)
            {
                if(M - N > 0)
                    matrix[N - i - 1, M - N + i] = sort_arr[k];

                else
                    matrix[N - i - 1, i] = sort_arr[k];

                k++;
            }

            for (int i = 0; i < lenght / 2; i++)
            {
                matrix[N - i - 1, matrix.Length / N - i - 1] = sort_arr[k];
                k++;
            }
            Console.WriteLine("Результат: ");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < matrix.Length / N; j++)
                {
                    if (is_sorted(i,j,matrix.Length))
                        Console.BackgroundColor = ConsoleColor.DarkRed;

                    Console.Write(matrix[i, j] + " ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
        }
        bool is_sorted(int i, int j, int lenght)
        {
            if(M - N < 0)
            {
                if (i + M - N == Math.Min(lenght / N, N) - j - 1 ||
                   (i + M - N > Math.Min(lenght / N, N) - j - 1) && (N - i == M - j))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (i == Math.Min(lenght / N, N) - j - 1 + (M - N > 0 ? (M - N) : 0) ||
                    (i > Math.Min(lenght / N, N) - j - 1) && (N - i == M - j))
                {
                    return true;
                }
                return false;
            }
        }
        void user_interface(string input_choise)
        {
            while (input_choise != "4")
            {
                input_choise = Console.ReadLine();

                if (input_choise == "1")
                {
                    Console.WriteLine("Введіть N");
                    N = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введіть M");
                    M = int.Parse(Console.ReadLine());
                    get_sorted_matrix(create_random_arr(N, M));
                }
                else if (input_choise == "2")
                {
                    get_sorted_matrix(get_example_array());
                }
                else if (input_choise == "3")
                {
                    Console.WriteLine("Введіть N");
                    N = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введіть M");
                    M = int.Parse(Console.ReadLine());
                    get_sorted_matrix(get_user_matrix(N, M));
                }
                else
                {
                    Console.WriteLine("Оберіть дію: " +
                        "\n1 - Напишіть щоб випадкого згенерувати матрицю" +
                        "\n2 - щоб вивести контрольний приклад роботи програми" +
                        "\n3 - щоб ввести свою матрицю" +
                        "\n4 - зупинити програму\n\n");
                }
            }
        }
    }
}
