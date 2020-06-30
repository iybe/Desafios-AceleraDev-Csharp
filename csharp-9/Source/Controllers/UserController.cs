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
    public class UserController : ControllerBase
    {
        private IUserService _service;
        private IMapper _mapper;
        private ConvertToListDTO<User, UserDTO> _serviceConvertToListDTO;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _serviceConvertToListDTO = new ConvertToListDTO<User, UserDTO>(mapper);
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {            
            if((accelerationName == null && companyId == null) || (accelerationName != null && companyId != null))
            {
                return StatusCode(204);
            }

            List<User> list;
            if (accelerationName != null)
            {
                list = _service.FindByAccelerationName(accelerationName).ToList();
            }
            else
            {
                list = _service.FindByCompanyId((int)companyId).ToList();
            }

            return Ok(_serviceConvertToListDTO.Execute(list));
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            var converted = _service.FindById(id);
            return Ok(_mapper.Map<UserDTO>(converted));
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            var converted = _mapper.Map<User>(value);
            var result = _service.Save(converted);
            return Ok(_mapper.Map<UserDTO>(result));
        }   
     
    }
}
