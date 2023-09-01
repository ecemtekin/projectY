using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;

namespace projectY //eksik yol ama çalışıyor :) // çağların notu : bu clası kloasör altına eklediğimiz için namespace projectY.DAtabase_Acces_Layer_Base oloarak gözüküyordu ancak biz son kısmı silerek projectY olarak bıraktık bu da bu clasın diğer claslar tarafından bulunması içindi.
{
    public sealed class SQLHelper
    {

        
        #region SqlHelper
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            // bir komut oluştur ve yürütmeye hazırla
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (commandParameters != null)
                foreach (SqlParameter p in commandParameters)
                    cmd.Parameters.Add(p);

            try
            {
                int result = cmd.ExecuteNonQuery(); // ! burası önemli 
                cmd.Parameters.Clear();
                ConnectionClose(connection);
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Number + ex.Message);
            }

        }
          
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parms) //connectionstring = -benim db adresim- , commandtext = save_user , commandtype = storedprocedure(4) , sqlparameter = dizide 2 parametre tutuluyor.
        {
            // burada sql sorgusau açılır ve usin deiğimye işi bitince kapatılır
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cn.Open();

                int ID = ExecuteNonQuery(cn, commandType, commandText, parms);
                ConnectionClose(cn);
                return ID;
            }
        }

       
        public static void ConnectionClose(SqlConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {

            }
        }
        #endregion



        //bu yapı sayesinde bütün bu kolaylıklar oluyor bu yapı GetFrame olarak belirlediğimiz yapı içerisinde ki sayı kadar meod geri giderak dahadoğrus geri gitmiyor tazılan kodlardan (tutulan geçmis datasından) bu methodun verilerine method info sayesinde alır.
        public static MethodInfo GetCallerMethod() //GetCallerMethod bir MethodInfo classına koyduğumuz isim yani biz yazdık  // MethodInfo >> Bir metodun niteliklerini keşfeder ve metod meta verilerine erişim sağlar.
        {
            var trace = new StackTrace(); // buraya gelinceye kadarki metotların içindeki veriler bunun içinde tutulacak. ctrl+z ctrl+y nasıl ki ileri gerinin bilgisini getiriyorsa bu bilgi stack trace ile tutulur.
            var vMethod = (MethodInfo)trace.GetFrame(2).GetMethod();  //bu satırda veri tipi çevirmesi yapılıyor.Geri dönüş tipi >> MethodInfo >> veri tipi, metodun özelliklerini göstrilmesini sağlar.
            return vMethod;  //bundan sonrasında DALBase sınıfındaki ExecuteNonQuery methoduna(fonksiyon) geri dönerek vMethod u info (MethodInfo değişken türündeki) değişkenine atar.
        }


        //Çağlar'a Sorulacaklar
        //Stack Trace 
        // anladığım şey şu >> GetCallerMethod() metoduna, 2 metod önceki metodun içine düşen veriler MethodInfo metodu sayesinde çekiliyor.
        // Fakat 'iki metod öncesi' ifadesi kafamda canlanmıyor iki metod öncesi hangi metod buna ne karar veriyor?

    }




    //bu metod içerisinde parametre üretilir, bu kod bloğu sayesinde sql ile işimiz kısmen biraz daha  azaltılmış olur.
    public class SQLParameterGenerator
    {
        public static SqlParameter[] GenerateParam(MethodInfo Method, params object[] Caglar) // MethodInfo Method = burada prosedürün değişken isimlerini aldım bunu "StackTrace" yardımıyla yaptık.
                                                                                              // params object[] Caglar= burada ise tarayıcımızdan girdiğimiz email ve password verilerini aldık.
        {
            SqlParameter[] sqlParam = new SqlParameter[Caglar.Length];
            // Generate command parameters
            int paramIndex = 0;
            ParameterInfo[] methodParameters = Method.GetParameters();  //burayı Çağlar'a sor
            foreach (ParameterInfo paramInfo in methodParameters) //paramInfo içerisi sırayla email ve password olarak geliyor.
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@" + paramInfo.Name; //paramInfo.Name = parametrenin adı buraya düşer. >> "@" + paramInfo.Name = @email ve @password (çıktılar)
                parameter.Value = Caglar[paramIndex];   //parameter.value = ecemtekim06@gmail.com(paramindex = 0) ve 454(şifrenin kendisi)(paramindex = 1)
                sqlParam[paramIndex] = parameter;  //parameter = @email(paramindex = 0) ve @password(paramindex = 1)
                paramIndex++;
            }
            return sqlParam;
        }
    }

}

