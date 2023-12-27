using Business.Dtos.Request;
using Business.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ILanguageService
    {
        Task<CreatedLanguageResponse> Add(CreateLanguageRequest request);
    }
}
