namespace Project_Forum.Services.Observe
{
    public interface IObserveService
    {
        Task<bool> HandleTagObservation(string tagName, string userId);

        Task<bool> IsTagObserved(string tagName, string userId);
    }
}
