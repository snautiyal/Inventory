<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historytransaction.aspx.cs" Inherits="Inventory.Historytransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />
   <asp:Button ID="Button1" runat="server" Visible="false" Text="+ ADD.." BackColor="#669900" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="33px" Width="94px" OnClick="Button1_Click" />

                    <div>
                     
            <%--<tr>
                <td colspan="2" align="center"><b>History Transaction</b></td>
            </tr>  --%>  
             <table style="width:100%">   
                 <asp:Label ID="lblmsgs" runat="server" Visible="False" ForeColor="#FF3300"  Font-Size="X-Large"></asp:Label>             
            <tr>
            <td colspan="2">
            <asp:GridView ID="gvHistoryTransactionDetails" CssClass="Grid" runat="server" Width="100%"
                        AutoGenerateColumns="false" ShowFooter="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                    onrowcommand="gvHistoryTransactionDetails_RowCommand"
                    onrowdeleting="gvHistoryTransactionDetails_RowDeleting"
                    onrowupdating="gvHistoryTransactionDetails_RowUpdating"
                    onrowcancelingedit="gvHistoryTransactionDetails_RowCancelingEdit"
                    onrowediting="gvHistoryTransactionDetails_RowEditing"
                    OnRowDataBound="gvHistoryTransactionDetails_RowDataBound" 
                    DataKeyNames="HistoryTransactionId,ProductId" 
                 >
                <Columns>           
                    <asp:TemplateField HeaderText="History Transaction Id" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblHistoryTransactionId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "HistoryTransactionId") %>'></asp:Label>
                        </ItemTemplate>
                     <%-- <EditItemTemplate>           
                            <asp:Label ID="lblEditEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:Label>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmpID" runat="server" ></asp:TextBox>
                        </FooterTemplate>--%>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Product ID" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product.ProductName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate> 
                            <asp:DropDownList ID="ddlEditProduct" runat="server"  Width="100%"></asp:DropDownList>                
                            <%--<asp:TextBox ID="txtEditItemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductID") %>'></asp:TextBox>           --%>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlAddProduct" CssClass="txtbox"  runat="server" Width="100%" ></asp:DropDownList>                
                            <%--<asp:TextBox ID="txtAddItemName" runat="server" ></asp:TextBox>--%>
                        </FooterTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Comments" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblCommentsId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>    
                                   <asp:TextBox ID="txtEditComments" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Comments") %>'></asp:TextBox>          
                        </EditItemTemplate>
                        <FooterTemplate>
                            <%--<asp:DropDownList ID="ddlAddCategory" runat="server" Width="100px" ></asp:DropDownList>                --%>
                            <asp:TextBox ID="txtAddComments" CssClass="txtbox"  Width="100%" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Count" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblCountId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>  
                            <%--<asp:DropDownList ID="ddlEditCount" runat="server" DataTextField='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:DropDownList>         --%>
                            <asp:TextBox ID="txtEditCount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Count") %>'></asp:TextBox>   
                        </EditItemTemplate>
                        <FooterTemplate>
                            <%--<asp:DropDownList ID="ddlAddCount" runat="server" Width="100px" ></asp:DropDownList>--%>
                            <asp:TextBox ID="txtAddCount" CssClass="txtbox" Width="100%" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" with="100%" Text='<%#DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>           
                            <asp:TextBox ID="txtEditTotalAmount" runat="server" with="100%" Text='<%#DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:TextBox>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddTotalAmount" CssClass="txtbox" with="100%" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
     
                      <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30%">
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
