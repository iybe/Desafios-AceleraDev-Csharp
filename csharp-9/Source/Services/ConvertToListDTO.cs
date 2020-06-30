using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class ConvertToListDTO<TypeOrigin, TypeDTO>
    {
        private IMapper _mapper;

        public ConvertToListDTO(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<TypeDTO> Execute(List<TypeOrigin> list)
        {
            List<TypeDTO> result = new List<TypeDTO>();
            foreach (var obj in list)
            {
                result.Add(_mapper.Map<TypeDTO>(obj));
            }
            return result;
        }
    }
}
