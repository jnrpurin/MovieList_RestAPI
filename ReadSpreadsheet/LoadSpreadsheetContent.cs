using System;
using System.IO;
using System.Windows.Forms;
using ReadSpreadsheet.Domain.Enum;
using ReadSpreadsheet.Domain.Interfaces.Service;

namespace ReadSpreadsheetUI
{
    public partial class LoadSpreadsheetContent : Form
    {
        private readonly ISpreadsheetService spreadsheetService;

        public LoadSpreadsheetContent(ISpreadsheetService spreadsheetService)
        {
            this.spreadsheetService = spreadsheetService;

            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openCSVFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtCsvFile.Text = openCSVFileDialog.FileName;

                if (spreadsheetService.CheckCsvFilePath(txtCsvFile.Text))
                {
                    spreadsheetService.SetFilePath(txtCsvFile.Text);

                    var obj = (string[])File.ReadAllLines(spreadsheetService.GetFilePath());
                    var movies = spreadsheetService.MovieS(obj);

                    ShowCsvFileContentInGridView(movies);
                }
                else
                {
                    MessageBox.Show("Invalid or nonexistent file!", "Error on selected file");
                    if (txtCsvFile.CanFocus)
                    {
                        txtCsvFile.Focus();
                    }
                }
            }
        }

        /// </summary>
        private void ShowCsvFileContentInGridView(System.Collections.Generic.IEnumerable<ReadSpreadsheet.Domain.Model.MoviesInfo> movies)
        {
            dataGridView.Columns.Add("year", "year");
            dataGridView.Columns.Add("title", "title");
            dataGridView.Columns.Add("studio", "studio");
            dataGridView.Columns.Add("producer", "producer");
            dataGridView.Columns.Add("winner", "winner");

            foreach (var movie in movies)
            {
                var index = dataGridView.Rows.Add();
                for (int i = 0; i < 6; i++)
                {
                    dataGridView.Rows[index].Cells[(int)SeqMovieColumn.Producer].Value = movie.Producer;
                    dataGridView.Rows[index].Cells[(int)SeqMovieColumn.Studio].Value = movie.Studio;
                    dataGridView.Rows[index].Cells[(int)SeqMovieColumn.Title].Value = movie.Title;
                    dataGridView.Rows[index].Cells[(int)SeqMovieColumn.Winner].Value = movie.Winner;
                    dataGridView.Rows[index].Cells[(int)SeqMovieColumn.Year].Value = movie.Year;
                }
            }
        }
    }
}
