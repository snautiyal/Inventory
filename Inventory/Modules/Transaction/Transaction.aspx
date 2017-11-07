<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction.aspx.cs" Inherits="Inventory.Transaction" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Js/metro.js"></script>
    <div style="width: 850px">
    Product <br />
    <asp:DropDownList ID="ddlAddProduct" runat="server" Width="200" Height="33px"></asp:DropDownList>
     Date From
    <div class="input-control text" id="datepicker" style="width: 155px">
    <asp:TextBox ID="tbxfrom" Height="33px" runat="server" OnTextChanged="tbxfrom_TextChanged" AutoPostBack="true"></asp:TextBox>
    <button class="button"><span class="mif-calendar"></span></button>
</div>
     To Date
    <div class="input-control text" id="datepicker1">
    <asp:TextBox ID="tbxto" Height="33px" runat="server"  OnTextChanged="tbxto_TextChanged" AutoPostBack="true"></asp:TextBox>
    <button class="button"><span class="mif-calendar"></span></button>
</div>
  <asp:Button ID="btnsearch" runat="server" Text="Search.." BackColor="#669900" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="35px" Width="94px" OnClick="btnsearch_Click"  />
        <center><asp:Label ID="lblrecord" runat="server"  Visible="false"></asp:Label></center>
           </div>        
     <div>
                     <table style="width:100%">
                         <asp:Label ID="lblmsgs" runat="server" Visible="False" ForeColor="#FF3300"  Font-Size="X-Large"></asp:Label>   
           <%-- <tr>
                <td colspan="2"><b>Transaction</b></td>
            </tr>  --%>       
            <tr>
            <td colspan="2">
            <asp:GridView ID="gvTransactionDetails" CssClass="Grid" runat="server" Width="100%"
                        AutoGenerateColumns="false" ShowFooter="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        PageSize="8" AllowPaging="true"
                     OnPageIndexChanging="gvTransactionDetails_PageIndexChanging"
                    onrowcommand="gvTransactionDetails_RowCommand"
                    onrowdeleting="gvTransactionDetails_RowDeleting"
                    onrowupdating="gvTransactionDetails_RowUpdating"
                    onrowcancelingedit="gvTransactionDetails_RowCancelingEdit"
                    onrowediting="gvTransactionDetails_RowEditing"
                    OnRowDataBound="gvTransactionDetails_RowDataBound"
                     DataKeyNames="TransactionID,ProductId"> 
                     
                <Columns>           
                    <asp:TemplateField HeaderText="Transaction Id" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionId" runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "TransactionId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="Product" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product.ProductName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate> 
                            <asp:DropDownList ID="ddlEditProduct" CssClass="txtbox"  runat="server"  Width="100%"></asp:DropDownList>                
                            <%--<asp:TextBox ID="txtEditItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:TextBox>           --%>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="Comments" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblCommentsId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>    
                                   <asp:TextBox ID="txtEditComments" Width="100%" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:TextBox>           
                        </EditItemTemplate>
                    </asp:TemplateField>--%>

                      <asp:TemplateField HeaderText="Stock In Price" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblStockInPrice" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StockInPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtStockInPrice" Width="100%" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StockInPrice") %>'></asp:TextBox>           
                        </EditItemTemplate>
                   
                    </asp:TemplateField>
     
                     <asp:TemplateField HeaderText="Price" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Price") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtPrice" Width="100%" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Price") %>'></asp:TextBox>           
                        </EditItemTemplate>
                   
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblCountId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>  
                            <%--<asp:DropDownList ID="ddlEditCount" runat="server" DataTextField='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:DropDownList>         --%>
                            <asp:TextBox ID="txtEditCount" runat="server" CssClass="txtbox" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:TextBox>           
                        </EditItemTemplate>
                    </asp:TemplateField>



 
                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditTotalAmount" Width="100%" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:TextBox>           
                        </EditItemTemplate>
                   
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Date of Issue" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblCreationtime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreationTime") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>  
                            <%--<asp:DropDownList ID="ddlEditCount" runat="server" DataTextField='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:DropDownList>         --%>
                            <asp:TextBox ID="txtEditCreation" CssClass="txtbox" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreationTime") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        
                    </asp:TemplateField>     
                     <asp:TemplateField HeaderText="Action" Visible="false">
                                <ItemTemplate>
                                    <asp:Button ID="imgbtnEdit" CssClass="txtbox Editbutton" runat="server"  CommandName="Edit" Width="40%" Text="Edit" Font-Bold="true" />
                                    <asp:Button ID="imgbtnDelete" CssClass="txtbox Deletebutton" runat="server" CommandName="Delete" Width="50%" Text="Delete" Font-Bold="true"/>
                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="40%" Width="40%" />--%>
                                    <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/del3.png" Height="40%"  Width="40%" />--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:Button ID="imgbtnUpdate" CssClass="txtbox Updatebutton" runat="server" CommandName="Update" Width="40%" Text="Update" Font-Bold="true"/>
                                    <asp:Button ID="imgbtnCancel" CssClass="txtbox Cancelbutton" runat="server" CommandName="Cancel" Width="50%" Text="Cancel" Font-Bold="true"/>
                                    <%--<asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png" Height="40%" Width="20%" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/cancel.jpg" Height="40%" Width="20%" />--%>
                                </EditItemTemplate>
                                
                            </asp:TemplateField>                  
                </Columns>  
                <FooterStyle BackColor="green" Font-Bold="True" ForeColor="White" />         
            </asp:GridView> 
            </td>
            </tr>
           <center><b><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></b></center>
        </table>
                        </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="View Report" BackColor="#669900" ForeColor="White"  OnClick="Button1_Click" Visible="false" />
       <div> <asp:ImageButton ID="Print" ImageAlign="Right" runat="server" ImageUrl="~/Images/print.png" OnClientClick="Print()" Visible="false" /></div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"></rsweb:ReportViewer>
    </div>
    <br />
    
   <%-- <asp:Button ID="btnPrint" runat="server" Text="Print" Width="20px" BackColor="#669900" ForeColor="White" OnClientClick="Print()" />--%>
    <%--<input type="button"  id="btnPrint"  value="Print" onclick="Print()" />--%>
 <script>
     $(function () {
         $("#datepicker").datepicker();
     });
</script>
     <script>
         $(function () {
             $("#datepicker1").datepicker();
         });
</script>

    <style type="text/css">
        .txtbox
        {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
    <script>
     function checkDate() {
         var EnteredDate = document.getElementById("tbxto").value; //for javascript

            var EnteredDate = $("#tbxto").val(); // For JQuery

            var date = EnteredDate.substring(0, 2);
            var month = EnteredDate.substring(3, 5);
            var year = EnteredDate.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var secondate = new Date(tbxfrom);

            if (myDate > today) {
                alert("Entered date is greater than today's date ");
            }
            else {
                alert("Entered date is less than today's date ");
            }
        }

</script>
    <script type="text/javascript">
        function Print() {
            var report = document.getElementById("<%=ReportViewer1.ClientID %>");
    var div = report.getElementsByTagName("DIV");
    var reportContents;
    for (var i = 0; i < div.length; i++) {
        if (div[i].id.indexOf("VisibleReportContent") != -1) {
            reportContents = div[i].innerHTML;
            break;
        }
    }
    var frame1 = document.createElement('iframe');
    frame1.name = "frame1";
    frame1.style.position = "absolute";
    frame1.style.top = "-1000000px";
    document.body.appendChild(frame1);
    var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
    frameDoc.document.open();
    frameDoc.document.write('<html><head><title>RDLC Report</title>');
    frameDoc.document.write('</head><body style = "font-family:arial;font-size:10pt;">');
    frameDoc.document.write(reportContents);
    frameDoc.document.write('</body></html>');
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
        document.body.removeChild(frame1);
    }, 500);
}
</script>
</asp:Content>
