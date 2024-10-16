using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ПР6
{
    public partial class MainWindow : Window
    {
        private int[] _array;// Массив для хранения случайных чисел

        public MainWindow()//Конструктор окна
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)//Обработчик события нажатия кнопки
        {
            Random random = new Random();// Создаем генератор случайных чисел
            int length = random.Next(10, 101);//Длина массива от 10 до 100
            _array = new int[length];// Инициализируем массив заданной длины(Инициализация (от англ. initialization, инициирование) — приведение цифрового устройства или его программы в состояние готовности к использованию.)


            for (int i = 0; i < length; i++)
            {
                _array[i] = random.Next(-100, 101);//Заполняем массив случайными числами от -100 до 100
            }

            ResultTextBlock.Text = "Сгенерированный массив: " + string.Join(", ", _array);// Отображаем сгенерированный массив в текстовом блоке
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)// Проверяем сгенерирование массива
        {
            if (_array == null)
            {
                MessageBox.Show("Массив сгенирируй да?!");// Ошибочка
                return;
            }

            var selectedAlgorithm = (SortAlgorithmComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();// Клонируем массив для сортировки 
            int[] arrayToSort = (int[])_array.Clone();// Запускаем таймер
            Stopwatch stopwatch = Stopwatch.StartNew();// Выбираем алгоритм сортировки в зависимости от выбора пользователя

            switch (selectedAlgorithm)
            {
                case "Сортировка пузырьком":
                    BubbleSort(arrayToSort);// Вызываем метод пузырьковой сортировки
                    break;
                case "Сортировка вставками":
                    InsertionSort(arrayToSort);// Вызываем метод сортировки вставками
                    break;
                case "Сортировка выбором":
                    SelectionSort(arrayToSort);// Вызываем метод опять ток другой
                    break;
                case "Быстрая сортировка":
                    QuickSort(arrayToSort);// Вызываем метод быстрой сортировки
                    break;
                default:
                    MessageBox.Show("Выбери алгоритм уже");// Сообщаем, если алгоритм не выбран
                    break;
            }

            stopwatch.Stop();// Останавливаем таймер
            ResultTextBlock.Text += $"\nОтсортированный массив: {string.Join(", ", arrayToSort)}";// Выводим отсортированный массив в текстовый блок
            ResultTextBlock.Text += $"\nВремя сортировки: {stopwatch.ElapsedMilliseconds} мс";//Выводим время сортировки в текстовый блок
        }


        private void BubbleSort(int[] array)
        {
            int n = array.Length;// Получаем длину массива
            for (int i = 0; i < n - 1; i++)// Внешний цикл по элементам массива
                for (int j = 0; j < n - i - 1; j++)// Внутренний цикл для сравнения соседних элементов
                    if (array[j] > array[j + 1])// Если текущий элемент больше следующего
                    {
                        int temp = array[j];// Меняем их местами
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
        }

        private void InsertionSort(int[] array)
        {
            int n = array.Length;// Меняем их местами
            for (int i = 1; i < n; i++)
            {
                int key = array[i];// Сохраняем текущий элемент
                int j = i - 1;// Индекс предыдущего элемента

                while (j >= 0 && array[j] > key)// Сдвигаем элементы, которые больше ключа
                {
                    array[j + 1] = array[j];// Сдвигаем элемент вправо
                    j--;// Переходим к следующему элементу слева
                }
                array[j + 1] = key;// Вставляем ключ на его место
            }
        }

        private void SelectionSort(int[] array)
        {
            int n = array.Length;// Получаем длину массива
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;// Предполагаем, что текущий элемент минимальный
                for (int j = i + 1; j < n; j++)// Находим индекс минимального элемента в оставшейся части массива
                    if (array[j] < array[minIndex])
                        minIndex = j;
                // Меняем местами минимальный элемент с текущим
                int temp = array[minIndex];
                array[minIndex] = array[i];
                array[i] = temp;
            }
        }

        private void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);// Запускаем быструю сортировку с начальным и конечным индексом
        }

        private void QuickSort(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(array, low, high);// Находим индекс опорного элемента
                QuickSort(array, low, pi - 1);// Рекурсивно сортируем левую часть массива
                QuickSort(array, pi + 1, high);// Рекурсивно сортируем правую часть массива
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];// Опорный элемент - последний элемент в массиве
            int i = (low - 1);// Индекс меньшего элемента

            for (int j = low; j < high; j++)// Проходим по всем элементам от low до high-1
            {
                if (array[j] < pivot)// Если текущий элемент меньше опорного
                {
                    i++;// Увеличиваем индекс меньшего элемента
                    int temp = array[i];// Меняем местами элементы
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];// Меняем опорный элемент на его правильное место
            array[i + 1] = array[high];
            array[high] = temp1;

            return i + 1;// Возвращаем индекс опорного элемента
        }
    }
}
