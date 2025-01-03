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
    public class MasterPemainController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataPemain([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataPemain", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult ExportPemain([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_exportDataPemain", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult CreatePemain([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_createPemain", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }


    }
}
