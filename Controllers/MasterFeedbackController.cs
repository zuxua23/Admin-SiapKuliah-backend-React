
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Admin_SiapKuliah_backend.Helper;
using System.Data;

namespace Admin_SiapKuliah_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterFeedbackController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataFeedback([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataFeedback", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreateFeedback([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_createFeedback", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

    }
}
