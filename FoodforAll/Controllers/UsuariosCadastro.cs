using FoodforAll.Models.ViewModels;
using FoodforAll.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UsuariosController : Controller
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Usuarios/Create
    public IActionResult Cadastro()
    {
        return View();
    }

    // POST: Usuarios/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastro(UsuarioCadastroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email,
            Password = model.Password,
            Telefone = model.Telefone,
            Endereco = model.Endereco,
            UserType = model.UserType
        };

        if (model.UserType == UserType.EstabelecimentoDoador)
        {
            var doador = new EstabelecimentoDoador
            {
                CNPJ = model.CNPJ!,
                NomeFantasia = model.NomeFantasia!,
                TransporteProprio = model.TransporteProprio ?? false
            };

            _context.EstabelecimentosDoadores.Add(doador);
            await _context.SaveChangesAsync();

            usuario.EstabelecimentoDoadorId = doador.Id;
        }
        else if (model.UserType == UserType.EstabelecimentoReceptor)
        {
            var receptor = new EstabelecimentoReceptor
            {
                NomeReceptor = model.NomeReceptor!,
                RegistroGoverno = model.RegistroGoverno!,
                Aprovada = model.Aprovada ?? false,
                RefeicoesDiarias = model.RefeicoesDiarias,
                ResponsavelTecnico = model.ResponsavelTecnico!,
                HorarioRecebimentoDoacoes = model.HorarioRecebimentoDoacoes
            };

            _context.EstabelecimentosReceptores.Add(receptor);
            await _context.SaveChangesAsync();

            usuario.EstabelecimentoReceptorId = receptor.Id;
        }


        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}
