﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Core.Entitis;
using BookingApp.Data.Data;
using BookingApp.Web.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BookingApp.Web.Extensions;
using BookingApp.Core.Repositories;
using AutoMapper;
using BookingApp.Core.ViewModels;
using Booking.Web.Filters;

namespace BookingApp.Web.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly IMapper mapper;
       // private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork uow;

        //private readonly GymClassRepository gymClassRepository;
        //private readonly ApplicationUserGymRepository userGymRepository;

        public GymClassesController(IMapper mapper ,/*ApplicationDbContext context*/ UserManager<ApplicationUser> userManager, IUnitOfWork uow)
        {
            this.mapper = mapper;
         //   db = context;
            this.userManager = userManager;
            //uow = new UnitOfWork(db);
            //gymClassRepository = new GymClassRepository(context);
            //userGymRepository = new ApplicationUserGymRepository(context);
            this.uow = uow;
        }

        // GET: GymClasses
        [AllowAnonymous]
        public async Task<IActionResult> Index(IndexViewModel viewModel)
        {

            if (User.Identity != null && !User.Identity.IsAuthenticated)
                return View(mapper.Map<IndexViewModel>(await uow.GymClassRepository.GetAsync()));

            var gymclasses = viewModel.ShowHistory ? 
                            await uow.GymClassRepository.GetHistoryAsync() : 
                            await uow.GymClassRepository.GetWithAttendinAsync();

            var model = mapper.Map<IndexViewModel>(gymclasses);
            

            return base.View(model);
        }

       

        // [Authorize]
        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id is null) return BadRequest();

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = userManager.GetUserId(User);

            if (userId == null) return BadRequest();

            var attending = await uow.UserGymRepository.FindAsync(userId, (int)id)!;

            if (attending == null)
            {
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = (int)id
                };

                uow.UserGymRepository.Add(booking); 

               // db.AppUserGyms.Add(booking);
            }
            else
            {
                uow.UserGymRepository.Remove(attending);
                //db.AppUserGyms.Remove(attending);
            }

            await uow.CompleteAsync();

            return RedirectToAction("Index");

        }

        // GET: GymClasses/Details/5
        [RequiredParameterRequiredModel("id")]
        public async Task<IActionResult> Details(int? id)
        {
            return View(await uow.GymClassRepository.GetAsync(id));
        }

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return Request.IsAjax() ? PartialView("CreatePartial") :  View();
        }

        public IActionResult FetchForm()
        {
            return PartialView("CreatePartial");
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                uow.GymClassRepository.Add(gymClass);
                await uow.CompleteAsync();
                return Request.IsAjax() ? PartialView("GymClass", gymClass) :  RedirectToAction(nameof(Index));
            }
            //Check if Ajax!
            if (Request.IsAjax())
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return PartialView("CreatePartial", gymClass);
            }

            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null )
        //    {
        //        return NotFound();
        //    }

        //    var gymClass = await db.GymClasses.FindAsync(id);
        //    if (gymClass == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(gymClass);
        //}

        //// POST: GymClasses/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,Duration,Description")] GymClass gymClass)
        //{
        //    if (id != gymClass.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Update(gymClass);
        //            await db.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GymClassExists(gymClass.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(gymClass);
        //}

        //// GET: GymClasses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || db.GymClasses == null)
        //    {
        //        return NotFound();
        //    }

        //    var gymClass = await db.GymClasses
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (gymClass == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(gymClass);
        //}

        //// POST: GymClasses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (db.GymClasses == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
        //    }
        //    var gymClass = await db.GymClasses.FindAsync(id);
        //    if (gymClass != null)
        //    {
        //        db.GymClasses.Remove(gymClass);
        //    }
            
        //    await db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GymClassExists(int id)
        //{
        //  return (db.GymClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
