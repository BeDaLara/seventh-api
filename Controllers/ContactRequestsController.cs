using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeventhGearApi.Models;
using SeventhGearApi.Services;

namespace SeventhGearApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRequestsController : ControllerBase
    {
        private readonly SeventhGearDbContext _context;
        private readonly IEmailService _emailService;

        public ContactRequestsController(SeventhGearDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult<ContactRequest>> PostContactRequest(ContactRequest contactRequest)
        {
            contactRequest.CreatedAt = DateTime.UtcNow;
            contactRequest.Status = "Pending";

            _context.ContactRequests.Add(contactRequest);
            await _context.SaveChangesAsync();

            // Send confirmation email
            var subject = "SeventhGear - Solicitação de Contato Recebida";
            var message = $@"
                <h1>Obrigado por entrar em contato, {contactRequest.FirstName}!</h1>
                <p>Sua solicitação foi recebida e um revendedor entrará em contato em breve.</p>
                <p><strong>Detalhes da solicitação:</strong></p>
                <ul>
                    <li>Revendedor: {contactRequest.DealerName}</li>
                    <li>Preferência de contato: {contactRequest.ContactPreference}</li>
                </ul>
                <p>Atenciosamente,<br>Equipe SeventhGear</p>";

            await _emailService.SendEmailAsync(
                contactRequest.Email,
                $"{contactRequest.FirstName} {contactRequest.LastName}",
                subject,
                message);

            return CreatedAtAction("GetContactRequest", new { id = contactRequest.Id }, contactRequest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactRequest>> GetContactRequest(int id)
        {
            var contactRequest = await _context.ContactRequests
                .Include(cr => cr.Configuration)
                .FirstOrDefaultAsync(cr => cr.Id == id);

            if (contactRequest == null)
            {
                return NotFound();
            }

            return contactRequest;
        }
    }
}