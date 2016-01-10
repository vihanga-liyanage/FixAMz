﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoreinfoSubcategory.aspx.cs" Inherits="FixAMz_WebApplication.MoreinfoSubcategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                        <li><a href="AdminUserHomeTab.aspx" class="active">HOME</a> </li>
                                        <li><a href="AdminUserPeopleTab.aspx">PEOPLE</a> </li>
                                        <li><a href="AdminUserSystemTab.aspx">SYSTEM</a> </li>
                                        <li><a href="AboutUs.aspx">ABOUT</a> </li>
                                        <li><a href="AdminHelpTab.aspx">HELP</a> </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
<!--Main contain-->
            <div  style="min-height: 500px;" class="row">
                <div id="new" class="col-md-4 col-md-offset-1" style="min-height: 500px;">
                    <div class="" id="AdvancedUserSearchHeader">
                        <h3>
                            <span>All Sub Categories (<span id="totsubcats" runat="server" class="custom-label"></span>)</span>
                        </h3>
                    </div>
                    <asp:GridView ID="SubCategorySearchGridView" runat="server" CssClass="table table-hover table-bordered" ></asp:GridView>
                </div>
                <div class="col-md-4 col-md-offset-2" style="position: relative; padding-left: 0px;">
                    <div class="admin-subcategory-icon hidden-sm hidden-xs" style="margin-top: 60px;"></div>
                </div>
            </div>
<!--Footer-->
            <div id="footer" class="row" style="margin-top: 0px;">
                <div class="row footer-up">
                    <ul class="footer-nav">
				        <li><a href="AboutUs.aspx">About</a></li>
				        <li><a href="AdminHelpTab.aspx">Help</a></li>
				        <li><a href="AdminSitemap.aspx">Site map</a></li>
			        </ul>
                </div>
                <div class="row footer-down">
                    <div class="col-md-5">Copyright &copy; 2015 National Water Supply and Drainage Board.<br>All Rights Reserved.</div>
		        
                    <div class="col-md-7 developer-link">    
                        <a href="#">Developer site<img src="img/developerIcon.png" /></a>
                    </div>
                    <img src="img/logoSimble.png" style="float:right; width:47px; opacity:0.25" />
                </div>
            </div>

        </div>
            <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
            <script src="Scripts/JQuery-1.11.3.min.js" type="text/javascript"></script>
            <script src="Scripts/CustomScripts.js" type="text/javascript"></script>
    </form>
</body>
</html>
