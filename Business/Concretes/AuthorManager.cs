using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Author;
using Business.Dtos.Request.AuthorRequests;
using Business.Dtos.Request.Category;
using Business.Dtos.Response.Author;
using Core.DataAccess.Paging;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AuthorManager : IAuthorService
    {
        protected readonly IAuthorDal _authorDal;
        protected readonly IMapper _mapper;

        public AuthorManager(IAuthorDal authorDal, IMapper mapper)
        {
            _authorDal = authorDal;
            _mapper = mapper;
        }
        public async Task<IResult> Add(CreateAuthorRequest request)
        {
            BusinessRules.Run(CapitalizeFirstLetter(request));

            Author author = _mapper.Map<Author>(request);

            var createdAuthor = await _authorDal.AddAsync(author);    
            
            if(createdAuthor is null)
            {
                return new ErrorResult(Messages.Error);
            }

            return new SuccessResult(Messages.AuthorAdded);
        }
        public async Task<IResult> Update(UpdateAuthorRequest request)
        {
            var result = BusinessRules.Run(CapitalizeFirstLetter(request));

            if(result is not null)
            {
                return result;
            }

            var authorToUpdate = await _authorDal.GetAsync(a => a.AuthorId == request.AuthorId);

            if(authorToUpdate is null) 
            {
                return new ErrorResult(Messages.Error);
            }

            _mapper.Map(request,authorToUpdate);

            await _authorDal.UpdateAsync(authorToUpdate);

            return new SuccessResult(Messages.AuthorUpdated);
        }
        public async Task<IResult> Delete(DeleteAuthorRequest request)
        {
            var authorToDelete = await _authorDal.GetAsync(a => a.AuthorId == request.AuthorId);

            if(authorToDelete is not null)
            {
                var result = BusinessRules.Run(CheckIfAuthorsHasBook(authorToDelete.AuthorBooks));

                if(result is not null)
                {
                    return result;
                }

                await _authorDal.DeleteAsync(authorToDelete);
                return new SuccessResult(Messages.AuthorDeleted);
            }
            return new ErrorResult(Messages.Error);
        }
        public async Task<IDataResult<Author>> GetAsync(Guid authorId)
        {
            var result = await _authorDal.GetAsync(a => a.AuthorId == authorId);

            if (result is not null)
            {
                return new SuccessDataResult<Author>(result,Messages.AuthorListed); 
            }

            return new ErrorDataResult<Author>(Messages.Error);
            
        }
        public async Task<IDataResult<IPaginate<GetListAuthorResponse>>> GetPaginatedListAsync(PageRequest pageRequest)
        {
            var data = await _authorDal.GetPaginatedListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize);

            if (data is not null)
            {
                var result = _mapper.Map<Paginate<GetListAuthorResponse>>(data);

                return new SuccessDataResult<IPaginate<GetListAuthorResponse>>(result, Messages.AuthorsListed);
            }

            return new ErrorDataResult<IPaginate<GetListAuthorResponse>>(Messages.Error);
        }
        public async Task<IDataResult<List<GetListAuthorResponse>>> GetListAsync()
        {
            var data = await _authorDal.GetListAsync(null);

            if (data is not null)
            {
                var authorResponse = _mapper.Map<List<GetListAuthorResponse>>(data);
                return new SuccessDataResult<List<GetListAuthorResponse>>(authorResponse, Messages.AuthorsListed);
            }
            return new ErrorDataResult<List<GetListAuthorResponse>> (Messages.Error);
        }
        public async Task<IDataResult<List<GetListAuthorResponse>>> GetListAsyncSortedByName()
        {
            var data = await _authorDal.GetListAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderBy(a => a.AuthorFirstName)
                );

            if(data is not null)
            {
                var authorResponse = _mapper.Map<List<GetListAuthorResponse>>(data);
                return new SuccessDataResult<List<GetListAuthorResponse>>(authorResponse,Messages.AuthorsListed);
            }

            return new ErrorDataResult<List<GetListAuthorResponse>>(Messages.Error);
        }
        public async Task<IDataResult<List<GetListAuthorResponse>>> GetListAsyncSortedByCreatedDate()
        {
            var data = await _authorDal.GetListAsyncOrderBy(
                predicate: null,
                orderBy: q => q.OrderBy(a => a.CreatedDate)
                );

            if( data is not null)
            {
                var authorResponse = _mapper.Map<List<GetListAuthorResponse>>(data);
                return new SuccessDataResult<List<GetListAuthorResponse>>(authorResponse);
            }

            return new ErrorDataResult<List<GetListAuthorResponse>>(Messages.Error);
        }

        #region Helper Methods

        private IDataResult<IAuthorRequest> CapitalizeFirstLetter(IAuthorRequest request)
        {
            var firstNameToArray = request.AuthorFirstName.Split(' ', ',', '.');
            var lastNameToArray = request.AuthorLastName.Split(' ', ',', '.');

            string[] arrayToFirstName = new string[firstNameToArray.Length];
            string[] arrayToLastName = new string[lastNameToArray.Length];

            int count = 0;

            foreach (var word in firstNameToArray)
            {
                var capitalizedFirstName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToFirstName[count] = capitalizedFirstName;
                count++;
            }

            count = 0;

            foreach (var word in lastNameToArray)
            {
                var capitalizedLastName = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                arrayToLastName[count] = capitalizedLastName;
                count++;
            }

            request.AuthorFirstName = string.Join(" ", arrayToFirstName);
            request.AuthorLastName = string.Join(" ", arrayToLastName);

            return new SuccessDataResult<IAuthorRequest>(request);
        }

        private IResult CheckIfAuthorsHasBook(ICollection<Book> authorBooks)
        {

            if (authorBooks.Count < 1)
            {
                return new SuccessResult();

            }
            return new ErrorResult(Messages.AuthorExistInBook);
        }


        #endregion
    }
}
