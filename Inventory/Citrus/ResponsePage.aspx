<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponsePage.aspx.cs" Inherits="Inventory.Citrus.response" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta HTTP-EQUIV="Content-Type" CONTENT="text/html;CHARSET=iso-8859-1">
    </head>
    <body>
     <%
        //Replace this with your secret key from the citrus panel
        string secret_key = "da9447b01138a13d533a44e32a8b2997df018aae";
        string data="";
        ///our reqired field/////////
        string Type = Request["Type"];
        string PayId = Request["PayId"];

       
        ///////////////////////////////
        string txnId=Request["TxId"];
        string txnStatus=Request["TxStatus"]; 
        string amount=Request["amount"]; 
        string pgTxnId=Request["pgTxnNo"];
        string issuerRefNo=Request["issuerRefNo"]; 
        string authIdCode=Request["authIdCode"];
        string firstName=Request["firstName"];
        string lastName=Request["lastName"];
        string pgRespCode=Request["pgRespCode"];
        string zipCode=Request["addressZip"];
        string resSignature=Request["signature"];
             
        bool flag = true;
        if (txnId != null) {
            data += txnId;
        }
        if (txnStatus != null) {
            data += txnStatus;
        }
        if (amount != null) {
            data += amount;
        }
        if (pgTxnId != null) {
            data += pgTxnId;
        }
        if (issuerRefNo != null) {
            data += issuerRefNo;
        }
        if (authIdCode != null) {
            data += authIdCode;
        }
        if (firstName != null) {
            data += firstName;
        }
        if (lastName != null) {
            data += lastName;
        }
        if (pgRespCode != null) {
            data += pgRespCode;
        }
        if (zipCode != null) {
            data += zipCode;
        }

        System.Security.Cryptography.HMACSHA1 myhmacsha1=new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(secret_key));
        System.IO.MemoryStream stream = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(data));
        string signature = BitConverter.ToString(myhmacsha1.ComputeHash(stream)).Replace("-", "").ToLower();
             
        if(resSignature !=null && !signature.Equals(resSignature)) {
            flag = false;
        }
        if (flag) {
    %>
        Your Unique Transaction/Order Id : <%=txnId%><br/>
        Transaction Status : <%=txnStatus%><br/>
    <% } else { %>
        <label width="125px;">Citrus Response Signature and Our (Merchant) Signature Mis-Mactch</label>
    <% } %>
    </body>
</html>
    