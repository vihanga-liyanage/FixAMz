<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report1.aspx.cs" Inherits="FixAMz_WebApplication.Report1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    Data : 
    <asp:TextBox ID="TextBox" class="expand-item-textbox" runat="server" Width="321px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" />
        <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>
        <asp:LinkButton ID="LinkButton1" runat="server">
            <div>
                <img src="img/DeleteIcon.png" />
                <a id='test' href='' runat='server' onserverclick='SampleLink_clicked'>Link</a>
                <div id="sample" runat="server">
                    adasdjnjasndansd aksjdads aks 
                </div>
            </div>
        </asp:LinkButton>
    </div>
    </form>
</body>
</html>
