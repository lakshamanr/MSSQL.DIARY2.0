using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSSQL.DIARY.SRV.Tests
{
    [TestClass]
    public class srvDatabaseObjectDependncyTests
    {
        [TestMethod]
        public void srvDatabaseObjectDependncyTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetObjectThatDependsOnTest()
        {
            var result =
                new SrvDatabaseObjectDependncy().GetObjectThatDependsOn("AdventureWorks2016",
                    "HumanResources.Employee");

            Assert.Fail();
        }

        [TestMethod]
        public void GetObjectOnWhichDependsTest()
        {
            Assert.Fail();
        }
    }
}