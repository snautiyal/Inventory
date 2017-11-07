<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Inventory.thermalprinter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <html xmlns="http://www.w3.org/1999/xhtml">
    02.
    <head runat="server" id="Head1">
        03.
        <title>Test ThermalLabel Client Printing</title>
        04.
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
        05.
        <script src="TLClientPrint.js" type="text/javascript"></script>
        06.
    </head>
    07.
    <body>
        08.
        <form id="Form2" runat="server">
            09.
            <div>
                10.
                <h1>ThermalLabel SDK Client Print Samples</h1>
                <br />
                11.
                <input type="button" value="Print Basic Label..." onclick="javascript: printThermalLabel()" />
                12.
                <br />
                13.
                <br />
                14.
                <cite>Remember that the client must install the TLClientPrint Utility first!</cite>
                15.
            </div>
            16.
        </form>
        17.
    </body>
    18.
    </html>
