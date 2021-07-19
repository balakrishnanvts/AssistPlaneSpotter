using APS.BAL;
using APS.Model;
using AssistPlaneSpotter.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace AssistPlaneSpotter.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(CustomExceptionFilter))]
    public class PlaneController : ControllerBase
    {
        private readonly IPlaneBAL _iPlaneBAL;
        public PlaneController(IPlaneBAL iPlaneBAL)
        {
            _iPlaneBAL = iPlaneBAL;
        }

        /// <summary>
        /// To get all the details or specific detail by id
        /// </summary>
        /// <param name="id">To check the value </param>
        /// <returns>List of Plane detail</returns>
        [HttpGet("{id?}")]
        public IActionResult Get(int id = 0)
        {
            return Ok(_iPlaneBAL.GetPlane(id).Result);
        }

        /// <summary>
        /// To Create and Modify the Plane
        /// </summary>
        /// <param name="plane">Details of Plane</param>
        /// <returns>Integer value to determain that manipulation successful or failure</returns>
        [HttpPost]
        public IActionResult Post()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var plane = JsonConvert.DeserializeObject<Plane>(reader.ReadToEndAsync().Result);
                return Ok(_iPlaneBAL.ManagePlane(plane).Result);
            }
        }

        /// <summary>
        /// To Delete the existing Plane details
        /// </summary>
        /// <param name="id">Id of Plane which need to be deleted</param>
        /// <returns>Integer value to determain that deletion successful or failure</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_iPlaneBAL.DeletePlane(id).Result);
        }
    }
}