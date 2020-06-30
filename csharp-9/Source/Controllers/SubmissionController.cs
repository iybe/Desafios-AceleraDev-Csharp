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
    public class SubmissionController : ControllerBase
    {
        private ISubmissionService _service;
        private IMapper _mapper;
        private ConvertToListDTO<Submission, SubmissionDTO> _serviceConvertToListDTO;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _serviceConvertToListDTO = new ConvertToListDTO<Submission, SubmissionDTO>(mapper);
        }

        // GET api/submission
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? accelerationId = null, int? challengeId = null)
        {
            if (accelerationId == null && challengeId == null)
            {
                return StatusCode(204);
            }

            List<Submission> list;
            list = (List<Submission>)_service.FindByChallengeIdAndAccelerationId((int)challengeId, (int)accelerationId);

            return Ok(_serviceConvertToListDTO.Execute(list));
        }

        // GET api/submission/higherScore
        [HttpGet("/higherScore")]
        public ActionResult<decimal> GetHigherScore(int challengeId)
        {
            var result = _service.FindHigherScoreByChallengeId(challengeId);
            return Ok(result);
        }

        // POST api/submission
        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
            var converted = _mapper.Map<Submission>(value);
            var result = _service.Save(converted);
            return Ok(_mapper.Map<SubmissionDTO>(result));
        }
    }
}
