using AutoMapper;
using Gymnastic.Application.Dto.DTOs;
using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using Gymnastic.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Categories.Commands.CreateCategoryCommand
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<CategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _environment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public async Task<BaseResponse<CategoryDTO>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var category = _mapper.Map<Category>(command);
                var result = await _unitOfWork.Category.AddAsync(category);
                await _unitOfWork.SaveAsync();
                var dto = _mapper.Map<CategoryDTO>(result.Entity);
                return BaseResponse<CategoryDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<CategoryDTO>.Fail(ex.Message);

                return BaseResponse<CategoryDTO>.Fail("An unexpected error occurred",
                    StatusCodes.Status500InternalServerError);
            }
        }
    }
}
