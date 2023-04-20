using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Xaml.Behaviors.Core;
using ParteVisualCalculadora.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace ParteVisualCalculadora.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string m_DisplayText = "0"; ////valor temporal para asignarle a la propiedad. Llegan a usar en underscore en vez de
        //private ObservableCollection<OperationClass> operations;

        public string DisplayText
        {
            get { return m_DisplayText; }
            set
            {
                m_DisplayText = value;
                RaisePropertyChanged("DisplayText");
            }
        }


        public RelayCommand NumberOnePressCommand { get; set; } //Es una propiedad y el relay esta quitando las funciones del comando para darselas a otro método.
        public RelayCommand NumberTwoPressCommand { get; set; }
        public RelayCommand NumberThreePressCommand { get; set; }
        public RelayCommand NumberFourPressCommand { get; set; }
        public RelayCommand NumberFivePressCommand { get; set; }
        public RelayCommand NumberSixPressCommand { get; set; }
        public RelayCommand NumberSevenPressCommand { get; set; }
        public RelayCommand NumberEightPressCommand { get; set; }
        public RelayCommand NumberNinePressCommand { get; set; }
        public RelayCommand NumberZeroPressCommand { get; set; }
        public RelayCommand EqualPressCommand { get; set; }
        public RelayCommand DotPressCommand { get; set; }
        public RelayCommand PlusPressCommand { get; set; }
        public RelayCommand MinusPressCommand { get; set; }
        public RelayCommand MultPressCommand { get; set; }
        public RelayCommand SlashPressCommand { get; set; }
        public RelayCommand OpenPaPressCommand { get; set; }
        public RelayCommand ClosePaPressCommand { get; set; }
        public RelayCommand DeletePressCommand { get; set; }
        public RelayCommand DelPressCommand { get; set; }

        public MainWindowViewModel()
        {
            NumberOnePressCommand = new RelayCommand(NumberOnePress); //Se instancia la propiedad de relay command en el nuevo método.
            NumberTwoPressCommand = new RelayCommand(NumberTwoPress);
            NumberThreePressCommand = new RelayCommand(NumberThreePress);
            NumberFourPressCommand = new RelayCommand(NumberFourPress);
            NumberFivePressCommand = new RelayCommand(NumberFivePress);
            NumberSixPressCommand = new RelayCommand(NumberSixPress);
            NumberSevenPressCommand = new RelayCommand(NumberSevenPress);
            NumberEightPressCommand = new RelayCommand(NumberEightPress);
            NumberNinePressCommand = new RelayCommand(NumberNinePress);
            NumberZeroPressCommand = new RelayCommand(NumberZeroPress);
            EqualPressCommand = new RelayCommand(EqualPress);
            DotPressCommand = new RelayCommand(DotPress);
            PlusPressCommand = new RelayCommand(PlusPress);
            MinusPressCommand = new RelayCommand(MinusPress);
            MultPressCommand = new RelayCommand(MultPress);
            SlashPressCommand = new RelayCommand(SlashPress);
            OpenPaPressCommand = new RelayCommand(OpenPaPress);
            ClosePaPressCommand = new RelayCommand(ClosePaPress);
            DeletePressCommand = new RelayCommand(DeletePress);
            DelPressCommand = new RelayCommand(DelPress);

        }

        private void DelPress()
        {
            DisplayText = String.Empty;
        }

        private void DeletePress()
        {
            DisplayText = String.Empty;
        }

        private void ClosePaPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + ")";
        }

        private void OpenPaPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "(";
        }

        private void SlashPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "/";
        }

        private void MultPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "*";
        }

        private void MinusPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "-";
        }

        private void PlusPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "+";
        }

        private void DotPress()
        {

            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + ".";
            if (!DisplayText.Contains("."))
            {
                DisplayText = DisplayText + ".";

            }

        }

        private void NumberZeroPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "0";
        }

        private void EqualPress()
        {
            RealizarCalculos();


            //CallAddTwoNumbersEndpoint();
        }

        private async void RealizarCalculos()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=OperationsCollection; Integrated Security=True;";
            //string connectionString = ConfigurationManager.ConnectionStrings["OperationsCollectionConnectionString"].ConnectionString;
            // Crear la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //     Abrir la conexión
                connection.Open();

                //     Crear la sentencia SQL para insertar la operación
                string sql = "INSERT INTO OperacionAlmacenada (Operacion) VALUES (@Operacion)";

                //     Crear el objeto de comando con la sentencia SQL y la conexión
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    //         Agregar el valor del DisplayText como parámetro
                    command.Parameters.AddWithValue("@Operacion", DisplayText);

                    //         Ejecutar el comando
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("http://localhost:5140/GetResult");
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                DisplayText = result;

            }
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand("SELECT TOP 1 Operacion FROM OperacionAlmacenada ORDER BY OperationID DESC", connection);
            //    SqlDataReader reader = command.ExecuteReader();
            //    if (reader.Read())
            //    {
            //        string operacion = reader.GetString(0);
            //        // Realizar el cálculo
            //        DataTable table = new DataTable();
            //        try
            //        {
            //            var result = table.Compute(operacion, "");
            //            DisplayText = result.ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Si hay un error, mostrar el mensaje en DisplayText
            //            DisplayText = ex.Message;
            //        }
            //    }
            //    reader.Close();
            //}


        }

        //private async void CallAddTwoNumbersEndpoint()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.GetAsync("http://localhost:5140/BasicOperation/AddTwoNumbers?number1=2&number2=3");
        //        response.EnsureSuccessStatusCode();
        //        var result = await response.Content.ReadAsStringAsync();
        //        DisplayText = result;
        //        // use the result as needed
        //    }
        //}

        private void NumberNinePress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "9";
        }

        private void NumberEightPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "8";
        }

        private void NumberSevenPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "7";
        }

        private void NumberSixPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "6";
        }

        private void NumberFivePress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "5";
        }

        private void NumberFourPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "4";
        }

        private void NumberThreePress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "3";
        }

        private void NumberTwoPress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "2";
        }

        private void NumberOnePress()
        {
            if (DisplayText == "0")
            {
                DisplayText = string.Empty;
            }
            DisplayText = DisplayText + "1";

        }



    }
}
