using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;
using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.SRV;
using MSSQL.DIARY.UI.Local_db;
using MSSQL.DIARY.UI.Local_db.Models;
using MSSQL.DIARY.UI.Models;

namespace MSSQL.DIARY.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseLoginController : ControllerBase
    {
        private static readonly Dictionary<string, ServerLogin> UserCache = new Dictionary<string, ServerLogin>();

        public DatabaseLoginController(ApplicationDbContext applicationDbContext)
        {
            SrvDatabaseLogin = new SrvDatabaseLogin();
            this.applicationDbContext = applicationDbContext;
        }

        private ApplicationDbContext applicationDbContext { get; }

        private SrvDatabaseLogin SrvDatabaseLogin { get; }

        [HttpGet("[action]")]
        public LoginModel IsLoginSuccessfully(string istrUserName, string istrPassword, string istrServerName)
        {
            var IblnIsLogin = false;
            var serverLogin = new ServerLogin();
            Users User = CheckIsUserIsExist(istrUserName, istrPassword, istrServerName);
            if (User.IsNotNull() && User.IsAdmin)
            {
                GetConnectionString(serverLogin, User);
                serverLogin.currentUser = istrUserName;
                AddUserInCacheIfNot(ref IblnIsLogin, serverLogin, User);
                return new LoginModel { iblnIsLoginSucessFully = IblnIsLogin, iblnIsAdminUser = User.IsAdmin };
            }
            else if (User.IsNotNull())
            {
                if (User.CONNECTION_STRING.IsNotNull())
                {
                    GetConnectionString(serverLogin, User);
                    serverLogin.currentUser = istrUserName;
                    if (UserCache.TryGetValue(serverLogin.currentUser, out ServerLogin serverLoginIfExists))
                    {
                        if (serverLoginIfExists.iblnIsLogin)
                        {
                            IblnIsLogin = serverLoginIfExists.iblnIsLogin;
                        }
                        else
                        {
                            if (SrvDatabaseLogin.IsLoginSuccessfully(serverLoginIfExists))
                            {
                                UserCache[serverLogin.currentUser].iblnIsAdmin = User.IsAdmin;
                                UserCache[serverLogin.currentUser].iblnIsLogin = true;
                                IblnIsLogin = true;
                            }
                        }
                    }
                    else
                    {
                        AddUserInCacheIfNot(ref IblnIsLogin, serverLogin, User);
                    }
                }
                return new LoginModel { iblnIsLoginSucessFully = IblnIsLogin, iblnIsAdminUser = User.IsAdmin };
            }
            return new LoginModel { iblnIsLoginSucessFully = IblnIsLogin, iblnIsAdminUser = false };
        }

        private Users CheckIsUserIsExist(string istrUserName, string istrPassword, string istrServerName)
        {
            return applicationDbContext.users.FirstOrDefault();
        }

        private ServerLogin AddUserInCacheIfNot(ref bool IblnIsLogin, ServerLogin serverLogin, Users User)
        {
            ServerLogin serverLoginIfExists;
            {
                serverLoginIfExists = new ServerLogin
                {
                    istrUserName = serverLogin.istrUserName,
                    istrPassword = serverLogin.istrPassword,
                    istrServerName = serverLogin.istrServerName,
                    istrDatabaseName = serverLogin.istrDatabaseName,
                    currentUser = serverLogin.currentUser,
                    iblnIsAdmin = User.IsAdmin
                };
                if (SrvDatabaseLogin.IsLoginSuccessfully(serverLoginIfExists))
                {
                    serverLoginIfExists.iblnIsLogin = true;
                    UserCache.AddIfNotContainsKey(serverLoginIfExists.currentUser, serverLoginIfExists);
                    IblnIsLogin = true;
                }
            }

            return serverLoginIfExists;
        }

        private static void GetConnectionString(ServerLogin serverLogin, Users User)
        {
            User.CONNECTION_STRING.Split(';').ToList().ForEach(x =>
            {
                var GetConnectionParts = x.Split('=');
                switch (GetConnectionParts[0].Trim())
                {
                    case "Data Source":
                        {
                            serverLogin.istrServerName = GetConnectionParts[1];
                        }
                        break;
                    case "Initial Catalog":
                        {
                            serverLogin.istrDatabaseName = GetConnectionParts[1];
                        }
                        break;
                    case "User Id":
                        {
                            serverLogin.istrUserName = GetConnectionParts[1];
                        }
                        break;
                    case "Password":
                        {
                            serverLogin.istrPassword = GetConnectionParts[1];
                        }
                        break;
                }
            });
        }

        [HttpGet("[action]")]
        public bool IsLogOutSuccessfully(string istrUserName)
        {
            ServerLogin serverLogin;
            if (UserCache.TryGetValue(istrUserName, out serverLogin))
            {
                serverLogin.iblnIsLogin = false;
                UserCache.Remove(istrUserName);
            }
            return SrvDatabaseLogin.IsLogOutSuccessfully();
        }

        [HttpGet("[action]")]
        public bool IsAlreadyLoggedIn(string istrUserName)
        {
            var serverLogin = new ServerLogin();
            if (UserCache.TryGetValue(istrUserName, out serverLogin)) return serverLogin.iblnIsLogin;
            return false;
        }

        [HttpGet("[action]")]
        public bool IsAdminUser(string istrUserName)
        {
            var serverLogin = new ServerLogin();
            if (UserCache.TryGetValue(istrUserName, out serverLogin)) return serverLogin.iblnIsAdmin;
            return false;
        }

        [HttpGet("[action]")]
        public List<Users> GetUserDetails(string istrUserName)
        {
            var userList = new List<Users>();
            ServerLogin serverLogin;
            if (UserCache.TryGetValue(istrUserName, out serverLogin))
                if (serverLogin.iblnIsAdmin)
                    userList = applicationDbContext.users.DistinctBy(x => x.SERVER_NAME).ToList();
            return userList;
        }

        [HttpGet("[action]")]
        public List<Users> GetUserDetailsByServerName(string istrUserName, string istrServerName)
        {
            var userList = new List<Users>();
            ServerLogin serverLogin;
            if (UserCache.TryGetValue(istrUserName, out serverLogin))
                if (serverLogin.iblnIsAdmin)
                    userList = applicationDbContext.users.Where(x => x.SERVER_NAME.Equals(istrServerName)).ToList();
            return userList;
        }

        [HttpGet("[action]")]
        public bool CreateDbUser(string istrUserName, string istrNewUserName, string istrPassword,
            string istrSERVER_NAME, string istrCONNECTION_STRING, string iblnIsAdmin)
        {
            var iblnReturn = false;
            ServerLogin serverLogin;
            if (UserCache.TryGetValue(istrUserName, out serverLogin))
            {
                if (serverLogin.iblnIsAdmin)
                {
                    var users = new Users
                    {
                        UserName = istrNewUserName,
                        Password = istrPassword,
                        SERVER_NAME = istrSERVER_NAME,
                        CONNECTION_STRING = istrCONNECTION_STRING,
                        IsAdmin = iblnIsAdmin.Equals("undefined") ? false : true
                    };
                    applicationDbContext.users.Add(users);
                    applicationDbContext.SaveChanges();
                }

                iblnReturn = true;
            }

            return iblnReturn;
        }

        [HttpGet("[action]")]
        public bool UpdateDbUser(string istrUserName, string id, string istrDbUserName, string istrPassword,
            string istrSERVER_NAME, string istrCONNECTION_STRING, string iblnIsAdmin)
        {
            var iblnResult = false;
            ServerLogin serverLogin;
            if (UserCache.TryGetValue(istrUserName, out serverLogin))
                if (serverLogin.iblnIsAdmin)
                {
                    var lUser = applicationDbContext.users.Where(x => x.Id.ToString().Equals(id)).FirstOrDefault();
                    if (lUser.IsNotNull())
                    {
                        lUser.UserName = istrDbUserName;
                        lUser.Password = istrPassword;
                        lUser.SERVER_NAME = istrSERVER_NAME;
                        lUser.CONNECTION_STRING = istrCONNECTION_STRING;
                        lUser.IsAdmin = iblnIsAdmin.Equals("undefined") ? false : true;
                    }

                    applicationDbContext.SaveChanges();
                }

            return iblnResult;
        }

        [HttpGet("[action]")]
        public bool DeleteDbUser(string istrUserName, int id)
        {
            var iblnResult = false;
            ServerLogin serverLogin;
            if (UserCache.TryGetValue(istrUserName, out serverLogin))
                if (serverLogin.iblnIsAdmin)
                {
                    var lUser = applicationDbContext.users.Where(x => x.Id == id).FirstOrDefault();
                    if (lUser.IsNotNull())
                    {
                        applicationDbContext.users.Remove(lUser);
                        applicationDbContext.SaveChanges();
                    }
                }

            return iblnResult;
        }
    }
}