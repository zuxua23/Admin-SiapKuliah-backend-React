using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Admin_SiapKuliah_backend.Helper;
using System.Data;

namespace Admin_SiapKuliah_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterQuizController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataQuiz([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataQuiz", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetDataQuizById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataQuizById", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateQuiz([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_createQuiz", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DetailQuiz([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_detailQuiz", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult EditQuiz([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_editQuiz", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult SetStatusQuiz([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_setStatusQuiz", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DeleteQuiz([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_deleteQuiz", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
