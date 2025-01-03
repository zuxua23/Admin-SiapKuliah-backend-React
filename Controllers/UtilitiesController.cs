using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Admin_SiapKuliah_backend.Helper;
using System.Data;
using System.Runtime.Versioning;

namespace Admin_SiapKuliah_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UtilitiesController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        readonly LDAPAuthentication adAuth = new(configuration);
        DataTable dt = new();

        [HttpPost]
        [SupportedOSPlatform("windows")]
        public IActionResult Login([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("sso_getAuthenticationPMB", EncodeData.HtmlEncodeObject(value));
                if (dt.Rows.Count == 0)
                    return Ok(JsonConvert.SerializeObject(new { Status = "LOGIN FAILED" }));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult GetListMenu([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("all_getListMenu", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
 
        [HttpPost]
        public IActionResult GetListKaryawan([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("pro_getListKaryawan", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch { return BadRequest(); }
        }
    }
}
