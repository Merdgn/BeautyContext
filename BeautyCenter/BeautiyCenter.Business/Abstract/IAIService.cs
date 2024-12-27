using BeautiyCenter.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BeautiyCenter.Business.Abstract
{
    public interface IAIService
    {
        Task<AIResponse> GetHairStyleSuggestionAsync(IFormFile image);
    }
}
