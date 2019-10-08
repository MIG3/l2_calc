using System;
using System.Collections.Generic;
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

namespace Calc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string leftOperand = "", rightOperand = "", operation = "";
        List<string> historyOp = new List<string>();

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Получаем текст кнопки
            string symbol = (string)((Button)e.OriginalSource).Content;
            textBlock.Text += symbol;
            //if (symbol != "del")
                //history.Text += symbol;
            int number;
            bool result = Int32.TryParse(symbol, out number);
            if (result)
            {
                // Если операция не задана
                if (operation == "")
                    // Добавляем к левому операнду
                    leftOperand += symbol;
                else
                    // Иначе к правому операнду
                    rightOperand += symbol;
            }
            else
            {
                // Если равно, то выводим результат операции
                if (symbol == "=")
                {
                    Update_RightOp();
                    textBlock.Text += rightOperand;
                    //history.Text += rightOperand;
                    historyOp.Add(textBlock.Text);
                    operation = "";
                }
                // Очищаем поле и переменные
                else if (symbol == "del")
                {
                    leftOperand = "";
                    rightOperand = "";
                    operation = "";
                    textBlock.Text = "";
                    //history = history1;
                }
                else if (symbol == "↺")
                {
                    textBlock.Text = "";
                    //list.Items.Add()
                    list.ItemsSource = historyOp;
                }
                // Получаем операцию
                else
                {
                    // Если правый операнд уже имеется, то присваиваем его значение левому
                    // операнду, а правый операнд очищаем
                    if (rightOperand != "")
                    {
                        Update_RightOp();
                        leftOperand = rightOperand;
                        rightOperand = "";
                    }
                    operation = symbol;
                }
            }
        }

        private void Update_RightOp()
        {
            int a = Int32.Parse(leftOperand);
            int b = Int32.Parse(rightOperand);
            // И выполняем операцию
            switch (operation)
            {
                case "+":
                    rightOperand = (a + b).ToString();
                    break;
                case "-":
                    rightOperand = (a - b).ToString();
                    break;
                case "*":
                    rightOperand = (a * b).ToString();
                    break;
                case "/":
                    if (b != 0)
                        rightOperand = (a / b).ToString();
                    else
                    {
                        rightOperand = null;
                        textBlock.Text = "Деление на ноль невозможно";
                    }
                    break;
            }
        }



        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                    ((Button)c).Click += ButtonClick;
            }
        }
    }
}
