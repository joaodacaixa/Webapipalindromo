using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using Webapipalindromo;

namespace PalindromeChecker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PalindromoController : ControllerBase
    {
        
    [HttpPost]
        public IActionResult Palindromocontadorletras([FromBody] inputpalindromo request)
        {

            // Remove espaços em branco e converte para minúsculas para simplificar a comparação
            request.texto = request.texto.Replace(" ", "").ToLower();
            // Testa se a string é vazia
            if (string.IsNullOrEmpty(request.texto))
            {
                return BadRequest("A String está vazia!");
            }
            // Verifica se a string de entrada é um palíndromo
            bool ePalindromo = EPalindromo(request.texto);

            // Conta a ocorrência de cada letra na string de entrada
            Dictionary<char, int> ocorrenciadeLetras = ContaocorrenciadeLetras(request.texto);

            // Prepara a resposta
            var response = new
            {
                EPalindromo = ePalindromo,
                OcorrenciadeLetras = ocorrenciadeLetras
            };

            return Ok(response);
        }

        private bool EPalindromo(string input)
        {   
            // Compara a string original com a string reversa
            return input == new string(input.Reverse().ToArray());
        }

        private Dictionary<char, int> ContaocorrenciadeLetras(string input)
        {
            // Remove espaços em branco e converte para minúsculas para simplificar a contagem
            input = input.Replace(" ", "").ToLower();

            var ocorrenciadeletras = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    if (ocorrenciadeletras.ContainsKey(c))
                    {
                        ocorrenciadeletras[c]++;
                    }
                    else
                    {
                        ocorrenciadeletras.Add(c, 1);
                    }
                }
            }

            return ocorrenciadeletras;
        }
    }
    public class inputpalindromo
    {
        public string texto { get; set; }
    }
}
