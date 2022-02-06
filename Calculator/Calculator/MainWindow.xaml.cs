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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber;
        string result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            
            // add listeners
            _acButton.Click += _acButton_Click;
            _negativeButton.Click += _negativeButton_Click;
            _percentageButton.Click += _percentageButton_Click;
            _equalButton.Click += _equalButton_Click;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(_resultLabel.Content.ToString(), out lastNumber))
            {
                _resultLabel.Content = "0";
            }

            switch (((Button)sender).Content.ToString())
            {
                case "+":
                    selectedOperator = SelectedOperator.PLUS;
                    break;
                case "-":
                    selectedOperator = SelectedOperator.MINUS;
                    break;
                case "/":
                    selectedOperator = SelectedOperator.DIV;
                    break;
                case "*":
                    selectedOperator = SelectedOperator.MULT;
                    break;
            }

        }

        private void _equalButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(_resultLabel.Content.ToString(), out newNumber))
            {
                switch(selectedOperator)
                {
                    case SelectedOperator.PLUS:
                        result = SimpleMath.Plus(lastNumber, newNumber);
                        break;
                    case SelectedOperator.MINUS:
                        result = SimpleMath.Minus(lastNumber, newNumber);
                        break;
                    case SelectedOperator.MULT:
                        result = SimpleMath.Mult(lastNumber, newNumber);
                        break;
                    case SelectedOperator.DIV:
                        result = SimpleMath.Div(lastNumber, newNumber);
                        break;
                }

                _resultLabel.Content = result;
            }
        }

        private void _percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tmpNumber;
            if (double.TryParse(_resultLabel.Content.ToString(), out tmpNumber))
            {
                tmpNumber /= 100;
                if (lastNumber != 0)
                {
                    tmpNumber *= lastNumber;
                }
                _resultLabel.Content = (tmpNumber).ToString();
            }
        }

        private void _negativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(_resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                _resultLabel.Content = lastNumber.ToString();
            }
        }

        private void _acButton_Click(object sender, RoutedEventArgs e)
        {
            _resultLabel.Content = "0";
            result = "0";
            lastNumber = 0;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var content = ((Button)sender).Content.ToString();
            if (_resultLabel.Content.ToString() == "0")
            {
                _resultLabel.Content = content;
            }
            else
            {
                _resultLabel.Content += content;
            }
        }

        private void _dotButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_resultLabel.Content.ToString().Contains("."))
            {
                _resultLabel.Content += ".";
            }
        }
    }

    public enum SelectedOperator
    {
        PLUS,
        MINUS,
        MULT,
        DIV
    }

    public class SimpleMath
    {
        public static string Plus(double n1, double n2)
        {
            return (n1 + n2).ToString();
        }
        public static string Minus(double n1, double n2)
        {
            return (n1 - n2).ToString();
        }
        public static string Mult(double n1, double n2)
        {
            return (n1 * n2).ToString();
        }
        public static string Div(double n1, double n2)
        {
            if (n2 != 0)
            {
                return (n1 / n2).ToString();
            }
            else
            {
                MessageBoxResult r =  MessageBox.Show("Are you Linus Torvalds?", "Quiz", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (r == MessageBoxResult.Yes)
                {
                    return "6.66";
                }
                else
                {
                    return "0";
                }
            }
        }
    }
}
