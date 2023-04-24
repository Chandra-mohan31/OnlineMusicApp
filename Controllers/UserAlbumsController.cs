using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineMusicApp.Areas.Identity.Data;
using OnlineMusicApp.Data;
using OnlineMusicApp.Models;

namespace OnlineMusicApp.Controllers
{
    public class UserAlbumsController : Controller
    {
        private readonly OnlineMusicAppContext _context;
        private readonly UserManager<OnlineMusicAppUser> userManager;

        public UserAlbumsController(OnlineMusicAppContext context,UserManager<OnlineMusicAppUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: UserAlbums
        public async Task<IActionResult> Index()
        {
              return _context.userAlbum != null ? 
                          View(await _context.userAlbum.ToListAsync()) :
                          Problem("Entity set 'OnlineMusicAppContext.userAlbum'  is null.");
        }

        // GET: UserAlbums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.userAlbum == null)
            {
                return NotFound();
            }

            var userAlbum = await _context.userAlbum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAlbum == null)
            {
                return NotFound();
            }

            return View(userAlbum);
        }

        // GET: UserAlbums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAlbums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackId,Name,Artist,Album,ImageUrl,PreviewUrl")] UserAlbum userAlbum)
        {
            var userId = userManager.GetUserId(this.User);
            Console.WriteLine("userid: " + userId);
            var user = await userManager.FindByIdAsync(userId);
            userAlbum.MusicAppUser = user;
            _context.Add(userAlbum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(userAlbum);
        }

        // GET: UserAlbums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.userAlbum == null)
            {
                return NotFound();
            }

            var userAlbum = await _context.userAlbum.FindAsync(id);
            if (userAlbum == null)
            {
                return NotFound();
            }
            return View(userAlbum);
        }

        // POST: UserAlbums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackId,Name,Artist,Album,ImageUrl,PreviewUrl")] UserAlbum userAlbum)
        {
            if (id != userAlbum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAlbum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAlbumExists(userAlbum.Id))
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
            return View(userAlbum);
        }

        // GET: UserAlbums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.userAlbum == null)
            {
                return NotFound();
            }

            var userAlbum = await _context.userAlbum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAlbum == null)
            {
                return NotFound();
            }

            return View(userAlbum);
        }

        // POST: UserAlbums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.userAlbum == null)
            {
                return Problem("Entity set 'OnlineMusicAppContext.userAlbum'  is null.");
            }
            var userAlbum = await _context.userAlbum.FindAsync(id);
            if (userAlbum != null)
            {
                _context.userAlbum.Remove(userAlbum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAlbumExists(int id)
        {
          return (_context.userAlbum?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
