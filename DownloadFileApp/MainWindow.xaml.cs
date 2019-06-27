using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace DownloadFileApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            fromAddressTextBox.Text = "http://speedtest.ftp.otenet.gr/files/test100Mb.db";
        }

        private void SetLoading(bool isLoading)
        {
            if (isLoading)
            {
                progressBar.Visibility = Visibility.Visible;
                statusTextBlock.Text = "Please wait...";
            }
            else
            {
                progressBar.Visibility = Visibility.Collapsed;
                statusTextBlock.Text = "Done";
            }
        }

        private async void DownloadFileButtonClick(object sender, RoutedEventArgs e)
        {
            if (fromAddressTextBox.Text == string.Empty || toAddressTextBox.Text == string.Empty)
            {
                MessageBox.Show("Заполните текстовое поле!");
                return;
            }

            SetLoading(true);
            downloadFileButton.IsEnabled = false;
            try
            {
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(fromAddressTextBox.Text, toAddressTextBox.Text);
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}");
                downloadFileButton.IsEnabled = true;
                SetLoading(false);
                return;
            }

            MessageBox.Show("Загрузка завершена!");
            downloadFileButton.IsEnabled = true;
            SetLoading(false);
        }
    }
}
