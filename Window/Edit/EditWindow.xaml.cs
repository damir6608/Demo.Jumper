using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Excel = Microsoft.Office.Interop.Excel;

namespace Demo.Jumper.Window.Edit
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : System.Windows.Window
    {
        private readonly Agent _agent;
        private readonly JumperEntities _jumperEntities = new JumperEntities();
        private const string successSave = "Успешно сохранено!";
        private string _imagePath;

        public EditWindow(Agent agent)
        {
            InitializeComponent();

            GridAgent.DataContext = agent;

            _agent = agent;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var updatedAgent = _jumperEntities.Agents.Find(_agent.Id);
                updatedAgent.Type = _agent.Type;
                updatedAgent.Name = _agent.Name;
                updatedAgent.Email = _agent.Email;
                updatedAgent.Phone = _agent.Phone;
                updatedAgent.Address = _agent.Address;
                updatedAgent.Priority = _agent.Priority;
                updatedAgent.Director = _agent.Director;
                updatedAgent.INN = _agent.INN;
                updatedAgent.KPP = _agent.KPP;
                updatedAgent.Icon = _agent.Icon;

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

        private void SavePDF_Click(object sender, RoutedEventArgs e)
        {
            var orders = _agent;
            var app = new Excel.Application
            {
                SheetsInNewWorkbook = 1
            };
            var workbook = app.Workbooks.Add(Type.Missing);

            Excel.Worksheet worksheet = app.Worksheets.Item[1];
            worksheet.Name = "Card";

            worksheet.Cells[1][1] = "Order number";
            worksheet.Cells[1][2] = "Product list";
            worksheet.Cells[1][3] = "Total cost";

            worksheet.Cells[2][1] = orders.Id;

            var fullProductList = string.Empty;
            fullProductList = orders.Address;
            worksheet.Cells[2][2] = fullProductList;
            worksheet.Cells[2][3] = orders.Name;

            worksheet.Columns.AutoFit();

            app.Visible = true;

            app.Application.ActiveWorkbook.SaveAs( @Directory.GetCurrentDirectory().Replace("bin\\Debug", "") + @"test.xlsx");

            var excelDocument = app.Workbooks.Open (@Directory.GetCurrentDirectory().Replace("bin\\Debug", "") + @"test.xlsx");
            excelDocument.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, @Directory.GetCurrentDirectory().Replace("bin\\Debug", "") + @"test.pdf");
            excelDocument.Close(false, "", false);
            app.Quit();
        }
    }
}
