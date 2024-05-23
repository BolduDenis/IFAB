using IFAB.Models;
namespace IFAB.Services
   
{
    public interface IRefereeService
    {
        List<Referee> GetAllReferees();
        Referee GetRefereeById(int id);
        void CreateReferee(Referee referee);
        void UpdateReferee(Referee referee);
        void DeleteReferee (int id);
    }
}
