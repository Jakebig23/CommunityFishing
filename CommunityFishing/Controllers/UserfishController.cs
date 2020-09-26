using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityFishing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CommunityFishing.Controllers
{
	public class UserfishController : Controller
	{
		private readonly CommunityFishingContext _context;
		private UserManager<IdentityUser> _userManager;
		private IHostingEnvironment _webroot;

		public UserfishController(CommunityFishingContext context, UserManager<IdentityUser> userManager, IHostingEnvironment webroot)
		{
			_context = context;
			_userManager = userManager;
			_webroot = webroot;
		}

		// GET: Userfish
		public async Task<IActionResult> Index()
		{
			var communityFishingContext = _context.Userfish.Include(u => u.FishNameNavigation).Include(u => u.UserAccount);
			return View(await communityFishingContext.ToListAsync());
		}

		// GET: Userfish/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userfish = await _context.Userfish
				.Include(u => u.FishNameNavigation)
				.Include(u => u.UserAccount)
				.FirstOrDefaultAsync(m => m.UserFishId == id);
			if (userfish == null)
			{
				return NotFound();
			}

			return View(userfish);
		}

		// GET: Userfish/Create
		public IActionResult Create()
		{
			ViewData["FishName"] = new SelectList(_context.FishInfo, "FishName", "FishName");
			// ViewData["UserAccountId"] = new SelectList(_context.UserProfile, "UserAccountId", "FirstName");
			return View();
		}

		// POST: Userfish/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("UserFishId,UserAccountId,FishName,FishLength,UserFishPhotoPath,CatchDate")] Userfish userfish,
			IFormFile FilePhoto)
		{
			if (FilePhoto.Length > 0)
			{
				string photoPath = _webroot.WebRootPath + "\\userPhoto\\";
				var filename = Path.GetFileName(FilePhoto.FileName);

				using (var stream = System.IO.File.Create(photoPath + filename))
				{
					await FilePhoto.CopyToAsync(stream);
					userfish.UserFishPhotoPath = filename;
				}
			}
			if (ModelState.IsValid)
			{
				_context.Add(userfish);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["FishName"] = new SelectList(_context.FishInfo, "FishName", "FishName", userfish.FishName);
			// ViewData["UserAccountId"] = new SelectList(_context.UserProfile, "UserAccountId", "FirstName", userfish.UserAccountId);
			return View(userfish);
		}

		// GET: Userfish/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userfish = await _context.Userfish.FindAsync(id);
			if (userfish == null)
			{
				return NotFound();
			}
			ViewData["FishName"] = new SelectList(_context.FishInfo, "FishName", "FishName", userfish.FishName);
			//ViewData["UserAccountId"] = new SelectList(_context.UserProfile, "UserAccountId", "FirstName", userfish.UserAccountId);
			return View(userfish);
		}

		// POST: Userfish/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("UserFishId,UserAccountId,FishName,FishLength,UserFishPhotoPath,CatchDate")] Userfish userfish,
			IFormFile FilePhoto)
		{
			if (FilePhoto.Length > 0)
			{
				string photoPath = _webroot.WebRootPath + "\\userPhoto\\";
				var filename = Path.GetFileName(FilePhoto.FileName);

				using (var stream = System.IO.File.Create(photoPath + filename))
				{
					await FilePhoto.CopyToAsync(stream);
					userfish.UserFishPhotoPath = filename;
				}
			}
			if (id != userfish.UserFishId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(userfish);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserfishExists(userfish.UserFishId))
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
			ViewData["FishName"] = new SelectList(_context.FishInfo, "FishName", "FishName", userfish.FishName);
			//ViewData["UserAccountId"] = new SelectList(_context.UserProfile, "UserAccountId", "FirstName", userfish.UserAccountId);
			return View(userfish);
		}

		// GET: Userfish/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userfish = await _context.Userfish
				.Include(u => u.FishNameNavigation)
				.Include(u => u.UserAccount)
				.FirstOrDefaultAsync(m => m.UserFishId == id);
			if (userfish == null)
			{
				return NotFound();
			}

			return View(userfish);
		}

		// POST: Userfish/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userfish = await _context.Userfish.FindAsync(id);
			_context.Userfish.Remove(userfish);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserfishExists(int id)
		{
			return _context.Userfish.Any(e => e.UserFishId == id);
		}


	}
}
