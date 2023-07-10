using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using ECinemaServices.Interface;
using ECinemaDomain.DTO;
using ECinemaDomain.Domain_Models;

namespace ECinema.Web.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService ticketService;

        public TicketsController(ITicketService ticketService )
        {
            this.ticketService = ticketService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(ticketService.GetAllTickets());
        }
        

        //Ovaa func prai data trasfer ovde za da ne ti treba cart id i da moesh posle preku controllerot za sp
        //da go napraish -- zatoa ti treba DTO -- aka avoid kodot dole so izmislena cart id vo htmlot
        public async Task<IActionResult> AddToCart(int ticketId)
        {
            var ticket = ticketService.GetDetailsForTicket(ticketId);
            var model = new AddToShoppingCartDTO();
            model.SelectedTicket = ticket;
            model.TicketId = ticket.Id;
            model.Quantity = 0;

            /*var cart = _context.ShoppingCarts.Where(z => z.ShoppingCartId == cartId).FirstOrDefault();
            var ticketInShoppingCart = new TicketInShoppingCart();
            ticketInShoppingCart.ShoppingCartId = cart.ShoppingCartId;
            ticketInShoppingCart.TicketId = ticket.TicketId;
            _context.TicketInShoppingCarts.Add(ticketInShoppingCart);
            _context.SaveChanges();*/


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(AddToShoppingCartDTO model)
        {
            /*var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Se zema idto na currently logged in user ez:)
            var user = _context.ECinemaUsers.Where(z => z.Id == userId).Include("UserShoppingCart.TicketsInShoppingCart").
                Include("UserShoppingCart.TicketsInShoppingCart.Ticket").FirstOrDefault(); //mozhe da ima greshka proveri iminja


            //Problem with adding the same tickets with same quantity
            var userShoppingCart = user.UserShoppingCart;


            if (userShoppingCart!=null)
            {
                var ticket = _context.Tickets.Find(model.TicketId);
                if (ticket!=null)
                {

                    var doesItExist = _context.TicketsInShoppingCart
                        .Where(z => z.TicketId == ticket.TicketId && z.ShoppingCartId == userShoppingCart.ShoppingCartId).FirstOrDefault();
                    if (doesItExist != null)
                    {
                        doesItExist.Quantity += model.Quantity;
                        _context.Update(doesItExist);
                        await _context.SaveChangesAsync();
                    }
                    //Composite keyto stopnuva dodavanje pak ist film hahahahha -- solved samo zgolemi quantity na existing ez :))                   
                    else
                    {
                        TicketInShoppingCart ticketToAdd = new TicketInShoppingCart
                        {
                            Ticket = ticket,
                            TicketId = ticket.TicketId,
                            ShoppingCart = userShoppingCart,
                            Quantity = model.Quantity
                        };
                        _context.Add(ticketToAdd);
                        await _context.SaveChangesAsync();
                    }
                }
            }*/
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this.ticketService.AddToShoppingCart(model, userId);
            if(result)
            {
                return RedirectToAction("Index", "Tickets");
            }
            return RedirectToAction("Index");
        }
        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetDetailsForTicket(id??0);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket t)
        {
            if (ModelState.IsValid)
            {
                ticketService.CreateNewTicket(t);
                return RedirectToAction(nameof(Index));
            }
            return View(t);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetDetailsForTicket(id??0);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket t)
        {
            if (id != t.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticketService.UpdateExistingTicket(t);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(t.Id))
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
            return View(t);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetDetailsForTicket(id??0);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ticketService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return ticketService.GetDetailsForTicket(id) != null;
        }
    }
}
