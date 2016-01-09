<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvancedUserSearch.aspx.cs" Inherits="FixAMz_WebApplication.AdvancedUserSearch" %>

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
                        <img src="img/fixamz.png" class="logo" />
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
                                    <li><a href="AdminUserPeopleTab.aspx">PEOPLE</a> </li>
                                    <li><a href="AdminUserSystemTab.aspx">SYSTEM</a> </li>
                                    <li><a href="AdminAboutUs.aspx">ABOUT</a> </li>
                                    <li><a href="AdminHelpTab.aspx">HELP</a> </li>
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
<!--Advanced user search-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/SearchIcon.png" />
                        <div class="" id="AdvancedUserSearchHeader">
                            Advanced User Search</div>
                        <div class="expand-item-content" id="AdvancedUserSearchContent" runat="server" style="display:block">
                            <div class="col-md-8">
                            <div id="UserSearchInitState" runat="server">
                                <div class="info-div">
                                    Enter any information you have on the user to search.</div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Cost Center</div>
                                    <asp:DropDownList ID="SearchUserCostNameDropDown" class="expand-item-textbox" runat="server" >
                                    </asp:DropDownList>
                                    <div class="validator" id="SearchUserCostNameValidator" runat="server"></div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        First Name</div>
                                    <asp:TextBox ID="SearchFirstNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Last Name</div>
                                    <asp:TextBox ID="SearchLastNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Email</div>
                                    <asp:TextBox ID="SearchEmailTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Contact</div>
                                    <asp:TextBox ID="SearchContactTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                               <!-- <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Username</div>
                                    <asp:TextBox ID="SearchUsernameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div> -->
                                <div class="row expand-item-row">
                                    <asp:Button ID="SearchUserBtn" runat="server" Text="Search" class="expand-item-btn"
                                        OnClick="SearchUserBtn_Click" OnClientClick="return isValidUserSearch()" />
                                    <asp:Button ID="CancelSearchBtn" runat="server" Text="Cancel" class="expand-item-btn"
                                        OnClick="cancel_clicked" />
                                </div>
                            </div>
                             
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="adv-user-search-icon">
                                </div>
                            </div>
                        </div>  
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="padding: 0px 30px 0px 30px;">
                <div id="UserSearchSecondState" runat="server">
                      <asp:GridView ID="UserSearchGridView" runat="server" CssClass="table table-hover table-bordered" ></asp:GridView>
                </div>
            </div>
        </div>
<!--Footer-->
        <div id="footer" class="row">
            <div class="row footer-up">
                <ul class="footer-nav">
                    <li><a href="AdminAboutUs.aspx">About</a></li>
                    <li><a href="AdminHelpTab.aspx">Help</a></li>
                    <li><a href="AdminSitemap.aspx">Site map</a></li>
                </ul>
            </div>
            <div class="row footer-down">
                <div class="col-md-5">
                    Copyright &copy; 2015 National Water Supply and Drainage Board.<br>
                    All Rights Reserved.</div>
                <div class="col-md-7 developer-link">
                    <a href="#">Developer site<img src="img/developerIcon.png" /></a>
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