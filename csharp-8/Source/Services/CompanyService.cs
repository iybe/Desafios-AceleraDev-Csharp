using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext context;

        public CompanyService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return context.Candidates.Where(c => c.AccelerationId == accelerationId).Select(c => c.Company).ToList(); ;
        }

        public Company FindById(int id)
        {
            var company = (
                from comp in context.Companies.ToList()
                where comp.Id == id
                select comp
            ).ToList().First();

            return company;
        }

        public IList<Company> FindByUserId(int userId)
        {
            var listaCompany = (
                from cd in context.Candidates
                join c in context.Companies on cd.CompanyId equals c.Id
                where cd.UserId == userId
                select c
            ).Distinct().ToList();

            return listaCompany;
        }

        public Company Save(Company company)
        {
            Company modificado;
            if (company.Id == 0)
            {
                modificado = context.Companies.Add(company).Entity;
            }
            else
            {
                modificado = FindById(company.Id);
                modificado.Name = company.Name;
                modificado.Slug = company.Slug;
                modificado.CreatedAt = company.CreatedAt;
            }

            context.SaveChanges();
            return company;
        }
    }
}