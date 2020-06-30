using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext context;

        public CandidateService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            var listaCandidatos = (
                from cand in context.Candidates.ToList()
                where cand.AccelerationId == accelerationId
                select cand
            ).ToList();

            return listaCandidatos;
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            var listaCandidatos = (
                from cand in context.Candidates.ToList()
                where cand.CompanyId == companyId
                select cand
            ).ToList();

            return listaCandidatos;
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            try
            {
                var candidato = (
                    from cand in context.Candidates.ToList()
                    where cand.CompanyId == companyId && cand.UserId == userId && cand.AccelerationId == accelerationId
                    select cand
                ).First();
                return candidato;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public Candidate Save(Candidate candidate)
        {
            Candidate modificado = FindById(candidate.UserId, candidate.CompanyId, candidate.AccelerationId);
            if (modificado == null)
            {
                modificado = context.Candidates.Add(candidate).Entity;
            }
            else
            {
                modificado.Status = candidate.Status;
                modificado.CreatedAt = candidate.CreatedAt;
            }

            context.SaveChanges();
            return modificado;
        }
    }
}
