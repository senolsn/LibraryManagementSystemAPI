using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Request.Create;
using Business.Dtos.Request.Delete;
using Business.Dtos.Request.Update;
using Business.Dtos.Response.Create;
using Business.Dtos.Response.Delete;
using Business.Dtos.Response.Update;
using Core.DataAccess.Paging;
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
        protected readonly IMapper _mapper;

        public LanguageManager(ILanguageDal languageDal,IMapper mapper)
        {
            _languageDal = languageDal;
            _mapper = mapper;

        }

        public async Task<CreatedLanguageResponse> Add(CreateLanguageRequest createLanguageRequest)
        {
            Language language = new Language() { LanguageId = Guid.NewGuid(), LanguageName = createLanguageRequest.LanguageName };

            var createdLanguage = await _languageDal.AddAsync(language);

            var createdLanguageResponse = new CreatedLanguageResponse() { IsAdded = true, LanguageId = createdLanguage.LanguageId, LanguageName = createdLanguage.LanguageName };

            return createdLanguageResponse;
        }

        public async Task<DeletedLanguageResponse> Delete(DeleteLanguageRequest request)
        {
            var languageToDelete = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if (languageToDelete == null)
            {
                return new DeletedLanguageResponse() { IsDeleted = false };
            }

            await _languageDal.DeleteAsync(languageToDelete);

            return new DeletedLanguageResponse() { IsDeleted = true };
        }

        public async Task<Language> GetAsync(Guid id)
        {
            return await _languageDal.GetAsync(l => l.LanguageId == id);
        }

        public async Task<IPaginate<Language>> GetListAsync(int index, int size)
        {

            return await _languageDal.GetListAsync(null, index, size);
        }

        public async Task<UpdatedLanguageResponse> Update(UpdateLanguageRequest request)
        {
            var languageToUpdate = await _languageDal.GetAsync(l => l.LanguageId == request.LanguageId);

            if (languageToUpdate == null)
            {
                return new UpdatedLanguageResponse() { IsUpdated = false };
            }

            UpdateLanguageFields(request, languageToUpdate);

            await _languageDal.UpdateAsync(languageToUpdate);
            return new UpdatedLanguageResponse() { IsUpdated = true };
        }


        protected void UpdateLanguageFields(UpdateLanguageRequest request, Language language)
        {
            var properties = typeof(UpdateLanguageRequest).GetProperties();

            foreach (var property in properties)
            {
                var requestValue = property.GetValue(request);
                if (requestValue != null && !string.IsNullOrEmpty(requestValue.ToString()))
                {
                    var productProperty = language.GetType().GetProperty(property.Name);
                    productProperty?.SetValue(language, requestValue);
                }

            }
        }
    }
}
