namespace LearningCenter.API.Learning.Resources;

public class TutorialResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryResource Category { get; set; }
}