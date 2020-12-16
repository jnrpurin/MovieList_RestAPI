namespace ReadSpreadsheet.Domain.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public static class SpreadsheetConfig
    {
        public const string ExcelMoviesFileName = "movielist.csv";

        public static readonly object[] SpreadsheetMoviesHeaderRow =
        {
            "year",
            "title",
            "studios",
            "producers",
            "winner"
        };
    }
}
