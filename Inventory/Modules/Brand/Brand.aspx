<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brand.aspx.cs" Inherits="Inventory.Brand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />
    <asp:Button ID="Button1" runat="server" Visible="false" Text="+ ADD.." BackColor="#669900" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="33px" Width="94px" OnClick="Button1_Click" />
    <div style="width:100%">
      <asp:Label ID="lblmsgs" runat="server" Visible="False" ForeColor="#FF3300"  Font-Size="X-Large"></asp:Label>
        
            <%--<tr>
                <td colspan="2"><b>Brand</b></td>
            </tr>  --%>
        <table style="width:100%">
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvBrandDetails" CssClass="Grid" runat="server" Width="100%"
                        AutoGenerateColumns="false" ShowFooter="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        PageSize="8" AllowPaging="true"
                        OnPageIndexChanging="gvBrandDetails_PageIndexChanging"
                        OnRowCommand="gvBrandDetails_RowCommand"
                        OnRowDeleting="gvBrandDetails_RowDeleting"
                        OnRowUpdating="gvBrandDetails_RowUpdating"
                        OnRowCancelingEdit="gvBrandDetails_RowCancelingEdit"
                        OnRowDataBound="gvBrandDetails_RowDataBound"
                        OnRowEditing="gvBrandDetails_RowEditing"
                        DataKeyNames="BrandId">
                        <Columns>
                            <asp:TemplateField HeaderText="Brand Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblBrandID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <asp:ListBox ID="lstCategories" runat="server" Height="50px" Width="200px" ></asp:ListBox>
                                    <%--<asp:Label ID="lblCategoryId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Category.CategoryName") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                   <%-- <asp:DropDownList ID="ddlEditCategory" CssClass="txtbox"  runat="server" Width="200px"></asp:DropDownList>--%>
                                    <asp:ListBox ID="ddlEditCategory" CssClass="txtbox" runat="server" Height="50px" SelectionMode="Multiple" Width="200px"></asp:ListBox>
                                    <%--<asp:TextBox ID="txtEditCategory" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CategoryId") %>'></asp:TextBox>           --%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:ListBox ID="lstCategory" CssClass="txtbox" runat="server" Height="50px" SelectionMode="Multiple" Width="200px"></asp:ListBox>
                                    <%--<asp:TextBox ID="txtAddCategory" runat="server" ></asp:TextBox>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditBrandName" CssClass="txtbox" runat="server" Width="200px"  Text='<%#DataBinder.Eval(Container.DataItem, "BrandName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddBrandName" Width="100%" TextMode="MultiLine" runat="server" CssClass="txtbox"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Brand Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditDescription" CssClass="txtbox" Width="100%" TextMode="MultiLine"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandDescription") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddDescription" runat="server" Width="100%" CssClass="txtbox" TextMode="MultiLine" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="imgbtnEdit" CssClass="txtbox Editbutton" runat="server"  CommandName="Edit" Width="40%" Text="Edit" Font-Bold="true" />
                                    <asp:Button ID="imgbtnDelete" CssClass="txtbox Deletebutton" runat="server" CommandName="Delete" Width="50%" Text="Delete" Font-Bold="true"/>
                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="40%" Width="40%" />--%>
                                    <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/del3.png" Height="40%"  Width="40%" />--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                     <asp:Button ID="imgbtnUpdate" CssClass="txtbox Updatebutton" runat="server" CommandName="Update" Width="40%" Text="Update" Font-Bold="true"/>
                                    <asp:Button ID="imgbtnCancel" CssClass="txtbox Cancelbutton" runat="server" CommandName="Cancel" Width="40%" Text="Cancel" Font-Bold="true"/>
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
      <style type="text/css">
        .txtbox
        {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
</asp:Content>
