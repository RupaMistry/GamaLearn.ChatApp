namespace GamaLearn.ChatApp.SignalRWebpack.Controllers
{
    /// <summary>
    /// ChatHub API Controller
    /// </summary>
    /// <param name="logger"></param>
    [ApiController]
    [Route("[controller]")]
    public class ChatHubController(ILogger<ChatHubController> logger) : ControllerBase
    {
        private readonly ILogger<ChatHubController> _logger = logger;
         
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> Get()
        {
            _logger.LogInformation("Service is up and running.");
            return Ok("Online");
        }
    }
}