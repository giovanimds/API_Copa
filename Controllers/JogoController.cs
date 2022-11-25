using System.Collections.Generic;
using System.Linq;
using api.Models;
using API_Copa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    
    [Route("api/jogo")]
    public class JogoController : ControllerBase
    {
        private readonly Context _context;
        public JogoController(Context context) =>
            _context = context;

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Jogo jogo)
        {
            Jogo jogo2 = new Jogo();
            jogo2.SelecaoA = _context.Selecoes.Find(jogo.SelecaoA.Id);
            jogo2.SelecaoB = _context.Selecoes.Find(jogo.SelecaoB.Id);
            _context.Jogos.Add(jogo2);
            _context.SaveChanges();
            return Created("", jogo2);
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            List<Jogo> jogos = _context.Jogos.Include(x => x.SelecaoA).Include(x => x.SelecaoB).ToList();
            return jogos.Count != 0 ? Ok(jogos) : NotFound();
        }
        
        [HttpGet]
        [Route("buscar/{id:int}")]
        public IActionResult Buscar([FromRoute] int id)
        {
            return Ok(_context.Jogos.Include(x => x.SelecaoA).Include(x => x.SelecaoB).FirstOrDefault(x => x.Id.Equals(id)));
        }

    }
}