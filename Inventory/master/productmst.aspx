<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productmst.aspx.cs" Inherits="Inventory.master.productmst" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; float: left">
    </div>
    <div style="background-color: #FFFFFF; float: Left; width: 100%; font-family: 'Arial Narrow';
        font-size: small;">
        <div align="center">
            <div id="PageHeaders">
                <asp:Label ID="lblPageHeading" runat="server" Text="Product Page" Font-Bold="True" Font-Size="Large"></asp:Label>
               
            </div>
            <br />
            <div id="divwelcome" runat="server" style="width: 100%;">
                <span class="style2"><span class="style3">Please choose one of the following task</span><br />
                </span>
            </div>
        </div>
 
                <div style="background-color: #FFFFFF; float: Left; width: 100%; font-family: 'Arial Narrow';
                    font-size: small;">
                    <div align="center">
                        <asp:Button ID="btnBackToMain" runat="server" OnClick="btnBackToMain_Click" Text="Go To Branche"
                            Visible="False" />
                        &nbsp;<asp:Button ID="btnAddBranch" runat="server" OnClick="btnAddBranch_Click" Text="Add" />
                        &nbsp;<asp:Button ID="btnModifyBranch" runat="server" OnClick="btnModifyBranch_Click"
                            Text="Modify" />
                        &nbsp;<asp:Button ID="btndeleteBranch" runat="server" OnClick="btndeleteBranch_Click"
                            Text="Delete" />
                        <br />
                        <span class="style4"><span class="style3">
                            <br />
                    </div>
                    <div id="searchBranch" visible="false" runat="server">
                        <div style="width: 30%; float: left">
                            Please enter the search keyowrds
                            <br />
                            Enter Branch Name:<br />
                            <asp:TextBox ID="tbxSearchBranchName" runat="server" BorderStyle="Double"></asp:TextBox>
                            <br />
                            <br />
                            Enter Company Name<br />
                            <asp:DropDownList ID="ddlsrchCompName" runat="server" Height="16px" Width="150px"
                                AutoPostBack="true">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:CheckBox ID="cbxCheck" runat="server" AutoPostBack="True" Text="If Exact Search"
                                Checked="True" />
                            <br />
                            <br />
                            <asp:Button ID="btnsearchBranch" runat="server" OnClick="btnsearchBranch_Click" Text="Search" />
                        </div>
                        <div style="width: 70%; float: left">
                            <center>
                                Seach Result<br />
                                <asp:Label ID="lblSearchmsg" runat="server" ForeColor="Red"></asp:Label>
                                <br />
                            </center>
                            <div>
                                <asp:GridView ID="grdSearchedBranch" runat="server" Width="200px" Style="font-family: Arial, Helvetica, sans-serif"
                                    BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3"
                                    CellSpacing="1" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                                    OnRowCommand="grdSearchedBranch_RowCommand" OnPageIndexChanging="grdSearchedBranch_PageIndexChanging">
                                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="m" CommandArgument='<%#Eval("BrAID") %>'>Select</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompName" HeaderText="Product Name" />
                                        <asp:BoundField DataField="BrName" HeaderText="Product Brand" />
                                        <asp:BoundField DataField="PrdDescrip" HeaderText="PrdDescrip" />
                                        <asp:BoundField DataField="PrdWeight" HeaderText="PrdWeight" />
                                        <asp:BoundField DataField="PrdPrice" HeaderText="PrdPrice" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div style="background-color: #FFFFFF; width: 100%; float: Left; font-family: 'Arial Narrow';">
                    <div id="Branchentry" visible="false" runat="server">
                        <span class="style6">Please fill in the required details</span>
                        <br />
                        <br />
                        Company Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList
                            ID="ddlCompanyName" runat="server" Width="191px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                            ID="lblBranchAID" runat="server"></asp:Label><asp:Label ID="lblPassedCompID" runat="server"
                                Visible="false" Text="Label"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp; &nbsp;<ul style="list-style-type: none;">
                            <li>Branch Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;<asp:TextBox ID="tbxBranchName" runat="server" BorderStyle="Double" Width="191px"></asp:TextBox>
                            </li>
                            <li>
                                <br class="style4" />
                                <li>Address:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp; &nbsp;
                                    <asp:TextBox ID="tbxAdress" runat="server" BorderStyle="Double" Width="191px" TextMode="MultiLine"></asp:TextBox>
                                </li>
                                <br class="style4" />
                                <li></li>
                                <li>City:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:TextBox
                                        ID="tbxCity" runat="server" BorderStyle="Double" Width="191px"></asp:TextBox>
                                </li>
                                <li></li>
                                <br class="style4" />
                                <li>State:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                    <asp:DropDownList ID="ddlBranchState" runat="server" Width="191px">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </li>
                                <li></li>
                                <br class="style4" />
                                <li>Pin No.:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;<asp:TextBox ID="tbxPinNo" runat="server" BorderStyle="Double" Width="191px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="tbxPinNo_FilteredTextBoxExtender" runat="server"
                                        Enabled="True" TargetControlID="tbxPinNo" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                    <br class="style4" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</li>
                        </ul>
                        <center>
                            <asp:Button ID="btnAddUpdDelBranch" runat="server" OnClick="btnAddUpdDelBranch_Click"
                                Text="Save" Style="height: 26px" Height="26px" />
                            &nbsp; &nbsp;<asp:Button ID="BtnCancel" runat="server" OnClick="BtnCancel_Click"
                                Text="Cancel" />
                        </center>
                    </div>
                    <div id="divdelconf" visible="false" runat="server">
                        <center>
                            Are You Sure! You Want To Delete the Branch Record :
                            <asp:Button ID="btnDelConfYes" runat="server" Text="Yes" OnClick="btnDelConfYes_Click" />
                            &nbsp;<asp:Button ID="btnDelConfNo" runat="server" Text="No" OnClick="btnDelConfNo_Click" />
                        </center>
                    </div>
                </div>
                </div>
                <div style="background-color: #FFFFFF; width: 100%; font-family: 'Arial Narrow';
                    font-size: small; color: #990000;">
                    <center>
                        <asp:Label ID="lblfrommassage" runat="server" ForeColor="Red" Style="font-size: medium"></asp:Label>
                    </center>
                </span></span>
          
       
        <span class="style4"><span class="style3">
            <br />
        </span></span></span>
    </div>
    </form>
</body>
</html>
