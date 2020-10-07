namespace Algorithms.Mfti
{
    public class QuickSorter
    {
        /// <summary>
        /// Быстрая сортировка.
        /// </summary>
        /// <param name="input">Сортируемый массив.</param>
        public void Sort(int[] input)
        {
            QuickSort(input, 0, input.Length - 1);
        }

        /// <summary>
        /// Быстрая сортировка с разбиением Хоара.
        /// </summary>
        /// <param name="array">Сортируемый массив.</param>
        /// <param name="left">Индекс начального положения слева.</param>
        /// <param name="right">Индекс начального положения справа.</param>
        private void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int jIndex = Partition(array, left, right);
                QuickSort(array, left, jIndex); // Сортировка левой части после разбиения.
                QuickSort(array, jIndex + 1, right); // Сортировка правой части после разбиения.
            }
        }

        /// <summary>
        /// Разбиение Хоара.
        /// Опрорным элементом является серидина.
        /// Идем к середине с двух сторон.
        /// </summary>
        /// <param name="array">Сортируемый массив.</param>
        /// <param name="left">Индекс начального положения слева.</param>
        /// <param name="right">Индекс начального положения справа.</param>
        /// <returns>Индес элемента, с которого начинаются элементы большие или равные опорному).</returns>
        private int Partition(int[] array, int left, int right)
        {
            int v = array[(left + right) / 2];
            int i = left;
            int j = right;
            while (i <= j)
            {
                // Определяем левый элемент для свапа.
                while (array[i] < v)
                    i++;

                // Определяем правый элемент для свапа.
                while (array[j] > v)
                    j--;

                // Нечего свапать.
                if (i >= j)
                    break;

                int buf = array[i];
                array[i] = array[j];
                array[j] = buf;
                i++;
                j--;
            }
            return j;
        }

        /// <summary>
        /// Упорядочивание входной последовательности для максимизации количества перестановок при быстрой сортировке.
        /// </summary>
        /// <param name="input">Упорядочиваемый массив.</param>
        public void AntiSort(int[] input)
        {
            for (int i = 2; i < input.Length; i++)
            {
                int buf = input[i];
                input[i] = input[i / 2];
                input[i / 2] = buf;
            }
        }

        /// <summary>
        /// Получение массив с установленными K порядковыми статистиками.
        /// </summary>
        /// <param name="array">Массив для сортировки.</param>
        /// <param name="k1">Индекс первой статистики интервала.</param>
        /// <param name="kn">Индекс последней статистики интервала.</param>
        public void GetKStats(int[] array, int k1, int kn)
        {
            GetKStatsByQuickSort(array, 0, array.Length - 1, k1, kn);
        }

        /// <summary>
        /// Получение массив с установленными K порядковыми статистиками с помощью быстрой сортировки.
        /// </summary>
        /// <param name="array">Массив для сортировки.</param>
        /// <param name="left"></param>
        /// <param name="rigth"></param>
        /// <param name="k1">Индекс первой статистики интервала.</param>
        /// <param name="kn">Индекс последней статистики интервала.</param>
        private void GetKStatsByQuickSort(int[] array, int left, int rigth, int k1, int kn)
        {
            if (left < rigth)
            {
                int jIndex = Partition(array, left, rigth);
                if (k1 <= jIndex && kn <= jIndex)
                    GetKStatsByQuickSort(array, left, jIndex, k1, kn);
                else if (k1 > jIndex && kn > jIndex)
                    GetKStatsByQuickSort(array, jIndex + 1, rigth, k1, kn);
                else
                {
                    GetKStatsByQuickSort(array, left, jIndex, k1, kn);
                    GetKStatsByQuickSort(array, jIndex + 1, rigth, k1, kn);
                }
            }
        }
    }
}
