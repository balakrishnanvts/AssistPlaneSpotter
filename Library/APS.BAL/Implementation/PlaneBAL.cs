using APS.DAL;
using APS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APS.BAL.Implementation
{
    public class PlaneBAL : IPlaneBAL
    {
        private readonly IPlaneDAL _apsPlaneDAL;
        public PlaneBAL(IPlaneDAL apsPlaneDAL)
        {
            _apsPlaneDAL = apsPlaneDAL;
        }
        public async Task<int> ManagePlane(Plane planeDetail)
        {
            return await _apsPlaneDAL.ManagePlane(planeDetail);
        }
        public async Task<IEnumerable<Plane>> GetPlane(int id)
        {
            return await _apsPlaneDAL.GetPlane(id);
        }

        public async Task<int> DeletePlane(int id)
        {
            return await _apsPlaneDAL.DeletePlane(id);
        }
    }
}
