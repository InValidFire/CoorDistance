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

            // load settings
            if (Properties.Settings.Default.bToggle == true)
            {
                bToggle.IsChecked = true;
            }
            else if (Properties.Settings.Default.bToggle == false)
            {
                bToggle.IsChecked = false;
            }

            if (Properties.Settings.Default.cbToggle == true)
            {
                cbToggle.IsChecked = true;
            }
            else if (Properties.Settings.Default.cbToggle == false)
            {
                cbToggle.IsChecked = false;
            }
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
            if (bToggle.IsChecked == true)
            {
                ResultText.Text = $"[x={xyz[0]},y={xyz[1]},z={xyz[2]},dx={dxyz[0]},dy={dxyz[1]},dz={dxyz[2]}]";
            }
            else if (bToggle.IsChecked == false)
            {
                ResultText.Text = $"x={xyz[0]},y={xyz[1]},z={xyz[2]},dx={dxyz[0]},dy={dxyz[1]},dz={dxyz[2]}";
            }
            if (cbToggle.IsChecked == true)
            {
                Clipboard.SetText(ResultText.Text);
            }
        }

        static int distanceFormula(int pos1, int pos2)
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

        private void bToggle_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.bToggle = true;
            Properties.Settings.Default.Save();
        }

        private void bToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.bToggle = false;
            Properties.Settings.Default.Save();
        }

        private void cbToggle_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.cbToggle = true;
            Properties.Settings.Default.Save();
        }

        private void cbToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.cbToggle = false;
            Properties.Settings.Default.Save();
        }
    }
}
