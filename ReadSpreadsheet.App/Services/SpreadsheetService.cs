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

        public SpreadsheetService() { }

        /// <inheritdoc cref="ISpreadsheetService.SetFilePath(string)" />
        public void SetFilePath(string filePath) => this.filePathExcel = filePath;

        /// <inheritdoc cref="ISpreadsheetService.GetFilePath" />
        public string GetFilePath() => filePathExcel;

        /// <inheritdoc cref="ISpreadsheetService.CheckCsvFilePath(string)" />
        public bool CheckCsvFilePath(string FilePath) => File.Exists(FilePath) && Path.GetExtension(FilePath).Equals(".csv");

        /// <inheritdoc cref="ISpreadsheetService.GetLongestAndFasterProducer()" />
        public IEnumerable<dynamic> GetLongestAndFasterProducer(string filePath)
        {
            if (!CheckCsvFilePath(filePath))
            {
                return null;
            }
            var obj = (string[])File.ReadAllLines(filePath);
            var movies = MovieS(obj).Where(m => m.Winner);

            return GetBothResults(movies);
        }

        /// <inheritdoc cref="ISpreadsheetService.MovieS(string[])" />
        public IEnumerable<MoviesInfo> MovieS(string[] spreadSheedMovies)
        {
            int idKey = 0;
            var movies = new List<MoviesInfo>();
            foreach (string movieRow in spreadSheedMovies.Where(sp => sp != null).Skip(1))
            {
                var movie = movieRow.Replace(',', ' ').Split(';');
                movies.Add(new MoviesInfo()
                {
                    MovieId = idKey++,
                    Year = int.Parse(movie[(int)SeqMovieColumn.Year].ToString()),
                    Producer = movie[(int)SeqMovieColumn.Producer].ToString(),
                    Studio = movie[(int)SeqMovieColumn.Studio].ToString(),
                    Title = movie[(int)SeqMovieColumn.Title].ToString(),
                    Winner = (String.IsNullOrWhiteSpace(movie[(int)SeqMovieColumn.Winner].ToString()) ? false :
                        movie[(int)SeqMovieColumn.Winner].Substring(0, 3).Equals("yes")),
                });
            }

            return movies;
        }

        #region Private methods
        /// </summary>
        private IEnumerable<dynamic> GetBothResults(IEnumerable<MoviesInfo> movies)
        {
            var l1 = ProducerFastWinner(movies);
            var l2 = ProducerLongestInterval(movies);

            return Enumerable.Concat(l1, l2);
        }

        /// <summary>
        /// Get the producer with the longest interval between two consecutive awards
        /// </summary>
        /// <param name="movies">List of winning films</param>
        /// <returns>List of winning producers longest interval</returns>
        private IEnumerable<dynamic> ProducerLongestInterval(IEnumerable<MoviesInfo> movies)
        {
            var query = movies.GroupBy(g => g.Producer)
                    .Where(g => g.Count() > 1).ToList();

            var longest = query
                .Select(g =>
                {
                    MoviesInfoResult Max = new MoviesInfoResult()
                    {
                        MaxInterval = Int32.MinValue
                        ,
                        Delta = 0
                        ,
                        PreviousWin = 0
                        ,
                        FollowingWin = 0
                        ,
                        Producer = ""
                    };

                    for (int i = 1; i < g.Count(); i++)
                    {
                        MoviesInfo previous = g.ElementAt(i - 1),
                              current = g.ElementAt(i);

                        Max.Delta = Math.Abs(current.Year - previous.Year);

                        if (Max.Delta > Max.MaxInterval)
                        {
                            Max.MaxInterval = Max.Delta;
                            Max.PreviousWin = previous.Year;
                            Max.FollowingWin = current.Year;
                            Max.Producer = current.Producer;
                        }
                    }

                    return new { Max };
                })
                .ToList();

            var biggerInterval = longest.Max(x => x.Max.MaxInterval);
            var biggerIntervalProducer = longest.Where(x => x.Max.MaxInterval == biggerInterval);

            return biggerIntervalProducer;
        }

        /// <summary>
        /// Get the producer who wins two awards faster
        /// </summary>
        /// <param name="movies">List of winning films</param>
        /// <returns>List of producers</returns>
        private IEnumerable<dynamic> ProducerFastWinner(IEnumerable<MoviesInfo> movies)
        {
            var query = movies.GroupBy(g => g.Producer)
                    .Where(g => g.Count() > 1).ToList();

            var faster = query
                .Select(g =>
                {
                    MoviesInfoResult Min = new MoviesInfoResult()
                    {
                        MinInterval = Int32.MaxValue
                        ,
                        Delta = 0
                        ,
                        PreviousWin = 0
                        ,
                        FollowingWin = 0
                        ,
                        Producer = ""
                    };

                    for (int i = 1; i < g.Count(); i++)
                    {
                        MoviesInfo previous = g.ElementAt(i - 1),
                              current = g.ElementAt(i);

                        Min.Delta = Math.Abs(current.Year - previous.Year);

                        if (Min.Delta < Min.MinInterval)
                        {
                            Min.MinInterval = Min.Delta;
                            Min.PreviousWin = previous.Year;
                            Min.FollowingWin = current.Year;
                            Min.Producer = current.Producer;
                        }
                    }

                    return new { Min };
                }).ToList();

            var shorterInterval = faster.Min(x => x.Min.MinInterval);
            var shorterIntervalProducer = faster.Where(x => x.Min.MinInterval == shorterInterval);

            return shorterIntervalProducer;
        }
        #endregion
    }
}
