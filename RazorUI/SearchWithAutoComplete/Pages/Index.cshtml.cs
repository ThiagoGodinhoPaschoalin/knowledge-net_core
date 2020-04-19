using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SearchWithAutoComplete.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly List<string> slugNames;

        public string MyInputText { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            slugNames = new List<string>
            {
                "testando", "godinho", "wakanda"
            };
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAutoComplete()
        {
            try
            {
                string TermSearch = string.Empty;
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    TermSearch = await reader.ReadToEndAsync();
                }

                if (string.IsNullOrWhiteSpace(TermSearch))
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(slugNames.Where(slug => slug.Contains(TermSearch)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deu ruim.");
                throw;
            }
        }

        public IActionResult OnPostLogin()
        {
            return RedirectToPage("Privacy");
        }
    }
}
