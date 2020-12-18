using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadSpreadsheet.Domain.Constants;
using ReadSpreadsheet.Domain.Interfaces.Service;

namespace LoadMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly ISpreadsheetService _spreadsheetService;
        
        public MoviesController(ILogger<MoviesController> logger, ISpreadsheetService spreadsheetService)
        {
            _logger = logger;
            _spreadsheetService = spreadsheetService;
        }

        // GET api/movies
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> Get()
        {
            try
            {
                var filePath = SpreadsheetConfig.CsvFilePath;
                var data = _spreadsheetService.GetLongestAndFasterProducer(filePath);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting data");
                return new StatusCodeResult(500);
            }
        }
    }
}
