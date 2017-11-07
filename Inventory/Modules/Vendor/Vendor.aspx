<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Vendor.aspx.cs" Inherits="Inventory.Modules.Vendor.Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />
    <asp:Button ID="Button1" runat="server" Visible="false" Text="+ ADD.." BackColor="#669900" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="33px" Width="94px" OnClick="Button1_Click" />
    <div>

        <%-- <tr>
                <td colspan="2" align="center"><b>Category</b></td>
            </tr> --%>
        <table style="width: 100%">
            <asp:Label ID="lblmsgs" runat="server" Visible="False" ForeColor="#FF3300" Font-Size="X-Large"></asp:Label>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvvendor" CssClass="Grid" runat="server" Width="100%"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        PageSize="8" AllowPaging="true"
                        AutoGenerateColumns="false" ShowFooter="true"
                        OnPageIndexChanging="gvvendor_PageIndexChanging"
                        OnRowCommand="gvvendor_RowCommand"
                        OnRowDeleting="gvvendor_RowDeleting"
                        OnRowUpdating="gvvendor_RowUpdating"
                        OnRowCancelingEdit="gvvendor_RowCancelingEdit"
                        OnRowEditing="gvvendor_RowEditing">
                        <Columns>
                            <asp:TemplateField HeaderText="Vendor Id" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblvendorId" runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "VendorId") %>'></asp:Label>
                                </ItemTemplate>
                                <%-- <EditItemTemplate>           
                            <asp:Label ID="lblEditEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BrandId") %>'></asp:Label>           
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmpID" runat="server" ></asp:TextBox>
                        </FooterTemplate>--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblVendorName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "VendorName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditVendorName" runat="server" TextMode="MultiLine" CssClass="txtbox" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "VendorName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                   
                                    <asp:TextBox ID="txtAddVendorName" Width="100%" TextMode="MultiLine" CssClass="txtbox" runat="server"></asp:TextBox>
                                  
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Address" HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Address") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditAddress" Width="100%" TextMode="MultiLine" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Address") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
           
                                    <asp:TextBox ID="txtAddAddress" runat="server" Width="100%" CssClass="txtbox" TextMode="MultiLine"></asp:TextBox>
                                   
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Contact No" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ContactNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditContactNo" Width="100%" TextMode="MultiLine" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ContactNo") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ErrorMessage="Enter Correct Mobile Number" ForeColor="Red" ControlToValidate="txtEditContactNo"
                                        ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtAddContactNo" runat="server" MaxLength="10" Width="100%" CssClass="txtbox" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ErrorMessage="Enter Correct Mobile Number" ForeColor="Red" ControlToValidate="txtAddContactNo"
                                        ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bill No" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BillNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditBillNo" Width="100%" TextMode="MultiLine" CssClass="txtbox" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BillNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    
                                    <asp:TextBox ID="txtAddBillNo" runat="server" Width="100%" CssClass="txtbox" TextMode="MultiLine"></asp:TextBox>
                                    
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="45%">
                                <ItemTemplate>
                                    <asp:Button ID="imgbtnEdit" CssClass="txtbox Editbutton" runat="server" CommandName="Edit" Width="40%" Text="Edit" Font-Bold="true" />
                                    <asp:Button ID="imgbtnDelete" CssClass="txtbox Deletebutton" runat="server" CommandName="Delete" Width="58%" Text="Delete" Font-Bold="true" />
                                    <%--<asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/edit.png" Height="40%" Width="40%" />--%>
                                    <%--<asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/del3.png" Height="40%"  Width="40%" />--%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="imgbtnUpdate" CssClass="txtbox Updatebutton" runat="server" CommandName="Update" Width="40%" Text="Update" Font-Bold="true" />
                                    <asp:Button ID="imgbtnCancel" CssClass="txtbox Cancelbutton" runat="server" CommandName="Cancel" Width="58%" Text="Cancel" Font-Bold="true" />
                                    <%--<asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Images/update.png" Height="40%" Width="20%" />
                                    <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/cancel.jpg" Height="40%" Width="20%" />--%>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="lbtnAdd" CssClass="txtbox Addbutton"  runat="server" CommandName="ADD" Width="40%" Text="ADD" Font-Bold="true" />
                                    <br />
                                       <br />
                                    
                                    <%--<asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD"  Width="100px"></asp:LinkButton>--%>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="green" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <center><b><asp:Label ID="lblMsg" runat="server"></asp:Label></b></center>
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
    <script>
        $('#lbtnAdd').bind('Addbutton', function (e) {
            var button = $('#lbtnAdd');

    // Disable the submit button while evaluating if the form should be submitted
    button.prop('disabled', true);

    var valid = true;    

    // Do stuff (validations, etc) here and set
    // "valid" to false if the validation fails

    if (!valid) { 
        // Prevent form from submitting if validation failed
        e.preventDefault();

        // Reactivate the button if the form was not submitted
        button.prop('disabled', false);
    }
        });
        </script>
</asp:Content>
