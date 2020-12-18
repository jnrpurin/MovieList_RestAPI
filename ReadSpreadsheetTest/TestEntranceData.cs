using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadSpreadsheet.App.Services;
using ReadSpreadsheet.Domain.Constants;
using ReadSpreadsheet.Domain.Interfaces.Service;
using ReadSpreadsheet.Domain.Model;
using System.Collections.Generic;

namespace ReadSpreadsheetTest
{
    [TestClass]
    public class TestEntranceData
    {
        private ISpreadsheetService spreadsheetService;
        private readonly string movieListPath = SpreadsheetConfig.CsvFilePath;

        /// <summary/>
        [TestInitialize]
        public void TestInit()
        {
            spreadsheetService = new SpreadsheetService();
        }

        [TestMethod]
        public void DataEntraceAndResultShoudBeEquals()
        {
            // Arrange
            var expectedResultML2 = new List<MoviesInfoResult>
            {
                new MoviesInfoResult()
                {
                    MaxInterval = 0
                    ,MinInterval = 4
                },
                new MoviesInfoResult()
                {
                    MaxInterval = 4
                    ,MinInterval = 0
                }
            };

            //TODO:Get values from list to compare or create a list same type including values from GetLongestAndFasterProducer()
            // Act
            var resultML2 = spreadsheetService.GetLongestAndFasterProducer(movieListPath);

            //TODO:Compare lists or values separated here 
            // Assert
            Assert.AreEqual(expectedResultML2[0].MaxInterval, 0);
        }
    }
}