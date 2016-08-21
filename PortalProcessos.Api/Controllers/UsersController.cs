using PortalProcessos.Api.Domain;
using PortalProcessos.Api.Models;
using PortalProcessos.Api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace PortalProcessos.Api.Controllers
{
    [Authorize]
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userService;

        public UsersController(IUserRepository userService)
        {
            _userService = userService;
        }

        [Route]
        public IEnumerable<UserModel> Get()
        {
            try
            {
                var usuarios = new List<UserModel>();
                var result = _userService.GetUsers().ToList();
                foreach (var item in result)
                {
                    usuarios.Add(new UserModel
                    {
                        Email = item.Email,
                        Id = item.Id,
                        LevelValue = 1,
                        Mobile = item.Mobile,
                        Name = item.Name,
                        Password = item.Password,
                        StatusValue = 1,
                        UserName = item.UserName
                    });
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("getLevel")]
        public List<UserLevelModel> EnumerateAllUserLevel()
        {
            var lista = new List<UserLevelModel>();
            int i = 1;
            foreach (UserLevel level in (UserLevel[])Enum.GetValues(typeof(UserLevel)))
            {
                var model = new UserLevelModel { Key = i, Value = level.ToString() };
                lista.Add(model);
                i++;
            }
            return lista;
        }



        [Route("{id:int}")]
        public UserModel GetById(int id)
        {
            var model = new UserModel();
            try
            {
                var result = _userService.GetUser(id);
                //todo
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("salvar")]
        public UserModel Save(UserModel model)
        {
            try
            {
                User user;

                if (model.Id > 0)
                {
                    user = _userService.GetUser(model.Id);


                    //Senha
                    if (!string.IsNullOrEmpty(model.Password) || model.UsarSenhaPadrao)
                    {
                        if (model.UsarSenhaPadrao)
                        {
                            if (model.UsarCriptografia)
                                user.ResetToDefaultPassword();
                            else
                                user.ResetToDefaultPassword(false);
                        }
                        else
                        {
                            if (model.UsarCriptografia)
                                user.SetPassword(model.Password);
                            else
                                user.SetPassword(model.Password, false);
                        }

                    }

                   

                    user.SetNome(model.Name);
                    user.SetEmail(model.Email);
                    user.SetMobile(model.Mobile);
                    user.SetNivel(RetornaUserLevel(model.LevelValue));
                    user.Activate();

                    _userService.SaveUser(user);
                }
                else
                {
                    //Senha
                    if (model.UsarSenhaPadrao)
                    {
                        user = new User(model.UserName, Guid.NewGuid().ToString(), model.Name, model.Email);

                        if (model.UsarCriptografia)
                            user.ResetToDefaultPassword();
                        else
                            user.ResetToDefaultPassword(false);
                    }
                    else
                    {
                        if (model.UsarCriptografia)
                            user = new User(model.UserName, model.Password, model.Name, model.Email);
                        else
                            user = new User(model.UserName, model.Password, model.Name, model.Email, false);
                    }


                    //adiciona o telefone
                    user.SetMobile(model.Mobile);

                    //adiciona o level do usuário
                    user.SetNivel(RetornaUserLevel(model.LevelValue));

                   

                    //Ativa o usuário
                    user.Activate();

                  _userService.SaveUser(user);
                }

                return new UserModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      

        public UserLevel RetornaUserLevel(int id)
        {
            switch (id)
            {
                case 1:
                    return UserLevel.User;
                case 2:
                    return UserLevel.SuperUser;
                case 3:
                    return UserLevel.Manager;
                case 4:
                    return UserLevel.Admin;
                case 5:
                    return UserLevel.SysAdmin;
                default:
                    return UserLevel.User;
            }
        }

      
    }
}
