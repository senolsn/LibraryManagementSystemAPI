using Business.Abstracts;
using Business.Dtos.Request;
using Business.Dtos.Response;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class LanguageManager : ILanguageService
    {

        protected readonly ILanguageDal _languageDal;

        public LanguageManager(ILanguageDal languageDal)
        {
            _languageDal = languageDal;
        }

        public async Task<CreatedLanguageResponse> Add(CreateLanguageRequest createLanguageRequest)
        {
            Language language = new Language() { LanguageId = Guid.NewGuid(), LanguageName= createLanguageRequest.LanguageName};

           Language createdLanguage = await _languageDal.AddAsync(language);

            CreatedLanguageResponse createdLanguageResponse = new CreatedLanguageResponse() {IsAdded = true,LanguageId = createdLanguage.LanguageId, LanguageName= createdLanguage.LanguageName };

            return createdLanguageResponse;
        }
    }
}
