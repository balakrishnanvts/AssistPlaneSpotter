using APS.GenericRepository;
using APS.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace APS.DAL.Implementation
{
    public class PlaneDAL : IPlaneDAL
    {
        private readonly IGenericRepository _genericRepository;
        public PlaneDAL(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<int> ManagePlane(Plane planeDetail)
        {
            GenericParameter genericParameter = new GenericParameter
            {
                SqlCommand = "USP_MANAGEPLANE",
                ExecuteType = CommandType.StoredProcedure
            };
            genericParameter.dynamicParameters.Add("@ID", planeDetail.Id);
            genericParameter.dynamicParameters.Add("@MAKE", planeDetail.Make);
            genericParameter.dynamicParameters.Add("@MODEL", planeDetail.Model);
            genericParameter.dynamicParameters.Add("@REGISTRATION", planeDetail.Registration);
            genericParameter.dynamicParameters.Add("@LOCATION", planeDetail.Location);
            genericParameter.dynamicParameters.Add("@ENTRYDATETIME", planeDetail.EntryDatetime);

            return await _genericRepository.Execute(genericParameter);
        }
        public async Task<IEnumerable<Plane>> GetPlane(int id)
        {
            GenericParameter genericParameter = new GenericParameter
            {
                SqlCommand = "USP_GETPLANE",
                ExecuteType = CommandType.StoredProcedure
            };
            genericParameter.dynamicParameters.Add("@ID", id);

            return await _genericRepository.ExecuteList<Plane>(genericParameter);
        }

        public async Task<int> DeletePlane(int id)
        {
            GenericParameter genericParameter = new GenericParameter
            {
                SqlCommand = "USP_DELETEPLANE",
                ExecuteType = CommandType.StoredProcedure
            };
            genericParameter.dynamicParameters.Add("@ID", id);

            return await _genericRepository.Execute(genericParameter);
        }
    }
}
