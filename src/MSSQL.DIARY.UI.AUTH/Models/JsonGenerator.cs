//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ConsoleApp5
//{
//    class Program
//    {
//        public static void Main(string[] args)
//        {
//            //Your code goes here

//            List<string> strings;
//            strings = new List<string>()
//            {
//                "HumanResources.Employee",
//"HumanResources.Employee/dbo.ufnGetContactInformation",
//"HumanResources.Employee/dbo.uspGetEmployeeManagers",
//"HumanResources.Employee/dbo.uspGetManagerEmployees",
//"HumanResources.Employee/HumanResources.dEmployee",
//"HumanResources.Employee/HumanResources.EmployeeDepartmentHistory",
//"HumanResources.Employee/HumanResources.EmployeePayHistory",
//"HumanResources.Employee/HumanResources.JobCandidate",
//"HumanResources.Employee/HumanResources.JobCandidate/dbo.uspSearchCandidateResumes",
//"HumanResources.Employee/HumanResources.JobCandidate/HumanResources.vJobCandidate",
//"HumanResources.Employee/HumanResources.JobCandidate/HumanResources.vJobCandidateEducation",
//"HumanResources.Employee/HumanResources.JobCandidate/HumanResources.vJobCandidateEmployment",
//"HumanResources.Employee/HumanResources.uspUpdateEmployeeHireInfo",
//"HumanResources.Employee/HumanResources.uspUpdateEmployeeLogin",
//"HumanResources.Employee/HumanResources.uspUpdateEmployeePersonalInfo",
//"HumanResources.Employee/HumanResources.vEmployee",
//"HumanResources.Employee/HumanResources.vEmployeeDepartment",
//"HumanResources.Employee/HumanResources.vEmployeeDepartmentHistory",
//"HumanResources.Employee/Production.Document",
//"HumanResources.Employee/Production.Document/Production.ProductDocument",
//"HumanResources.Employee/Purchasing.PurchaseOrderHeader",
//"HumanResources.Employee/Purchasing.PurchaseOrderHeader/Purchasing.iPurchaseOrderDetail",
//"HumanResources.Employee/Purchasing.PurchaseOrderHeader/Purchasing.PurchaseOrderDetail",
//"HumanResources.Employee/Purchasing.PurchaseOrderHeader/Purchasing.uPurchaseOrderDetail",
//"HumanResources.Employee/Purchasing.PurchaseOrderHeader/Purchasing.uPurchaseOrderHeader",
//"HumanResources.Employee/Sales.SalesPerson",
//"HumanResources.Employee/Sales.SalesPerson/Sales.SalesOrderHeader",
//"HumanResources.Employee/Sales.SalesPerson/Sales.SalesOrderHeader/Sales.iduSalesOrderDetail",
//"HumanResources.Employee/Sales.SalesPerson/Sales.SalesOrderHeader/Sales.SalesOrderDetail",
//"HumanResources.Employee/Sales.SalesPerson/Sales.SalesOrderHeader/Sales.SalesOrderHeaderSalesReason",
//"HumanResources.Employee/Sales.SalesPerson/Sales.SalesPersonQuotaHistory",
//"HumanResources.Employee/Sales.SalesPerson/Sales.SalesTerritoryHistory",
//"HumanResources.Employee/Sales.SalesPerson/Sales.Store",
//"HumanResources.Employee/Sales.SalesPerson/Sales.Store/Sales.Customer",
//"HumanResources.Employee/Sales.SalesPerson/Sales.Store/Sales.Customer/Sales.vIndividualCustomer",
//"HumanResources.Employee/Sales.SalesPerson/Sales.Store/Sales.vStoreWithAddresses",
//"HumanResources.Employee/Sales.SalesPerson/Sales.Store/Sales.vStoreWithContacts",
//"HumanResources.Employee/Sales.SalesPerson/Sales.Store/Sales.vStoreWithDemographics",
//"HumanResources.Employee/Sales.SalesPerson/Sales.uSalesOrderHeader",
//"HumanResources.Employee/Sales.vSalesPerson",
//"HumanResources.Employee/Sales.vSalesPersonSalesByFiscalYears"
//            };
//            eat e = new eat(strings);
//            var str = "\"data\"" + ":{\""+"name\""+":"+"\"\""+"},";
//            e.root.iblnFirstNode = true;
//            Console.WriteLine(e.root.ToJson() );
//            Console.ReadLine();
//        }
//    }
//    public class eat
//    {
//        public node root;
//        public eat(List<string> l)
//        {
//            root = new node("");
//            foreach (string s in l)
//            {
//                addRow(s);
//            }

//        }
//        public void addRow(string s)
//        {
//            List<string> l = s.Split('/').ToList<String>();
//            node state = root;
//            foreach (string ss in l)
//            {
//                addSoon(state, ss);
//                state = getSoon(state, ss);
//            }
//        }
//        private void addSoon(node n, string s)
//        {
//            bool f = false;
//            foreach (node ns in n.soon)
//            {
//                if (ns.name == s) { f = !f; }
//            }
//            if (!f) { n.soon.Add(new node(s)); }

//        }
//        private node getSoon(node n, string s)
//        {
//            foreach (node ns in n.soon)
//            {
//                if (ns.name == s) { return ns; }
//            }
//            return null;
//        }
//    }
//    public class node
//    {
//        public bool iblnFirstNode { get; set; }
//        public node(string n)
//        {
//            name = n;
//            soon = new List<node>();
//        }
//        public string name;
//        public List<node> soon;

//        public string ToJson()
//        {
//            String s = "";
//            if (iblnFirstNode)
//            {
//                s = s + "{\"data\":{\"" + "name"+"\":"+"\""+name+ "\"}" + ",\"data\":[";
//                iblnFirstNode = true;
//            }
//            else
//            {
//                s = s
//                    //+ "{\"data\":{\"" 
//                  + "{\"label\":\"" 

//                    + name
//                    + "\"," 
//                        +"\"" +"expandedIcon"
//                        +"\""
//                        +":"
//                        +"\""
//                        + "fa fa-folder-open"
//                        +"\""
//                        +","
//                        + "\""+"collapsedIcon"
//                        + "\""
//                        + ":"
//                        + "\""
//                        + "fa fa-folder-close"+"\"" 

//                    //        "expandedIcon": "fa fa-folder-open",
//                    //"collapsedIcon": "fa fa-folder",
//                    + ",\"children\":["

//                    ;
//            }
//            bool f = true;
//            foreach (node n in soon)
//            {
//                if (f) { f = !f; } else { s = s + ","; }
//                s = s + n.ToJson();
//            }
//            s = s + "]}";
//            return s;
//        }
//    }
//}

