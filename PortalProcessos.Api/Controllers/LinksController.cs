using PortalProcessos.Api.Domain;
using PortalProcessos.Api.Models;
using PortalProcessos.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PortalProcessos.Api.Controllers
{
    [Authorize]
    [RoutePrefix("links")]
    public class LinksController : ApiController
    {
        private readonly IPortalRepository _repository;
        public LinksController(IPortalRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("getAll")]
        public List<LinksPortalModel> GetLinks()
        {
            var result = new List<LinksPortalModel>();
            var links = _repository.GetLinks();
            foreach (var item in links)
            {
                result.Add(new LinksPortalModel
                {
                    DataInclusao = item.DataInclusao,
                    IdLink = item.IdLink,
                    NomeLink = item.NomeLink,
                    UrlLink = item.UrlLink,
                    IdSetor = item.Setor.IdSetor,
                    NomeSetor = item.Setor.NomeSetor
                });
            }
            return result;
        }



        [HttpGet]
        [Route("excluir")]
        public void Excluir(int idLink)
        {
            try
            {
                var link = _repository.GetLinkById(idLink);
                _repository.ExcluirLink(link);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o link. " + ex.Message);
            }
        }

        [HttpPost]
        [Route("save")]
        public void Save(LinksPortalModel link)
        {
            try
            {
                var novoLink = new LinksPortal();
                var setor = _repository.GetSetorById(link.IdSetor);
                if (setor != null)
                {
                    novoLink.Setor = setor;
                    novoLink.NomeLink = link.NomeLink;
                    novoLink.UrlLink = link.UrlLink;
                    novoLink.DataInclusao = DateTime.Now;
                }
                else
                {
                    throw new Exception("Ocorreu um erro ao recuperar o setor selecionado.");
                }
                _repository.SaveLink(novoLink);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao cadastrar o novo link. " + ex.Message);
            }

        }
        [HttpGet]
        [Route("getByIdSetor")]
        public List<LinksPortalModel> GetLinksBySetor(int idSetor)
        {
            var result = new List<LinksPortalModel>();
            var links = _repository.GetLinksBySetor(idSetor);
            foreach (var item in links)
            {
                result.Add(new LinksPortalModel
                {
                    DataInclusao = item.DataInclusao,
                    IdLink = item.IdLink,
                    NomeLink = item.NomeLink,
                    UrlLink = item.UrlLink,
                    IdSetor = item.Setor.IdSetor,
                    NomeSetor = item.Setor.NomeSetor
                });
            }
            return result;
        }
    }
}

