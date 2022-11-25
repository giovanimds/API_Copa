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
            List<Jogo> jogos = _context.Jogos
                .Include(t => t.SelecaoA)
                .Include(t => t.SelecaoB)
                .ToList();
            return jogos.Count != 0 ? Ok(jogos) : NotFound();
        }
        
        [HttpGet]
        [Route("buscar/{id:int}")]
        public IActionResult Buscar([FromRoute] int id)
        {
            Jogo jogo = _context.Jogos.Single(x => x.Id.Equals(id));

            _context.Entry(jogo).Reference(x => x.SelecaoA).Load();
            _context.Entry(jogo).Reference(x => x.SelecaoB).Load();
            
            return Ok(jogo);
        }

        [HttpPost]
        [Route("palpite")]
        public IActionResult CadastrarPalpite([FromBody] Palpite palpite)
        {
            Selecao Selecao1 = _context.Selecoes.Find(palpite.Jogo.SelecaoA.Id);
            Selecao Selecao2 = _context.Selecoes.Find(palpite.Jogo.SelecaoB.Id);

            
            palpite.Jogo = _context.Jogos
                .First(x => x.Id.Equals(palpite.Jogo.Id));

            palpite.Jogo.SelecaoA = Selecao1;
            palpite.Jogo.SelecaoB = Selecao2;
            
            _context.Palpites.Add(palpite);
            _context.SaveChanges();
            
            return Ok(palpite);
        }
    }
}