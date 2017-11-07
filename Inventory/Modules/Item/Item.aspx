<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item.aspx.cs" Inherits="Inventory.Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" /> 
  
    

    <asp:Button ID="Button1" runat="server" Visible="false" Text="Manage Item" BackColor="#669900" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="33px" Width="104px" OnClick="Button1_Click" />
    <div>

        <%-- <tr>
             <td colspan="2"><b>Item</b></td>
            </tr>  --%>
        <table style="width: 100%">
            <asp:Label ID="lblmsgs" runat="server" Visible="False" ForeColor="#FF3300" Font-Size="X-Large"></asp:Label>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvItemDetails" CssClass="Grid" runat="server" Width="100%"
                        AutoGenerateColumns="false" ShowFooter="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        PageSize="8" AllowPaging="true"
                        OnPageIndexChanging="gvItemDetails_PageIndexChanging"
                        OnRowCommand="gvItemDetails_RowCommand"
                        OnRowDeleting="gvItemDetails_RowDeleting"
                        OnRowUpdating="gvItemDetails_RowUpdating"
                        OnRowCancelingEdit="gvItemDetails_RowCancelingEdit"
                        OnRowEditing="gvItemDetails_RowEditing"
                        OnRowDataBound="gvItemDetails_RowDataBound"
                        DataKeyNames="ProductId,VendorId,BillNo">
                        <Columns>

                            <asp:TemplateField HeaderText="ProductName" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product.ProductName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditProduct" CssClass="Edit" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtEditItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:TextBox>           --%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlAddProduct" CssClass="txtbox" runat="server" Width="100%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtAddItemName" runat="server" ></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="VendorName" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblvendorid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Vendor.VendorName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditvendor" CssClass="Edit" runat="server" Width="100%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtEditItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:TextBox>           --%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlAddvendor" CssClass="txtbox" runat="server" Width="100%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtAddItemName" runat="server" ></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="BillNo." HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Billno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                   <%-- <asp:DropDownList ID="ddlEditBillno" CssClass="Edit" runat="server" Width="100%"></asp:DropDownList>--%>
                                    <asp:TextBox ID="ddlEditBillno" runat="server" CssClass="Edit" Text='<%#DataBinder.Eval(Container.DataItem, "Billno") %>'></asp:TextBox>         
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <%-- <asp:DropDownList ID="ddlAddBillno" CssClass="txtbox" runat="server"  Width="100%" ></asp:DropDownList> --%>
                                    <asp:TextBox ID="ddlAddBillno" CssClass="txtbox" Width="100%" runat="server"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtAddItemName" runat="server" ></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Count" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditCount" runat="server" CssClass="Edit" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddCount" CssClass="txtbox" Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date Of Purchase" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CreationTime") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditCreation" CssClass="Edit" Enabled="false" runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "CreationTime") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxfrom" runat="server" CssClass="txtbox datepicker" Text='<%# DateTime.Now.ToString("MM/dd/yyyy")%>'></asp:TextBox>
                                   
                                </FooterTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Thresh" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblThresh" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ThreshHold") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditThresh" runat="server" CssClass="Edit"  Text='<%#DataBinder.Eval(Container.DataItem, "ThreshHold") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddThresh" CssClass="txtbox" Width="100%" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="StockInPrice">
                        <ItemTemplate>
                            <asp:Label ID="lblStockInPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StockInPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditStockInPrice" runat="server" CssClass="Edit" Text='<%#DataBinder.Eval(Container.DataItem, "StockInPrice") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddStockInPrice" CssClass="txtbox" Width="100%" runat="server" ></asp:TextBox>
                        </FooterTemplate>--%>
                            <%--</asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" >
                                <ItemTemplate>
                                    <asp:Button ID="imgbtnEdit" CssClass="txtbox Editbutton" runat="server" CommandName="Edit" Width="40%" Text="Edit" Font-Bold="true" Visible="false"/>
                                    <asp:Button ID="imgbtnDelete" CssClass="txtbox Deletebutton" runat="server" CommandName="Delete" Width="58%" Text="Delete" Font-Bold="true" Visible="false" />
                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="40%" Width="40%" />--%>
                                    <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/del3.png" Height="40%"  Width="40%" />--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="imgbtnUpdate" CssClass="txtbox Updatebutton" runat="server" CommandName="Update" Width="40%" Text="Update" Font-Bold="true" Visible="false" />
                                    <asp:Button ID="imgbtnCancel" CssClass="txtbox Cancelbutton" runat="server" CommandName="Cancel" Width="58%" Text="Cancel" Font-Bold="true" Visible="false" />
                                    <%--<asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png" Height="40%" Width="20%" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/cancel.jpg" Height="40%" Width="20%" />--%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="lbtnAdd" CssClass="txtbox Addbutton" runat="server" CommandName="ADD" Width="40%" Text="ADD" Font-Bold="true" />
                                    <%--<asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD"  Width="100px"></asp:LinkButton>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="green" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>

                </td>
            </tr>
            <center><b><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></b></center>
        </table>
    </div>
    <br />
  <link rel="stylesheet" href="../../css/jquery-ui.css">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="../../js/jquery-1.12.4.js"></script>
  <script src="../../js/jquery-ui.js"></script>

  <script>
      $(function () {
          $(".datepicker").datepicker();
      });
  </script>
    <style type="text/css">
        .txtbox {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
</asp:Content>
