using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   
        [Route("api/[controller]")]
        [ApiController]
        public class WeatherForecastController : ControllerBase
        {
            [HttpGet]
            public IEnumerable<string> Get()
            {
                return new string[] { "value1", "value2" };
            }
        }
    }
