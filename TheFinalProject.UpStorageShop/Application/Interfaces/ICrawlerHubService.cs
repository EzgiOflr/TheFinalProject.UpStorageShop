using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICrawlerHubService
    {
        //kazınacak ürünler icin 
        Task CrawledService(Guid Id,CancellationToken cancellationToken);    
    }
}
