using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;

namespace KIU_2016
{
    public class DataGrid
    {
        public string month { get; set; }
        public string date { get; set; }
        public string credit { get; set; }
        public string mainPay { get; set; }
        public string percent { get; set; }
        public string service { get; set; }
        public string totalPay { get; set; }
    }

    public partial class MainWindow
    {
        private double percent;
        private double servicePay;
        private double totalPay;
        private double prevPay;
        private double sumpercent;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculate_button_Click(object sender, RoutedEventArgs e)
        {
            if (diffPay.IsChecked == true)
                if (sumCredit.Text != null && stavka.Text != null && DatePicker1.SelectedDate != null &&
                    srok.Text != null && monthFee.Text != null)
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                    {
                        resultTab.IsEnabled = true;
                        solutionTab.IsEnabled = true;
                        diff_Calculate();
                        servicePay = 0;
                        sumpercent = 0;
                        resultTab.IsSelected = true;
                    }));
                else this.ShowMessageAsync("Ошибка", "Все поля должны быть заполенны");
            else
                if (sumCredit.Text != null && stavka.Text != null && DatePicker1.SelectedDate != null &&
                    srok.Text != null && monthFee.Text != null)
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    resultTab.IsEnabled = true;
                    solutionTab.IsEnabled = true;
                    annu_Calculate();
                    servicePay = 0;
                    sumpercent = 0;
                    totalPay = 0;
                    prevPay = 0;
                    resultTab.IsSelected = true;
                }));
            else this.ShowMessageAsync("Ошибка", "Все поля должны быть заполенны");

        }

        private void diff_Calculate()
        {
            mainPay2.Content = Math.Round(Convert.ToDouble(sumCredit.Text) / Convert.ToDouble(srok.Text), 2).ToString();
            servicePay = Math.Round(Convert.ToDouble(sumCredit.Text) * Convert.ToDouble(monthFee.Text) * 0.01 * Convert.ToDouble(srok.Text), 2);
            dataGrid.Items.Clear();
            listBox.Items.Clear();
            for (int i = 0; i < Convert.ToInt32(srok.Text); i++)
            {
                dataGrid.Items.Add(new DataGrid { month = Convert.ToString(i + 1), date = DatePicker1.SelectedDate.Value.AddMonths(i).ToShortDateString(), credit = Math.Round(Convert.ToDouble(sumCredit.Text) - i * (Convert.ToDouble(sumCredit.Text) / Convert.ToDouble(srok.Text)), 2).ToString(), mainPay = mainPay2.Content.ToString(), percent = Math.Round(Convert.ToDouble(mainPay2.Content) + (Convert.ToDouble(sumCredit.Text) - Convert.ToDouble(mainPay2.Content) * i) * Convert.ToDouble(stavka.Text) * 0.01 / 12 - Convert.ToDouble(mainPay2.Content), 2).ToString(), service = Math.Round(Convert.ToDouble(sumCredit.Text) * Convert.ToDouble(monthFee.Text) * 0.01, 2).ToString(), totalPay = Math.Round(Convert.ToDouble(mainPay2.Content) + (Convert.ToDouble(sumCredit.Text) - Convert.ToDouble(mainPay2.Content) * i) * Convert.ToDouble(stavka.Text) * 0.01 / 12, 2).ToString() });
                sumpercent = sumpercent +
                    Math.Round(
                        Convert.ToDouble(mainPay2.Content) +
                        (Convert.ToDouble(sumCredit.Text) - Convert.ToDouble(mainPay2.Content) * i) *
                        Convert.ToDouble(stavka.Text) * 0.01 / 12 - Convert.ToDouble(mainPay2.Content), 2);
                listBox.Items.Add(i + 1 + " месяц: " + mainPay2.Content + " + (" + sumCredit.Text + " - (" +
                                  mainPay2.Content +
                                  " * " + (i + 1) + "))*" + Math.Round(Convert.ToDouble(stavka.Text) * 0.01, 2) + "/12");
            }
            percentPayed2.Content = sumpercent.ToString();
            servicePay2.Content = servicePay.ToString();
            sumTotal2.Content = Math.Round(sumpercent + servicePay + Convert.ToDouble(sumCredit.Text), 2).ToString();
        }

        private void annu_Calculate()
        {
            totalPay = Math.Round(Convert.ToDouble(sumCredit.Text) * (Convert.ToDouble(stavka.Text) * 0.01 / 12 + Convert.ToDouble(stavka.Text) * 0.01 / 12 / (Math.Pow(1 + Convert.ToDouble(stavka.Text) * 0.01 / 12, Convert.ToDouble(srok.Text)) - 1)), 2);
            mainPay2.Content = Math.Round(Convert.ToDouble(sumCredit.Text) / Convert.ToDouble(srok.Text), 2).ToString();
            servicePay = Math.Round(Convert.ToDouble(sumCredit.Text) * Convert.ToDouble(monthFee.Text) * 0.01 * Convert.ToDouble(srok.Text), 2);
            prevPay = Math.Round(Convert.ToDouble(sumCredit.Text), 2);
            dataGrid.Items.Clear();
            listBox.Items.Clear();
            for (int i = 0; i < Convert.ToInt32(srok.Text); i++)
            {
                percent = Math.Round(prevPay * (Convert.ToDouble(stavka.Text) * 0.01 / 12), 2);
                dataGrid.Items.Add(new DataGrid { month = Convert.ToString(i + 1), date = DatePicker1.SelectedDate.Value.AddMonths(i).ToShortDateString(), credit = prevPay.ToString(), mainPay = Math.Round(totalPay - percent, 2).ToString(), percent = percent.ToString(), service = Math.Round(Convert.ToDouble(sumCredit.Text) * Convert.ToDouble(monthFee.Text) * 0.01, 2).ToString(), totalPay = totalPay.ToString() });
                listBox.Items.Add(i + 1 + " месяц: " + " Остаток кредита: " + prevPay + " Проценты: " + prevPay + " * " +
                                  Convert.ToDouble(stavka.Text) * 0.01 + " / 12" + " = " + percent + " Основной долг: " +
                                  totalPay + " - " + percent + " = " + Math.Round(totalPay - percent, 2));
                prevPay = Math.Round(prevPay - (totalPay - percent), 2);
                sumpercent = sumpercent + percent;
            }
            percentPayed2.Content = sumpercent.ToString();
            servicePay2.Content = servicePay.ToString();
            sumTotal2.Content = Math.Round(sumpercent + servicePay + Convert.ToDouble(sumCredit.Text), 2).ToString();
        }

        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            var Sum = sumCredit.Text;
            for (var i = 0; i < Sum.Length; i++)
            {
                listBox.Items.Add(Sum[i].ToString());
            }


            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                resultTab.IsEnabled = true;
                solutionTab.IsEnabled = true;
                servicePay = 0;
                sumpercent = 0;
                totalPay = 0;
                prevPay = 0;
                solutionTab.IsSelected = true;
                
            }));
        }
    }
}
