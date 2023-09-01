using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;

namespace projectY
{
    public class DALBase2
    {
        #region Properties
        //bu kısımda connecttion stringimi tanımlayark diğer 
        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        // bu bir açıklamadır.
        #endregion

        #region Constructor

        public DALBase2()
        {
            ConnectionString = "Data Source=DESKTOP-E6MC5SR\\SQL2023;Initial Catalog=projectY;Integrated Security=True";

        }
        public DALBase2(string constring)
        {
            _connectionString = constring;
        }


        #endregion
        #region Helper Methods

        protected int ExecuteNonQuery(params object[] prms) 
        {
            // hemen altdaki kod iki önceki methodun ismini kayıt eder.
            MethodInfo info = SQLHelper.GetCallerMethod();                                             
            return SQLHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, info.Name, SQLParameterGenerator.GenerateParam(info, prms)); //info = void save_user 
        }
        // SQLParameterGenerator.GenerateParam(info, prms) bu yapı değiskenlein başına @ işareti getirerek stored procedure de kullanımına hazırlar.  
        
        protected int Matching (params object[] prms)
        {
            MethodInfo info = SQLHelper.GetCallerMethod();
            return SQLHelper.

        }

        #endregion
    }
}