﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inforce.Data;
using Inforce.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Inforce.Controllers;

public class ShortenerController : DI_BaseController
{

    public ShortenerController(ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
    {

    }
    public string Shorten(string longUrl)
    {
        string shortUrl = null;

        using (WebClient client = new WebClient())
        {
            shortUrl = client.DownloadString("http://tinyurl.com/api-create.php?url=" + longUrl);
        }

        return shortUrl;
    }


    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await Context.UrlShortener.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || Context.UrlShortener == null)
        {
            return NotFound();
        }

        var urlShortener = await Context.UrlShortener
            .FirstOrDefaultAsync(m => m.Id == id);
        if (urlShortener == null)
        {
            return NotFound();
        }

        return View(urlShortener);
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LongUrl,ShortUrl,CreatedBy,CreatedDate")] UrlShortener urlShortener)
    {
        if (Context.UrlShortener.Where(x => x.LongUrl == urlShortener.LongUrl).FirstOrDefault() != null)
            return NotFound("This url is already exists!");
        urlShortener.CreatedDate = DateTime.Now;
        urlShortener.CreatorId = UserManager.GetUserId(User);
        urlShortener.CreatedBy = UserManager.GetUserName(User);
        urlShortener.ShortUrl = Shorten(urlShortener.LongUrl);

        Context.Add(urlShortener);
        await Context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || Context.UrlShortener == null)
        {
            return NotFound();
        }

        var urlShortener = await Context.UrlShortener.FindAsync(id);
        if (urlShortener == null)
        {
            return NotFound();
        }
        return View(urlShortener);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,LongUrl,ShortUrl,CreatedBy,CreatedDate")] UrlShortener urlShortener)
    {
        if (id != urlShortener.Id)
        {
            return NotFound();
        }

        try
        {
            urlShortener.CreatorId = Context.UrlShortener.Select(x => x.CreatorId).First();
            urlShortener.CreatedBy = Context.UrlShortener.Select(x => x.CreatedBy).First();
            urlShortener.CreatedDate = Context.UrlShortener.Select(x => x.CreatedDate).First();
            urlShortener.ShortUrl = Shorten(urlShortener.LongUrl);
            Context.Update(urlShortener);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UrlShortenerExists(urlShortener.Id))
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

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || Context.UrlShortener == null)
        {
            return NotFound();
        }

        var urlShortener = await Context.UrlShortener
            .FirstOrDefaultAsync(m => m.Id == id);
        if (urlShortener == null)
        {
            return NotFound();
        }

        return View(urlShortener);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (Context.UrlShortener == null)
        {
            return Problem("Entity set 'ApplicationDbContext.UrlShortener'  is null.");
        }
        var urlShortener = await Context.UrlShortener.FindAsync(id);
        if (urlShortener != null)
        {
            Context.UrlShortener.Remove(urlShortener);
        }

        await Context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UrlShortenerExists(int id)
    {
        return Context.UrlShortener.Any(e => e.Id == id);
    }
}