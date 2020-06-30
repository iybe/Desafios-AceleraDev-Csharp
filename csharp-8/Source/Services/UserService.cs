using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext context;

        public UserService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            var accelerationId = context.Accelerations.Where(a => a.Name == name).FirstOrDefault().Id;

            return context.Candidates.Where(c => c.AccelerationId == accelerationId).Select(c => c.User).ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            var listaIdUsuarios = (
                from cand in context.Candidates.ToList()
                where companyId == cand.CompanyId
                select cand.UserId
            ).ToList();

            var listaUsuarios = (
                from user in context.Users.ToList()
                join idUser in listaIdUsuarios on user.Id equals idUser
                select user
            ).Distinct().ToList();

            return listaUsuarios;
        }

        public User FindById(int id)
        {
            return (
                from user in context.Users.ToList()
                where user.Id == id
                select user
            ).ToList().First();
        }

        public User Save(User user)
        {
            User modificado;
            if (user.Id == 0)
            {
                modificado = context.Users.Add(user).Entity;
            }
            else
            {
                modificado = FindById(user.Id);
                modificado.Nickname = user.Nickname;
                modificado.Password = user.Password;
                modificado.FullName = user.FullName;
                modificado.Email = user.Email;
                modificado.CreatedAt = user.CreatedAt;
            }

            context.SaveChanges();
            return user;
        }
    }
}
