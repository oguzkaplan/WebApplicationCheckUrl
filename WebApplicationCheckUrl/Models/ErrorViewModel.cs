using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace WebApplicationCheckUrl.Models
{
    public class ErrorViewModel:PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
