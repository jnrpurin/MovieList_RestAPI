using ReadSpreadsheet.Domain.Model;
using System.Collections.Generic;

namespace ReadSpreadsheet.Domain.Interfaces.Service
{
    public interface ISpreadsheetService
    {
        /// <summary>
        /// Get the producers by rules:
        ///     1 - the producer with the longest interval between two consecutive awards
        ///     2 - the producer who wins two awards faster;
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>List of movies info</returns>
        IEnumerable<dynamic> GetLongestAndFasterProducer(string filePath);

        /// <summary>
        /// Get the file path value
        /// </summary>
        /// <returns>File path</returns>
        string GetFilePath();

        /// <summary>
        /// Set the file path value
        /// </summary>
        /// <param name="filePath">File path</param>
        void SetFilePath(string filePath);

        /// <summary>
        /// Check csv extension and if file exists
        /// </summary>
        /// <param name="FilePath">file path from csv</param>
        /// <returns>True if valid path otherwise false</returns>
        bool CheckCsvFilePath(string FilePath);

        /// <summary>
        /// Build the movies list information
        /// </summary>
        /// <param name="spreadSheedMovies">Movies spreadsheet</param>
        /// <returns>List of all movies</returns>
        IEnumerable<MoviesInfo> MovieS(string[] spreadSheedMovies);

    }
}
