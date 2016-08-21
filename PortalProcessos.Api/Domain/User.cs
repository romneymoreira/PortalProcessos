using PortalProcessos.Api.PortalContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalProcessos.Api.Domain
{
    public enum UserLevel
    {
        SysAdmin = 5,       //Administrador Técnico (FullAdmin)
        Admin = 4,          //Administrador do sistema
        Manager = 3,        //Acessa todas as funcionalidades do sistema, exceto funcionalidades de administração - Nao requer role associada
        SuperUser = 2,      //Acessa todas as funcionalidades do sistema, mas só altera as do seu papel (role) e definidas no seu usuario (UserFeature)
        User = 1            //Usuário padrão - Acessa apenas as funcionalidades definidas no seu papel(role) e funcionalidades definidas no seu usuario (UserFeature)
    }

    public enum UserStatus
    {
        Active = 1,              //Ativo
        Inactive = 0,            //Inativo
        PendingActivation = 2    //Pendente de ativação
    }

    public class User : DomainObject
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Mobile { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }
        public DateTime? TokenExpiration { get; private set; }
        public virtual UserLevel Level { get; private set; }
        public virtual UserStatus Status { get; private set; }

        protected User() { }

        public User(string userName, string password, string name, string email, bool encryptPassword = true)
        {
            if (userName == null)
                throw new ArgumentNullException("userName");
            if (password == null)
                throw new ArgumentNullException("password");
            if (name == null)
                throw new ArgumentNullException("name");
            if (email == null)
                throw new ArgumentNullException("email");


            this.UserName = userName;
            this.Name = name;
            this.Email = email;
            this.Status = UserStatus.Active;
            this.Level = UserLevel.User;
            SetPassword(password, encryptPassword);
            RequestValidationToken();
        }


        /// <summary>
        /// Ativa um usuário
        /// </summary>
        public void Activate()
        {
            this.Token = null;
            this.TokenExpiration = null;
            Status = UserStatus.Active;
        }

        /// <summary>
        /// Ativa um usuário alterando sua senha
        /// </summary>
        /// <param name="password">Nova senha</param>
        public void Activate(string password)
        {
            Activate();
            SetPassword(password);
        }

        /// <summary>
        /// Desativa um usuário
        /// </summary>
        public void Deactivate()
        {
            Status = UserStatus.Inactive;
        }

        /// <summary>
        /// Cria um novo token de validação para o usuário
        /// </summary>
        /// <param name="isPasswordReset">Booleano indicando se é reset de senha ou não. Caso positivo, o status do usuário permanece inalterado.</param>
        public void RequestValidationToken(bool isPasswordReset = false)
        {
            this.Token = Guid.NewGuid().ToString("N");
            this.TokenExpiration = DateTime.Now.AddMonths(1);
            if (!isPasswordReset)
                this.Status = UserStatus.PendingActivation;
        }

        /// <summary>
        /// Indica se existe token associado
        /// </summary>
        /// <returns>True para token associado</returns>
        public bool HasRequestedToken()
        {
            return !string.IsNullOrEmpty(this.Token);
        }

        /// <summary>
        /// Valida um token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>True para token válido</returns>
        public bool MatchToken(string token)
        {
            return this.Token == token;
        }

        /// <summary>
        /// Verifica se um token está expirado
        /// </summary>
        /// <returns>True para token na validade</returns>
        public bool IsTokenUpDate()
        {
            return this.TokenExpiration >= DateTime.Now;
        }


        public void ResetToDefaultPassword(bool encryptPassword = true)
        {
            SetPassword("mudar1234", encryptPassword);
        }

        public bool MatchPassword(string password)
        {
            return Hash(password) == this.Password;
        }

        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        public static string CreateUrl(string baseUrl, params string[] urlParams)
        {
            return baseUrl + urlParams.Aggregate(string.Empty, (current, s) => current + ("/" + s));
        }

        public void SetPassword(string password, bool encryptPassword = true)
        {
            if (password.Length < 6 || password.Length > 200)
                throw new Exception("A senha deve conter entre 6 e 20 caracteres");
            if (encryptPassword)
                this.Password = Hash(password);
            else
                this.Password = password;
        }

        public void SetNome(string nome)
        {
            if (!String.IsNullOrEmpty(nome))
                this.Name = nome;
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }

        public void SetMobile(string mobile)
        {
            if (!String.IsNullOrEmpty(mobile))
                this.Mobile = mobile;
        }

        public void SetNivel(UserLevel level)
        {
            this.Level = level;
        }
    }
}
