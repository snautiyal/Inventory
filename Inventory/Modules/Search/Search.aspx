<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Inventory.Modules.Search.Search" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../../GridCss/Gridstyle.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.validate.min.js" type="text/javascript"></script>

    <link href="css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <style type="text/css">
        .warning {
            border: 1px solid red;
            background-color: red;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    var text = request.term;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        //url: "http://43.224.136.220/AutoCompleteService1.asmx/GetAutoCompleteData",
                        url: "http://localhost:49594/AutoCompleteService1.asmx/GetAutoCompleteData",
                        data: "{'ProductName':'" + request.term + "'}",
                        dataType: "json",
                        success: function (data) {
                            //var textBox = $("input[value="+text+"]")
                            //var row = textBox.closest('tr');
                            //var val= data.d[0].split('-')[1]
                            //$.ajax({
                            //    type: "POST",
                            //    contentType: "application/json; charset=utf-8",
                            //    url: "http://localhost:49594/AutoCompleteService1.asmx/GetItemDetails",
                            //    data: "{'ProductId':'" + val + "'}",
                            //    dataType: "json",
                            //    success: function (data) {
                            //        var details = data.d[0].split('-');
                            //        var name = details[0];
                            //        var desc = details[1];
                            //        var weight = details[2];
                            //        var price = details[3];
                            //        var stock = details[4];
                            //        var count = details[5];

                            //        console.log(name+','+desc + ',' + weight + ',' + price + ',' + stock + ',' + count);
                            //        //$("[id*=txtdescription]").val(desc);
                            //        //$("[id*=txtweight]").val(weight);
                            //        //$("[id*=txtprice]").val(price);
                            //        //$("[id*=txtstock]").val(stock);
                            //        //$("[id*=txtAddCount]").val(count);
                            //        $(row).find('.name').val(name);
                            //        $(row).find('.description').val(desc);
                            //        $(row).find('.weight').val(weight);
                            //        $(row).find('.price').val(price);
                            //        $(row).find('.stock').val(stock);
                            //        $(row).find('.AddCount').val(count);
                            //        $(row).find("[id*=hfProductId]").val(val);


                            //    },
                            //    error: function (result) {
                            //        alert("Error");
                            //    }
                            //});

                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                            //response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (e, i) {
                    $("[id*=hfProductId]").val(i.item.val);
                    //var row = e.srcElement.closest('tr');
                    var row = e.target.closest('tr');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        // url: "http://192.168.10.11:91/AutoCompleteService1.asmx/GetItemDetails",
                        url: "http://localhost:49594/AutoCompleteService1.asmx/GetItemDetails",
                        data: "{'ProductId':'" + i.item.val + "'}",
                        dataType: "json",
                        success: function (data) {
                            var details = data.d[0].split('-');
                            var name = details[0];
                            var desc = details[1];
                            var weight = details[2];
                            var price = details[3];
                            var stock = details[4];
                            var stockin = details[5];
                            var thresh = details[6];
                            var count = details[7];
                            if (parseFloat(stock) == 0) {
                                $(function () {
                                    $("input.myclass").attr("disabled", true);
                                    alert("Stock is '0' can not be any Transaction");
                                });
                            }
                            else {
                                if (parseFloat(stock) <= parseFloat(thresh)) {
                                    //$(row).find("[id*=hftxtstock]").attr('title', 'Stock has low');

                                    $(row).find('.stock').addClass('warning');
                                    $("input.myclass").attr("disabled", false);

                                }
                                else {
                                    $("input.myclass").attr("disabled", false);
                                    $(row).find('.stock').remove('warning');
                                }
                                console.log(name + ',' + desc + ',' + weight + ',' + price + ',' + stock + ',' + stockin + ',' + count + ',');
                                $(row).find('.name').val(name);
                                $(row).find('.description').val(desc);
                                $(row).find('.weight').val(weight);
                                $(row).find('.price').val(price);
                                $(row).find('.stock').val(stock);
                                $(row).find('.stockin').val(stockin);
                                $(row).find('.AddCount').val(count);
                                $(row).find("[id*=HiddenField1]").val(i.item.val);
                                $(row).find("[id*=hftxtname]").val(name);
                                $(row).find("[id*=hftxtdescription]").val(desc);
                                $(row).find("[id*=hftxtweight]").val(weight);
                                $(row).find("[id*=hftxtprice]").val(price);
                                $(row).find("[id*=hftxtstock]").val(stock);
                                $(row).find("[id*=hftxtstockin]").val(stockin);
                                $(row).find("[id*=hfthreshhold]").val(thresh);


                            }
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
        });
    </script>




    <%--<asp:TextBox ID="txtSearch" runat="server" class="autosuggest"></asp:TextBox>--%>
    <div hidden="hidden">
        <asp:DropDownList ID="ddlCategory" runat="server" Width="200" Height="33px"></asp:DropDownList>
        <asp:DropDownList ID="ddlBrand" runat="server" Width="200" Height="33px"></asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="Search.." BackColor="#2E8B5C" BorderColor="#669900" BorderStyle="Outset" ForeColor="White" Height="33px" Width="94px" OnClick="Button1_Click" />
    </div>
    <div>

        <%--<tr>
                <td colspan="2"><b>Search</b></td>
            </tr>--%>
        <span id="displayShow" style="color: #ce352c"></span>
        <table style="width: 100%">
            <tr>
                <td colspan="2">
                    <%-- <asp:GridView ID="gvProductDetails" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                        runat="server" AutoGenerateColumns="true"
                        ShowHeader="true">
                        
                    </asp:GridView>--%>

                    <asp:GridView ID="gvProductDetails" CssClass="Grid" runat="server" Width="100%"
                        AutoGenerateColumns="false" ShowFooter="true"
                        AlternatingRowStyle-CssClass="alt"
                        PagerStyle-CssClass="pgr"
                        AllowPaging="true"
                        OnRowCommand="gvProductDetails_RowCommand"
                        OnRowDataBound="gvProductDetails_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Search Product" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtSearch"  runat="server" CssClass="autosuggest" OnTextChanged="txtSearch_TextChanged" Value='<%#DataBinder.Eval(Container.DataItem, "Product Name") %>' ReadOnly="False" AutoPostBack="True"></asp:TextBox>--%>
                                    <input type="text" id="txtSearch" name="Name" class="autosuggest txtbox" value='<%#DataBinder.Eval(Container.DataItem, "Product Name") %>' />
                                    <%--<asp:TextBox ID="txtSearch" CssClass="name readonly txtbox" runat="server" Width="100%" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "ProductId") %>'></asp:TextBox>--%>
                                    <asp:HiddenField ID="hfProductId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ProductId") %>' />
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("ProductId") %>' />
                                    <%--<textarea id="txtSearch" name="Name" class="autosuggest txtbox"  ><%#DataBinder.Eval(Container.DataItem, "Bar Code") %></textarea>--%>

                                    <%--<asp:TextBox ID="tbxname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product.ProductName") %>'></asp:TextBox>--%>
                                    <%--<asp:Label ID="lblName" runat="server" Text='<%#DataBin"der.Eval(Container.DataItem, "Product.ProductName") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <%--<asp:Label ID="txtname" CssClass="name readonly" runat="server" Width="150"  Text='<%#DataBinder.Eval(Container.DataItem, "Product Name") %>' ></asp:Label>--%>
                                    <asp:TextBox ID="txtname" CssClass="name readonly txtbox" runat="server" Width="100%" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "Product Name") %>'></asp:TextBox>
                                    <%--<asp:Label ID="txtname" CssClass="name txtbox" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product Name") %>'></asp:Label>--%>
                                    <asp:HiddenField ID="hftxtname" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Product Name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <%--<asp:Label ID="txtdescription" CssClass="description readonly" Width="150" runat="server"   Text='<%#DataBinder.Eval(Container.DataItem, "Product description") %>'></asp:Label>--%>
                                    <asp:TextBox ID="txtdescription" CssClass="description readonly txtbox" Enabled="false" Width="100%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Product description") %>'></asp:TextBox>
                                    <%--<asp:Label ID="txtdescription" CssClass="description" runat="server" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "Product description")%>'></asp:Label>--%>
                                    <asp:HiddenField ID="hftxtdescription" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Product description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Weight" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtweight" runat="server" CssClass="weight readonly txtbox" Width="100%" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "Product Weight") %>'></asp:TextBox>
                                    <%--<asp:Label ID="txtweight" runat="server" CssClass="weight" Width="100%" Text='<%#DataBinder.Eval(Container.DataItem, "Product Weight") %>'></asp:Label>--%>
                                    <asp:HiddenField ID="hftxtweight" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Product Weight") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Stock In price" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtstockin" runat="server" Width="100%" CssClass="stockin readonly txtbox" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "Stock in") %>'></asp:TextBox>
                                    <%--<asp:Label ID="txtprice" runat="server" Width="100%" CssClass="price" Text='<%#DataBinder.Eval(Container.DataItem, "Product Price") %>'></asp:Label>--%>

                                    <asp:HiddenField ID="hftxtstockin" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Stock in") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Price" HeaderStyle-Width="10%">
                                <ItemTemplate>

                                    <asp:TextBox ID="txtprice" runat="server" Width="100%" CssClass="price readonly txtbox" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "Product Price") %>'></asp:TextBox>
                                    <%--<asp:Label ID="txtprice" runat="server" Width="100%" CssClass="price" Text='<%#DataBinder.Eval(Container.DataItem, "Product Price") %>'></asp:Label>--%>
                                    <asp:HiddenField ID="hftxtprice" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "Product Price") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stock" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtstock" runat="server" CssClass="stock txtbox" Width="100%" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "StocK Value") %>'></asp:TextBox>
                                    <%--<input type="text" id="txtstock"  class="stock txtbox"   value='<%#DataBinder.Eval(Container.DataItem, "StocK Value") %>' />--%>
                                    <asp:HiddenField ID="hftxtstock" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "StocK Value") %>' />
                                    <%--<asp:Label ID="txtstock"  runat="server" CssClass="stock" Width="100%"  Text='<%#DataBinder.Eval(Container.DataItem, "StocK Value") %>'></asp:Label>--%>
                                    <asp:HiddenField ID="hfthreshhold" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ThreshHold") %>' />

                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Quantity/Issue" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAddCount" CssClass="AddCount txtbox myclass" onkeyup="MinimumNValidate()" Width="100%" onkeydown="return (event.keyCode!=13);" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:TextBox>
                                    <%--<input type="text" id="txtAddCount"  class="AddCount txtbox"  value='<%#DataBinder.Eval(Container.DataItem, "Quantity") %>' />--%>
                                    <%-- <asp:RequiredFieldValidator runat="server" id="reqName" controltovalidate="txtAddCount" errormessage="Please enter the value!" />--%>
                                    <%--<asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txtAddCount" ControlToCompare="txtstock" Operator="LessThanEqual" Type="Integer" ErrorMessage="unavailable stock!" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtcomments" runat="server" Width="100%" CssClass="txtbox" Text='<%#DataBinder.Eval(Container.DataItem, "Comment") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>

                                    <asp:ImageButton CssClass="addbutton" ID="imgbtnAdd" runat="server" ImageUrl="~/Images/add.jpg" OnClick="imgbtnAdd_Click" />
                                    <asp:ImageButton CssClass="deletebutton" ID="imgbtnDelete" runat="server" ImageUrl="~/Images/delete-icon-md.png" OnClick="imgbtnDelete_Click"  />
                                    <%--<asp:ImageButton CssClass="txtbox deletebutton" ID="imgbtnDelete"  CommandName="Delete" runat="server" ImageUrl="~/Images/delete-icon-md.png" OnClick="imgbtnAdd_Click" Visible="true" />--%>
                                    <%-- <asp:Button  ID="imgbtnDelete" CssClass="txtbox deletebutton" runat="server" CommandName="Delete" Width="58%" Text="Delete" Font-Bold="true" Visible="true" />--%>
                                    <%--<input type="button" id="imgbtnDelete" class="deletebutton" style="background-image: url(http://localhost:49594//Images/delete-icon-md.png); width: 105%; background-color: #e7e7e7; background-repeat: no-repeat;" onclick="$(this).closest('tr').remove();">--%>
                                    <%--<asp:ImageButton ID="imgbtnAdd" runat="server" CommandArgument="imgbtnAdd" ImageUrl="~/Images/checkout.png" Text="Add" Width="100px" />--%>
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="green" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <br />
                    <a style="float: right;">
                        <asp:Button ID="imgbtnCheckout" CommandArgument="imgbtnCheckout" runat="server" Height="50px" Width="100%" CssClass="txtbox myclass" Text="Checkout with local Printer " BackColor="#669900" Font-Bold="True" ForeColor="White" OnClick="imgbtnCheckout_Click" /></a>
                    <asp:Button ID="btnchckserver" runat="server" Width="20%" CssClass="txtbox myclass" Text="Checkout with server printer" BackColor="#669900" Height="50px" Font-Bold="True" ForeColor="White" OnClick="btnchckserver_Click" />
                    <br />
                    <table>
                        <td style="margin: 0.5px; right: 15px; left: 15px; font-weight: bold; padding-top: inherit; padding-right: inherit; padding-bottom: inherit; padding-left: 5px;"></td>
                    </table>
                    <a href="#" class="buttonprimary" onclick="printIt(document.getElementById('reportkadiv').innerHTML); return false" id="printButton" runat="server" visible="false" style="background-color: #3399FF">
                        <span>Print</span>
                    </a>
                    <div id="reportkadiv">
                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowToolBar="false" ZoomMode="PageWidth"></rsweb:ReportViewer>
                    </div>
                    <br />

                    <center><b><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></b></center>

                    <%--<asp:ImageButton ID="imgbtnCheckout" runat="server" CommandArgument="imgbtnCheckout" ImageUrl="~/Images/checkout.png" Text="Add"  ImageAlign="Right" OnClick="imgbtnCheckout_Click" />--%>
                    <br />
                    <br />

                </td>
            </tr>
        </table>
    </div>

    <br />
    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('.readonly').attr('readonly', true);
           // $('[id*=print]').click( function () {
                var printWindow = window.open('about:blank', 'print', 'left=50000,top=50000,width=0,height=0');
                var element = $('[id*=ReportViewer1_ctl09]');
                //printWindow.document.write(element.html());
                printWindow.document.write(document.getElementById("VisibleReportContentctl00_MainContent_ReportViewer1_ctl09").innerHTML);
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
                //printWindow.close();
            //});
            //$('[id*=txtname]').Attributes.Add("readonly", "true");
            //$("input[id*=txtname]").attr('readonly', true);
            //$("input[id*=txtdescription]").attr('readonly', true);
        });
</script>--%>
    <style type="text/css">
        .txtbox {
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
    <%--  <script>
        function bindForm() {
           $.validator.unobtrusive.parse('form');
        }
    </script>--%>
    <%-- <script>
        //var $j = jQuery.noConflict();
        $(document).ready(function () {
            $(document).on('change', '#MainContent_gvProductDetails_txtAddCount_0', function () {
                alert("ok");
            });
        });
        
     </script>--%>
    <%-- <script>
        $(document).ready(function () {
            var isValOk = false;
            var isPerOk = false;

            //$("input[name=ctl00$MainContent$gvProductDetails$ctl02$txtAddCount]").on('change', function () {
            //    alert("ok ");
            //});

            $('#MainContent_gvProductDetails_txtAddCount_0').bind("change", function () {
                if (parseFloat($("#MainContent_gvProductDetails_txtAddCount_0").val()) > parseFloat($("#MainContent_gvProductDetails_txtstock_0").val())) {
                    $("#displayShow").text("Maximum Value should be greater than or equal to Minimum Value");
                    isValOk = false;
                }
                else {
                    $("#displayShow").text("");
                    isValOk = true;
                }
            });
            $(this).bind('submit', function () {
                $('#MainContent_gvProductDetails_txtAddCount_0').trigger('change');
                return isValOk;
            });

        });
       </script>--%>
    <script type="text/javascript">
        $(".Grid tr").prev().find("td:last").find('.addbutton').hide();
        $(".Grid tr:last").prev().find("td:last").find('.addbutton').show();
        $(".Grid tr:last").prev().find("td:last").find('.deletebutton').hide();
        this.dataGridView1.Refresh();
        var win = null;
        function printIt(printThis) {
            win = window.open();
            self.focus();
            win.document.open();
            win.document.write('<' + 'html' + '><' + 'head' + '><' + 'style' + '>');
            win.document.write('body, td {margin-left:15px;margin-right: 15px;font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:20pt;}');
            win.document.write('<' + '/' + 'style' + '><' + '/' + 'head' + '><' + 'body' + '>');
            win.document.write(printThis);
            win.document.write('<' + '/' + 'body' + '><' + '/' + 'html' + '>');
            win.document.close();
            win.print();
            win.close();
            document.getElementById("reportkadiv").innerHTML = " ";

            document.getElementById("MainContent_printButton").style.display = "none";
        }

    </script>
    <script>
        function check() {
            document.getElementById("chkRow").checked = true;
        }

        function uncheck() {
            document.getElementById("chkRow").checked = false;
        }
    </script>

    <script type="text/javascript">
        $('.stock').each(function () {
            var thershhold = $(this).parent().find('input[id*=hfthreshhold]')
            var quantity = $(this).parent().parent().find('input[id*=txtAddCount]')
            if (parseInt(thershhold.val()) > parseInt($(this).val()) - parseInt(quantity.val())) {
                $(this).addClass('warning');
            }
        })
        $('input[id*=txtAddCount]').focusout(function () {
            var count = parseInt($(this).val());
            var stock = parseInt($(this).parent().prev().find('input').val());

            if (count > stock) {
                alert('Sorry! The quantity is greater than stock');
                $(this).parent().next().next().find('.addbutton').attr('disabled', 'disabled');
            }
            else {
                $(this).parent().next().next().find('.addbutton').removeAttr('disabled');
            }
        })
        <%--$('.addbutton').click(function Validate() {
            var stock = document.getElementById("txtstock.ClientID");
            <%--var grid = document.getElementById("<%= gvProductDetails.ClientID%>")
            //var txtAmountReceive = $("input[id*=txtstock]");
            var count = parseInt(document.getElementById("txtAddCount.ClientID").value);
            if (count >= stock) {
                alert("Must be smaller");
                return false;
            }
            return true;
        });--%>
</script>
   <%-- <script>
        function RemoveRow(item) {
            var table = document.getElementById('MainContent_gvProductDetails');
            table.deleteRow(item.parentNode.parentNode.rowIndex);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lnk").click(function () {
                $("#<%=imgbtnAdd.\")[0].click();
            });
        });
    </script>--%>
</asp:Content>

