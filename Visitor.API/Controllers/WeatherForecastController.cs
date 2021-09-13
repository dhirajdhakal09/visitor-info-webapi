using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Visitor.Service;
using Visitor.Domain.DTO;
using Newtonsoft.Json;

namespace Visitor.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitorController : ControllerBase
    {

        private readonly IVisitorsService _visitorsService;

        public VisitorController(IVisitorsService visitorsService)
        {
            _visitorsService = visitorsService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            IEnumerable<VisitorDTO> visitors = _visitorsService.GetVisitors();

            if (visitors.Count() > 0)
            {
                string jsonRes = JsonConvert.SerializeObject(visitors);
                return Ok(jsonRes);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Route("getcheckedin")]
        public IActionResult GetCheckedInVisitor()
        {
            VisitorDTO visitor = _visitorsService.GetCheckedInVisitor();

            if (visitor != null)
            {
                string jsonRes = JsonConvert.SerializeObject(visitor);
                return Ok(jsonRes);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("create")]
        public IActionResult Post(Visitor.Domain.DTO.VisitorDTO visitorDTO)
        {
            bool itemCreate = _visitorsService.CreateVisitor(visitorDTO);
            if (itemCreate) return Ok();
            else return BadRequest();
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Put(Visitor.Domain.DTO.VisitorDTO visitorDTO)
        {
            bool itemUpdate = _visitorsService.UpdateVisitor(visitorDTO);
            if (itemUpdate)
            {
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(int visitorId)
        {
            return null;
        }

    }
}
