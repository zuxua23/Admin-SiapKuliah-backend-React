using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Admin_SiapKuliah_backend.Helper;
using System.Data;

namespace Admin_SiapKuliah_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterTebakGambarController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();

        [HttpPost]
        public IActionResult GetDataTebakGambar([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataTebakGambar", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetDataTebakGambarById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataTebakGambarById", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateTebakGambar([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_createTebakGambar", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DetailTebakGambar([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_detailTebakGambar", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult EditTebakGambar([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_editTebakGambar", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {   
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult SetStatusTebakGambar([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_setStatusTebakGambar", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DeleteTebakGambar([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_deleteTebakGambar", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
