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

namespace CoorDistance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> pos1 = new List<int>();
            List<int> pos2 = new List<int>();
            List<int> xyz = new List<int>();
            List<int> dxyz = new List<int>();
            try
            {
                pos1.Add(Int16.Parse(Pos1X.Text));
                pos1.Add(Int16.Parse(Pos1Y.Text));
                pos1.Add(Int16.Parse(Pos1Z.Text));
                pos2.Add(Int16.Parse(Pos2X.Text));
                pos2.Add(Int16.Parse(Pos2Y.Text));
                pos2.Add(Int16.Parse(Pos2Z.Text));
            }
            catch
            {
                ResultText.Text = "Error: Check your values";
                return;
            }

            foreach(int value in pos1)
            {
                xyz.Add(Math.Min(value,pos2[pos1.IndexOf(value)])); //Generates list of x,y,z coords.
                dxyz.Add(distanceFormula(value, pos2[pos1.IndexOf(value)]));
            }
            ResultText.Text = $"[x={xyz[0]},y={xyz[1]},z={xyz[2]},dx={dxyz[0]},dy={dxyz[1]},dz={dxyz[2]}]";
            if (cbToggle.IsChecked == true)
            {
                Clipboard.SetText(ResultText.Text);
            }
        }

        public int distanceFormula(int pos1, int pos2)
        {
            return Convert.ToInt32(Math.Sqrt(Math.Pow(pos2 - pos1,2)));
        }

        private void Credits_Open(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.github.com/InValidFire",
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
    }
}
