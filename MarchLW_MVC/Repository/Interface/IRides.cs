using MarchLW_MVC.Models;

namespace MarchLW_MVC.Repository.Interface
{
    public interface IRides
    {
        IEnumerable<Rides> GenerateRow(string name);
        /*        IEnumerable<Rides> SearchRides(string searchQuery);
        */
        public Task<IEnumerable<Rides>> SearchRides(string searchQuery);

    }
}
