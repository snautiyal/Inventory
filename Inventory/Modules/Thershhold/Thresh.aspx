<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Thresh.aspx.cs" Inherits="Inventory.Modules.Thershhold.Thresh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />

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
                        OnPageIndexChanging="gvItemDetails_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="PrductID" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductId") %>'></asp:Label>
                                </ItemTemplate>
                                <%-- <EditItemTemplate>           
                            <asp:Label ID="lblEditEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:Label>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmpID" runat="server" ></asp:TextBox>
                        </FooterTemplate>--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditProduct" CssClass="Edit" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtEditItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditProduct" CssClass="Edit" runat="server" Enabled="false" Width="100%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtEditItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:TextBox>--%>
                                </EditItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Leftout quantity" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditCount" runat="server" CssClass="Edit" Text='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:TextBox>
                                </EditItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Thresh" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblThresh" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ThreshHold") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditThresh" runat="server" CssClass="Edit" Text='<%#DataBinder.Eval(Container.DataItem, "ThreshHold") %>'></asp:TextBox>
                                </EditItemTemplate>

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

                        </Columns>

                        <FooterStyle BackColor="green" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <center><b><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></b></center>
        </table>
    </div>
    <br />

    <style type="text/css">
        .txtbox {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
</asp:Content>
