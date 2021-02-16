using Results.Repository.Common;
using System;
using System.Data.SqlClient;
using Results.Common.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class ResultsRepository: IResultsRepository
    {
        //example repository
        static string con = ConnectionString.GetDefaultConnectionString();
        static SqlConnection conn = new SqlConnection(con);
    }
}
