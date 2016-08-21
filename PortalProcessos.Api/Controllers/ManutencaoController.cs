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
    [RoutePrefix("manutencao")]
    public class ManutencaoController : ApiController
    {
        private readonly IPortalRepository _repository;
        public ManutencaoController(IPortalRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Route("save")]
        public ProcessoAtividadeModel Save(ProcessoAtividadeModel model)
        {
            try
            {
                var setor = _repository.GetSetorById(model.IdSetor);

                if (setor != null)
                {
                    var atividade = new ProcessoAtividade();
                    atividade.Atividade = model.Atividade;
                    atividade.DataSolicitacao = DateTime.Now;
                    atividade.Observacao = model.Observacao;
                    atividade.Responsavel = model.Responsavel;
                    atividade.Tipo = model.Tipo;
                    atividade.Status = 1;//insere como pendente

                    var result = _repository.SaveAtividade(atividade);
                }
                else
                {
                    throw new Exception("Ocorreu um erro ao recuperar o setor.");
                }

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao incluir a atividade." + ex.Message);
            }
        }

        [HttpGet]
        [Route("listar")]
        public List<ProcessoAtividade> Listar()
        {
            return _repository.ListarAtividades();
        }

        [HttpGet]
        [Route("setores")]
        public List<SetorModel> Setores()
        {
            var lista = new List<SetorModel>();
            var setores = _repository.Setores();
            foreach (var item in setores)
            {
                lista.Add(new SetorModel
                {
                    IdSetor = item.IdSetor,
                    NomeSetor = item.NomeSetor
                });
            }
            return lista;
        }

        [HttpGet]
        [Route("byId")]
        public ProcessoAtividade GetById(int id)
        {
            return _repository.GetById(id);
        }
        [HttpGet]
        [Route("getAllUsers")]
        public List<UserModel> GetAllUsers()
        {
            var model = new List<UserModel>();
            var users = _repository.GetAllUsers();

            foreach (var item in users)
            {
                model.Add(new UserModel {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return model;
        }

        [HttpGet]
        [Route("excluirAtividade")]
        public void ExcluirAtividade(int id)
        {
            var atividade = _repository.GetById(id);
            _repository.ExcluirAtividade(atividade);
        }
    }
}
