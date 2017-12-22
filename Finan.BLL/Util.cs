using Finan.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finan.BLL
{
   public static class Util
    {
        public static string TrataException(Exception erro)
        {
            try 
	        {
                string s = erro.Message;
                if (erro.InnerException != null)
                {
                    s += "\n" + erro.InnerException.Message;

                    if (erro.InnerException.InnerException != null)
                    {
                        s += "\n" + erro.InnerException.InnerException.Message;

                        if (erro.InnerException.InnerException.InnerException != null)
                        {
                            s += "\n" + erro.InnerException.InnerException.InnerException.Message;

                            if (erro.InnerException.InnerException.InnerException.InnerException != null)
                            {
                                s += "\n" + erro.InnerException.InnerException.InnerException.InnerException.Message;
                            }
                        }
                    }
                }

                s = s.Replace("An error occurred while updating the entries.", "");
                s = s.Replace("See the inner exception for details.", "");

                return s;
	        }
	        catch (Exception erro2)
	        {
                return erro.Message + " + " + erro2.Message;
	        }
        }

        public static string TrataException(DbEntityValidationException erro)
        {
            try
            {
                string s = erro.Message;
                if (erro.InnerException != null)
                {
                    s += "\n" + erro.InnerException.Message;

                    if (erro.InnerException.InnerException != null)
                    {
                        s += "\n" + erro.InnerException.InnerException.Message;

                        if (erro.InnerException.InnerException.InnerException != null)
                        {
                            s += "\n" + erro.InnerException.InnerException.InnerException.Message;

                            if (erro.InnerException.InnerException.InnerException.InnerException != null)
                            {
                                s += "\n" + erro.InnerException.InnerException.InnerException.InnerException.Message;
                            }
                        }
                    }
                }

                foreach (var eve in erro.EntityValidationErrors)
                {
                    string titulo = string.Format("Entidade do tipo '{0}' apresentou os seguintes erros de validação:\n", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    string prop = "";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        prop += string.Format("{0}\n", ve.ErrorMessage);
                    }
                    s += prop;
                }

                s = s.Replace("An error occurred while updating the entries.", "");
                s = s.Replace("See the inner exception for details.", "");

                return s;
            }
            catch (Exception erro2)
            {
                return erro.Message + " + " + erro2.Message;
            }
        }

        public static Int64 ToInt64(this string value)
        {
            try
            {
                value = value.SoNum();
                return Convert.ToInt64(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int ToInt(this string value)
        {
            try
            {
                value = value.SoNum();
                int n = 0;
                int.TryParse(value, out n);
                return n;
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static int ToInt(this object value)
        {
            try
            {
                string texto = value.ToString();
                
                texto = texto.SoNum();

                return Convert.ToInt32(texto);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static bool ToBool(this string value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool ToBool(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Converte string para inteiro ou para nulo
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return null;
            else
            {
                int i = 0;
                int.TryParse(valor, out i);
                return i;
            }
        }

        public static double ToDouble(this string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return 0;

                return Convert.ToDouble(value);
            }
            catch (Exception)
            {
                return 0;
            }


        }

        public static double ToDouble(this object value)
        {
            try
            {
                string texto = value.ToString();

                if (string.IsNullOrEmpty(texto))
                    return 0;

                return Convert.ToDouble(texto);
            }
            catch (Exception)
            {
                return 0;
            }


        }

        /// <summary>
        /// Converte string para double ou para nulo
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static double? ToDoubleOrNull(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return null;
            else
            {
                double i = 0;
                double.TryParse(valor, out i);
                return i;
            }
        }


        public static DateTime ToDate(this string texto)
        {
            try
            {
                return Convert.ToDateTime(texto);
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

        public static DateTime? ToDate(this object value)
        {
            try
            {
                string texto = value.ToString();

                return Convert.ToDateTime(texto);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string SoNum(this string value)
        {
            try
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i].ToString() != "0"
                     && value[i].ToString() != "1"
                     && value[i].ToString() != "2"
                     && value[i].ToString() != "3"
                     && value[i].ToString() != "4"
                     && value[i].ToString() != "5"
                     && value[i].ToString() != "6"
                     && value[i].ToString() != "7"
                     && value[i].ToString() != "8"
                     && value[i].ToString() != "9")
                    {
                        value = value.Replace(value[i].ToString(), " ");
                    }
                }
                value = value.Replace(" ", "");
                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Cortar(this string texto, int cortar_ate)
        {
            if (cortar_ate == 0 || cortar_ate > texto.Length)
                return texto;
            else
                return texto.Substring(0, cortar_ate - 1);
        }

        public static string PadCentro(this string texto, int tamanho, char caracter = ' ')
        {
            try
            {
                if (texto.Length > tamanho)
                    texto = texto.Cortar(tamanho);

                int metade = texto.Length / 2;
                if (metade > 0)
                {
                    int inicio = (tamanho / 2) - metade;
                    texto = new string(caracter, inicio) + texto + new string(caracter, tamanho - (inicio + texto.Length));
                }
                return texto;

            }
            catch (Exception erro)
            {
                throw new Exception("Falha na função PadC: " + erro.Message);
            }
        }

       /// <summary>
        /// Retorna uma string com a primeira letra maiúscula
       /// </summary>
       /// <param name="texto"></param>
       /// <returns></returns>
        public static string ToFirstUpper(this string texto)
        {
            string strResult = "";

            if (texto.Length > 0)
            {

                strResult += texto.Substring(0, 1).ToUpper();

                strResult += texto.Substring(1, texto.Length - 1).ToLower();

            }

            return strResult;
        }

        public static string ToStringXML(this Enum value)
        {
            return (Convert.ToInt32(value)).ToString();
        }

        /// <summary>
        /// Utilizado para converter tipo de dado do xml da NFe para o tipo c#
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static double XmlToDouble(this object valor)
        {
            try
            {
                if (valor == null)
                    return 0;

                string vAux = Convert.ToString(valor);

                double dValor = Convert.ToDouble(vAux.Replace(".", ","));

                return dValor;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static DateTime ZeroHora(this DateTime data_atual)
        {
            try
            {
                DateTime data_nova = new DateTime(data_atual.Year, data_atual.Month, data_atual.Day);
                return data_nova;
            }
            catch (Exception)
            {
                return data_atual;
            }
        }
        
    }

   public static class Crypto
   {
       public static string MD5Hash(string text)
       {
           MD5 md5 = new MD5CryptoServiceProvider();

           //Calcular hash do Texto em bytes
           md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

           //Obter o resultado da Hash depois de Processado
           byte[] result = md5.Hash;

           StringBuilder strBuilder = new StringBuilder();
           for (int i = 0; i < result.Length; i++)
           {
               //Transforma em dois dígitos hexadecimais para cada byte 
               strBuilder.Append(result[i].ToString("x2"));
           }

           return strBuilder.ToString();
       }

       public static string EncryptString(string mensagem, string senha)
       {

           byte[] results; System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
           // Passo 1. Calculamos o hash da senha usando MD5
           // Usamos o gerador de hash MD5 como o resultado é um array de bytes de 128 bits
           // que é um comprimento válido para o codificador TripleDES usado abaixo
           MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
           byte[] tDESKey = hashProvider.ComputeHash(UTF8.GetBytes(senha));

           // Passo 2. Cria um objeto new TripleDESCryptoServiceProvider
           TripleDESCryptoServiceProvider tDESAlgorithm = new TripleDESCryptoServiceProvider();

           // Passo 3. Configuração do codificador
           tDESAlgorithm.Key = tDESKey;
           tDESAlgorithm.Mode = CipherMode.ECB;
           tDESAlgorithm.Padding = PaddingMode.PKCS7;

           // Passo 4. Converta a seqüência de entrada para um byte []
           byte[] dataToEncrypt = UTF8.GetBytes(mensagem);
           // Passo 5. Tentativa para criptografar a seqüência de caracteres
           try
           {
               ICryptoTransform encryptor = tDESAlgorithm.CreateEncryptor();
               results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
           }
           finally
           {
               // Limpe as tripleDES e serviços hashProvider de qualquer informação sensível
               tDESAlgorithm.Clear();
               hashProvider.Clear();
           }
           // Passo 6. Volte a seqüência criptografada como uma string base64 codificada
           return Convert.ToBase64String(results);
       }

       public static string DecryptString(string mensagem, string senha)
       {
           byte[] results;
           System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

           // Passo 1. Calculamos o hash da senha usando MD5
           // Usamos o gerador de hash MD5 como o resultado é um array de bytes de 128 bits
           // que é um comprimento válido para o codificador TripleDES usado abaixo
           MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
           byte[] tDESKey = hashProvider.ComputeHash(UTF8.GetBytes(senha));

           // Passo 2. Cria um objeto new TripleDESCryptoServiceProvider 
           TripleDESCryptoServiceProvider tDESAlgorithm = new TripleDESCryptoServiceProvider();
           // Passo 3. Configuração do codificador
           tDESAlgorithm.Key = tDESKey;
           tDESAlgorithm.Mode = CipherMode.ECB;
           tDESAlgorithm.Padding = PaddingMode.PKCS7;
           // Passo 4. Converta a seqüência de entrada para um byte []
           byte[] dataToDecrypt = Convert.FromBase64String(mensagem);
           // Passo 5. Tentativa para criptografar a seqüência de caracteres
           try
           {
               ICryptoTransform Decryptor = tDESAlgorithm.CreateDecryptor();
               results = Decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
           }
           finally
           {
               // Limpe as tripleDES e serviços hashProvider de qualquer informação sensível
               tDESAlgorithm.Clear();
               hashProvider.Clear();
           }

           // Passo 6. Volte a seqüência criptografada como uma string base64 codificada 
           return UTF8.GetString(results);
       }


   }
   public static class Validacoes
   {
       public static bool ValidaCPF(string cpf)
       {
           try
           {
               int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
               int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
               string tempCpf;
               string digito;
               int soma;
               int resto;

               //retira caracteres especiais
               cpf = cpf.Trim();
               cpf = cpf.Replace(".", "").Replace("-", "").Replace("_", "").Replace("/", "");

               //confere se tem o tamanho minimo
               if (cpf.Length != 11)
                   return false;

               if (cpf == "00000000000")
                   return false;

               //pega os 9 primeiros digitos
               tempCpf = cpf.Substring(0, 9);

               //faz a soma dos numeros
               soma = 0;
               for (int i = 0; i < 9; i++)
                   soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

               //é divisor de 11
               resto = soma % 11;

               if (resto < 2)
                   resto = 0;
               else
                   resto = 11 - resto;

               digito = resto.ToString();
               tempCpf = tempCpf + digito;
               soma = 0;

               for (int i = 0; i < 10; i++)
                   soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
               resto = soma % 11;

               if (resto < 2)
                   resto = 0;
               else
                   resto = 11 - resto;
               digito = digito + resto.ToString();
               return cpf.EndsWith(digito);
           }
           catch (Exception)
           {
               //throw new Exception("Falha ao válidar cpf: " + erro.Message);
               return false;

           }
       }

       public static bool ValidaCNPJ(string cnpj)
       {
           int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
           int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
           int soma;
           int resto;
           string digito;
           string tempCnpj;
           if (cnpj == null)
               cnpj = "";

           cnpj = cnpj.Trim();
           cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("_", "");

           if (cnpj == "00000000000000")
               return false;

           if (cnpj.Length != 14)
               return false;
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
           return cnpj.EndsWith(digito);
       }

       public static bool ValidaTelefone(string fone)
       {

           fone = fone.Replace("(", "");
           fone = fone.Replace(")", "");
           fone = fone.Replace("-", "");
           fone = fone.Replace(" ", "");
           fone = fone.Replace("_", "");

           return (fone.Length >= 10) || (fone.Length == 0);
       }

       public static bool ValidaCep(string cep)
       {
           if (string.IsNullOrEmpty(cep))
               return true;

           cep = cep.Replace("-", "");
           return (cep.Length == 8);
       }

       public static bool ValidaIE(string pUF, string pInscr)
       {
           if (string.IsNullOrEmpty(pUF) || string.IsNullOrEmpty(pInscr))
               return true;

           //verifica pois alguns estados a funcao nao esta correta
           //exemplo IE: 132070685 - MT

           bool retorno = false;
           string strBase;
           string strBase2;
           string strOrigem;
           string strDigito1;
           string strDigito2;
           int intPos;
           int intValor;
           int intSoma = 0;
           int intResto;
           int intNumero;
           int intPeso = 0;
           strBase = "";
           strBase2 = "";
           strOrigem = "";

           if ((pInscr.Trim().ToUpper() == "ISENTO"))
               return true;
           for (intPos = 1; intPos <= pInscr.Trim().Length; intPos++)
           {
               if ((("0123456789P".IndexOf(pInscr.Substring((intPos - 1), 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                   strOrigem = (strOrigem + pInscr.Substring((intPos - 1), 1));
           }
           switch (pUF.ToUpper())
           {
               case "AC":
                   #region
                   strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);
                   if (strBase.Substring(0, 2) == "01")
                   {
                       intSoma = 0;
                       intPeso = 4;
                       for (intPos = 1; (intPos <= 11); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           if (intPeso == 1) intPeso = 9;
                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       intSoma = 0;
                       strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                       intPeso = 5;
                       for (intPos = 1; (intPos <= 12); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           if (intPeso == 1) intPeso = 9;
                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intResto = (intSoma % 11);
                       strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 12) + strDigito2);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "AL":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   if ((strBase.Substring(0, 2) == "24"))
                   {
                       //24000004-8
                       //98765432
                       intSoma = 0;
                       intPeso = 9;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intSoma = (intSoma * 10);
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto == 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto == 10) ? "0" : Convert.ToString(intResto)).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "AM":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   intPeso = 9;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));

                       intSoma += intValor * intPeso;
                       intPeso--;
                   }
                   intResto = (intSoma % 11);
                   if (intSoma < 11)
                       strDigito1 = (11 - intSoma).ToString();
                   else
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));

                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "AP":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intPeso = 9;
                   if ((strBase.Substring(0, 2) == "03"))
                   {
                       strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intResto = (intSoma % 11);
                       intValor = (11 - intResto);

                       strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "BA":
                   #region
                   if (strOrigem.Length == 8)
                       strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                   else if (strOrigem.Length == 9)
                       strBase = (strOrigem.Trim() + "00000000").Substring(0, 9);
                   if ((("0123458".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                   {
                       #region
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 6); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           if (intPos == 1) intPeso = 7;
                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intResto = (intSoma % 10);
                       strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));
                       strBase2 = strBase.Substring(0, 7) + strDigito2;
                       if (strBase2 == strOrigem)
                           retorno = true;

                       if (retorno)
                       {
                           intSoma = 0;
                           intPeso = 0;
                           for (intPos = 1; (intPos <= 7); intPos++)
                           {
                               intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                               if (intPos == 7)
                                   intValor = int.Parse(strBase.Substring((intPos), 1));
                               if (intPos == 1) intPeso = 8;
                               intSoma += intValor * intPeso;
                               intPeso--;
                           }
                           intResto = (intSoma % 10);
                           strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));
                           strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);
                           if ((strBase2 == strOrigem))
                               retorno = true;
                       }
                       #endregion
                   }
                   else if ((("679".IndexOf(strBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 8)
                   {
                       #region
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 6); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           if (intPos == 1) intPeso = 7;
                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intResto = (intSoma % 11);
                       strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = strBase.Substring(0, 7) + strDigito2;
                       if (strBase2 == strOrigem)
                           retorno = true;
                       if (retorno)
                       {
                           intSoma = 0;
                           intPeso = 0;
                           for (intPos = 1; (intPos <= 7); intPos++)
                           {
                               intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                               if (intPos == 7)
                                   intValor = int.Parse(strBase.Substring((intPos), 1));
                               if (intPos == 1) intPeso = 8;
                               intSoma += intValor * intPeso;
                               intPeso--;
                           }
                           intResto = (intSoma % 11);
                           strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                           strBase2 = (strBase.Substring(0, 6) + strDigito1 + strDigito2);
                           if ((strBase2 == strOrigem))
                               retorno = true;
                       }
                       #endregion
                   }
                   else if ((("0123458".IndexOf(strBase.Substring(1, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0) && strBase.Length == 9)
                   {
                       #region
                       /* Segundo digito */
                       //1000003
                       //8765432
                       intSoma = 0;

                       for (intPos = 1; (intPos <= 7); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           if (intPos == 1) intPeso = 8;
                           intSoma += intValor * intPeso;
                           intPeso--;
                       }
                       intResto = (intSoma % 10);
                       strDigito2 = ((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((10 - intResto))).Length - 1));
                       strBase2 = strBase.Substring(0, 8) + strDigito2;
                       if (strBase2 == strOrigem)
                           retorno = true;
                       if (retorno)
                       {
                           //1000003 6
                           //9876543 2
                           intSoma = 0;
                           intPeso = 0;

                           for (intPos = 1; (intPos <= 8); intPos++)
                           {
                               intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                               if (intPos == 8)
                                   intValor = int.Parse(strBase.Substring((intPos), 1));
                               if (intPos == 1) intPeso = 9;
                               intSoma += intValor * intPeso;
                               intPeso--;
                           }
                           intResto = (intSoma % 10);
                           strDigito1 = ((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto == 0) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                           strBase2 = (strBase.Substring(0, 7) + strDigito1 + strDigito2);
                           if ((strBase2 == strOrigem))
                               retorno = true;
                       }
                       #endregion
                   }
                   #endregion
                   break;
               case "CE":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * (10 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   intValor = (11 - intResto);
                   if ((intValor > 9))
                       intValor = 0;
                   strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "DF":
                   #region
                   strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                   if ((strBase.Substring(0, 3) == "073"))
                   {
                       intSoma = 0;
                       intPeso = 2;
                       for (intPos = 11; (intPos >= 1); intPos = (intPos + -1))
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso > 9))
                               intPeso = 2;
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 11) + strDigito1);
                       intSoma = 0;
                       intPeso = 2;
                       for (intPos = 12; (intPos >= 1); intPos = (intPos + -1))
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso > 9))
                               intPeso = 2;
                       }
                       intResto = (intSoma % 11);
                       strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 12) + strDigito2);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "ES":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * (10 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "GO":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   if ((("10,11,15".IndexOf(strBase.Substring(0, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * (10 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intResto = (intSoma % 11);
                       if ((intResto == 0))
                           strDigito1 = "0";
                       else if ((intResto == 1))
                       {
                           intNumero = int.Parse(strBase.Substring(0, 8));
                           strDigito1 = (((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Substring(((((intNumero >= 10103105) && (intNumero <= 10119997)) ? "1" : "0").Length - 1));
                       }
                       else
                           strDigito1 = Convert.ToString((11 - intResto)).Substring((Convert.ToString((11 - intResto)).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "MA":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   if ((strBase.Substring(0, 2) == "12"))
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * (10 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "MT":
                   #region
                   strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                   intSoma = 0;
                   intPeso = 2;
                   for (intPos = 10; intPos >= 1; intPos = (intPos + -1))
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * intPeso);
                       intSoma = (intSoma + intValor);
                       intPeso = (intPeso + 1);
                       if ((intPeso > 9))
                           intPeso = 2;
                   }
                   intResto = (intSoma % 11);
                   strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase.Substring(0, 10) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "MS":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   if ((strBase.Substring(0, 2) == "28"))
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * (10 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "MG":
                   #region
                   strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                   strBase2 = (strBase.Substring(0, 3) + ("0" + strBase.Substring(3, 8)));
                   intNumero = 2;
                   string strSoma = "";
                   for (intPos = 1; (intPos <= 12); intPos++)
                   {
                       intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                       intNumero = ((intNumero == 2) ? 1 : 2);
                       intValor = (intValor * intNumero);

                       intSoma = (intSoma + intValor);
                       strSoma += intValor.ToString();
                   }
                   intSoma = 0;
                   //Soma -se os algarismos, não o produto
                   for (int i = 0; i < strSoma.Length; i++)
                   {
                       intSoma += int.Parse(strSoma.Substring(i, 1));
                   }
                   intValor = int.Parse(strBase.Substring(8, 2));
                   strDigito1 = (intValor - intSoma).ToString();
                   strBase2 = (strBase.Substring(0, 11) + strDigito1);
                   if ((strBase2 == strOrigem.Substring(0, 12)))
                       retorno = true;
                   if (retorno)
                   {
                       intSoma = 0;
                       intPeso = 3;
                       for (intPos = 1; (intPos <= 12); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           if (intPeso < 2)
                               intPeso = 11;

                           intSoma += (intValor * intPeso);
                           intPeso--;
                       }
                       intResto = (intSoma % 11);
                       intValor = 11 - intResto;
                       strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 12) + strDigito2);
                       if (strBase2 == strOrigem)
                           retorno = true;
                   }

                   #endregion
                   break;
               case "PA":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   if ((strBase.Substring(0, 2) == "15"))
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * (10 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "PB":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * (10 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   intValor = (11 - intResto);
                   if ((intValor > 9))
                       intValor = 0;
                   strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "PE":
                   #region
                   strBase = (strOrigem.Trim() + "00000000000000").Substring(0, 14);
                   intSoma = 0;
                   intPeso = 2;
                   for (intPos = 7; (intPos >= 1); intPos = (intPos + -1))
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * intPeso);
                       intSoma = (intSoma + intValor);
                       intPeso = (intPeso + 1);
                       if ((intPeso > 9))
                           intPeso = 2;
                   }
                   intResto = (intSoma % 11);
                   intValor = (11 - intResto);
                   if ((intValor > 9))
                       intValor = (intValor - 10);
                   strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                   strBase2 = (strBase.Substring(0, 7) + strDigito1);
                   if ((strBase2 == strOrigem.Substring(0, 8)))
                       retorno = true;
                   if (retorno)
                   {
                       intSoma = 0;
                       intPeso = 2;
                       for (intPos = 8; (intPos >= 1); intPos = (intPos + -1))
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso > 9))
                               intPeso = 2;
                       }
                       intResto = (intSoma % 11);
                       intValor = (11 - intResto);
                       if ((intValor > 9))
                           intValor = (intValor - 10);
                       strDigito2 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito2);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "PI":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * (10 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "PR":
                   #region
                   strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                   intSoma = 0;
                   intPeso = 2;
                   for (intPos = 8; (intPos >= 1); intPos = (intPos + -1))
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * intPeso);
                       intSoma = (intSoma + intValor);
                       intPeso = (intPeso + 1);
                       if ((intPeso > 7))
                           intPeso = 2;
                   }
                   intResto = (intSoma % 11);
                   strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   intSoma = 0;
                   intPeso = 2;
                   for (intPos = 9; (intPos >= 1); intPos = (intPos + -1))
                   {
                       intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                       intValor = (intValor * intPeso);
                       intSoma = (intSoma + intValor);
                       intPeso = (intPeso + 1);
                       if ((intPeso > 7))
                           intPeso = 2;
                   }
                   intResto = (intSoma % 11);
                   strDigito2 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase2 + strDigito2);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "RJ":
                   #region
                   strBase = (strOrigem.Trim() + "00000000").Substring(0, 8);
                   intSoma = 0;
                   intPeso = 2;
                   for (intPos = 7; (intPos >= 1); intPos = (intPos + -1))
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * intPeso);
                       intSoma = (intSoma + intValor);
                       intPeso = (intPeso + 1);
                       if ((intPeso > 7))
                           intPeso = 2;
                   }
                   intResto = (intSoma % 11);
                   strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase.Substring(0, 7) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "RN": //Verficar com 10 digitos
                   #region
                   if (strOrigem.Length == 9)
                       strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   else if (strOrigem.Length == 10)
                       strBase = (strOrigem.Trim() + "000000000").Substring(0, 10);
                   if ((strBase.Substring(0, 2) == "20") && strBase.Length == 9)
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * (10 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intSoma = (intSoma * 10);
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto > 9) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 9) ? "0" : Convert.ToString(intResto)).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   else if (strBase.Length == 10)
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 9); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * (11 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intSoma = (intSoma * 10);
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto > 10) ? "0" : Convert.ToString(intResto)).Substring((((intResto > 10) ? "0" : Convert.ToString(intResto)).Length - 1));
                       strBase2 = (strBase.Substring(0, 9) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "RO":
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   strBase2 = strBase.Substring(3, 5);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 5); intPos++)
                   {
                       intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                       intValor = (intValor * (7 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   intValor = (11 - intResto);
                   if ((intValor > 9))
                       intValor = (intValor - 10);
                   strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   break;
               case "RR":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   if ((strBase.Substring(0, 2) == "24"))
                   {
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = intValor * intPos;
                           intSoma += intValor;
                       }
                       intResto = (intSoma % 9);
                       strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "RS":
                   #region
                   strBase = (strOrigem.Trim() + "0000000000").Substring(0, 10);
                   intNumero = int.Parse(strBase.Substring(0, 3));
                   if (((intNumero > 0) && (intNumero < 468)))
                   {
                       intSoma = 0;
                       intPeso = 2;
                       for (intPos = 9; (intPos >= 1); intPos = (intPos + -1))
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso > 9))
                               intPeso = 2;
                       }
                       intResto = (intSoma % 11);
                       intValor = (11 - intResto);
                       if ((intValor > 9))
                           intValor = 0;
                       strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                       strBase2 = (strBase.Substring(0, 9) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
               case "SC":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * (10 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "SE":
                   #region
                   strBase = (strOrigem.Trim() + "000000000").Substring(0, 9);
                   intSoma = 0;
                   for (intPos = 1; (intPos <= 8); intPos++)
                   {
                       intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                       intValor = (intValor * (10 - intPos));
                       intSoma = (intSoma + intValor);
                   }
                   intResto = (intSoma % 11);
                   intValor = (11 - intResto);
                   if ((intValor > 9))
                       intValor = 0;
                   strDigito1 = Convert.ToString(intValor).Substring((Convert.ToString(intValor).Length - 1));
                   strBase2 = (strBase.Substring(0, 8) + strDigito1);
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "SP":
                   #region
                   if ((strOrigem.Substring(0, 1) == "P"))
                   {
                       strBase = (strOrigem.Trim() + "0000000000000").Substring(0, 13);
                       strBase2 = strBase.Substring(1, 8);
                       intSoma = 0;
                       intPeso = 1;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso == 2))
                               intPeso = 3;
                           if ((intPeso == 9))
                               intPeso = 10;
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                       strBase2 = (strBase.Substring(0, 9) + (strDigito1 + strBase.Substring(10, 3)));
                   }
                   else
                   {
                       strBase = (strOrigem.Trim() + "000000000000").Substring(0, 12);
                       intSoma = 0;
                       intPeso = 1;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso == 2))
                               intPeso = 3;
                           if ((intPeso == 9))
                               intPeso = 10;
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                       strBase2 = (strBase.Substring(0, 8) + (strDigito1 + strBase.Substring(9, 2)));
                       intSoma = 0;
                       intPeso = 2;
                       for (intPos = 11; (intPos >= 1); intPos = (intPos + -1))
                       {
                           intValor = int.Parse(strBase.Substring((intPos - 1), 1));
                           intValor = (intValor * intPeso);
                           intSoma = (intSoma + intValor);
                           intPeso = (intPeso + 1);
                           if ((intPeso > 10))
                               intPeso = 2;
                       }
                       intResto = (intSoma % 11);
                       strDigito2 = Convert.ToString(intResto).Substring((Convert.ToString(intResto).Length - 1));
                       strBase2 = (strBase2 + strDigito2);
                   }
                   if ((strBase2 == strOrigem))
                       retorno = true;
                   #endregion
                   break;
               case "TO":
                   #region
                   strBase = (strOrigem.Trim() + "00000000000").Substring(0, 11);
                   if ((("01,02,03,99".IndexOf(strBase.Substring(2, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                   {
                       strBase2 = (strBase.Substring(0, 2) + strBase.Substring(4, 6));
                       intSoma = 0;
                       for (intPos = 1; (intPos <= 8); intPos++)
                       {
                           intValor = int.Parse(strBase2.Substring((intPos - 1), 1));
                           intValor = (intValor * (10 - intPos));
                           intSoma = (intSoma + intValor);
                       }
                       intResto = (intSoma % 11);
                       strDigito1 = ((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Substring((((intResto < 2) ? "0" : Convert.ToString((11 - intResto))).Length - 1));
                       strBase2 = (strBase.Substring(0, 10) + strDigito1);
                       if ((strBase2 == strOrigem))
                           retorno = true;
                   }
                   #endregion
                   break;
           }
           return retorno;
       }

       public static bool ValidaEmail(string email)
       {
           if (string.IsNullOrEmpty(email))
           {
               return false;
           }
           bool Valido = false;
           Regex regEx = new Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", RegexOptions.IgnoreCase);
           Valido = regEx.IsMatch(email);
           return Valido;
       }

       public static bool ValidaCidade(int cidade)
       {
           Contexto bd = new Contexto();
           cidade cid = bd.Cidades.AsNoTracking().FirstOrDefault(a => a.id_cidade == cidade);
           return (cid != null);
       }
   }
}
