using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class FiltroBuscaController : ControllerBase

    {
        fastradeContext _contexto = new fastradeContext ();
        //GET:Api/Filtro de busca
        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetPorProdutoNome (Produto produto) {

            try {
                List<Produto> listaprodutos = await _contexto.Produto.Where (x => x.Nome == produto.Nome).ToListAsync ();

                return listaprodutos;
            } catch (System.Exception ex) {
                return BadRequest (ex);
            }
        }

    }
}