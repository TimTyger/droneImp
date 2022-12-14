using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_implementation.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace drone_implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : BaseController<Drone>
    {
        private readonly ILogger _logger;
        private readonly IDroneService _droneService;
        public DispatchController(IBaseService<Drone,BaseDto> baseService, IBaseResponse<object> baseResponse,ILogger logger, IDroneService droneService, IMedicationService medicationService) : base(baseService, baseResponse)
        {
            _logger = logger;
            _droneService = droneService;
        }


        [HttpGet]
        [Route("FetchAllDrones")]

        [ProducesResponseType(typeof(BaseResult<List<DroneResp>>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> GetAllDrones()
        {
            var response =  await _droneService.FetchDrones();
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpGet]
        [Route("FetchAvailableDrones")]

        [ProducesResponseType(typeof(BaseResult<List<DroneResp>>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> GetAllAvailbleDrones()
        {
            var response =  await _droneService.FetchAvailbleDrones();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{serialNo}")]
        [Route("FetchDroneItems")]

        [ProducesResponseType(typeof(BaseResult<List<MedicationResp>>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> GetDroneItems([FromRoute] string serialNo)
        {
            var response = await _droneService.FetchDroneItems(serialNo);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{serialNo}")]
        [Route("GetDroneBatteryLevel")]

        [ProducesResponseType(typeof(BaseResult<int>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> GetDroneBatteryLevel([FromRoute] string serialNo)
        {
            var response = await _droneService.GetDroneBatteryLevel(serialNo);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("RegisterDrone")]
        [ProducesResponseType(typeof(BaseResult<>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> RegisterDrone([FromBody] RegisterDroneDto newDrone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResult<object>
                {
                    ResponseCode = "99",
                    ResponseMessage = "Bad Request",
                    Success = false,
                    StatusCode = 400
                });

            }
            var response = await _droneService.RegisterDrone(newDrone);

            return StatusCode(response.StatusCode, response);

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
