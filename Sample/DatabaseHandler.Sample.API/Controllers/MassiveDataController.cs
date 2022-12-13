using DatabaseHandler.Sample.Domain.Interfaces.Services;
using DatabaseHandler.Sample.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DatabaseHandler.Sample.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MassiveDataController : Controller
    {
        private readonly IMassiveDataService _massiveService;
        public MassiveDataController(IMassiveDataService massiveService) => _massiveService = massiveService;

        [HttpPost()]
        [Route("/sql-server/with-identity")]
        public async Task<IActionResult> InsertMassiveDataWithIdentityToSqlServer([Required][FromBody] InsertMassiveDataRequest request)
        {
            await _massiveService.InsertMassiveDataWithIdentityToSqlServer(request.QuantityToGenerate);
            return Ok();
        }

        [HttpPost()]
        [Route("/sql-server/without-identity")]
        public async Task<IActionResult> InsertMassiveDataWithoutIdentityToSqlServer([Required][FromBody] InsertMassiveDataRequest request)
        {
            await _massiveService.InsertMassiveDataWithoutIdentityToSqlServer(request.QuantityToGenerate);
            return Ok();
        }

        [HttpPost()]
        [Route("/oracle/sequence-with-trigger")]
        public async Task<IActionResult> InsertMassiveDataSequenceWithTriggerToOracle([Required][FromBody] InsertMassiveDataRequest request)
        {
            await _massiveService.InsertMassiveDataSequenceWithTriggerToOracle(request.QuantityToGenerate);
            return Ok();
        }

        [HttpPost()]
        [Route("/oracle/sequence-without-trigger")]
        public async Task<IActionResult> InsertMassiveDataSequenceWithoutTriggerToOracle([Required][FromBody] InsertMassiveDataRequest request)
        {
            await _massiveService.InsertMassiveDataSequenceWithoutTriggerToOracle(request.QuantityToGenerate);
            return Ok();
        }

        [HttpPost()]
        [Route("/oracle/guid-with-trigger")]
        public async Task<IActionResult> InsertMassiveDataGuidWithTriggerToOracle([Required][FromBody] InsertMassiveDataRequest request)
        {
            await _massiveService.InsertMassiveDataGuidWithTriggerToOracle(request.QuantityToGenerate);
            return Ok();
        }

        [HttpPost()]
        [Route("/oracle/guid-without-trigger")]
        public async Task<IActionResult> InsertMassiveDataGuidWithoutTriggerToOracle([Required][FromBody] InsertMassiveDataRequest request)
        {
            await _massiveService.InsertMassiveDataGuidWithoutTriggerToOracle(request.QuantityToGenerate);
            return Ok();
        }
    }
}
