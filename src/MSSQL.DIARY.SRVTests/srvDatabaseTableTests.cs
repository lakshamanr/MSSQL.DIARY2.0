using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSSQL.DIARY.SRV.Tests
{
    [TestClass]
    public class srvDatabaseTableTests
    {
        [TestMethod]
        public void CreateOrGetcacheTableThatDependsOnTest()
        {
            new SrvDatabaseTable().CreateOrGetcacheTableThatDependsOn("AdventureWorks2016", "HumanResources.Employee");
            Assert.Fail();
        }
    }
}