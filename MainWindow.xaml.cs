using RestoCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
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

namespace Resto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            boxHeight.Text = "50";
            boxwidth.Text = "50";
            
            grid = MyGrid; 
        }

        public static RestaurantTable MyGridstatic { get; set;  }
        public static Grid grid { get; set; }
        

        private void boxHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
                if (boxHeight.Text != null && boxHeight.Text != "" && RestaurantTable.actualSelectedTable != null)
                RestaurantTable.actualSelectedTable.Height = Convert.ToDouble(boxHeight.Text); 
        }

        private void boxwidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (boxwidth.Text != null && boxwidth.Text != "" && RestaurantTable.actualSelectedTable != null)
                RestaurantTable.actualSelectedTable.Width = Convert.ToDouble(boxwidth.Text);
        }

        private void RadioButtonYes_Checked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonNo != null)
            {
                RestaurantTable.actualSelectedTable.Reservation_Enable(); 
            }            
        }

        private void RadioButtonNo_Checked(object sender, RoutedEventArgs e)
        {
            RestaurantTable.actualSelectedTable.Reservation_Disable(); 
        }

        private void NumberOfSeats_Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(NumberOfSeats_Box.Text != null && NumberOfSeats_Box.Text != "")
                RestaurantTable.actualSelectedTable.UpdateNumberOfSeats(Convert.ToInt32(NumberOfSeats_Box.Text)); 
        }

        private void NewTable_Click(object sender, RoutedEventArgs e)
        {
            RestaurantTable table = new RestaurantTable();
            table.Rectus.Width = 50;
            table.Rectus.Height = 50;
            table.HorizontalAlignment = HorizontalAlignment.Right;  
            grid.Children.Add(table);
            Client.CreateClients(RestaurantTable.actualSelectedTable.numberOfSeats, RestaurantTable.actualSelectedTable.clients); 
        }
    }
}
