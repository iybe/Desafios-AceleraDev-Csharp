using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext context;

        public ChallengeService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            var listaIdAceleraçoes = (
                from cand in context.Candidates.ToList()
                where cand.UserId == userId && cand.AccelerationId == accelerationId
                select cand.AccelerationId
            ).ToList();

            var listaIdDesafios = (
                from aceleraçao in context.Accelerations.ToList()
                join id in listaIdAceleraçoes on aceleraçao.Id equals id
                select aceleraçao.ChallengeId
            ).ToList();

            var listaDesafios = (
                from desafio in context.Challenges.ToList()
                join id in listaIdDesafios on desafio.Id equals id
                select desafio
            ).Distinct().ToList();

            return listaDesafios;
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            Models.Challenge modificado;
            if (challenge.Id == 0)
            {
                modificado = context.Challenges.Add(challenge).Entity;
            }
            else
            {
                modificado = (
                    from desafio in context.Challenges.ToList()
                    where desafio.Id == challenge.Id
                    select desafio
                ).ToList().First();
                modificado.Name = challenge.Name;
                modificado.Slug = challenge.Slug;
                modificado.CreatedAt = challenge.CreatedAt;
            }

            context.SaveChanges();
            return challenge;
        }
    }
}