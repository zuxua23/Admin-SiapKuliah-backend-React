using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Admin_SiapKuliah_backend.Helper;
using System.Data;

namespace Admin_SiapKuliah_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MasterPuzzleController(IConfiguration configuration) : Controller
    {
        readonly PolmanAstraLibrary.PolmanAstraLibrary lib = new(configuration.GetConnectionString("DefaultConnection"));
        DataTable dt = new();
            
        [HttpPost]
        public IActionResult GetDataPuzzle([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataPuzzle", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult GetDataPuzzleById([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_getDataPuzzleById", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreatePuzzle([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_createPuzzle", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DetailPuzzle([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_detailPuzzle", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult EditPuzzle([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_editPuzzle", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult SetStatusPuzzle([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_setStatusPuzzle", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult DeletePuzzle([FromBody] dynamic data)
        {
            try
            {
                JObject value = JObject.Parse(data.ToString());
                dt = lib.CallProcedure("skh_deletePuzzle", EncodeData.HtmlEncodeObject(value));
                return Ok(JsonConvert.SerializeObject(dt));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
 