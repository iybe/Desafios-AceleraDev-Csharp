using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService _service;
        private IMapper _mapper;
        private ConvertToListDTO<Acceleration, AccelerationDTO> _serviceConvertToListDTO;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _serviceConvertToListDTO = new ConvertToListDTO<Acceleration, AccelerationDTO>(mapper);
        }

        // GET api/acceleration
        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId == null)
            {
                return StatusCode(204);
            }

            List<Acceleration> list;
            list = _service.FindByCompanyId((int)companyId).ToList();

            return Ok(_serviceConvertToListDTO.Execute(list));
        }

        // GET api/acceleration/{id}
        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            var result = _service.FindById(id);
            return Ok(_mapper.Map<AccelerationDTO>(result));
        }

        // POST api/acceleration
        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            var converted = _mapper.Map<Acceleration>(value);
            var result = _service.Save(converted);
            return Ok(_mapper.Map<AccelerationDTO>(result));
        }
    }
}
