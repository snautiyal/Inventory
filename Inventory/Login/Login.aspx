<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Login.aspx.cs" Inherits="Inventory.Login.Login" %>

<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1.0" />--%>
    <title><%: Page.Title %>Inventory System</title>
  
    <script></script>
    <asp:PlaceHolder runat="server">

        <%--<%: Scripts.Render("~/bundles/modernizr") %>--%>


    </asp:PlaceHolder>
    <%--<webopt:bundlereference runat="server" path="~/Content/css" />--%>
    <%--<link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <script src="~/js/jquery-1.8.3.js"></script>
    <script src="~/js/jquery.meanmenu.js"></script>
    <link href="~/Styles/responsiveMenu.css" rel="stylesheet" />
    <link href="~/Styles/meanmenu.css" rel="stylesheet" />--%>

    <%--////////////////////Bundle/////////////////////////--%>


    <link href="../Content/Css/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="../Content/Css/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Scripts/Js/jquery.dataTables.min.js"></script>

    <script src="../Scripts/Js/docs.js"></script>
    <script src="../Scripts/Js/ga.js"></script>
    <script src="../Scripts/Js/prettify/run_prettify.js"></script>

    <script src="../Scripts/Js/metro.js"></script>
    <link href="../Content/Css/Metro/css/media.css" rel="stylesheet" />
    <link href="../Content/Css/Metro/css/metro-icons.css" rel="stylesheet" />
    <link href="../Content/Css/Metro/css/metro-responsive.css" rel="stylesheet" />
    <link href="../Content/Css/Metro/css/metro-schemes.css" rel="stylesheet" />
    <link href="../Content/Css/Metro/css/metro.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 15%;
        }
    </style>

</head>

<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div class="app-bar fixed-top darcula" data-role="appbar" style="background-color: #34af80">
            <a class="app-bar-element branding" href="#">
                <asp:Image ID="Image1" runat="server" />
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/logo.png" Height="80%" class="logo-custom" />
            </a>
            <span class="app-bar-divider"></span>
            <ul class="app-bar-menu">
                <li><a href=""></a></li>
                <li>
                    <a href="" class=""></a>
                    <ul class="d-menu" data-role="">
                        <li><a href=""></a></li>
                        <li class="divider"></li>
                        <li>
                            <a href="" class="">Reopen</a>
                            <ul class="d-menu" data-role="dropdown">
                                <li><a href="">Project 1</a></li>
                                <li><a href="">Project 2</a></li>
                                <li><a href="">Project 3</a></li>
                                <li class="divider"></li>
                                <li><a href="">Clear list</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li><a href=""></a></li>
                <li><a href=""></a></li>
                <li>
                    <a href="" class=""></a>
                    <ul class="d-menu" data-role="">
                        <li><a href="">ChatOn</a></li>
                        <li><a href="">Community support</a></li>
                        <li class="divider"></li>
                        <li><a href="">About</a></li>
                    </ul>
                </li>
            </ul>
            <div class="app-bar-element place-right">
                <span class=""><span class=""></span></span>
                <div class="app-bar-drop-container padding10 place-right no-margin-top block-shadow fg-dark" data-role="dropdown" data-no-close="true" style="width: 220px">
                    <ul class="unstyled-list fg-dark">
                        <li><a href="" class="fg-white1 fg-hover-yellow"><span class="mif-cog">&nbspProfile</span></a></li>
                        <li><a href="" class="fg-white2 fg-hover-yellow"><span class="mif-security">&nbspSecurity</span></a></li>
                        <li><a href="" class="fg-white3 fg-hover-yellow"><span class="mif-exit">&nbspExit</span></a></li>
                    </ul>
                </div>
            </div>
        </div>
      <%--  <div class="page-content" style="width: 100%; height: 100%">
            <div class="flex-grid no-responsive-future" style="height: 100%; width: 100%;">
                <div class="row" style="height: auto; width: 100%; background-color: #34af80;">
                    <div class="cell size-x200" id="cell-sidebar" style="background-color: #34af80; height: auto; width: 20%">
                        <ul class="sidebar" visible="false">
                            <li>
                                <a href="#">
                                    <span class="mif-search icon"></span>
                                    <span class="title">Search</span>
                                    <span class="counter"></span>
                                </a>
                            </li>
                            <%-- <li>
                        <a href="#">
                            <span class="mif-user-check icon"></span>
                            <span class="title">Check IN</span>
                            <span class="counter"></span>
                        </a>
                    </li>--%>
                           
                    <div class="cell auto-size padding20 bg-white" id="cell-content" style="width: 100%; height: 100%;" >
                        <html>
                        <head>
                            <meta charset="utf-8">
                            <title>Best Login Page design in html and css</title>
                            <style type="text/css">
                                body {
                                    background-color: #f4f4f4;
                                    color: #5a5656;
                                    font-family: 'Open Sans', Arial, Helvetica, sans-serif;
                                    font-size: 16px;
                                    line-height: 1.5em;
                                }

                                a {
                                    text-decoration: none;
                                }

                                h1 {
                                    font-size: 1em;
                                }

                                h1, p {
                                    margin-bottom: 10px;
                                }

                                strong {
                                    font-weight: bold;
                                }

                                .uppercase {
                                    text-transform: uppercase;
                                }

                                /* ---------- LOGIN ---------- */
                                #login {
                                    margin: 50px auto;
                                    width: 300px;
                                }

                                form fieldset input[type="text"], input[type="password"] {
                                    background-color: #e5e5e5;
                                    border: none;
                                    border-radius: 3px;
                                    -moz-border-radius: 3px;
                                    -webkit-border-radius: 3px;
                                    color: #5a5656;
                                    font-family: 'Open Sans', Arial, Helvetica, sans-serif;
                                    font-size: 14px;
                                    height: 50px;
                                    outline: none;
                                    padding: 0px 10px;
                                    width: 280px;
                                    -webkit-appearance: none;
                                }

                                form fieldset input[type="submit"] {
                                    background-color: #189D33;
                                    border: none;
                                    border-radius: 3px;
                                    -moz-border-radius: 3px;
                                    -webkit-border-radius: 3px;
                                    color: #f4f4f4;
                                    cursor: pointer;
                                    font-family: 'Open Sans', Arial, Helvetica, sans-serif;
                                    height: 50px;
                                    text-transform: uppercase;
                                    width: 300px;
                                    -webkit-appearance: none;
                                }

                                form fieldset a {
                                    color: #5a5656;
                                    font-size: 10px;
                                }

                                    form fieldset a:hover {
                                        text-decoration: underline;
                                    }

                                .btn-round {
                                    background-color: #5a5656;
                                    border-radius: 50%;
                                    -moz-border-radius: 50%;
                                    -webkit-border-radius: 50%;
                                    color: #f4f4f4;
                                    display: block;
                                    font-size: 12px;
                                    height: 50px;
                                    line-height: 50px;
                                    margin: 30px 125px;
                                    text-align: center;
                                    text-transform: uppercase;
                                    width: 50px;
                                }

                                

                               
                            </style>
                        </head>
                        <body>
                            <div id="login">
                                <h1><strong>Welcome.</strong> Please login.</h1>
                                <form>
                                    <fieldset>
                                        <label><b>Username</b></label>
                                        <p>
                                            <asp:TextBox ID="tbxuser"  runat="server" ></asp:TextBox>
                                            </p>
                                        <label><b>Password</b></label>
                                        <p>
                                            <asp:TextBox ID="tbxpwd"  runat="server" TextMode="Password" ></asp:TextBox>
                                            </p>
                                        <%--<p><a href="#">Forgot Password?</a></p>--%>
                                        <p>
                                            <asp:Button ID="btnsubmit"  runat="server" Text="Login"  OnClick="btnsubmit_Click" />
                                            </p>
                                    </fieldset>
                                    <asp:Label ID="lblstatus" runat="server" Text="Invalid UserName OR Password" ForeColor="Red" Visible="false"></asp:Label>
                                </form>
                               
                               
                               
                            </div>
                            <!-- end login -->
                        </body>
                        </html>


                    </div>
              
        <hr />

        <footer>
            <%-- ///////////// Footer//////////////////--%>
        </footer>

    </form>

</body>
</html>
