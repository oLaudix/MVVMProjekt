using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace WPF_MVVM.ViewModels
{
    public class TodosVM
    {
        public ObservableCollection<Todo> Todos { get; set; }
        public TodosVM()
        {
            Todos = new ObservableCollection<Todo>();
        }

        internal void AddParticipantToTodo(Todo selectedItem)
        {
            selectedItem.NumberOfParticipants++;
        }

        internal void ClearTodo()
        {
            Todos.Clear();
        }

        public async Task AddTodoFromWebAsync(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try
                {
                    string asciiEquivalents = Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(name));
                    HttpResponseMessage response = await client.GetAsync("http://api.apixu.com/v1/forecast.json?key=cadcc3e9f8ce4ac7a4483425190106&q=" + asciiEquivalents);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic Data = JsonConvert.DeserializeObject(responseBody);
                    DateTime dt = Convert.ToDateTime(Convert.ToString(Data.current.last_updated));
                    Todo webTodo = new Todo(Convert.ToString(Data.location.name), Convert.ToInt32(Data.current.temp_c), dt, Convert.ToInt32(Data.current.wind_kph));
                    Todos.Add(webTodo);
                }
                catch (HttpRequestException e)
                {
                }
            }
        }

        public void LoadSettings()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                var fileName = openFileDialog.FileName;
                var json = File.ReadAllText(fileName);
                try
                {
                    dynamic Data = JsonConvert.DeserializeObject(json);
                    DateTime dt = Convert.ToDateTime(Convert.ToString(Data.Beginning));
                    Todo webTodo = new Todo(Convert.ToString(Data.Name), Convert.ToInt32(Data.NumberOfParticipants), dt, Convert.ToInt32(Data.Wind_kph));
                    Todos.Add(webTodo);
                    MessageBox.Show("Wczytano pomyślnie");
                }
                catch
                {
                    MessageBox.Show("Błąd odczytu!");
                }
            }
        }
    }
}
