using MSSQL.DIARY.COMN.Cache;
using MSSQL.DIARY.COMN.Models;
using MSSQL.DIARY.EF;
using System;
using System.Collections.Generic;

namespace MSSQL.DIARY.SRV
{
    public class SrvDatabaseLogin
    {
        public static NaiveCache<ServerLogin> LoginCache = new NaiveCache<ServerLogin>();
        public static List<ServerLogin> LoginsCache = new List<ServerLogin>();

        public bool IsLoginSuccessfully(ServerLogin serverLogin)
        {
            if (serverLogin.istrDatabaseName == "undefined" || serverLogin.istrPassword == "undefined" ||
                serverLogin.istrUserName == "undefined" || serverLogin.istrServerName == "undefined")
            {
                MssqlDiaryContext.IsAlreadyLogin = false;
                return false;
            }

            MssqlDiaryContext.LoginDetails = serverLogin;
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                if (!IsAlreadyLoggedIn(serverLogin) && dbSqldocContext.IsLoginSuccessfully(serverLogin))
                {
                    try
                    {
                        LoginCache.Cache.Remove(serverLogin.istrServerName + serverLogin.istrDatabaseName +
                                                 serverLogin.istrUserName + serverLogin.istrPassword +
                                                 serverLogin.iblnIsLogin);
                    }
                    catch (Exception)
                    {
                    }

                    serverLogin.iblnIsLogin = true;
                    LoginCache.GetOrCreate(
                        serverLogin.istrServerName + serverLogin.istrDatabaseName + serverLogin.istrUserName +
                        serverLogin.istrPassword + serverLogin.iblnIsLogin, () => LoginSuccessfully(serverLogin));

                    MssqlDiaryContext.IsAlreadyLogin = true;
                }
            }

            return MssqlDiaryContext.IsAlreadyLogin;
        }

        public bool IsLogOutSuccessfully()
        {
            using (MssqlDiaryContext dbSqldocContext = new MssqlDiaryContext())
            {
                MssqlDiaryContext.LoginDetails = new ServerLogin();
                MssqlDiaryContext.IsAlreadyLogin = false;
            }

            return true;
        }

        public bool IsAlreadyLoggedIn(ServerLogin serverLogin)
        {
            ServerLogin output = new ServerLogin();
            LoginCache.Cache.TryGetValue(serverLogin.istrServerName + serverLogin.istrDatabaseName +
                                          serverLogin.istrUserName +
                                          serverLogin.istrPassword + serverLogin.iblnIsLogin, out output);
            if (output != null && output.iblnIsLogin)
            {
                return MssqlDiaryContext.IsAlreadyLogin;
            }

            return false;
        }

        private ServerLogin LoginSuccessfully(ServerLogin serverLogin)
        {
            return serverLogin;
        }
    }
}