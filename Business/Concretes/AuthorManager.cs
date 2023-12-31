using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Request.Create;
using Business.Dtos.Request.Delete;
using Business.Dtos.Request.Update;
using Business.Dtos.Response.Create;
using Business.Dtos.Response.Read;
using Business.Dtos.Response.Update;
using Core.DataAccess.Paging;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<CreatedAuthorResponse> Add(CreateAuthorRequest request)
        {
            Author author = _mapper.Map<Author>(request);

            var createdAuthor = await _authorDal.AddAsync(author);

            var createdAuthorResponse = _mapper.Map<CreatedAuthorResponse>(createdAuthor);

            return createdAuthorResponse;
        }

        public async Task<UpdatedAuthorResponse> Update(UpdateAuthorRequest request)
        {
            var authorToUpdate = await _authorDal.GetAsync(a => a.AuthorId == request.AuthorId);

            if(authorToUpdate is null) 
            {
                return new UpdatedAuthorResponse() { IsUpdated = false };
            }

            _mapper.Map(request,authorToUpdate);

            await _authorDal.UpdateAsync(authorToUpdate);

            return new UpdatedAuthorResponse();
        }

        public async Task Delete(DeleteAuthorRequest request)
        {
            var authorToDelete = await _authorDal.GetAsync(a => a.AuthorId == request.AuthorId);

           if( authorToDelete is not null)
            {
                await _authorDal.DeleteAsync(authorToDelete);
            }

            throw new Exception("Something went wrong!");
        }

        public async Task<Author> GetAsync(Guid authorId)
        {
            return await _authorDal.GetAsync(a => a.AuthorId == authorId);
        }

        public async Task<IPaginate<GetListAuthorResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _authorDal.GetListAsync(
                null,
                index: pageRequest.PageIndex,
                size: pageRequest.PageSize);

            var result = _mapper.Map<Paginate<GetListAuthorResponse>>(data);
            return result;
        }
    }
}
