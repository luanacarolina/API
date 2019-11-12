using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class CatProdutoController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();


        //Get: Api/Catproduto
        /// <summary>
        /// Aqui são todas as Categorias de Produtos
        /// </summary>
        /// <returns>Lista de Categoria de produtos</returns>
        [HttpGet]
        public async Task<ActionResult<List<CatProduto>>> Get () {

            var catprodutos = await _contexto.CatProduto.ToListAsync ();

            if (catprodutos == null) {
                return NotFound();
            }
            return catprodutos;
        }
        //Get: Api/Catproduto
        /// <summary>
        /// Aqui pegamos apenas uma categoria de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unico Id de categoria de produtos</returns>
        [HttpGet ("{id}")]
        public async Task<ActionResult<CatProduto>> Get(int id){
            var catproduto = await _contexto.CatProduto.FindAsync (id);

            if (catproduto == null){
                return NotFound ();
            }
            return catproduto;
        }
        //Post: Api/CatProduto
        /// <summary>
        /// Aqui enviamos mais categorias de produtos
        /// </summary>
        /// <param name="catProduto">Envia uma categoria de produtos</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CatProduto>> Post (CatProduto catProduto){
            try{
                await _contexto.AddAsync (catProduto);

                await _contexto.SaveChangesAsync();
                

                }catch (DbUpdateConcurrencyException){
                    throw;
            }
            return catProduto;
        }
        //Put: Api/CatProduto
        /// <summary>
        /// Aqui alteramos dados das categorias de produto
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catProduto"></param>
        /// <returns>Alteração de categoria de produto</returns>
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, CatProduto catProduto){
            if(id != catProduto.IdCatProduto){
                
                return BadRequest ();
            }
            _contexto.Entry (catProduto).State = EntityState.Modified;
            try{
                await _contexto.SaveChangesAsync ();
            }catch (DbUpdateConcurrencyException){
                var CatProduto_valido = await _contexto.CatProduto.FindAsync (id);

                if(CatProduto_valido == null) {
                    return NotFound ();
                }else{
                    throw;
                }
            }
            return NoContent();
        }
         // DELETE api/CatProduto/id
         /// <summary>
         /// Aqui deletamos uma categoria de produtos
         /// </summary>
         /// <param name="id"></param>
         /// <returns>Deleta uma categoria</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CatProduto>> Delete(int id){

            var catProduto = await _contexto.CatProduto.FindAsync(id);
            if(catProduto == null){
                return NotFound();
            }

            _contexto.CatProduto.Remove(catProduto);
            await _contexto.SaveChangesAsync();

            return catProduto;
        }  
    }
}