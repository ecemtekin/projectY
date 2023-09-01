using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectY.Prasentation_Layer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Users kullanicilar = new Users(); //Users.cs ten save_user metodunu çekmek için oluşturduğumuz Users Constructor nesnesini kullanicilar adı ile çağırıyoruz.
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            kullanicilar.Name = TextBox2.Text;
            kullanicilar.save_user(kullanicilar.Name, TextBox1.Text ); 
        }

        protected void ButtonSignIn_Click(object sender, EventArgs e)
        {
            
        }
    }
}