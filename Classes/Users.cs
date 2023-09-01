using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectY
{
    public class Users : DALBase2

    {
        public int sonuc = 0;
        public int sonuc2 = 0;
        #region Properties
        //variables
        private string Vname;
        private string Vpassword;

        //properties >> bunları nerede kullandık? kullanmamışız gibi görünüyor
        public string Name { get { return Vname; } set { Vname = value; } }
        public string Password { get => Vpassword; set => Vpassword = value; }

        #endregion

        #region Constructor
        public Users()
        {

        }
        #endregion


        public void save_user(string email, string password)
        {

            try
            {
                sonuc = ExecuteNonQuery(email, password);
            }
            catch (Exception ex)
            {

            }

            //ecem

        }

        public void sign_in (string email, string password)
        {

            try
            {
                sonuc2 = Matching(email, password);
            }
            catch (Exception ex)
            {

            }
        }
    }
}









        
