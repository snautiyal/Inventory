<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Report.aspx.cs" Inherits="Inventory.HMBS.Report" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <script src="../../Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../Scripts/Js/metro.js"></script>
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

        
             <div class="input-control text" id="datepicker" style="width: 155px">
            <asp:TextBox ID="tbxfrom" Height="33px" runat="server"></asp:TextBox>
            <button class="button"><span class="mif-calendar"></span></button>
        </div>

        <div class="input-control text" id="datepicker1" style="width: 155px">
            <asp:TextBox ID="tbxto" Height="33px" runat="server"></asp:TextBox>
            <button class="button"><span class="mif-calendar"></span></button>
        </div>

       
       
    <div>
        <asp:DropDownList ID="ddlinventory" runat="server" Width="200px" Height="25px"></asp:DropDownList>
        <asp:Button ID="fetchreport" runat="server" Text="Report" OnClick="fetchreport_Click" />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="655px"></rsweb:ReportViewer>
    </div>
        
    </asp:Content>