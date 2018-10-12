using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const string _fastlyServiceId = "4GHzntx9u1dckDbdefGo6k";
        private readonly HttpClient _fastlyApi;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _fastlyApi = httpClientFactory.CreateClient("fastly");
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Response.Headers.Add("Surrogate-Control", "max-age=300");
            Response.Headers.Add("Surrogate-Key", $"values value-{id}");

            return $"Server processed response for ID {id} at {DateTime.Now}";
        }
        
        // GET api/values/5/flush
        [HttpGet("{id}/flush")]
        public async Task<IActionResult> Flush(int id)
        {
            await _fastlyApi.PostAsync($"/service/{_fastlyServiceId}/purge/value-{id}", null);
            return Ok($"Flushed value: {id}");
        }

        // GET api/values
        [HttpGet("flush")]
        public async Task<IActionResult> Flush()
        {
            await _fastlyApi.PostAsync($"/service/{_fastlyServiceId}/purge/values", null);
            return Ok("Flushed all values");
        }
    }
}