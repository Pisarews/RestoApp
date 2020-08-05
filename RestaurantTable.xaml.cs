using RestoCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
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
    /// Logika interakcji dla klasy RestaurantTable.xaml
    /// </summary>
    public partial class RestaurantTable : UserControl
    {
        public RestaurantTable()
        {
            InitializeComponent();

            if (isReserved == true)
            {
                Rectus.Background = Brushes.Red;
            }
            else
            {
                Rectus.Background = Brushes.Green;
            }
            
        }
        
        public bool isReserved { get; set; }
        public int numberOfSeats { get; set;}
        public int NumberOfTable { get; set; }
        public bool isMove { get; set; }
        public PropertyChangedCallback PropertyChanged;
        public List<Client> clients = new List<Client>(); 

        public static RestaurantTable actualSelectedTable { get; set; }

        private Point? _buttonPosition;
        private double deltaX;
        private double deltaY;
        private bool _isMoving;
        private TranslateTransform _currentTT;

        public void Reservation_Enable()
        {
            this.Rectus.Background = Brushes.Red;
            this.isReserved = true;       
        }

        public void Reservation_Disable()
        {
            this.Rectus.Background = Brushes.Green;
            this.isReserved = false;
        }

        public void UpdateNumberOfSeats(int x)
        {
            Rectus.Content = x.ToString(); 
        }

        private void Rectus_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            actualSelectedTable = this; 
            MainWindow.MyGridstatic = this;
            if (_buttonPosition == null)
                _buttonPosition = Rectus.TransformToAncestor(MainWindow.MyGridstatic).Transform(new Point(0, 0));
            var mousePosition = Mouse.GetPosition(MainWindow.MyGridstatic);
            deltaX = mousePosition.X - _buttonPosition.Value.X;
            deltaY = mousePosition.Y - _buttonPosition.Value.Y;
            _isMoving = true;
        }

        private void Rectus_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _currentTT = Rectus.RenderTransform as TranslateTransform;
            _isMoving = false;
        }

        private void Rectus_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMoving) return;

            var mousePoint = Mouse.GetPosition(MainWindow.MyGridstatic);

            #region axe X 
            var offsetX = (_currentTT == null ? _buttonPosition.Value.X : _buttonPosition.Value.X - _currentTT.X) + deltaX - mousePoint.X;
            
            if ( offsetX < 0)
            {
                offsetX = 0; 
            }
            if(offsetX > MainWindow.grid.ActualWidth)
            {
                offsetX = MainWindow.grid.ActualWidth; 
            }
            #endregion


            #region YAxe 
            var offsetY = (_currentTT == null ? _buttonPosition.Value.Y : _buttonPosition.Value.Y - _currentTT.Y) + deltaY - mousePoint.Y;
            if (offsetY < 0 -MainWindow.grid.ActualHeight / 2) offsetY = 0 - MainWindow.grid.ActualHeight / 2;
            if (offsetY > MainWindow.grid.ActualHeight) offsetY = MainWindow.grid.ActualHeight;
            #endregion
            this.Rectus.RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }

        private void Rectus_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MyGridstatic = this;
        }
    }
}
