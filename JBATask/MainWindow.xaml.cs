using Microsoft.Win32;
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
using JBATaskCode;

namespace JBATask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath;

        public MainWindow()
        {
            InitializeComponent();
            BTNCreteDBTable.IsEnabled = false;
        }

        private void BTNSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = ".pre file (*.pre)|*.pre";
            openfile.Multiselect = false;
            openfile.ShowDialog();
            filePath = openfile.FileName;
            if (filePath != null)
                BTNCreteDBTable.IsEnabled = true;
        }
        int min = 0;
        int sec = 0;

        private void ATimer_Tick(object sender, EventArgs e)
        {
            sec++;
            if (sec == 60)
            {
                sec = 0;
                min++;
            }
            secLBL.Content = sec;
            minLBL.Content = min;

        }

        private async void BTNCreteDBTable_Click(object sender, RoutedEventArgs e)
        {
            HeaderCreator headerCreator = new HeaderCreator(filePath);
            HeaderLV.Items.Clear();
            List<string> header = headerCreator.CreateHeader();
            foreach (string line in header)
                HeaderLV.Items.Add(line);
            try
            {
                string connectionString = DBConnectionString.Text;
                RecordFactory creator = new RecordFactory(filePath);

                CreateTableInDB tableCreator = new CreateTableInDB(connectionString);
                await tableCreator.CreateTable(@"If not exists (select name from sysobjects where name = 'Precipitation') CREATE TABLE Precipitation (Xref int, Yref int, Date date, Value int);");

                var ATimer = new System.Windows.Threading.DispatcherTimer();
                ATimer.Tick += ATimer_Tick;
                ATimer.Interval = new TimeSpan(0, 0, 0, 1);
                ATimer.Start();

                InsertRecordToDBTable recordInsert = new InsertRecordToDBTable(connectionString);
                while (creator.EndOfFile == false)
                {
                    await recordInsert.InsertRecordToTable(creator.GetNextRecord());
                }
                ATimer.Stop();
                MessageBox.Show("Finished adding records to DB.");
                recordInsert.CloseConnection();
                
                GetDataFromTable getData = new GetDataFromTable(connectionString);
                await getData.GetData("SELECT * FROM Precipitation ORDER BY Xref, Yref, Date ASC");
                while (getData.reader.Read())
                {
                    var data = new Record((int)getData.reader["Xref"], (int)getData.reader["Yref"], (DateTime)getData.reader["Date"], (int)getData.reader["Value"]);
                    GeneratedTable.Items.Add(data);
                }
                getData.CloseConnection();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
