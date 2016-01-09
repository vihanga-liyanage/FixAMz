<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUserHomeTab.aspx.cs" Inherits="FixAMz_WebApplication.AdminUserHomeTab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                                        <li><a href="Help.aspx">HELP</a> </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

<!--Main content-->
            <div class="row" style="min-height: 270px;">
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-aqua" style="margin-left: 30px;margin-top: 30px;">
                        <div class="inner">
                            <div id="totalusers" runat="server" class="custom-label newstyle" ></div>

                            <p style="padding-top: 15px;">Total Users</p>
                        </div>
                        <div class="icon">
                            <img src="img/multy-user.png"  />
                        </div>
                    <a href="AdvancedUserSearch.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-aqua" style="margin-left: 30px;margin-top: 30px;">
                        <div class="inner">
                            <div id="totalcats" runat="server" class="custom-label newstyle" ></div>

                            <p style="padding-top: 15px;">Total Categories</p>
                        </div>
                        <div class="icon">
                            <img src="img/Fixed-Assets-Icon.png" />
                        </div>
                    <a href="MoreinfoCategory.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-aqua" style="margin-left: 30px;margin-top: 30px;">
                        <div class="inner">
                            <div id="totalsubcats" runat="server" class="custom-label newstyle" ></div>

                            <p style="padding-top: 15px;">Total Sub Categories</p>
                        </div>
                        <div class="icon">
                            <img src="img/sub.png"  />
                        </div>
                    <a href="MoreinfoSubcategory.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                    </div>
                </div>

                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="small-box bg-aqua" style="margin-left: 30px;margin-top: 30px;">
                        <div class="inner">
                            <div id="totalcostcenter" runat="server" class="custom-label newstyle" ></div>

                            <p style="padding-top: 15px;">Total Cost Centers</p>
                        </div>
                        <div class="icon">
                            <img src="img/costcenter.png"/>
                        </div>
                    <a href="MoreinfoCostcenter.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
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
