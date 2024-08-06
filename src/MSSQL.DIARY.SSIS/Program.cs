using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQL.DIARY.SSIS
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadSSISFiles loadSSISFiles = new LoadSSISFiles(@"C:\Users\lakshamanr\source\repos\SSISPackageInfoHandler\TestSSISPackages Details\bin\Debug\netcoreapp2.2\TestSSISPackage");
            loadSSISFiles.LoadpackageDetails();
            foreach(var data in loadSSISFiles.SSISPackages_Details)
            {
                var JsonObject = JsonConvert.SerializeObject(data.Value);
                Console.WriteLine(JsonObject);
            }
            
           
        }
    }
}
