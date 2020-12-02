using System;
using System.Security.Cryptography;
using System.Text;
using FluentValidation.Attributes;
using RestaurantServices.Restaurant.Modelo.Validaciones;

namespace RestaurantServices.Restaurant.Modelo.Clases
{
    [Validator(typeof(UsuarioValidator))]
    public class Usuario
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoUsuario { get; set; }
        public Persona Persona { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        /// <summary>
        /// Aes Encrypt
        /// </summary>
        /// <returns></returns>
        public string EncryptPassword(string contrasena)
        {
            var objDesCrypto = new TripleDESCryptoServiceProvider();
            var objHashMd5 = new MD5CryptoServiceProvider();
            var byteHash = objHashMd5.ComputeHash(Encoding.ASCII.GetBytes("KEY"));
            objDesCrypto.Key = byteHash;
            objDesCrypto.Mode = CipherMode.ECB;
            var byteBuff = Encoding.ASCII.GetBytes(contrasena);
            Contrasena = Convert.ToBase64String(objDesCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return Contrasena;
        }

        public string DecryptPassword(string contrasena)
        {
            var objDesCrypto = new TripleDESCryptoServiceProvider();
            var objHashMd5 = new MD5CryptoServiceProvider();
            var byteHash = objHashMd5.ComputeHash(Encoding.ASCII.GetBytes("KEY"));
            objDesCrypto.Key = byteHash;
            objDesCrypto.Mode = CipherMode.ECB; //CBC, CFB
            var byteBuff = Convert.FromBase64String(contrasena);
            var strDecrypted = Encoding.ASCII.GetString(objDesCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return strDecrypted;
        }
    }
}
