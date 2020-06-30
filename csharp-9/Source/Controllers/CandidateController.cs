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
    public class CandidateController : ControllerBase
    {
        private ICandidateService _service;
        private IMapper _mapper;
        private ConvertToListDTO<Candidate, CandidateDTO> _serviceConvertToListDTO;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _serviceConvertToListDTO = new ConvertToListDTO<Candidate, CandidateDTO>(mapper);
        }

        // GET api/candidate
        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {
            if ((accelerationId == null && companyId == null) || (accelerationId != null && companyId != null))
            {
                return StatusCode(204);
            }

            List<Candidate> list;
            if (accelerationId != null)
            {
                list = _service.FindByAccelerationId((int)accelerationId).ToList();
            }
            else
            {
                list = _service.FindByCompanyId((int)companyId).ToList();
            }

            return Ok(_serviceConvertToListDTO.Execute(list));
        }

        // GET api/candidate/{userId}/{accelerationId}/{companyId}
        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            var result = _service.FindById(userId, accelerationId, companyId);
            return Ok(_mapper.Map<CandidateDTO>(result));
        }

        // POST api/candidate
        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            var converted = _mapper.Map<Candidate>(value);
            var result = _service.Save(converted);
            return Ok(_mapper.Map<CandidateDTO>(result));
        }
    }
}
