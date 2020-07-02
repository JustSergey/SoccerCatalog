using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerCatalog.Data;
using SoccerCatalog.Models;

namespace SoccerCatalog.Controllers
{
    public class PlayersController : Controller
    {
        private readonly SoccerCatalogContext _context;

        public PlayersController(SoccerCatalogContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var players = _context.Players
                .Include(p => p.Country)
                .Include(p => p.Gender)
                .Include(p => p.Team);
            return View(await players.ToListAsync());
        }

        public IActionResult Add()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Title");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Title");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,FirstName,LastName,GenderId,Birthday,TeamId,CountryId")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Title", player.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Title", player.GenderId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Title", player.TeamId);
            return View(player);
        }

        public IActionResult AddTeam()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeam([Bind("Id,Title")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Add));
            }
            return NoContent();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var player = await _context.Players
                .Include(p => p.Country)
                .Include(p => p.Gender)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
                return NotFound();

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Title", player.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Title", player.GenderId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Title", player.TeamId);
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,GenderId,Birthday,TeamId,CountryId")] Player player)
        {
            if (id != player.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Title", player.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Title", player.GenderId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Title", player.TeamId);
            return View(player);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var player = await _context.Players
                .Include(p => p.Country)
                .Include(p => p.Gender)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
                return NotFound();

            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
                return NotFound();

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
