using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private CodenationContext context;

        public AccelerationService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            var listaIdAceleracao = (
                from cand in context.Candidates.ToList()
                where cand.CompanyId == companyId
                select cand.AccelerationId
            ).ToList();

            var listaAceleraçoes = (
                from aceleraçao in context.Accelerations.ToList()
                from id in listaIdAceleracao
                where aceleraçao.Id == id
                select aceleraçao
            ).Distinct().ToList();

            return listaAceleraçoes;
        }

        public Acceleration FindById(int id)
        {
            var aceleraçao = (
                from ace in context.Accelerations.ToList()
                where ace.Id == id
                select ace
            ).ToList().First();

            return aceleraçao;
        }

        public Acceleration Save(Acceleration acceleration)
        {
            Acceleration modoficado;
            if (acceleration.Id == 0)
            {
                modoficado = context.Accelerations.Add(acceleration).Entity;
            }
            else
            {
                modoficado = FindById(acceleration.Id);
                modoficado.Name = acceleration.Name;
                modoficado.Slug = acceleration.Slug;
                modoficado.ChallengeId = acceleration.ChallengeId;
                modoficado.CreatedAt = acceleration.CreatedAt;
            }
            context.SaveChanges();
            return acceleration;
        }
    }
}
