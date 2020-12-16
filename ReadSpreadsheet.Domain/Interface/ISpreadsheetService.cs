using ReadSpreadsheet.Domain.Model;
using System.Collections.Generic;

namespace ReadSpreadsheet.Domain.Interfaces
{
    public interface ISpreadsheetService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SheetMovies> GetMovies();
    }
}
