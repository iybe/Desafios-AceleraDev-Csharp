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
    public class CompanyController : ControllerBase
    {
        private ICompanyService _service;
        private IMapper _mapper;
        private ConvertToListDTO<Company, CompanyDTO> _serviceConvertToListDTO;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _serviceConvertToListDTO = new ConvertToListDTO<Company, CompanyDTO>(mapper);
        }

        // GET api/company/{id}
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            var result = _service.FindById(id);
            return Ok(_mapper.Map<CompanyDTO>(result));
        }

        // GET api/company
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if((accelerationId == null && userId == null) || (accelerationId != null && userId != null))
            {
                return StatusCode(204);
            }
            List<Company> list;
            if (accelerationId != null)
            {
                list = _service.FindByAccelerationId((int)accelerationId).ToList();
            }
            else
            {
                list = _service.FindByUserId((int)userId).ToList();
            }

            return Ok(_serviceConvertToListDTO.Execute(list));
        }

        // POST api/company
        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            var converted = _mapper.Map<Company>(value);
            var result = _service.Save(converted);
            return Ok(_mapper.Map<CompanyDTO>(result));
        }

    }
}
