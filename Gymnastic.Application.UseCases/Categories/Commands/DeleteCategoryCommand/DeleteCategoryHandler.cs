using Gymnastic.Application.Interface.Persistence;
using Gymnastic.Application.UseCases.Commons.Bases;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Gymnastic.Application.UseCases.Categories.Commands.DeleteCategoryCommand
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<BaseResponse<bool>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(command.Id);
                if (category is null)
                    return BaseResponse<bool>.Fail($"Category not found", StatusCodes.Status404NotFound);

                _unitOfWork.Category.Delete(category);
                await _unitOfWork.SaveAsync();

                return BaseResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    return BaseResponse<bool>.Fail(ex.Message);
                return BaseResponse<bool>.Fail("An unexpected error occurred", StatusCodes.Status500InternalServerError);
            }
        }
    }
}
