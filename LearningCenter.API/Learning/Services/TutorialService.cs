using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Services;

public class TutorialService : ITutorialService
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public TutorialService(ITutorialRepository tutorialRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _tutorialRepository = tutorialRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }


    public async Task<IEnumerable<Tutorial>> ListAsync()
    {
        return await _tutorialRepository.ListAsync();
    }

    public async Task<IEnumerable<Tutorial>> ListByCategoryIdAsync(int categoryId)
    {
        return await _tutorialRepository.FindByCategoryIdAsync(categoryId);
    }

    public async Task<TutorialResponse> SaveAsync(Tutorial tutorial)
    {
        // Validate CategoryId

        var existingCategory = _categoryRepository.FindByIdAsync(tutorial.CategoryId);

        if (existingCategory == null)
            return new TutorialResponse("Invalid Category");
        
        // Validate Name

        var existingTutorialWithName = await _tutorialRepository.FindByNameAsync(tutorial.Name);

        if (existingTutorialWithName != null)
            return new TutorialResponse("Tutorial Name already exists.");

        try
        {
            await _tutorialRepository.AddAsync(tutorial);
            await _unitOfWork.CompleteAsync();

            return new TutorialResponse(tutorial);
        }
        catch (Exception e)
        {
            return new TutorialResponse($"An error occurred while saving the tutorial: {e.Message}");
        }
    }

    public async Task<TutorialResponse> UpdateAsync(int id, Tutorial tutorial)
    {
        var existingTutorial = await _tutorialRepository.FindByIdAsync(id);

        // Validate TutorialId
        
        if (existingTutorial == null)
            return new TutorialResponse("Tutorial not found.");

        // Validate CategoryId

        var existingCategory = _categoryRepository.FindByIdAsync(tutorial.CategoryId);

        if (existingCategory == null)
            return new TutorialResponse("Invalid Category");
        
        // Validate Name

        var existingTutorialWithName = await _tutorialRepository.FindByNameAsync(tutorial.Name);

        if (existingTutorialWithName != null && existingTutorialWithName.Id != existingTutorial.Id)
            return new TutorialResponse("Tutorial Name already exists.");

        existingTutorial.Name = tutorial.Name;
        existingTutorial.Description = tutorial.Description;

        try
        {
            _tutorialRepository.Update(existingTutorial);
            await _unitOfWork.CompleteAsync();

            return new TutorialResponse(existingTutorial);
        }
        catch (Exception e)
        {
            return new TutorialResponse($"An error occurred while updating the tutorial: {e.Message}");
        }

    }

    public Task<TutorialResponse> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}