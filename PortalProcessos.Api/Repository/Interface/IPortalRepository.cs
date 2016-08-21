using PortalProcessos.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Repository.Interface
{
    public interface IPortalRepository
    {
        #region[Atividades]
        List<ProcessoAtividade> ListarAtividades();
        ProcessoAtividade SaveAtividade(ProcessoAtividade model);
        ProcessoAtividade GetById(int id);
        void ExcluirAtividade(ProcessoAtividade atividade);
        #endregion

        #region[Setores]

        Setores GetSetorById(int idSetor);
        List<Setores> Setores();

        #endregion

        #region[Links Uteis]
        List<LinksPortal> GetLinks();
        List<LinksPortal> GetLinksBySetor(int idSetor);
        LinksPortal GetLinkById(int idLink);
        void ExcluirLink(LinksPortal link);
        void SaveLink(LinksPortal link);
        #endregion

        #region[Visitas ao portal]
        void AddVisita(VisitasPortal model);
        List<VisitasPortal> TotalVisitas();

        #endregion

        #region [User]
        User GetUserById(int idUser);
        List<User> GetUserByName(string name);
        List<User> GetAllUsers();
        #endregion
    }
}
