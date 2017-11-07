<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gridexample.aspx.cs" Inherits="Inventory.gridexample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table width="600px" align="center">
            <tr>
                <td colspan="2" align="center"><b>Employee Details</b></td>
            </tr>          
            <tr>
            <td colspan="2">
            <asp:GridView ID="gvEmployeeDetails" runat="server" Width="600px" 
                    AutoGenerateColumns="false" ShowFooter="true"
                    onrowcommand="gvEmployeeDetails_RowCommand" 
                    onrowdeleting="gvEmployeeDetails_RowDeleting" 
                    onrowupdating="gvEmployeeDetails_RowUpdating" 
                    onrowcancelingedit="gvEmployeeDetails_RowCancelingEdit" 
                    onrowediting="gvEmployeeDetails_RowEditing"
                    HeaderStyle-BackColor="#4D4D4D"
                    HeaderStyle-ForeColor="White">
                <Columns>            
                    <asp:TemplateField HeaderText="Employee ID">
                        <ItemTemplate>
                            <asp:Label ID="lblEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "empid") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "empid") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmpID" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "name") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddName" runat="server" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label ID="lblDesignation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "designation") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditDesignation" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "designation") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddDesignation" runat="server" Width="150px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate>
                            <asp:Label ID="lblCity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "city") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditCity" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "city") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCity" runat="server" Width="80px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Country">
                        <ItemTemplate>
                            <asp:Label ID="lblCountry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "country") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditCountry" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "country") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCountry" runat="server" Width="80px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icon-edit.png" Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/Delete.png"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/icon-update.png"/>
                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/icon-Cancel.png"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                           <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add" Width="80px"></asp:LinkButton> 
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                </Columns>            
            </asp:GridView>

            </td>
            </tr>
        </table>
    </div>

</asp:Content>
