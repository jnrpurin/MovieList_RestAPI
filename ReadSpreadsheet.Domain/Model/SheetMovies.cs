namespace ReadSpreadsheet.Domain.Model
{
    public class SheetMovies
    {
        public int MovieId { get; set; }

        public int Year { get; set; }

        public string Title { get; set; }

        public string Studio { get; set; }

        public string Producer { get; set; }

        public bool Winner { get; set; }
    }
}
