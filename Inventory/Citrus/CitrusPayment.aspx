<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CitrusPayment.aspx.cs" Inherits="Inventory.Citrus.CitrusPayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/reset.css">

    <link rel='stylesheet prefetch' href="../Styles/style1.css">

    <link rel="stylesheet" href="css/style.css">
    <script src="http://code.jquery.com/jquery-1.11.1.min.js"> </script>
    <script src="https://icp.citruspay.com/js/citrus.js"> </script>
    <script src="https://icp.citruspay.com/js/jquery.payment.min.js"> </script>


    <script type="text/javascript">
        CitrusPay.Merchant.Config = {
            // Merchant details
            Merchant: {
                accessKey: '9Y10BK9T7YG2ZFOE8BEE', //Replace with your access key
                vanityUrl: 'CitrusPayment'  //Replace with your vanity URL
            }
        };
    </script>
    <script type="text/javascript">

        fetchPaymentOptions();

        function handleCitrusPaymentOptions(citrusPaymentOptions) {
            if (citrusPaymentOptions.netBanking != null)
                for (i = 0; i < citrusPaymentOptions.netBanking.length; i++) {
                    var obj = document.getElementById("citrusAvailableOptions");
                    var option = document.createElement("option");
                    option.text = citrusPaymentOptions.netBanking[i].bankName;
                    option.value = citrusPaymentOptions.netBanking[i].issuerCode;
                    obj.add(option);
                }
        }
    </script>


    <script type="text/javascript">

        function citrusServerErrorMsg(errorResponse) {
            alert(errorResponse);
            console.log(errorResponse);
        }
        function citrusClientErrMsg(errorResponse) {
            alert(errorResponse);
            console.log(errorResponse);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <%
            string secret_key = "da9447b01138a13d533a44e32a8b2997df018aae";

            //Need to change with your Access Key
            string access_key = "9Y10BK9T7YG2ZFOE8BEE";

            //Should be unique for every transaction
            string txnID = System.DateTime.Now.ToString("yyyyMMddHHmmssffff");

            //Need to change with your Order Amount
            string amount = "1.00";

            string PayId = "s41";

            string Type = "m";
            //Need to change with your Return URL

            string returnURL = "http://adingen-demo.cloudapp.net/inventory/Citrus/ResponsePage?type=" + Type + "&PayId=" + PayId;
            //Need to change with your Notify URL
            string notifyUrl = "http://www.google.com";

            ///Pay Id////


            string data = "merchantAccessKey=" + access_key + "&transactionId=" + txnID + "&amount=" + amount;
            System.Security.Cryptography.HMACSHA1 myhmacsha1 = new System.Security.Cryptography.HMACSHA1(Encoding.ASCII.GetBytes(secret_key));
            System.IO.MemoryStream stream = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(data));
            string securitySignature = BitConverter.ToString(myhmacsha1.ComputeHash(stream)).Replace("-", "").ToLower();

            Response.Write(securitySignature);

        %>

        <%-- -----------------comment---------------------------------%>

        <div class="input-control text" style="background-color: #CCCCFF; width: 100%; height: 100%;">
            <asp:Label ID="Label2" runat="server" BackColor="#003399" BorderStyle="Outset" Width="100%" Font-Bold="True" ForeColor="White">Personal Details</asp:Label>
            <br />
            <br />
            <center>
        <table>
            <tr>
                <td style="width:50%">
        <asp:Label ID="lblname" runat="server" Text="First Name" Width="100px"></asp:Label>
        <input  id="citrusFirstName" class="round" value="Varun" style="width: 181px" type="text" />
                    <br />
        <asp:Label ID="Label4" runat="server" Text="Email" Width="100px"></asp:Label>
        <span class="mif-user prepend-icon"></span>
       <input type="text" id="citrusEmail" class="round" value="jaydhoundiyal21@gmail.com" style="width: 181px" />
       
                    <br />
                     <asp:Label ID="Label9" runat="server" Text="Amount" Width="100px"></asp:Label>
        <span class="mif-user prepend-icon"></span>
         <input style="width: 181px" type="text" class="round" readonly id="citrusAmount" value="<%=amount%>" />
                    <br />
                </td>
                <td style="width:50%">

                    <asp:Label ID="Label7" runat="server" Text="Last Name " Width="100px"></asp:Label>
                    <input style="width: 181px" type="text" class="round" id="citrusLastName" value="Kumar"/><br />
                    <asp:Label ID="Label8" runat="server" Text="Mobile" Width="100px"></asp:Label>
                    <input style="width: 181px" type="text" class="round" id="citrusMobile" value="9999461080" /><br />
                     <asp:Label ID="Label14" runat="server" class="round" Text="Txn Id" Width="100px"></asp:Label>
                     <input type="text" style="width: 181px" class="round"  readonly id="citrusMerchantTxnId" value="<%=txnID%>" />

                </td>
            </tr>
           
        
        
        
        

            <tr>
                <td>
                 
       <span class="mif-user prepend-icon"> </span>
      <input style="width: 181px" type="hidden" readonly id="citrusSignature" class="round" value="<%= securitySignature %>" /><br />
       <input style="width: 181px" type="hidden" readonly id="citrusReturnUrl" class="round" value="<%= returnURL %>" /><br />
       <span class="mif-user prepend-icon"></span>
       <input style="width: 181px" type="hidden" readonly id="citrusNotifyUrl" class="round" value="<%= notifyUrl %>" />
        
                </td>
                <td>

                </td>
            </tr>

        </table>
            </center>


            <br />




            <br />
        </div>
        <div class="input-control text" style="background-color: #CCCCFF; width: 100%; height: 100%;">

            <asp:Label ID="Label1" runat="server" BackColor="#003399" BorderStyle="Outset" Width="100%" Font-Bold="True" ForeColor="White">Payment Option</asp:Label>
            <br />
            <%--            <center>
  <input type="radio" name="gender"  value="male" id="post-format-gallery"> Debit/Credit
  <input type="radio" name="gender"  value="female" id="Netbanking" checked>Net banking<br>
  
       </center>--%>
            <br />
            <table>
                <tr>
                    <%-- <div class="container" style="margin: 50px;">
                        <div class="row">
                            <div class="col-xs-12 col-md-4">
                                <!-- CREDIT CARD FORM STARTS HERE -->
                                <div class="panel panel-default credit-card-box">
                                    <div class="panel-heading display-table">
                                        <div class="row display-tr">
                                            <h3 class="panel-title display-td">Payment Details</h3>
                                            <div class="display-td">
                                                <img class="img-responsive pull-right" src="http://i76.imgup.net/accepted_c22e0.png">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <form role="form" id="payment-form">
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        
                                                            <select id="citrusCardType" class="round">
                                                                <option selected="selected" value="credit">Credit</option>
                                                                <option value="debit">Debit</option>
                                                            </select>
                                                         <select id="citrusScheme" class="round">
                            <option selected="selected" value="VISA">VISA</option>
                            <option value="mastercard">MASTER</option>
                        </select>

                                                        <label for="cardNumber">CARD NUMBER</label>
                                                        <div class="input-group">
                                                            <input
                                                               type="text"
                                                                id="citrusNumber"
                                                                class="form-control"
                                                                name="cardNumber"
                                                                placeholder="Valid Card Number"
                                                                autocomplete="cc-number"
                                                                required autofocus />
                                                            <span class="input-group-addon"><i class="fa fa-credit-card"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-7 col-md-7">
                                                    <div class="form-group">
                                                        <label for="cardExpiry"><span class="hidden-xs">EXPIRATION</span><span class="visible-xs-inline">EXP</span> DATE</label>
                                                        <input
                                                            type="text"
                                                            id="citrusExpiry"
                                                            class="form-control"
                                                            name="cardExpiry"
                                                            placeholder="MM / YY"
                                                            autocomplete="cc-exp"
                                                            required />
                                                    </div>
                                                </div>
                                                <div class="col-xs-5 col-md-5 pull-right">
                                                    <div class="form-group">
                                                        <label for="cardCVC">CV CODE</label>
                                                        <input
                                                           type="text"
                                                            id="citrusCvv"
                                                            class="form-control"
                                                            name="cardCVC"
                                                            placeholder="CVC"
                                                            autocomplete="cc-csc"
                                                            required />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class="form-group">
                                                        <label for="couponCode">Card Holder</label>
                                                        <input type="text" id="citrusCardHolder" class="form-control" name="couponCode" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <input type="button" value="Pay Now" id="citrusCardPayButton" />
                                                    <button class="btn btn-success btn-lg btn-block" id="citrusCardPayButton" type="submit">Start Subscription</button>
                                                </div>
                                            </div>
                                            <div class="row" style="display: none;">
                                                <div class="col-xs-12">
                                                    <p class="payment-errors"></p>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                              </div>-
                                <!-- CREDIT CARD FORM ENDS HERE -->

                          </div>
                        </div>
                    </div>--%>
                    <td style="width: 70%">
                        <br />
                        <asp:Label ID="Label5" runat="server" Text="Card payment" Width="100px"></asp:Label>
                        <span class="mif-user prepend-icon"></span>
                        <select id="citrusCardType" class="round">
                            <option selected="selected" value="credit">Credit</option>
                            <option value="debit">Debit</option>
                        </select>
                        <select id="citrusScheme" class="round">
                            <option selected="selected" value="VISA">VISA</option>
                            <option value="mastercard">MASTER</option>
                        </select>
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/card.png" />
                        <br />
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="Card Number*" Width="200px"></asp:Label>
                        <input type="text" id="citrusNumber" class="round" style="width: 181px" value="" />

                        <br />
                        <asp:Label ID="Label10" runat="server" Text="Card holder Name*" Width="200px"></asp:Label>
                        <input type="text" id="citrusCardHolder" class="round" style="width: 181px" value="" />

                        <br />
                        <asp:Label ID="Label11" runat="server" Text="Expiry*" Width="200px"></asp:Label>
                        <input type="text" id="citrusExpiry" class="round" style="width: 181px" value="" />

                        <br />
                        <asp:Label ID="Label12" runat="server" Text="Cvv*" Width="200px"></asp:Label>
                        <input type="text" id="citrusCvv" class="round" style="width: 181px" value="" />

                        <br />
                        <p></p>
                        <input type="button" value="Pay Now" id="citrusCardPayButton" />
                        <br />
                    </td>
                    <td>

                        <label>Net Banking</label>
                        <br />
                        <select id="citrusAvailableOptions" class="round">
                        </select>
                        <input type="button" value="Pay by netbanking" id="citrusNetbankingButton" />
                    </td>

                </tr>


            </table>

            <br />

        </div>
        <div class="input-control text" style="background-color: #CCCCFF; width: 100%; height: 100%;">
            </center>
       <asp:Label ID="Label3" runat="server" BackColor="#003399" BorderStyle="Outset" Width="100%" Font-Bold="True" ForeColor="White">Card holder Details</asp:Label>
            <br />
            <br />
            <center>
              <table>
                  <tr>
                      <td>  
         <asp:Label ID="Label15" runat="server" Text="Street1" Width="100px"></asp:Label>
         <span class="mif-user prepend-icon"></span>
        <input type="text" class="round" id="citrusStreet1" style="width: 181px">
                      </td>
                      <td>
<asp:Label ID="Label16" runat="server" Text="Street2" Width="100px"></asp:Label>
        <input type="text" class="round" id="citrusStreet2" style="width: 181px">

                      </td>

                  </tr>
                   <tr>
                      <td>  
         <asp:Label ID="Label17" runat="server" Text="City" Width="100px"></asp:Label>
         <span class="mif-user prepend-icon"></span>
        <input type="text" class="round" id="citrusCity" style="width: 181px">
                      </td>
                      <td>
        <asp:Label ID="Label18" runat="server" Text="State" Width="100px"></asp:Label>
        <input type="text" class="round" id="citrusState" style="width: 181px">

                      </td>

                  </tr>
                     <tr>
                      <td>  
         <asp:Label ID="Label19" runat="server" Text="Country" Width="100px"></asp:Label>
         <span class="mif-user prepend-icon"></span>
        <input type="text" id="citrusCountry" class="round" style="width: 181px">
                      </td>
                      <td>
        <asp:Label ID="Label20" runat="server" Text="zip" Width="100px"></asp:Label>
        <input type="text" id="citrusZip" class="round" style="width: 181px">

                      </td>

                  </tr>

              </table>
              </center>


            <br />
            <br />
            <br />
        </div>
        <style>
            .round {
                border-radius: 5px;
                height: 25px;
                width: 80px;
            }
        </style>

        <%-- <input type="hidden" id="citrusFirstName" value="Varun" />
        <input type="hidden" id="citrusLastName" value="Kumar" />
        <input type="text" id="citrusEmail" value="jaydhoundiyal21@gmail.com" />
        <input type="hidden" id="citrusStreet1" value="" />
        <input type="hidden" id="citrusStreet2" value="" />
        <input type="hidden" id="citrusCity" value="" />
        <input type="hidden" id="citrusState" value="" />
        <input type="hidden" id="citrusCountry" value="" />
        <input type="hidden" id="citrusZip" value="" />
        <input type="text" id="citrusMobile" value="9999461080" />
        <br />

        <input type="text" readonly id="citrusAmount" value="<%=amount%>" />
        <input type="text" readonly id="citrusMerchantTxnId" value="<%=txnID%>" />
        <input type="hidden" readonly id="citrusSignature" value="<%= securitySignature %>" />
        <input type="hidden" readonly id="citrusReturnUrl" value="<%= returnURL %>" />
        <input type="hidden" readonly id="citrusNotifyUrl" value="<%= notifyUrl %>" />

        <select id="citrusCardType">
            <option selected="selected" value="credit">Credit</option>
            <option value="debit">Debit</option>
        </select>
        <select id="citrusScheme">
            <option selected="selected" value="VISA">VISA</option>
            <option value="mastercard">MASTER</option>
        </select>
        <input type="text" id="citrusNumber" value="" />
        <input type="text" id="citrusCardHolder" value="" />
        <input type="text" id="citrusExpiry" value="" />
        <input type="text" id="citrusCvv" value="" />
        <input type="button" value="Pay Now" id="citrusCardPayButton" />
        <br />
      <select id="citrusAvailableOptions">
    </select>
    <input type="button" value="Pay by netbanking" id="citrusNetbankingButton" />

        <%------------Saved cards-----------------%>
        <%--<input id="citrusEmail" type="text" value="xyz.xyz@xyz.com" /> <br/> //Replace with consumer Email Id registered with citrus
<inpu<%--t id="citrusPassword" type="password" value="password" /> <br/> //Replace with consumer Password registered with citrus
<ul id="walletData"></ul> <br/>
            
<input type="button" class="PayFromWallet" value="Proceed to Payment"/>--%>
    </form>
    <%-- <script src="js/index.js"></script>--%>

    <script type="text/javascript">
        //Net Banking
        $('#citrusNetbankingButton').on("click", function () { makePayment("netbanking") });
        //Card Payment
        $("#citrusCardPayButton").on("click", function () { makePayment("card") });
    </script>
    <%-- <script>
    $(function() {
        $('input[name=gender]').on('click', function () {
            
            $('#gallery-box').toggle($('#post-format-gallery').prop('checked'));
            $('#gallery-box').toggle($('Netbanking').prop('checked'));
    }).trigger('gender');
    });
        </script>--%>
</body>
</html>
