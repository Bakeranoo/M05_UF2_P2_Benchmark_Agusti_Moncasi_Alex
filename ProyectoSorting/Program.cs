using System;
using System.Diagnostics;

namespace ProyectoSorting
{
    public class SortingArray
    {
        public int[] array;
        public int[] arrayCreciente;
        public int[] arrayDecreciente;

        public SortingArray(int elements, Random random)
        {
            array = new int[elements];
            arrayCreciente = new int[elements];
            arrayDecreciente = new int[elements];
            for (int i = 0; i < elements; i++)
            {
                array[i] = random.Next(100);
            }
            Array.Copy(array, arrayCreciente, elements);
            Array.Sort(arrayCreciente);
            Array.Copy(arrayCreciente, arrayDecreciente, elements);
            Array.Reverse(arrayDecreciente);
        }

        public void Sort(Action<int[]> func)
        {
            Stopwatch time = new Stopwatch();
            int[] temp = new int[array.Length];
            Array.Copy(array, temp, array.Length);

            Console.WriteLine(func.Method.Name);

            time.Start();

            func(temp);

            time.Stop();

            Console.WriteLine("Initial: " + time.ElapsedMilliseconds + "ms " + time.ElapsedTicks + "ticks");

            time.Reset();

            time.Start();

            func(temp);

            time.Stop();

            Console.WriteLine("Increasing: " + time.ElapsedMilliseconds + "ms " + time.ElapsedTicks + "ticks");

            time.Reset();

            Array.Reverse(temp);

            time.Start();

            func(temp);

            time.Stop();

            Console.WriteLine("Decreasing: " + time.ElapsedMilliseconds + "ms " + time.ElapsedTicks + "ticks");
        }
        public void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
        public void BubbleSortEarlyExit(int[] array)
        {
            bool ordered = true;
            for (int i = 0; i < array.Length - 1; i++)
            {
                ordered = true;
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        ordered = false;
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
                if (ordered)
                    return;
            }
        }
        public void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        public void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = QuickSortPivot(array, left, right);
                QuickSort(array, left, pivot);
                QuickSort(array, pivot + 1, right);
            }
        }
        public int QuickSortPivot(int[] array, int left, int right)
        {
            int pivot = array[(left + right) / 2];
            while (true)
            {
                while (array[left] < pivot)
                {
                    left++;
                }
                while (array[right] > pivot)
                {
                    right--;
                }
                if (left >= right)
                {
                    return right;
                }
                else
                {
                    int temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;
                    right--; left++;
                }
            }
        }

        public void InsertionSort(int[] array)
        {
            //Consideraremos que el primer elemento del array ya esta ordenado (inv = 1).
            for (int inv = 1; inv < array.Length; inv++)
            {
                int num = array[inv];   //Cogemos el valor del número situado en la posición "inv" del array.
                int j = inv - 1;        //Guardamos la posición del último elemento de la parte ordenada.

                while (j >= 0 && array[j] > num) //Mientras que no hayamos llegado al primer valor ordenado y los valores sean menores al guardado en "num", 
                {
                    array[j + 1] = array[j];    //Correremos los valores una posición a la derecha (simplemente nos desaparecera la posición de array[inv] y no afectará
                                                //al array ya que lo tenemos guardado en la variable "num" y posteriormente la colocaremos en el espació que quedará libre).
                    j--;
                }
                array[j + 1] = num;     //Cuando los valores ya no sean más grandes que "num" pondremos el valor en el espacio vacío (j + 1).
            }
        }

        public void SelectionSort(int[] array)
        {
            //Recorremos todo el array
            for (int i = 0; i < array.Length - 1; i++)
            {
                //Creamos la variable "min" donde guardaremos la posición donde pondremos el valor más bajo una vez detectado. En esta ocasión guardamos "i" ya que
                //si ninguno de los valores es más pequeño que el de dicha posición, no hace falta ordenarlo.
                int min = i;
                //Ahora recorremos el array desde la última posición ordenada hasta el final del array.
                for (int j = i + 1; j < array.Length; j++)
                {
                    //Comparamos los valores del array con el primer elemento no ordenado. En caso de que alguno sea menor, guardaremos en "min" el índice de dicho valor
                    //de la array. De esta manera siempre obtendremos el valor mínimo.
                    if (array[j] < array[min])
                    {
                        min = j;
                    }
                }
                //Finalmente cambiamos los valores de posición con la función swap.
                swap(array, i, min);
            }
        }

        public void swap(int[] array, int x, int y)
        {
            int aux = array[x];
            array[x] = array[y];
            array[y] = aux;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many elements do you want?");
            int elements = int.Parse(Console.ReadLine());

            Console.WriteLine("What seed do you want to use?");
            int seed = int.Parse(Console.ReadLine());

            Random random = new Random(seed);
            SortingArray array = new SortingArray(elements, random);
            array.Sort(array.BubbleSort);
            array.Sort(array.BubbleSortEarlyExit);
            array.Sort(array.QuickSort);
            array.Sort(array.InsertionSort);
            array.Sort(array.SelectionSort);
        }
    }
}
