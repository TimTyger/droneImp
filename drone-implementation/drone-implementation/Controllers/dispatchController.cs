﻿using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_implementation.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace drone_implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : BaseController<Drone>
    {
       // private readonly ILogger _logger;
        private readonly IDroneService _droneService;
        private readonly IDroneItemService _droneItemService;
        public DispatchController(IBaseService<Drone,BaseDto> baseService, IBaseResponse<object> baseResponse, IDroneItemService droneItemService, IDroneService droneService) : base(baseService, baseResponse)
        {
            //_logger = logger;
            _droneService = droneService;
            _droneItemService = droneItemService;
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


        [HttpPut]
        [Route("LoadDrone")]
        [ProducesResponseType(typeof(BaseResult<>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> LoadDrone([FromBody] LoadDroneDto loadDroneDto)
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
            var response = await _droneItemService.LoadDrone(loadDroneDto);

            return StatusCode(response.StatusCode, response);

        }

        [HttpGet()]
        [Route("FetchDroneItems/{serialNo}")]

        [ProducesResponseType(typeof(BaseResult<List<MedicationResp>>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> GetDroneItems([FromRoute] string serialNo)
        {
            var response = await _droneItemService.FetchDroneItems(serialNo);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet()]
        [Route("GetDroneBatteryLevel/{serialNo}")]

        [ProducesResponseType(typeof(BaseResult<int>), 200)]
        [ProducesResponseType(typeof(BaseResult<>), 400)]
        [ProducesResponseType(typeof(BaseResult<>), 500)]
        public async Task<IActionResult> GetDroneBatteryLevel([FromRoute] string serialNo)
        {
            var response = await _droneService.GetDroneBatteryLevel(serialNo);
            return StatusCode(response.StatusCode, response);
        }

    }
}
