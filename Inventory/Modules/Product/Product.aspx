<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Inventory.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />
    <asp:Button ID="Button1" runat="server" Visible="false" Text="+ ADD.." BackColor="#669900" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="33px" Width="94px" OnClick="Button1_Click" />

    <div>
        
           <%-- <tr>
                <td colspan="2"><b>Product</b></td>
            </tr>--%>
        <table style="width:100%">
             <asp:Label ID="lblmsgs" runat="server" Visible="False" ForeColor="#FF3300"  Font-Size="X-Large"></asp:Label>   
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvProductDetails" CssClass="Grid" runat="server" Height="100%" Width="100%"
                         AutoGenerateColumns="false" ShowFooter="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        PageSize="8" AllowPaging="true"
                        OnPageIndexChanging="gvProductDetails_PageIndexChanging"
                        OnRowCommand="gvProductDetails_RowCommand"
                        OnRowDeleting="gvProductDetails_RowDeleting"
                        OnRowUpdating="gvProductDetails_RowUpdating"
                        OnRowCancelingEdit="gvProductDetails_RowCancelingEdit"
                        OnRowEditing="gvProductDetails_RowEditing"
                        OnRowDataBound="gvProductDetails_RowDataBound"
                        DataKeyNames="ProductId,CategoryId"
                        >
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductId") %>'></asp:Label>
                                </ItemTemplate>
                                <%-- <EditItemTemplate>           
                            <asp:Label ID="lblEditEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:Label>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmpID" runat="server" ></asp:TextBox>
                        </FooterTemplate>--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Category" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Category.CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditCategory" Width="100%" Enabled="false" runat="server"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtEditCategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CategoryId") %>'></asp:TextBox>           --%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlAddCategory" Width="100%" CssClass="txtbox"  runat="server"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtAddCategory" runat="server" ></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>

                           <%-- <asp:TemplateField HeaderText="Brand" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrandName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Brand.BrandName")%>'></asp:Label>
                                    <%-- <asp:Label ID="ddlBrandId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEditBrand" Width="100%" Enabled="false" CssClass="txtbox" runat="server"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtEditBrand" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:TextBox> 
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlAddBrand" Width="100%" CssClass="txtbox"  runat="server"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtAddBrand" runat="server" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Product" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditProductName" Width="100%" runat="server"  Enabled="false" CssClass="txtbox"  Text='<%#DataBinder.Eval(Container.DataItem, "ProductName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddProductName" Width="100%" CssClass="txtbox" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "ProductDescription") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddDescription" CssClass="txtbox" Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblWeight"  runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "ProductWeight") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditWeight"  CssClass="txtbox" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductWeight") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddWeight" CssClass="txtbox" Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Stock In Price" HeaderStyle-Width="10%" >
                                <ItemTemplate>
                                    <asp:Label ID="lblstockingpricePrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StockInPrice") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtstockinEditPrice"   CssClass="txtbox" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "StockInPrice") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtstockinAddPrice"  CssClass="txtbox" Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Price" HeaderStyle-Width="10%" >
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductPrice") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditPrice"   CssClass="txtbox" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductPrice") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddPrice"  CssClass="txtbox" Width="100%" runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <%--<asp:TemplateField HeaderText="Barcode" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblbarcode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BarCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditbarcode" Width="100%" CssClass="barCodeDedector txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BarCode") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddbarcode" Width="100%" CssClass="txtbox"   runat="server"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Thresh" HeaderStyle-Width="10%">
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

                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="40%">
                                <ItemTemplate>
                                    <asp:Button ID="imgbtnEdit" CssClass="txtbox Editbutton" runat="server"  CommandName="Edit" Width="40%" Text="Edit" Font-Bold="true" />
                                    <asp:Button ID="imgbtnDelete" CssClass="txtbox Deletebutton" runat="server" CommandName="Delete" Width="55%" Text="Delete" Font-Bold="true"/>
                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="40%" Width="40%" />--%>
                                    <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/del3.png" Height="40%"  Width="40%" />--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:Button ID="imgbtnUpdate" CssClass="txtbox Updatebutton" runat="server" CommandName="Update" Width="55%" Text="Update" Font-Bold="true"/>
                                    <asp:Button ID="imgbtnCancel" CssClass="txtbox Cancelbutton" runat="server" CommandName="Cancel" Width="40%" Text="Cancel" Font-Bold="true"/>
                                    <%--<asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png" Height="40%" Width="20%" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/cancel.jpg" Height="40%" Width="20%" />--%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="lbtnAdd" CssClass="txtbox Addbutton" runat="server" CommandName="ADD" Width="45%" Text="ADD" Font-Bold="true" />
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
     <style type="text/css">
        .txtbox
        {
           
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
<%--    <script type="text/javascript">
    function disableDel(delButton) {
        delButton.disabled = true;
    }
</script>--%>
</asp:Content>
