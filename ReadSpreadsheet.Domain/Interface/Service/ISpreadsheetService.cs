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
        /// <returns>List of movies info</returns>
        IEnumerable<MoviesInfo> GetBiggerAndFasterProducer();

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

    }
}
