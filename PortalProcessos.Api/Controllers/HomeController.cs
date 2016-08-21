using PortalProcessos.Api.Domain;
using PortalProcessos.Api.Models;
using PortalProcessos.Api.PortalContext;
using PortalProcessos.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PortalProcessos.Api.Controllers
{
    [Authorize]
    [RoutePrefix("home")]
    public class HomeController : ApiController
    {
        private readonly IPortalRepository _repository;
        public HomeController(IPortalRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("getLinks")]
        public int GetTotalLinks()
        {
            var links = _repository.GetLinks();
            return links.Count;
        }
        [HttpGet]
        [Route("getDocs")]
        public int QuantidadeDocumentos()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Documentos\\";
            //string path = Directory.GetCurrentDirectory();
            DirectoryInfo Dir = new DirectoryInfo(path);
            FileInfo[] Files = Dir.GetFiles("*", SearchOption.AllDirectories);
            return Files.Count();
            //return 0;
        }

        [HttpGet]
        [Route("addVisita")]
        public void AdicionarVisita(int idUser)
        {
            var user = _repository.GetUserById(idUser);
            if (user != null)
            {
                var visita = new VisitasPortal
                {
                    DataVisita = DateTime.Now,
                    Usuario = user
                };
                _repository.AddVisita(visita);
            }
            else
            {
                throw new ApiException("Erro ao recuperar o usuário logado.");
            }
        }
        [HttpGet]
        [Route("totalVisitas")]
        public VisitasModel TotalVisitasPortal(int idUser)
        {
            var model = new VisitasModel();
            var visitas = _repository.TotalVisitas();

            model.TotalVisitas = visitas.Count;
            model.TotalVisitasUsuario = visitas.Where(x => x.Usuario.Id == idUser).Count();

            return model;

        }
    }
}
