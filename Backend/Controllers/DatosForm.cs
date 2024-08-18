using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using dotenv.net;
using Backend.SendEmail;
namespace Backend.Controllers
{
    //[EnableCors("AllowAnyOriginPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DatosFormController : Controller
    {
        private readonly ILogger<DatosFormController> _logger;
        private readonly string key;
        public DatosFormController(ILogger<DatosFormController> logger)
        {
            _logger = logger;
            DotEnv.Load();
            key=Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY")??"";
        }
        [HttpPost]
         public async Task<IActionResult> Post([FromBody] JsonObject dato){
            await Task.Delay(100); //TODO: eliminate this time and refactor response
            var data =  JsonConvert.DeserializeObject<MyData>(dato.ToString());
            Console.WriteLine(data?.name);
            Console.WriteLine(data?.message);
            Console.WriteLine(this.key);

            Email email = new Email(data?.name??"", data?.message??"");
            email.SendEmail();
            return Ok(200);
        }
        [HttpGet(Name ="GetKey")]
        public async Task<string> Get(){
            await Task.Delay(1);
            Console.WriteLine(key);
            return System.Text.Json.JsonSerializer.Serialize(key);
        }
    }
    //TODO: move class to models
    public class MyData{
        public string name {get; set;}="";
        public string message {get; set;}="";
    }
}