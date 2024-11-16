using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace QuickStart
{
    public class LocalMethods
    {
        public async Task<object> GetAppDomainDirectory(dynamic input)
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public async Task<object> GetCurrentTime(dynamic input)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public async Task<object> UseDynamicInput(dynamic input)
        {
            return $".NET {input.framework} welcomes {input.node}";
        }
        public async Task<object> ThrowException(dynamic input)
        {
            throw new Exception("Sample Exception");
        }
        
        public async Task<object> ListCertificates(dynamic input)
        {
            X509Store store = new X509Store((string)input.storeName, (StoreLocation)Enum.Parse(typeof(StoreLocation), (string)input.storeLocation));
            store.Open(OpenFlags.ReadOnly);
            try
            {
                List<string> result = new List<string>();
                foreach (X509Certificate2 certificate in store.Certificates)
                {
                    result.Add(certificate.Subject);
                }

                return result;
            }
            finally
            {
                store.Close();
            }
        }


        public async Task<object> TestWithSqlDb(dynamic input)
        {
            var tableName = "adapters";
            SqlDataAdapter select = new SqlDataAdapter("select * from adapters order by idName",
                "Data Source=(localdb)\\Local;Initial Catalog=Octopus;Integrated Security=True");
            DataSet dataSet = new DataSet(tableName);
            select.Fill(dataSet, tableName);
            var adaptersTable = dataSet.Tables[0];
            var sourcesNames = new List<string>();
            foreach (DataRow a_row in adaptersTable.Rows)
            {
                var adapter_class = a_row["AdapterClassName"].ToString();
                sourcesNames.Add(a_row["sourceName"].ToString());
            }
            return string.Join(",",sourcesNames);
        }
    }
}
