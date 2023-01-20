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

namespace HW_WpfRevenueCalc2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Пуск таймера для проверки времени работы программы
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            decimal total = 0;
            object lockObj = new object();

            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                string incomeFile = @"D:\FilesToRead\income.txt";
                string[] incomeLines = System.IO.File.ReadAllLines(incomeFile);
                foreach (string line in incomeLines)
                {
                    //lockTotalCounter.AddTotal(decimal.Parse(line));
                    lock (lockObj) { total += decimal.Parse(line); }
                }

                if (tasks[1].IsCompleted)
                {
                    // Останов таймера
                    stopWatch.Stop();
                    // Получение прошедшего времени
                    TimeSpan ts = stopWatch.Elapsed;
                    Dispatcher.Invoke(() => textBoxTime.Text = $"Время вычисления: {ts.Milliseconds.ToString()} мс.");
                    Dispatcher.Invoke(() => textBoxTotal.Text = $"Прибыль: {total.ToString()}");
                }
            });

            tasks[1] = Task.Factory.StartNew(() =>
            {
                string outcomeFile = @"D:\FilesToRead\outcome.txt";
                string[] outcomeLines = System.IO.File.ReadAllLines(outcomeFile);
                foreach (string line in outcomeLines)
                {
                    lock (lockObj) { total -= decimal.Parse(line); }
                }

                if (tasks[0].IsCompleted)
                {
                    // Останов таймера
                    stopWatch.Stop();
                    // Получение прошедшего времени
                    TimeSpan ts = stopWatch.Elapsed;
                    Dispatcher.Invoke(() => textBoxTime.Text = $"Время вычисления: {ts.Milliseconds.ToString()} мс.");
                    Dispatcher.Invoke(() => textBoxTotal.Text = $"Прибыль: {total.ToString()}");
                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Пуск таймера для проверки времени работы программы
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            decimal totalIncome = 0;
            decimal totalOutcome = 0;
            object lockObj = new object();

            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                string incomeFile = @"D:\FilesToRead\income.txt";
                string[] incomeLines = System.IO.File.ReadAllLines(incomeFile);
                foreach (string line in incomeLines)
                {
                    lock (lockObj) 
                    { totalIncome += decimal.Parse(line); }
                }

                if (tasks[1].IsCompleted)
                {
                    // Останов таймера
                    stopWatch.Stop();
                    // Получение прошедшего времени
                    TimeSpan ts = stopWatch.Elapsed;
                    Dispatcher.Invoke(() => textBoxTime.Text = $"Время вычисления: {ts.Milliseconds.ToString()} мс.");
                    Dispatcher.Invoke(() => textBoxTotal.Text = $"Прибыль: {(totalIncome - totalOutcome).ToString()}");
                }
            });

            tasks[1] = Task.Factory.StartNew(() =>
            {
                string outcomeFile = @"D:\FilesToRead\outcome.txt";
                string[] outcomeLines = System.IO.File.ReadAllLines(outcomeFile);
                foreach (string line in outcomeLines)
                {
                    lock (lockObj) 
                    { totalOutcome += decimal.Parse(line); }
                }

                if (tasks[0].IsCompleted)
                {
                    // Останов таймера
                    stopWatch.Stop();
                    // Получение прошедшего времени
                    TimeSpan ts = stopWatch.Elapsed;
                    Dispatcher.Invoke(() => textBoxTime.Text = $"Время вычисления: {ts.Milliseconds.ToString()} мс.");
                    Dispatcher.Invoke(() => textBoxTotal.Text = $"Прибыль: {(totalIncome - totalOutcome).ToString()}");
                }
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Пуск таймера для проверки времени работы программы
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            decimal totalIncome = 0;
            decimal totalOutcome = 0;

            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() =>
            {
                string incomeFile = @"D:\FilesToRead\income.txt";
                string[] incomeLines = System.IO.File.ReadAllLines(incomeFile);
                foreach (string line in incomeLines)
                {
                    totalIncome += decimal.Parse(line); 
                }

                if (tasks[1].IsCompleted)
                {
                    // Останов таймера
                    stopWatch.Stop();
                    // Получение прошедшего времени
                    TimeSpan ts = stopWatch.Elapsed;
                    Dispatcher.Invoke(() => textBoxTime.Text = $"Время вычисления: {ts.Milliseconds.ToString()} мс.");
                    Dispatcher.Invoke(() => textBoxTotal.Text = $"Прибыль: {(totalIncome - totalOutcome).ToString()}");
                }
            });

            tasks[1] = Task.Factory.StartNew(() =>
            {
                string outcomeFile = @"D:\FilesToRead\outcome.txt";
                string[] outcomeLines = System.IO.File.ReadAllLines(outcomeFile);
                foreach (string line in outcomeLines)
                {
                    totalOutcome += decimal.Parse(line); 
                }

                if (tasks[0].IsCompleted)
                {
                    // Останов таймера
                    stopWatch.Stop();
                    // Получение прошедшего времени
                    TimeSpan ts = stopWatch.Elapsed;
                    Dispatcher.Invoke(() => textBoxTime.Text = $"Время вычисления: {ts.Milliseconds.ToString()} мс.");
                    Dispatcher.Invoke(() => textBoxTotal.Text = $"Прибыль: {(totalIncome - totalOutcome).ToString()}");
                }
            });
        }
    }
}
