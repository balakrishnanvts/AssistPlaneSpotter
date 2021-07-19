using Dapper;
using System.Data;

namespace APS.GenericRepository
{
    public class GenericParameter : SqlMapper.IDynamicParameters
	{
		public DynamicParameters dynamicParameters = new DynamicParameters();

		GenericParameter _inputParameters;

		/// <summary>
		/// Initialize sql query 
		/// </summary>
		public string SqlCommand
		{
			get; set;
		}

		/// <summary>
		/// Execute type text or stored procedure
		/// </summary>
		public CommandType ExecuteType { get; set; }

		/// <summary>
		/// Connection String based on the job and residing site
		/// </summary>
		public string ConnectionString { get; set; }

		/// <summary>
		/// create instance for dynamic parameters
		/// </summary>
		public GenericParameter InputParameters => _inputParameters ?? (_inputParameters = new GenericParameter());

		public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
		{
			((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);
		}
	}
}