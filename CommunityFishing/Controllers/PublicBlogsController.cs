using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommunityFishing.Models;

namespace CommunityFishing.Controllers
{
	public class PublicBlogsController : Controller
	{
		private readonly CommunityFishingContext _context;

		public PublicBlogsController(CommunityFishingContext context)
		{
			_context = context;
		}

		// GET: PublicBlogs
		public async Task<IActionResult> Index()
		{
			var communityFishingContext = _context.PublicBlog.Include(p => p.UserFish);
			return View(await communityFishingContext.ToListAsync());
		}

		// GET: PublicBlogs/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publicBlog = await _context.PublicBlog
				.Include(p => p.UserFish)
				.FirstOrDefaultAsync(m => m.BlogId == id);
			if (publicBlog == null)
			{
				return NotFound();
			}

			return View(publicBlog);
		}

		// GET: PublicBlogs/Create
		public IActionResult Create()
		{
			ViewData["UserFishId"] = new SelectList(_context.Userfish, "UserFishId", "UserFishId");
			return View();
		}

		// POST: PublicBlogs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("BlogId,UserFishId,BlogTitle,Content,BlogDate")] PublicBlog publicBlog)
		{
			if (ModelState.IsValid)
			{
				_context.Add(publicBlog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["UserFishId"] = new SelectList(_context.Userfish, "UserFishId", "UserFishId", publicBlog.UserFishId);
			return View(publicBlog);
		}

		// GET: PublicBlogs/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publicBlog = await _context.PublicBlog.FindAsync(id);
			if (publicBlog == null)
			{
				return NotFound();
			}
			ViewData["UserFishId"] = new SelectList(_context.Userfish, "UserFishId", "UserFishId", publicBlog.UserFishId);
			return View(publicBlog);
		}

		// POST: PublicBlogs/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("BlogId,UserFishId,BlogTitle,Content,BlogDate")] PublicBlog publicBlog)
		{
			if (id != publicBlog.BlogId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(publicBlog);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PublicBlogExists(publicBlog.BlogId))
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
			ViewData["UserFishId"] = new SelectList(_context.Userfish, "UserFishId", "FishLength", publicBlog.UserFishId);
			return View(publicBlog);
		}

		// GET: PublicBlogs/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publicBlog = await _context.PublicBlog
				.Include(p => p.UserFish)
				.FirstOrDefaultAsync(m => m.BlogId == id);
			if (publicBlog == null)
			{
				return NotFound();
			}

			return View(publicBlog);
		}

		// POST: PublicBlogs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var publicBlog = await _context.PublicBlog.FindAsync(id);
			_context.PublicBlog.Remove(publicBlog);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PublicBlogExists(int id)
		{
			return _context.PublicBlog.Any(e => e.BlogId == id);
		}
	}
}
