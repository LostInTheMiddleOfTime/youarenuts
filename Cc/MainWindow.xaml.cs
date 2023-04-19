using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Cc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // sender - тот кто создал событие 
        private void Button_Click_Num(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button; // Преобразовать к типу кнопки
            if (b != null)
            {
                string txt = b.Content.ToString(); // То что написано на кнопке
                Pu.Text += txt;  // Добавляю это в текстбокс
            }
        }
        private void Button_Click_Deistv(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button; // Преобразовать к типу кнопки
            if (b != null)
            {
                string txt = b.Content.ToString(); // То что написано на кнопке
                string deistv = T2.Content.ToString();

                if (deistv.Length > 0) // Если действие уже нажато
                {
                    // Заменяю действие
                    deistv = deistv.Substring(0, deistv.Length - 1);
                    deistv += txt;
                    T2.Content = deistv;
                }
                else
                {
                    string num1 = Pu.Text;
                    num1 += txt;
                    T2.Content = num1;
                    Pu.Text = "";
                }
            }
        }
        private void Button_Click_Point(object sender, RoutedEventArgs e)
        {//Постановка дес. дроби
            string cStr = Pu.Text;
            if (cStr.IndexOf(",") >= 0)
            {
                return;
            }
            else
            {
                if (cStr.Length == 0){cStr += "0";}
                cStr += ",";
                Pu.Text = cStr;
            }

        }
        private void Button_Click_e(object sender, RoutedEventArgs e)
        {//Равно
            T.Text = "";
            string num1 = T2.Content.ToString();
            string num2 = Pu.Text;
            double a, b;
            if (num1.Length == 0)
            {
                return;
            }
            string deistv = num1.Substring(num1.Length - 1, 1); // Последний символ
            num1 = num1.Substring(0, num1.Length - 1); // Все без последнего символа
            
            try {a = double.Parse(num1);b = double.Parse(num2);}
            catch
            {
                if (deistv == "√")
                {
                    //MessageBox.Show("Please enter: *number* √ *degree of the root*");
                    try{b = double.Parse(num2);a = 2;}
                    catch { MessageBox.Show("Mischief Managed!"); return; }

                }
                else if (deistv == "-")
                {
                    try{a = 0;b = double.Parse(num2);}
                    catch{MessageBox.Show("Mischief Managed!");return;}
                }
                else if (deistv == "!")
                {
                    try
                    {
                        a = double.Parse(num1);
                        try { b = double.Parse(num2); }
                        catch { b = 1; }
                        if (a % 1 > 0) { MessageBox.Show("x! x ∈ N"); return; }
                    }
                    catch{MessageBox.Show("Mischief Managed!");return;}
                }
                else if (deistv == "π")
                {
                    try
                    {
                        a = double.Parse(num1);
                        b = 1; 
                    }
                    catch { a = 1; b = 1; }
                }
                else { MessageBox.Show("Mischief Managed!"); return; }//Если что-то ускользнуло 

        }
        double res = 0;
            if (deistv == "+") res = a + b;
            if (deistv == "-") res = a - b;
            if (deistv == "×") res = a * b;
            if (deistv == "/")
            {
                if (b == 0) MessageBox.Show("undefined  x/y, y≠0");
                else res = a / b;
            }
            if (deistv == "√") res = Math.Pow(b, 1 / a);//Корни (включая дробные и отрицательные)
            if (deistv == "^") res = Math.Pow(a, b);//Степень
            if (deistv == "π") { res = Math.PI * a; T.Text = $"{a}*π"; }
            if (deistv == "!") //Факториал 
            {
                res = 1;
                while (a > 0)
                {
                    res = res * a; 
                    a -= 1;
                }
                res = res * b;
            }
            Pu.Text = res.ToString();
            T2.Content = "";
        }
        private void Button_Click_BS(object sender, RoutedEventArgs e)
        {
            string st = Pu.Text;
            if (st.Length > 0)
            {
                st = st.Substring(0, st.Length - 1); // Все без последнего символа
                Pu.Text = st;
            }
        }
        private void Button_Click_C(object sender, RoutedEventArgs e)
        {//Очистка всех полей ввода
            Pu.Text = "";
            T2.Content = "";
            T.Text = "";
        }
        private void Button_Click_asin(object sender, RoutedEventArgs e)
        {
            string num = Pu.Text;
            double a=0, resd = 0, res = 0;
            if (num.Length > 0)
            {
                a = double.Parse(num);
                if (a < -1) { MessageBox.Show("asin(x)  -1>x>1"); return; } //Проверка на возможность 
                if (a > 1) { MessageBox.Show("asin(x) - 1>x>1"); return; }
                try { res = Math.Asin(a); }
                catch { MessageBox.Show("Mischief Managed!"); return; }
                // Если что-то упустили при проверке (или кто-то умный решит, но не сможет исправить)
                resd = res/Math.PI*180;
                //if ( pm.Text == "1")
                //res = res/Math.PI*180;
            }
            Pu.Text = res.ToString(); 
            T2.Content = "";
            T.Text = $"asin({a})="+resd.ToString()+ "°"; //В градусах на доп. поле
        }
        private void Button_Click_acos(object sender, RoutedEventArgs e)
        {
            string num = Pu.Text;
            double a=0,resd = 0, res = 0;
            if (num.Length > 0)
            {
                a = double.Parse(num);
                if (a < -1) { MessageBox.Show("acos(x)  -1>x>1"); return; }
                if (a > 1) { MessageBox.Show("acos(x) - 1>x>1"); return; }
                try { res = Math.Acos(a); }
                catch { MessageBox.Show("Mischief Managed!"); return; } 
                resd = res/Math.PI * 180;
            }
            Pu.Text = res.ToString();
            T2.Content = "";
            T.Text = $"cos({a}) "+resd.ToString()+ "°";
        }

    }
}
/*private void Button_Click_gr(object sender, RoutedEventArgs e)
        {
            T.Text = "Start";
            Button b = sender as Button; // Преобразовать к типу кнопки
            if (b != null)
            {
                string gr = b.Content.ToString();
 
                if (gr == "Rad")
                {
                   gr = "Deg";
                    T.Text = "1";
                }
                if (gr == "Deg")
                {
                    gr = "Rad";
                    T.Text = "0";
                }
                b.Content = gr;
            }

                <Button x:Name="switch" Content="Rad" Grid.Column="0" Grid.Row="1" FontSize="16" FontWeight="Bold" FontFamily="Poor Richard" Background="#FFD1EDDF" Click="Button_Click_gr"/>
        <TextBox x:Name="pm" TextWrapping="Wrap" Text="" Background="#FFE1F9ED" Height="0" FontWeight="Bold" FontFamily="Old English Text MT" Grid.Row="4" Grid.ColumnSpan="4" HorizontalContentAlignment="Stretch" VerticalContentAlignment="Stretch" Width="0"/>
    

        }*/