using System;

namespace ReadSpreadsheet.Domain.Model
{
    public class MoviesInfoResult
    {
        public int MinInterval { get; set; }

        public int MaxInterval { get; set; }

        public int Delta { get; set; }

        public int PreviousWin { get; set; }

        public int FollowingWin { get; set; }

        public string Producer { get; set; }
    }
}
