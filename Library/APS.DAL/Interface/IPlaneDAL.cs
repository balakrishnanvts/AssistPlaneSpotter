using APS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APS.DAL
{
    public interface IPlaneDAL
    {
        Task<int> ManagePlane(Plane planeDetail);
        Task<IEnumerable<Plane>> GetPlane(int id);
        Task<int> DeletePlane(int id);
    }
}
