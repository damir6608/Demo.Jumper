using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Demo.Jumper.Window.Create
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : System.Windows.Window
    {
        private readonly Agent _agent;
        private readonly JumperEntities _jumperEntities = new JumperEntities();
        private const string successSave = "Успешно сохранено!";
        private string _imagePath;

        public Create(Agent agent)
        {
            InitializeComponent();

            GridAgent.DataContext = agent;

            _agent = agent;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _agent.Icon = _imagePath;
                _jumperEntities.Agents.Add(_agent);

                _jumperEntities.SaveChanges();
                MessageBox.Show(successSave);
            }
            catch
            {

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            new MainWindow().ShowDialog();
        }

        private void EditPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Picture = _agent.Icon;
                OpenFileDialog opFD = new OpenFileDialog();
                opFD.ShowDialog();
                var imag = opFD.FileName;
                var s = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug", string.Empty).Replace("\\", "/");
                string dest = @s + "/Photos/agents/" + System.IO.Path.GetFileName(imag);
                Image image = new Image();
                var bi = new BitmapImage(new Uri(imag));
                Photo.Source = bi;
                var pr = _jumperEntities.Agents.ToList().Find(f => f.Id == _agent.Id);
                if (pr == null)
                    _imagePath = opFD.SafeFileName;
                else
                {
                    _agent.Icon = "/agents/" + opFD.SafeFileName;
                    _jumperEntities.SaveChanges();
                }

                GridAgent.DataContext = pr;
                File.Copy(imag, dest);
            }
            catch
            {

            }
        }
    }
}
