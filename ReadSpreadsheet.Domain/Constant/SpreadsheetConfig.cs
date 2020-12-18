namespace ReadSpreadsheet.Domain.Constants
{
    public static class SpreadsheetConfig
    {
        public static readonly object[] SpreadsheetMoviesHeaderRow =
        {
            "year",
            "title",
            "studios",
            "producers",
            "winner"
        };

        public static string CsvFilePath => "C:\\Temp\\movielist2.csv";
    }
}
