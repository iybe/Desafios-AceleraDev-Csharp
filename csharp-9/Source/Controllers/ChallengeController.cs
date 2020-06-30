using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Codenation.Challenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private IChallengeService _service;
        private IMapper _mapper;
        private ConvertToListDTO<Models.Challenge, ChallengeDTO> _serviceConvertToListDTO;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _serviceConvertToListDTO = new ConvertToListDTO<Models.Challenge, ChallengeDTO>(mapper);
        }
        
        // GET api/challenge
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId == null && userId == null)
            {
                return StatusCode(204);
            }
            List<Models.Challenge> list;
            list = _service.FindByAccelerationIdAndUserId((int)accelerationId, (int)userId).ToList();

            return Ok(_serviceConvertToListDTO.Execute(list));
        }

        // POST api/challenge
        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody] ChallengeDTO value)
        {
            var converted = _mapper.Map<Models.Challenge>(value);
            var result = _service.Save(converted);
            return Ok(_mapper.Map<ChallengeDTO>(result));
        }
    }
}
