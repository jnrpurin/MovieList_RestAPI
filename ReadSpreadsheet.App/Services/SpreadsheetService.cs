using ReadSpreadsheet.Domain.Interfaces;
using ReadSpreadsheet.Domain.Model;
using System;
using System.Collections.Generic;

namespace ReadSpreadsheet.App.Services
{
    public class SpreadsheetService : ISpreadsheetService
    {
        public SpreadsheetService()
        {

        }

        public IEnumerable<SheetMovies> GetMovies()
        {
            throw new NotImplementedException();
        }
    }
}
