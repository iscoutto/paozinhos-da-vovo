using Microsoft.AspNetCore.Http;
using System;

namespace Proj_Interdisciplinar.Models
{
    public class ProdutoViewModel
    {
        public IFormFile Imagem { get; set; }
        public byte[] ImagemEmByte { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Info_nutricional { get; set; }
        public string Alergia { get; set; }
        public string Preco { get; set; }
        public string Unidade { get; set; }
        public string Categoria { get; set; }
        public string ImagemEmBase64
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                else return string.Empty;
            }
        }
    }
}
