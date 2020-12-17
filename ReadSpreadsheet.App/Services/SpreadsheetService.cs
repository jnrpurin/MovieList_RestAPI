using System.IO;
using System.Linq;
using System.Collections.Generic;
using ReadSpreadsheet.Domain.Enum;
using ReadSpreadsheet.Domain.Model;
using ReadSpreadsheet.Domain.Interfaces.Service;
using System;

namespace ReadSpreadsheet.App.Services
{
    public class SpreadsheetService : ISpreadsheetService
    {
        private string filePathExcel;

        public SpreadsheetService()
        {
        }

        /// <inheritdoc cref="ISpreadsheetService.GetFilePath" />
        public string GetFilePath()
        {
            return filePathExcel;
        }

        /// <inheritdoc cref="ISpreadsheetService.SetFilePath" />
        public void SetFilePath(string filePath)
        {
            this.filePathExcel = filePath;
        }

        /// <inheritdoc cref="ISpreadsheetService.GetBiggerAndFasterProducer()" />
        public IEnumerable<MoviesInfo> GetBiggerAndFasterProducer()
        {
            /*return all records from csv:
            var obj = (string[])File.ReadAllLines(@"C:\Users\320049484\Desktop\Ademir\MyDocs\MovieList_RestAPI\movielist.csv");
            var movies = MovieSpreadsheetToMoviesInfo(obj);
            return movies;*/

            var obj = (string[])File.ReadAllLines(@"C:\Users\320049484\Desktop\Ademir\MyDocs\MovieList_RestAPI\movielist.csv");
            var movies = MovieSpreadsheetToMoviesInfo(obj);

            return ProducerLongestInterval(movies);
        }

        /// <summary>
        /// Get the producer with the longest interval between two consecutive awards
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of producers</returns>
        private IEnumerable<MoviesInfo> ProducerLongestInterval(IEnumerable<MoviesInfo> movies)
        {
            //winners
            //producers
            //diference between 2 awards
            var winners = movies.Where(m => m.Winner);

            var query = winners.OrderBy(o => o.Year)
                .GroupBy(g => new { g.Year, g.Producer })
                .Where(g => g.Count() > 1)
                .Select(s => new MoviesInfo { Producer = s.Key.Producer, Year = s.Key.Year})
                .ToList();

            List<MoviesInfo> result = new List<MoviesInfo>();
            foreach (var prod in query)
            {
                result.Add(prod);
            }

            return result;
        }

        /// <summary>
        /// Get the producer who wins two awards faster
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of producers</returns>
        private IEnumerable<MoviesInfo> ProducerFaster()
        {
            return null;
        }

        /// <summary>
        /// Build the movies list information
        /// </summary>
        /// <param name="spreadSheedMovies">Movies spreadsheet</param>
        /// <returns>List of all movies</returns>
        private IEnumerable<MoviesInfo> MovieSpreadsheetToMoviesInfo(string[] spreadSheedMovies)
        {
            int idKey = 0;

            foreach (string movieRow in spreadSheedMovies.Where(sp => sp != null).Skip(1))
            {
                var movie = movieRow.Split(';');
                yield return new MoviesInfo
                {
                    MovieId = idKey++,
                    Year = int.Parse(movie[(int)SeqMovieColumn.Year].ToString()),
                    Producer = movie[(int)SeqMovieColumn.Producer].ToString(),
                    Studio = movie[(int)SeqMovieColumn.Studio].ToString(),
                    Title = movie[(int)SeqMovieColumn.Title].ToString(),
                    Winner = movie[(int)SeqMovieColumn.Winner].ToString().Equals("yes"),
                };
            }
        }
    }
}
