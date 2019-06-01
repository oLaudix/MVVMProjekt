using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_MVVM.Models;
using WPF_MVVM.ViewModels;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace WPF_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public TodosVM vm { get { return DataContext as TodosVM; }}
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void AddButtonFromWeb_Click(object sender, RoutedEventArgs e)
        {
            //vm.ClearTodo();
            await vm.AddTodoFromWebAsync(InputTodoName.Text);
        }

        private async void AddOneParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            var serializedObject = JsonConvert.SerializeObject((Todo)TodosListView.SelectedItem);

            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Filter = "Txt files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                await Task.Run(() => File.WriteAllText(fileName, serializedObject));
                MessageBox.Show("Zapisano pomyślnie");
                return;
            }

            MessageBox.Show("Błąd zapisu");
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            await App.Current.Dispatcher.Invoke(async () => { vm.LoadSettings(); });

        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Window1();
            newWindow.Show();
        }



    }
}
