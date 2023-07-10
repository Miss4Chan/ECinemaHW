using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
