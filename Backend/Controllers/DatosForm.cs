using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    //[EnableCors("AllowAnyOriginPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DatosFormController : Controller
    {
        private readonly ILogger<DatosFormController> _logger;
        private readonly string key="AIzaSyDQhZgf_6KYm1zQVOLeGCrH5TkRWxH04sg";
        public DatosFormController(ILogger<DatosFormController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
         public async Task<IActionResult> Post([FromBody] JsonObject dato){
            await Task.Delay(100); //TODO: eliminate this time and refactor response
            var data =  JsonConvert.DeserializeObject<MyData>(dato.ToString());
            Console.WriteLine(data?.name);
            Console.WriteLine(data?.message);
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