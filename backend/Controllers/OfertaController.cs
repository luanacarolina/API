using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase {
        fastradeContext _contexto = new fastradeContext ();

        //Get: Api/Oferta
        [HttpGet]
        public async Task<ActionResult<List<Oferta>>> Get () {
            //Include("")
            var oferta = await _contexto.Oferta.Include ("IdProdutoNavigation").Include ("IdUsuarioNavigation").ToListAsync ();
            if (oferta == null) {
                return NotFound ();
            }
            return oferta;
        }
        //Get: Api/Oferta/2
        [HttpGet ("{id}")]
        public async Task<ActionResult<Oferta>> Get (int id) {
            var oferta = await _contexto.Oferta.Include ("IdProdutoNavigation").Include ("IdUsuarioNavigation").FirstOrDefaultAsync (e => e.IdOferta == id);

            if (oferta == null) {
                return NotFound ();
            }
            return oferta;
        }
        //Post: Api/Oferta
        [HttpPost]
        public async Task<ActionResult<Oferta>> Post (Oferta oferta) {
            try {
                await _contexto.AddAsync (oferta);

                await _contexto.SaveChangesAsync ();

            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return oferta;
        }
        //Put: Api/Oferta
        [HttpPut ("{id}")]
        public async Task<ActionResult> Put (int id, Oferta oferta) {
            if (id != oferta.IdOferta) {

                return BadRequest ();
            }
            _contexto.Entry (oferta).State = EntityState.Modified;
            try {
                await _contexto.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var oferta_valido = await _contexto.Receita.FindAsync (id);

                if (oferta_valido == null) {
                    return NotFound ();
                } else {
                    throw;
                }
            }
            return NoContent ();
        }
        // DELETE api/Oferta/id
        [HttpDelete ("{id}")]
        public async Task<ActionResult<Oferta>> Delete (int id) {

            var oferta = await _contexto.Oferta.FindAsync (id);
            if (oferta == null) {
                return NotFound ();
            }

            _contexto.Oferta.Remove (oferta);
            await _contexto.SaveChangesAsync ();

            return oferta;
        }

        [HttpGet ("MaiorPreco")]

        public ActionResult<List<Oferta>> GetOrdenar () {

            List<Oferta> ofertas;
            using (fastradeContext _contexto = new fastradeContext ()) {
                ofertas = _contexto.Oferta.OrderByDescending (c => c.Preco).ToList ();
            }
            return ofertas;
        }

        [HttpGet ("MenorPreco")]

        public ActionResult<List<Oferta>> GetAsc () {

            List<Oferta> ofertas;
            using (fastradeContext _contexto = new fastradeContext ()) {
                ofertas = _contexto.Oferta.OrderBy (c => c.Preco).ToList ();

                return ofertas;
            }

           
        }
         [HttpGet("Validade")]

         public async Task<ActionResult<List<Oferta>>> GetValidar([FromForm]OfertaViewModel oferta){
             var Validade = oferta.Validade;

             DateTime DataAtual = DateTime.Now.AddDays(Convert.ToInt32(Validade));

             if (Validade !=null){
                 return await RetornaValidade(oferta,DataAtual);
             }


         }
         
             private async Task<ActionResult<List<Oferta>>> RetornaValidade(OfertaViewModel oferta ,DateTime data){
                 List<Oferta> produtos = new List<Oferta>();
                 var ofertas = await _contexto.();

                 foreach(var item in ofertas){

                     if(item.Quantidade>0 && item.Validade.Date == data.Date){
                         produtos.Add(item);
                     }
                 }
                 return ofertas();
             }
    }

}



 