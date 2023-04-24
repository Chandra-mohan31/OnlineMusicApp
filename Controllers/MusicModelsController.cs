using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OnlineMusicApp.Data;
using OnlineMusicApp.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace OnlineMusicApp.Controllers
{
    public class MusicModelsController : Controller
    {
        private readonly OnlineMusicAppContext _context;

        public MusicModelsController(OnlineMusicAppContext context)
        {
            _context = context;
        }




        // GET: MusicModels
        public async Task<IActionResult> Index()
        {
              return _context.MusicModel != null ? 
                          View(await _context.MusicModel.ToListAsync()) :
                          Problem("Entity set 'OnlineMusicAppContext.MusicModel'  is null.");
        }

        // GET: MusicModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.MusicModel == null)
            {
                return NotFound();
            }

            var musicModel = await _context.MusicModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicModel == null)
            {
                return NotFound();
            }

            return View(musicModel);
        }

        // GET: MusicModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MusicModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Artist,Album,ImageUrl,PreviewUrl")] MusicModel musicModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musicModel);
        }

        // GET: MusicModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.MusicModel == null)
            {
                return NotFound();
            }

            var musicModel = await _context.MusicModel.FindAsync(id);
            if (musicModel == null)
            {
                return NotFound();
            }
            return View(musicModel);
        }

        // POST: MusicModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Artist,Album,ImageUrl,PreviewUrl")] MusicModel musicModel)
        {
            if (id != musicModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicModelExists(musicModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(musicModel);
        }

        // GET: MusicModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.MusicModel == null)
            {
                return NotFound();
            }

            var musicModel = await _context.MusicModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicModel == null)
            {
                return NotFound();
            }

            return View(musicModel);
        }

        // POST: MusicModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.MusicModel == null)
            {
                return Problem("Entity set 'OnlineMusicAppContext.MusicModel'  is null.");
            }
            var musicModel = await _context.MusicModel.FindAsync(id);
            if (musicModel != null)
            {
                _context.MusicModel.Remove(musicModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicModelExists(string id)
        {
          return (_context.MusicModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
