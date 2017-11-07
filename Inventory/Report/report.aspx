<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="report.aspx.cs" Inherits="Inventory.Report.report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../Scripts/Js/metro.js"></script>
    <div>
       <table>
<tr>
<td style="width:200">
    <asp:Label ID="Label1" runat="server" Width="200" ><h4>Report</h4></asp:Label>
</td>
    <td>
        <asp:Label ID="lblCategory" runat="server" Width="200" Visible="false" ><h4>Category</h4></asp:Label>

    </td>
    <td>
        <asp:Label ID="lblproduct" runat="server" Width="200" Visible="false" ><h4>Product</h4></asp:Label>

    </td>
     <td>
        <asp:Label ID="lblfrom" runat="server" Width="200" Visible="false" ><h4>From</h4></asp:Label>

    </td>
      <td>
        <asp:Label ID="lblto" runat="server" Width="200" Visible="false" ><h4>To</h4></asp:Label>

    </td>
</tr>


       </table>
        
       
       <table>
           <tr>
          <td>
        <asp:DropDownList ID="ddlinventory" runat="server"  Width="200" Height="33px" AutoPostBack="true" OnSelectedIndexChanged="ddlinventory_SelectedIndexChanged"></asp:DropDownList>
        <asp:DropDownList ID="ddlcategory" runat="server"  Width="200" Height="33px" AutoPostBack="true" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged" Visible="false" ></asp:DropDownList>
        <asp:DropDownList ID="ddlproduct" runat="server"  Width="200" Height="33px" AutoPostBack="true" OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged" Visible="false" ></asp:DropDownList>
        </td>
<td id="fromdate" runat="server" visible="false">

        <div class="input-control text" id="datepicker"  style="width: 155px">
            <asp:TextBox ID="tbxfrom" Height="33px" runat="server"></asp:TextBox>
            <button class="button"><span class="mif-calendar"></span></button>
        </div>
    </td>
               <td id="todate" runat="server" visible="false">
        <div class="input-control text" id="datepicker1" style="width: 155px">
            <asp:TextBox ID="tbxto" Height="33px" runat="server"></asp:TextBox>
            <button class="button"><span class="mif-calendar"></span></button>
        </div>
                   </td>
               <td style="width:300px">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" BackColor="#006600" Font-Bold="True" ForeColor="White" />
                   </td>
               </tr>
           
           <center><b><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></b></center>
          
           </table>
       
        <div> <asp:ImageButton ID="Print" ImageAlign="Right" runat="server" ImageUrl="~/Images/print.png" OnClientClick="Print()" Visible="false" /></div>

        <%--<rsweb:reportviewer id="ReportViewer1" runat="server" width="1400" height="500"></rsweb:reportviewer>--%>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" width="100%" height="500"></rsweb:ReportViewer>

    </div>

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
            frameDoc.document.write('<html><head><title> Report</title>');
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
