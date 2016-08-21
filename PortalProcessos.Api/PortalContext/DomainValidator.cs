using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PortalProcessos.Api.PortalContext
{
    /// <summary>
    /// Classe para validações básicas de objetos do domínio
    /// </summary>
    public sealed class DomainValidator
    {
        private readonly string _field;
        private readonly object _value;
        private readonly bool _isNull;

        /// <summary>
        /// Construtor do objeto de validacao
        /// </summary>
        /// <param name="fieldName">Nome do campo a ser validado</param>
        /// <param name="value">Valor do campo a ser validado</param>
        public DomainValidator(string fieldName, object value)
        {
            _field = fieldName;
            _value = value;
            _isNull = IsNull(value);
        }

        /// <summary>
        /// Valida o MaxLength de um campo
        /// </summary>
        /// <param name="length">Largura máxima do campo</param>
        public DomainValidator MaxLength(int length)
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    if ((_value as string).Length > length)
                        throw new ArgumentOutOfRangeException(_field, string.Format("O campo '{0}' excedeu o limite de {1} caracteres", _field, length.ToString()));
                }
            }
            return this;
        }

        /// <summary>
        /// Valida o MinLength de um campo
        /// </summary>
        /// <param name="length">Largura mínima do campo</param>
        public DomainValidator MinLength(int length)
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    if ((_value as string).Length < length)
                        throw new ArgumentOutOfRangeException(_field, string.Format("O campo '{0}' deve ter no minimo {1} caracteres", _field, length.ToString()));
                }
            }
            return this;
        }

        /// <summary>
        /// Valida o Length de um campo
        /// </summary>
        /// <param name="length">Largura exata do campo</param>
        public DomainValidator ExactLength(int length)
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    if ((_value as string).Length != length)
                        throw new ArgumentOutOfRangeException(_field, string.Format("O campo '{0}' deve ter {1} caracteres", _field, length.ToString()));
                }
            }
            return this;
        }
        /// <summary>
        /// Valida um objeto non-nullable
        /// </summary>
        public DomainValidator IsNotNull()
        {
            if (_isNull)
                throw new ArgumentNullException(_field, "O campo '" + _field + "' não pode ser NULL");

            return this;
        }

        /// <summary>
        /// Valida se uma string é null ou vazia
        /// </summary>
        public DomainValidator IsNullOrEmpty()
        {
            bool valid = false;
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    if (!string.IsNullOrEmpty(_value as string))
                        valid = true;
                }
            }

            if (!valid)
                throw new ArgumentException(string.Format("O campo '{0}' não pode ser NULL ou VAZIO", _field));

            return this;
        }

        /// <summary>
        /// Valida se uma string é numérica
        /// </summary>
        public DomainValidator IsNumber()
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    Regex regex = new Regex(RegexTypes.OnlyNumbers);
                    if (!regex.IsMatch(_value as string))
                        throw new ArgumentException(string.Format("'{0}' deve conter apenas digitos numericos", _field));
                }
            }
            return this;
        }

        /// <summary>
        /// Valida se o valor é maior que zero
        /// </summary>
        public DomainValidator IsGreaterThanZero()
        {
            if (!_isNull)
            {
                try
                {
                    decimal value = Convert.ToDecimal(_value);
                    if (value <= 0)
                        throw new ArgumentException(string.Format("'{0}' deve ser maior que zero", _field));
                }
                catch (Exception)
                {
                    throw new ArgumentException(string.Format("Falha na conversão para validação do Metodo 'IsGreaterThanZero' no campo '{0}' para o valor '{1}'", _field, _value.ToString()));
                }
            }
            return this;
        }

        /// <summary>
        /// Valida se o valor é maior OU igual a zero
        /// </summary>
        public DomainValidator IsGreaterOrEqualToZero()
        {
            if (!_isNull)
            {
                try
                {
                    decimal value = Convert.ToDecimal(_value);
                    if (value < 0)
                        throw new ArgumentException(string.Format("'{0}' deve ser maior ou igual a zero", _field));
                }
                catch (Exception)
                {
                    throw new ArgumentException(string.Format("Falha na conversão para validação do Metodo 'IsGreaterOrEqualToZero' no campo '{0}' para o valor '{1}'", _field, _value.ToString()));
                }
            }
            return this;
        }

        /// <summary>
        /// Verifica se uma data é válida
        /// </summary>
        public DomainValidator IsValidDateTime()
        {
            if (!_isNull)
            {
                if (CheckType<DateTime>())
                {
                    if ((DateTime)_value == DateTime.MinValue)
                        throw new ArgumentException(string.Format("'{0}' não é uma data válida para o campo '{1}'", ((DateTime)_value).ToString(), _field));
                }
            }
            return this;
        }

        public DomainValidator IsValidMail()
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    const String pattern =
                   @"^([0-9a-zA-Z]" +
                   @"([\+\-_\.][0-9a-zA-Z]+)*" +
                   @")+" +
                   @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";

                    if (!Regex.IsMatch(_value.ToString(), pattern))
                        throw new ArgumentException(string.Format("O campo '{0}' não é válido", _field));
                }
            }

            return this;
        }

        public DomainValidator IsValidCpf()
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {
                    int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                    int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                    string TempCPF;
                    string Digito;
                    int soma;
                    int resto;

                    string CPF = _value.ToString();
                    CPF = CPF.Trim();
                    CPF = CPF.Replace(".", "").Replace("-", "");

                    if (CPF.Length != 11)
                        throw new ArgumentException(string.Format("O campo '{0}' não é válido", _field));

                    TempCPF = CPF.Substring(0, 9);
                    soma = 0;
                    for (int i = 0; i < 9; i++)
                        soma += int.Parse(TempCPF[i].ToString()) * mt1[i];

                    resto = soma % 11;
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;

                    Digito = resto.ToString();
                    TempCPF = TempCPF + Digito;
                    soma = 0;

                    for (int i = 0; i < 10; i++)
                        soma += int.Parse(TempCPF[i].ToString()) * mt2[i];

                    resto = soma % 11;
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;

                    Digito = Digito + resto.ToString();
                    if (!CPF.EndsWith(Digito))
                        throw new ArgumentException(string.Format("O campo '{0}' não é válido", _field));
                }
            }
            return this;
        }

        public DomainValidator IsValidCnpj()
        {
            if (!_isNull)
            {
                if (CheckType<string>())
                {

                    int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                    int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                    int soma;
                    int resto;
                    string digito;
                    string tempCnpj;
                    string cnpj = _value.ToString();
                    cnpj = cnpj.Trim();
                    cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                    if (cnpj.Length != 14)
                        throw new ArgumentException(string.Format("O campo '{0}' não é válido", _field));

                    tempCnpj = cnpj.Substring(0, 12);
                    soma = 0;
                    for (int i = 0; i < 12; i++)
                        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                    resto = (soma % 11);
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;
                    digito = resto.ToString();
                    tempCnpj = tempCnpj + digito;
                    soma = 0;
                    for (int i = 0; i < 13; i++)
                        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                    resto = (soma % 11);
                    if (resto < 2)
                        resto = 0;
                    else
                        resto = 11 - resto;
                    digito = digito + resto.ToString();
                    if (!cnpj.EndsWith(digito))
                        throw new ArgumentException(string.Format("O campo '{0}' não é válido", _field));
                }
            }

            return this;
        }


        #region Private Methods

        private bool CheckType<T>([CallerMemberName] string memberName = "")
        {
            if (_value is T)
                return true;
            else
                throw new ArgumentException("O método de validação '" + memberName + "' espera o tipo '" + (typeof(T)).Name + "', mas recebeu o tipo '" + _value.GetType().Name + "'");
        }

        private bool IsNull(object value)
        {
            bool isNull = false;

            if (value == null)
                isNull = true;
            else
            {
                Type type = value.GetType();
                //else if (!type.IsValueType)
                //    isNull = true;
                if (Nullable.GetUnderlyingType(type) != null)
                    isNull = true;
            }

            return isNull;
        }

        #endregion
    }
}
