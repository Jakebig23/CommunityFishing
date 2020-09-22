using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityFishing.Models;
using Microsoft.AspNetCore.Identity;

namespace CommunityFishing.Controllers
{
	public class UserProfilesController : Controller
	{
		private readonly CommunityFishingContext _context;
		private UserManager<IdentityUser> _userManager;

		public UserProfilesController(CommunityFishingContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: UserProfiles
		public async Task<IActionResult> Index()
		{
			return View(await _context.UserProfile.ToListAsync());
		}

		// GET: UserProfiles/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = await _context.UserProfile
				.FirstOrDefaultAsync(m => m.ProfileId == id);
			if (userProfile == null)
			{
				return NotFound();
			}

			return View(userProfile);
		}

		// GET: UserProfiles/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: UserProfiles/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ProfileId,FirstName,LastName,YearsFishing,DateCreated,UserAccountId")] UserProfile userProfile)
		{
			if (ModelState.IsValid)
			{
				_context.Add(userProfile);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(userProfile);
		}

		// GET: UserProfiles/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = await _context.UserProfile.FindAsync(id);
			if (userProfile == null)
			{
				return NotFound();
			}
			return View(userProfile);
		}

		// POST: UserProfiles/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ProfileId,FirstName,LastName,YearsFishing,DateCreated,UserAccountId")] UserProfile userProfile)
		{
			if (id != userProfile.ProfileId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(userProfile);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserProfileExists(userProfile.ProfileId))
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
			return View(userProfile);
		}

		// GET: UserProfiles/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = await _context.UserProfile
				.FirstOrDefaultAsync(m => m.ProfileId == id);
			if (userProfile == null)
			{
				return NotFound();
			}

			return View(userProfile);
		}

		// POST: UserProfiles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var userProfile = await _context.UserProfile.FindAsync(id);
			_context.UserProfile.Remove(userProfile);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool UserProfileExists(int id)
		{
			return _context.UserProfile.Any(e => e.ProfileId == id);
		}

		public IActionResult ProfileInfo()
		{
			string userID = _userManager.GetUserId(User);
			UserProfile profile = _context.UserProfile.FirstOrDefault(p => p.UserAccountId == userID);

			if (profile == null)
			{
				return RedirectToAction("Create");
			}
			return View(profile);
		}

		
	}
}
