using ECinema.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Services.Interface
{
    public interface IEmailService 
    {
        Task SendEmailAsync(List<EmailMessage> allMails);
    }
}
