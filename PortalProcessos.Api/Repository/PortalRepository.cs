using PortalProcessos.Api.Domain;
using PortalProcessos.Api.Models;
using PortalProcessos.Api.PortalContext;
using PortalProcessos.Api.Repository.Interface;
using PortalProcessos.Api.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Repository
{
    public class PortalRepository : RepositoryBase<PortalContext.PortalContext>, IPortalRepository
    {
        public PortalRepository(IUnitOfWork<PortalContext.PortalContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        #region[Visitas ao portal]

        public List<VisitasPortal> TotalVisitas()
        {
            return Context.Select<VisitasPortal>().ToList();
        }

        public void AddVisita(VisitasPortal model)
        {
            Context.Insert(model);

            Context.Save();
        }

        #endregion

        #region[Atividades]
        public List<ProcessoAtividade> ListarAtividades()
        {
            return Context.Select<ProcessoAtividade>().ToList();
        }

        public List<ProcessoAtividade> AtividadesSearch(BuscaAtividadeModel model)
        {
            var predicate = PredicateBuilder.True<ProcessoAtividade>();
            if(model.IdSetor > 0)
                predicate = predicate.And(x => x.IdSetor == model.IdSetor);

            if(model.Status > 0)
                predicate = predicate.And(x => x.Status == model.Status);

            if (model.Tipo > 0)
                predicate = predicate.And(x => x.Tipo == model.Tipo);

            if(model.DataInicio != DateTime.MinValue && model.DataFim != DateTime.MinValue)
                predicate = predicate.And(x => x.DataSolicitacao >= model.DataInicio && x.DataSolicitacao <= model.DataFim);

            if (!String.IsNullOrEmpty(model.Responsavel))
                predicate = predicate.And(x => x.Responsavel == model.Responsavel);

            return Context.Select<ProcessoAtividade>().Where(predicate).ToList();
        }


        public void ExcluirAtividade(ProcessoAtividade atividade)
        {
            Context.Delete(atividade);
            Context.Save();
        }
        public ProcessoAtividade SaveAtividade(ProcessoAtividade model)
        {
            try
            {
                if (model.Id > 0)
                    Context.Update(model);
                else
                    Context.Insert(model);

                Context.Save();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return model;
        }

        public ProcessoAtividade GetById(int id)
        {
            return Context.Select<ProcessoAtividade>().Where(x => x.Id == id).FirstOrDefault();
        }

        #endregion

        #region[Setores]

        public List<Setores> Setores()
        {
            try
            {
                return Context.Select<Setores>().ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
            return null;
        }

        public Setores GetSetorById(int idSetor)
        {
            return Context.Select<Setores>().Where(x => x.IdSetor == idSetor).FirstOrDefault();
        }

        #endregion

        #region[Links Uteis]
        public List<LinksPortal> GetLinks()
        {
            return Context.Select<LinksPortal>().ToList();
        }

        public List<LinksPortal> GetLinksBySetor(int idSetor)
        {
            return Context.Select<LinksPortal>().Where(x => x.IdSetor == idSetor).ToList();
        }

        public LinksPortal GetLinkById(int idLink)
        {
            return Context.Select<LinksPortal>().Where(x => x.IdLink == idLink).FirstOrDefault();
        }

        public void ExcluirLink(LinksPortal link)
        {
            Context.Delete(link);

            Context.Save();
        }

        public void SaveLink(LinksPortal link)
        {
            Context.Insert(link);

            Context.Save();
        }
        #endregion

        #region[User]

        public User GetUserById(int idUser)
        {
            return Context.Select<User>().Where(x => x.Id == idUser).FirstOrDefault();
        }
        public List<User> GetUserByName(string name)
        {
            return Context.Select<User>().Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
        }

        public List<User> GetAllUsers()
        {
            return Context.Select<User>().ToList();
        }

        #endregion
    }
}
