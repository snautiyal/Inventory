using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.Citrus
{
    public partial class CitrusPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Need to change with your Secret Key
            //string secret_key = "da9447b01138a13d533a44e32a8b2997df018aae";

            ////Need to change with your Access Key
            //string access_key = "9Y10BK9T7YG2ZFOE8BEE";

            ////Should be unique for every transaction
            //string txn_id = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");

            ////Need to change with your Order Amount
            //string amount = "1.00";

            ////Need to change with your Return URL
            //string returnURL = "http://adingen-demo.cloudapp.net/inventory/Citrus/ResponsePage";
            ////Need to change with your Notify URL
            //string notifyUrl = "http://adingen-demo.cloudapp.net/inventory/Citrus/notifyResponsePage.aspx";

            //string data = "merchantAccessKey=" + access_key + "&transactionId=" + txn_id + "&amount=" + amount;
            //System.Security.Cryptography.HMACSHA1 myhmacsha1 = new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(secret_key));
            //System.IO.MemoryStream stream = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(data));
            //string securitySignature = BitConverter.ToString(myhmacsha1.ComputeHash(stream)).Replace("-", "").ToLower();

            //Response.Write(securitySignature);
        }
    }
}