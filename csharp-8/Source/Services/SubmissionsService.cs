using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext context;

        public SubmissionService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            var idUsuariosPorAceleraçao = (
                from cand in context.Candidates.ToList()
                where cand.AccelerationId == accelerationId
                select cand.UserId
            );

            var submissoes = (
                from sub in context.Submissions.ToList()
                join id in idUsuariosPorAceleraçao on sub.UserId equals id
                where sub.ChallengeId == challengeId
                select sub
            ).Distinct().ToList();

            return submissoes;
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            var maiorScore = (
                from sub in context.Submissions.ToList()
                where sub.ChallengeId == challengeId
                orderby sub.Score descending
                select sub.Score
            ).First();

            return maiorScore;
        }

        public Submission FindByIds(int UserId, int ChallengeId)
        {
            try
            {
                var submissão = (
                    from sub in context.Submissions.ToList()
                    where sub.UserId == UserId && sub.ChallengeId == ChallengeId
                    select sub
                ).First();
                return submissão;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public Submission Save(Submission submission)
        {
            Submission modificado = FindByIds(submission.UserId, submission.ChallengeId);

            if (modificado == null)
            {
                modificado = context.Submissions.Add(submission).Entity;
            }
            else
            {
                modificado.Score = submission.Score;
                modificado.CreatedAt = submission.CreatedAt;
            }
            context.SaveChanges();
            return modificado;
        }
    }
}
