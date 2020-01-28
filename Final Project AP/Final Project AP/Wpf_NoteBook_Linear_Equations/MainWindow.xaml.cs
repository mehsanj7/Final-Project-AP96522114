using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Wpf_NoteBook_Linear_Equations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManageDb _objExcelSer;
        Information info = new Information();

        static int linex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        //

        private static readonly Regex _regexEqu = new Regex("^(.*)\\1{16}(.*)\\2{11}$");
        private static bool IsEquCheck(string text)
        {
            return _regexEqu.IsMatch(text);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int a = 0, b = 0, c = 0, d = 0, f = 0, g = 0, h = 0, i = 0, j = 0, k = 0, m = 0, n = 0;
            _objExcelSer = new ManageDb();
            if (txtName.Text == "" || txtLName.Text == "" || txtCity.Text == "" || txtAge.Text == "" || txtEqu1.Text == "" || txtEqu2.Text == "")
            {
                MessageBox.Show("پر کردن تمامی فیلدها الزامی میباشد! معادله سه اختیاری می باشد");
            }
            else
            {
                //
                info.ID = 5;
                info.FName = txtName.Text;
                info.Lname = txtLName.Text;
                info.City = txtCity.Text;
                info.Age = int.Parse(txtAge.Text);
                info.Equx1 = txtEqu1.Text;
                info.Equx2 = txtEqu2.Text;

                if (txtEqu1.Text == txtEqu2.Text)
                {
                    MessageBox.Show("هر دو معادله یکسان می باشد");
                    return;
                }
                if (_objExcelSer.ExistData(info) == true)
                {
                    MessageBox.Show("برخی از داده ها تکراری و از قبل در دیتابیس موجود می باشد");
                    return;
                }

                string m1 = txtEqu1.Text;
                string m2 = txtEqu2.Text;
                string m3 = txtEqu3.Text;

                if (m1.Contains("z"))
                {
                    if (m1.Contains("x") && m1.Contains("y") && m1.Contains("=") && m1.Contains("z"))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-", "z" };
                        Int32 count = 6;

                        String[] strlist = m1.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        a = int.Parse(strlist[0]);
                        b = int.Parse(strlist[1]);
                        c = int.Parse(strlist[2]);
                        d = int.Parse(strlist[3]);
                    }
                    else
                    {
                        MessageBox.Show("معادله سه مجهولی یک اشتباه می باشد");
                        return;
                    }
                    if (m2.Contains("x") && m2.Contains("y") && m2.Contains("=") && m2.Contains("z"))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-", "z" };
                        Int32 count = 6;

                        String[] strlist = m2.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        f = int.Parse(strlist[0]);
                        g = int.Parse(strlist[1]);
                        h = int.Parse(strlist[2]);
                        i = int.Parse(strlist[3]);
                    }
                    else
                    {
                        MessageBox.Show("معادله سه مجهولی دو اشتباه می باشد");
                        return;
                    }
                    if (m3.Contains("x") && m3.Contains("y") && m3.Contains("=") && m3.Contains("z"))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-", "z" };
                        Int32 count = 6;

                        String[] strlist = m3.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        j = int.Parse(strlist[0]);
                        k = int.Parse(strlist[1]);
                        m = int.Parse(strlist[2]);
                        n = int.Parse(strlist[3]);
                    }
                    else
                    {
                        MessageBox.Show("معادله سه مجهولی سه اشتباه می باشد");
                        return;
                    }

                    double[,] zarib = {{ a, b, c, d },
                        { f, g, h, i },
                        { j, k, m, n }};
                    info.Answer = Equations.findSolution(zarib);
                    bool act = _objExcelSer.SaveData(info);
                    if (act == true)
                    {
                        MessageBox.Show("اطلاعات با موفقیت با معادله سه مجهولی ذخیره شد");

                    }
                    else
                    {
                        MessageBox.Show("ثبت نشد");
                    }

                }
                else
                {
                    if (m1.Contains("x") && m1.Contains("y") && m1.Contains("="))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-" };
                        Int32 count = 5;

                        String[] strlist = m1.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        a = int.Parse(strlist[0]);
                        b = int.Parse(strlist[1]);
                        m = int.Parse(strlist[2]);
                    }
                    else
                    {
                        MessageBox.Show("معادله یک اشتباه می باشد");
                        return;
                    }

                    if (m2.Contains("x") && m2.Contains("y") && m2.Contains("="))
                    {
                        String[] spearator = { "x", "y", "=", "+", "-" };
                        Int32 count = 5;

                        String[] strlist = m1.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        c = int.Parse(strlist[0]);
                        d = int.Parse(strlist[1]);
                        n = int.Parse(strlist[2]);
                    }
                    else
                    {
                        MessageBox.Show("معادله دو اشتباه می باشد");
                        return;
                    }
                    info.Answer = SolveTow(a, b, m, c, d, n);
                    bool act = _objExcelSer.SaveData(info);
                    if (act == true)
                    {
                        MessageBox.Show("اطلاعات با موفقیت با معادله دوم مجهولی ذخیره شد");

                    }
                    else
                    {
                        MessageBox.Show("ثبت نشد");
                    }
                }
            }
        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }



        public string SolveTow(double a, double b, double m, double c, double d, double n)
        {
            //ax+by=m
            //cx+dy=n

            double determinant = a * d - b * c;
            if (determinant != 0)
            {
                double x = (m * d - b * n) / determinant;
                double y = (a * n - m * c) / determinant;
                // MessageBox.Show(string.Format("result -> x ={0}, y = {1}", x, y));
                return "x= " + Math.Round(x, 2) + " * " + "y= " + Math.Round(y, 2);
            }
            else
            {
                //  MessageBox.Show("Not Found");
                return "Not Found";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            _objExcelSer = new ManageDb();
            try
            {
                dataGrid.ItemsSource = _objExcelSer.ReadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //  static List<Information> infos = new List<Information>();


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Information> infos = _objExcelSer.ReadData();

            if (txtAge1.Text == "" || txtAge2.Text == "" || txtCityS.Text == "")
            {
                MessageBox.Show("سن ها یا شهر را وارد کنید");
            }
            else
            {
                var query = from i in infos
                            where i.Age >= int.Parse(txtAge1.Text) && i.Age <= int.Parse(txtAge2.Text) && i.City == txtCityS.Text
                            select new
                            {
                                ID = i.ID,
                                FName = i.FName,
                                Lname = i.Lname,
                                City = i.City,
                                Age = i.Age,
                                Equx1 = i.Equx1,
                                Equx2 = i.Equx2,
                                Equx3 = i.Equx3,
                                Answer = i.Answer

                            };
                dataGrid.ItemsSource = query;
            }
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            var mw = new Clock
            {
                DataContext = new Clock()
            };
            mw.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnSaveList_Click(object sender, RoutedEventArgs e)
        {
            //int a=   textBox.LineCount;

            linex = textBox.GetLastVisibleLineIndex() + 1;

            string[] arr = new string[7];

            if (linex <= 7)
            {
                for (int v = 0; v < linex; v++)
                {
                    arr[v] = textBox.GetLineText(v);
                }
            }

            //**********************************************************************************//

            int a = 0, b = 0, c = 0, d = 0, f = 0, g = 0, h = 0, i = 0, j = 0, k = 0, m = 0, n = 0;



            _objExcelSer = new ManageDb();
            if (linex < 6)
            {
                MessageBox.Show("پر کردن تمامی فیلدها الزامی میباشد! معادله سه اختیاری می باشد");
                return;
            }
            if (linex == 6)
            {
                string m1 = arr[4].Trim();
                string m2 = arr[5].Trim();

                if (!IsTextAllowed(arr[3].Trim()))
                {
                    MessageBox.Show("سن باید به عدد وارد شود");
                    return;
                }

                info.ID = 5;
                info.FName = arr[0].Trim();
                info.Lname = arr[1].Trim();
                info.City = arr[2].Trim();
                info.Age = int.Parse(arr[3].Trim());
                info.Equx1 = arr[4].Trim();
                info.Equx2 = arr[5].Trim();
                info.Equx3 = arr[6];

                if (m1 == m2)
                {
                    MessageBox.Show("هر دو معادله یکسان می باشد");
                    return;
                }
                if (_objExcelSer.ExistData(info) == true)
                {
                    MessageBox.Show("برخی از داده ها تکراری و از قبل در دیتابیس موجود می باشد");
                    return;
                }

                if (m1.Contains("x") && m1.Contains("y") && m1.Contains("="))
                {

                    String[] spearator = { "x", "y", "=", "+", "-" };
                    Int32 count = 5;

                    String[] strlist = m1.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                    a = int.Parse(strlist[0]);
                    b = int.Parse(strlist[1]);
                    m = int.Parse(strlist[2]);
                }
                else
                {
                    MessageBox.Show("معادله یک اشتباه می باشد");
                    return;
                }

                if (m2.Contains("x") && m2.Contains("y") && m2.Contains("="))
                {
                    String[] spearator = { "x", "y", "=", "+", "-" };
                    Int32 count = 5;

                    String[] strlist = m1.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                    c = int.Parse(strlist[0]);
                    d = int.Parse(strlist[1]);
                    n = int.Parse(strlist[2]);
                }
                else
                {
                    MessageBox.Show("معادله دو اشتباه می باشد");
                    return;
                }
                info.Answer = SolveTow(a, b, m, c, d, n);
                bool act = _objExcelSer.SaveData(info);
                if (act == true)
                {
                    MessageBox.Show("اطلاعات با موفقیت با معادله دوم مجهولی ذخیره شد");

                }
                else
                {
                    MessageBox.Show("ثبت نشد");
                }
            }
            if (linex == 7 && arr[6] != "")
            {
                string m1 = arr[4].Trim();
                string m2 = arr[5].Trim();
                string m3 = arr[6].Trim();
                if (m1.Contains("z"))
                {
                    if (m1.Contains("x") && m1.Contains("y") && m1.Contains("=") && m1.Contains("z"))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-", "z" };
                        Int32 count = 6;

                        String[] strlist = m1.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        a = int.Parse(strlist[0]);
                        b = int.Parse(strlist[1]);
                        c = int.Parse(strlist[2]);
                        d = int.Parse(strlist[3]);
                    }
                    else
                    {
                        MessageBox.Show("معادله سه مجهولی یک اشتباه می باشد");
                        return;
                    }
                    if (m2.Contains("x") && m2.Contains("y") && m2.Contains("=") && m2.Contains("z"))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-", "z" };
                        Int32 count = 6;

                        String[] strlist = m2.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        f = int.Parse(strlist[0]);
                        g = int.Parse(strlist[1]);
                        h = int.Parse(strlist[2]);
                        i = int.Parse(strlist[3]);
                    }
                    else
                    {
                        MessageBox.Show("معادله سه مجهولی دو اشتباه می باشد");
                        return;
                    }
                    if (m3.Contains("x") && m3.Contains("y") && m3.Contains("=") && m3.Contains("z"))
                    {

                        String[] spearator = { "x", "y", "=", "+", "-", "z" };
                        Int32 count = 6;

                        String[] strlist = m3.Split(spearator, count, StringSplitOptions.RemoveEmptyEntries);

                        j = int.Parse(strlist[0]);
                        k = int.Parse(strlist[1]);
                        m = int.Parse(strlist[2]);
                        n = int.Parse(strlist[3]);
                    }
                    else
                    {
                        MessageBox.Show("معادله سه مجهولی سه اشتباه می باشد");
                        return;
                    }

                    double[,] zarib = {{ a, b, c, d },
                        { f, g, h, i },
                        { j, k, m, n }};
                    info.Answer = Equations.findSolution(zarib);
                    bool act = _objExcelSer.SaveData(info);
                    if (act == true)
                    {
                        MessageBox.Show("اطلاعات با موفقیت با معادله سه مجهولی ذخیره شد");

                    }
                    else
                    {
                        MessageBox.Show("ثبت نشد");
                    }

                }
                else
                {
                    MessageBox.Show("معادله سه مجهولی باید باشد!");
                    return;
                }
            }
            else if (arr[6] == "")
            {
                MessageBox.Show("پر کردن تمامی فیلدها الزامی میباشد! معادله سه الزامی می باشد اگر یک اینتر زدید میتونید حذف کنید");
                return;
            }

        }


    }
}
