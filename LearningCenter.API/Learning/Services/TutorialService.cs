using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Services;

public class TutorialService : ITutorialService
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TutorialService(ITutorialRepository tutorialRepository, IUnitOfWork unitOfWork)
    {
        _tutorialRepository = tutorialRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<Tutorial>> ListAsync()
    {
        return await _tutorialRepository.ListAsync();
    }

    public async Task<IEnumerable<Tutorial>> ListByCategoryIdAsync(int categoryId)
    {
        return await _tutorialRepository.FindByCategoryIdAsync(categoryId);
    }

    public Task<TutorialResponse> SaveAsync(Tutorial tutorial)
    {
        throw new NotImplementedException();
    }

    public Task<TutorialResponse> UpdateAsync(int id, Tutorial tutorial)
    {
        throw new NotImplementedException();
    }

    public Task<TutorialResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}