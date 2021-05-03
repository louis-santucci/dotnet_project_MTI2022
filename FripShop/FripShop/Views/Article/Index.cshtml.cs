using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FripShop.DataAccess.Interfaces;
using FripShop.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FripShop.Views.Article
{
    /// <summary>
    /// Class for index article view
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DataAccess.Interfaces.IArticleRepo _articleRepo;
        private readonly DataAccess.Interfaces.IUserRepo _userRepo;
        private string _sessionId;

        public IndexModel(ILogger<IndexModel> logger, DataAccess.Interfaces.IArticleRepo articleRepo, IUserRepo userRepo)
        {
            _logger = logger;
            _articleRepo = articleRepo;
            _userRepo = userRepo;

        }
    }
}
