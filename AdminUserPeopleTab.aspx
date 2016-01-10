<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="AdminUserPeopleTab.aspx.cs"
    Inherits="FixAMz_WebApplication.AdminUserPeopleTab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>FixAMz</title>
    <meta name="description" content="Source code generated using layoutit.com">
    <meta name="author" content="LayoutIt!">
    <link href="Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Styles/CustomStyles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:SqlDataSource ID="SqlDataSourceFixAMz" runat="server" ConnectionString="<%$ ConnectionStrings:SystemUserConnectionString %>"
        SelectCommand="SELECT * FROM [SystemUser]"></asp:SqlDataSource>
    <div class="container-fluid">
<!--Header-->
        <div class="row">
            <div class="col-md-12">
                <div class="row header">
                    <div class="col-md-3 col-md-offset-1">
                        <a href="AdminUserHomeTab.aspx"><img src="img/fixamz.png" class="logo" /></a>
                    </div>
                    <div class="col-md-8 header-right">
                        <div class="col-sm-12" id="user-name-box">
                            <span id="userName" runat="server"></span>
                                | 
                            <span id="notification_li">
						        <span id="notification_count" runat="server"></span>
						        <a href="#" id="notificationLink">
                                    <img src="img/bell.jpg" style="width: 27px;"/>
                                </a>
						        <div id="notificationContainer">
							        <div id="notificationTitle" runat="server">Notifications</div>
							        <div id="notificationsBody" class="notifications" runat="server">
                                        <!-- Generated code -->
							        </div>
							        <div id="notificationFooter"><a href="#">See All</a></div>
						        </div>
					        </span>
                                | 
                            <a id="A1" href="#" runat="server" onserverclick="SignOutLink_clicked">Sign out</a>
                        </div>
                        <div class="col-sm-12 nav-bar-container">
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                                    <span class="sr-only">Toggle navigation</span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>
                            </div>
                            <div class="collapse navbar-collapse" style="float:right;">
                                <ul class="custom-nav-bar nav nav-tabs navbar-nav">
                                    <li><a href="AdminUserHomeTab.aspx">HOME</a> </li>
                                    <li><a href="AdminUserPeopleTab.aspx" class="active">PEOPLE</a> </li>
                                    <li><a href="AdminUserSystemTab.aspx">SYSTEM</a> </li>
                                    <li><a href="AboutUs.aspx">ABOUT</a> </li>
                                    <li><a href="Help.aspx">HELP</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<!--Main content-->
        <div class="row">
            <div class="col-md-9 col-xs-offset-2 expand-item-container">
                <div id="responseBoxGreen" runat="server">
                    <a href="" onclick="this.parentNode.style.display = 'none';">
                        <span class="glyphicon glyphicon-remove" aria-hidden="true" style="float: right; color: #B8F0AD; margin-top: 5px;"></span>
                    </a>
                    <div id="responseMsgGreen" runat="server"></div>
                </div>
                <div id="responseBoxRed" runat="server">
                    <a href="" onclick="this.parentNode.style.display = 'none';">
                        <span class="glyphicon glyphicon-remove" aria-hidden="true" style="float: right; color: #F0AEAE;; margin-top: 5px;"></span>
                    </a>
                    <div id="responseMsgRed" runat="server"></div>
                </div>
<!--Add user-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AddNewUserHeader">
                            Add New User</div>
                        <div class="expand-item-content" id="AddNewUserContent" runat="server">
                            <div class="col-md-8">
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Employee ID</div>
                                    <div id="AddNewEmpID" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        First Name</div>
                                    <asp:TextBox ID="AddNewFirstNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="AddNewFirstNameValidator" runat="server">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Last Name</div>
                                    <asp:TextBox ID="AddNewLastNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="AddNewLastNameValidator" runat="server">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Email</div>
                                    <asp:TextBox ID="AddNewEmailTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="AddNewEmailValidator" runat="server">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Contact</div>
                                    <asp:TextBox ID="AddNewContactTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="AddNewContactValidator" runat="server">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Cost Center</div>
                                    <asp:DropDownList ID="AddUserCostNameDropDown" class="expand-item-textbox" runat="server" >
                                    </asp:DropDownList>
                                    <div class="validator" id="AddUserCostNameValidator" runat="server"></div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Access Level</div>
                                    <asp:DropDownList ID="TypeDropDown" class="expand-item-textbox" runat="server" OnSelectedIndexChanged="AddUserTypeDropDown_Selected" AutoPostBack="true">
                                        <asp:ListItem Text="-- Select Level --" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Administrator" Value="admin"></asp:ListItem>
                                        <asp:ListItem Text="Asset Manager" Value="manageAssetUser"></asp:ListItem>
                                        <asp:ListItem Text="Report Generator" Value="generateReportUser"></asp:ListItem>
                                        <asp:ListItem Text="Asset Manager and Report Generator" Value="manageReport"></asp:ListItem>
                                        <asp:ListItem Text="Asset Owner" Value="owner"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="validator" id="TypeValidator" runat="server"></div>
                                </div>
                                <div id="AddUserLoginDetailContainer" runat="server">
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Username</div>
                                        <asp:TextBox ID="AddNewUsernameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="AddNewUsernameValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Password</div>
                                        <asp:TextBox ID="AddNewPasswordTextBox" class="expand-item-textbox" runat="server"
                                            type="password"></asp:TextBox>
                                        <div class="validator" id="AddNewPasswordValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Confirm Password</div>
                                        <asp:TextBox ID="AddNewConfirmPasswordTextBox" class="expand-item-textbox" runat="server"
                                            type="password"></asp:TextBox>
                                        <div class="validator" id="AddNewConfirmPasswordValidator" runat="server">
                                        </div>
                                    </div>

                                </div>
                                <div class="expand-item-row" style="padding-right: 8.7%;">
                                    <asp:Button ID="AddUserBtn" runat="server" Text="Add User" OnClick="AddUserBtn_Click"
                                        class="expand-item-btn" OnClientClick="return isValidAddNew()" /> 
                                    <asp:Button ID="AddNewCancelBtn" runat="server" Text="Cancel" class="expand-item-btn"
                                        OnClick="cancel_clicked" />
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="add-user-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Advanced user search-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/SearchIcon.png" />
                        <div class="expand-item-title" id="AdvancedUserSearchHeader">
                            <a href="AdvancedUserSearch.aspx">Advanced user search</a>    
                        </div>
                    </div>
                </div>
<!--Update user-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <div id="Div2" runat="server">
                        </div>
                        <img src="img/UpdateIcon.png" />
                        <div class="expand-item-title" id="UpdateUserHeader">
                            Update User</div>
                        <div class="expand-item-content" id="UpdateUserContent" runat="server">
                            <div class="col-md-8">
                                <div id="updateUserInitState" runat="server">
                                    <div class="info-div">
                                        Enter employee ID to start or use the advanced search above.</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Employee ID</div>
                                        <asp:TextBox ID="UpdateEmpIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="UpdateEmpIDValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row" style="padding-right: 8.7%;">
                                        <asp:Button ID="UpdateUserFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidUpdateEmpID()" OnClick="UpdateUserFindBtn_Click" />
                                    </div>
                                </div>
                                <div id="updateUserSecondState" runat="server">
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">
                                            Employee ID</div>
                                        <div id="UpdateEmpID" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            First Name</div>
                                        <asp:TextBox ID="UpdateFirstNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="UpdateFirstNameValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Last Name</div>
                                        <asp:TextBox ID="UpdateLastNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="UpdateLastNameValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Email</div>
                                        <asp:TextBox ID="UpdateEmailTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="UpdateEmailValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Contact</div>
                                        <asp:TextBox ID="UpdateContactTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="UpdateContactValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Cost Center Name</div>
                                        <asp:DropDownList ID="UpdateCostCenterDropDown" class="expand-item-textbox" runat="server">
                                        </asp:DropDownList>
                                        <div class="validator" id="UpdateCostCenterValidator" runat="server"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Access Level</div>
                                    <asp:DropDownList ID="UpdateTypeDropDown" class="expand-item-textbox" runat="server" OnSelectedIndexChanged="AddUserTypeDropDown_Selected" AutoPostBack="true">
                                        <asp:ListItem Text="Administrator" Value="admin"></asp:ListItem>
                                        <asp:ListItem Text="Asset Manager" Value="manageAssetUser"></asp:ListItem>
                                        <asp:ListItem Text="Report Generator" Value="generateReportUser"></asp:ListItem>
                                        <asp:ListItem Text="Asset Manager and Report Generator" Value="manageReport"></asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="validator" id="UpdateTypeValidator" runat="server"></div>
                                    </div>
                                    <div class="row expand-item-row" style="padding-right: 8.7%;">
                                        <asp:Button ID="UpdateUserBtn" runat="server" Text="Update User" class="expand-item-btn"
                                            OnClientClick="return isValidUpdates()" OnClick="UpdateUserBtn_Click" />
                                        <asp:Button ID="UpdateUserCancelBtn" runat="server" Text="Cancel" class="expand-item-btn"
                                            OnClick="cancel_clicked" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="update-user-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Reset Password-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <div id="Div3" runat="server">
                        </div>
                        <img src="img/UpdateIcon.png" />
                        <div class="expand-item-title" id="ResetPasswordHeader">
                            Reset User Password</div>
                        <div class="expand-item-content" id="ResetPasswordContent" runat="server">
                            <div class="col-md-8">
                                <div id="resetPasswordInitState" runat="server">
                                    <div class="info-div">
                                        Enter username to reset user Password </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Username</div>
                                        <asp:TextBox ID="ResetPasswordUsernameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="ResetPasswordUsernameValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row" style="padding-right: 8.7%;">
                                        <asp:Button ID="ResetPasswordFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidResetPasswordUsername()" OnClick="ResetPasswordFindBtn_Click" />
                                    </div>
                                </div>
                                <div id="resetPasswordSecondState" runat="server">
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">
                                            Username</div>
                                        <div id="ResetUsername" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            New Password</div>
                                        <asp:TextBox ID="ResetNewPasswordTextBox" class="expand-item-textbox" runat="server"
                                            type="password"></asp:TextBox>
                                        <div class="validator" id="ResetNewPasswordValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Confirm  New Password</div>
                                        <asp:TextBox ID="ResetNewConfirmPasswordTextBox" class="expand-item-textbox" runat="server"
                                            type="password"></asp:TextBox>
                                        <div class="validator" id="ResetNewConfirmPasswordValidator" runat="server">
                                        </div>
                                    </div>
                                   
                                    <div class="row expand-item-row" style="padding-right: 8.7%;">
                                        <asp:Button ID="ResetPasswordBtn" runat="server" Text="Reset Password" class="expand-item-btn"
                                            OnClientClick="return isValidResetPassword()" OnClick="ResetPasswordBtn_Click" />
                                        <asp:Button ID="Button3" runat="server" Text="Cancel" class="expand-item-btn"
                                            OnClick="cancel_clicked" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="update-user-icon">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Disable user-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <div id="Div1" runat="server">
                        </div>
                        <img src="img/DeleteIcon.png" />
                        <div class="expand-item-title" id="DeleteUserHeader">
                            Delete User</div>
                        <div class="expand-item-content" id="DeleteUserContent" runat="server">
                            <div class="col-md-8">
                                <div id="deleteUserInitState" runat="server">
                                    <div class="info-div">
                                        Enter employee ID to delete the user</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Employee ID</div>
                                        <asp:TextBox ID="DeleteUserEmpIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="DeleteUserEmpIDValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row" style="padding-right: 8.7%;">
                                        <asp:Button ID="DeleteUserFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidDeleteEmpID()" OnClick="DeleteUserFindBtn_Click" />
                                    </div>
                                </div>
                                <div id="deleteUserSecondState" runat="server">
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Employee ID</div>
                                        <div id="DeleteEmpID" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            First Name</div>
                                        <div id="DeleteFirstName" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Last Name</div>
                                        <div id="DeleteLastName" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Email</div>
                                        <div id="DeleteEmail" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Contact</div>
                                        <div id="DeleteContact" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row" style="padding-right: 8.7%;">
                                        <asp:Button ID="DeleteUserBtn" runat="server" Text="Delete User" class="expand-item-btn"
                                            OnClick="DeleteUserBtn_Click" OnClientClick="return window.confirm('Confirm user deletion.')" />
                                        <asp:Button ID="DeleteUserCancelBtn" runat="server" Text="Cancel" class="expand-item-btn"
                                            OnClick="cancel_clicked" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="delete-user-icon">
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Ectra div for space-->
                    <div class="row">
                    </div>
                </div>
            </div>
        </div>
<!--Footer-->
        <div id="footer" class="row">
            <div class="row footer-up">
                <ul class="footer-nav">
                    <li><a href="AboutUs.aspx">About</a></li>
                    <li><a href="Help.aspx">Help</a></li>
                    <li><a href="AdminSitemap.aspx">Site map</a></li>
                </ul>
            </div>
            <div class="row footer-down">
                <div class="col-md-5">
                    Copyright &copy; 2015 National Water Supply and Drainage Board.<br>
                    All Rights Reserved.</div>
                <div class="col-md-7 developer-link">
                    <a href="https://www.facebook.com/FixAMzDevelopers/?fref=ts&__mref=message_bubble">Developer site<img src="img/developerIcon.png" /></a>
                </div>
                <img src="img/logoSimble.png" style="float: right; width: 47px; opacity: 0.25" />
            </div>
        </div>
    </div>

    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/JQuery-1.11.3.min.js" type="text/javascript"></script>
    <script src="Scripts/CustomScripts.js" type="text/javascript"></script>
    </form>
</body>
</html>
