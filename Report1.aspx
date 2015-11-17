<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report1.aspx.cs" Inherits="FixAMz_WebApplication.Report1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>


    <!--<asp:TextBox ID="AssetIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>-->
    <asp:Button ID="CalDepreciationBtn" runat="server" Text="Update on Year End"
                                        OnClick="CalDepreciationBtn_Click"  class="expand-item-btn" />

    
    
    </div> 


    </form>
</body>
</html>
