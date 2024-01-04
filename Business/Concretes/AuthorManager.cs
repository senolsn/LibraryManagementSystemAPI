using AutoMapper;
using Business.Abstracts;
using Business.Constants;
using Business.Dtos.Request.Author;
using Business.Dtos.Response.Author;
using Core.DataAccess.Paging;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class AuthorManager : IAuthorService
    {
        protected readonly IAuthorDal _authorDal;
        protected readonly IBookAuthorDal _bookAuthorDal;
        protected readonly IMapper _mapper;

        public AuthorManager(IAuthorDal authorDal, IBookAuthorDal bookAuthorDal, IMapper mapper)
        {
            _authorDal = authorDal;
            _bookAuthorDal = bookAuthorDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CreateAuthorRequest request)
        {
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

           if( authorToDelete is not null)
            {
                if (CheckIfExistInBookAuthors(request.AuthorId))
                {
                    return new ErrorResult(Messages.AuthorExistInBookAuthors);
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

        public async Task<IDataResult<IPaginate<GetListAuthorResponse>>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _authorDal.GetListAsync(
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

        private bool CheckIfExistInBookAuthors(Guid authorId)
        {
            if(_bookAuthorDal.GetAsync(ba => ba.AuthorId == authorId) is null)
            {
                return false;
            }
            return true;
        }
    }
}
