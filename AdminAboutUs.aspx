<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAboutUs.aspx.cs" Inherits="FixAMz_WebApplication.AdminAboutUs" %>

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
                                    <li><a href="AdminAboutUs.aspx" class="active">ABOUT</a> </li>
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

<!--NWSDB-->
                <div class="row">
                    <img src="img/AboutUsNWSDB.png"/>
                </div>
                        <h3>About Us</h3> <br/>
                      
                        <p>The National Water Supply and Drainage Board had its beginning as a sub department under the Public Works Department, for Water Supply & Drainage. 
                           In 1965, it became a division under the Ministry of Local Government.
                           From 1970, this division functioned as a separate department under the Ministry of Irrigation, 
                           Power & Highways and remained so until the present Board was established in January 1975 by an Act of Parliament.</p>

                           <p>The National Water Supply Drainage Board (NWS&DB), which presently functions under the Ministry of Urban Development, 
                           Water Supply & Drainage is the principal authority providing safe drinking water and facilitating the provision of sanitation in Sri Lanka. 
                           In accordance with the NWS&DB Act, a number of major Urban Water Supply Schemes operated by Local Authorities were taken over by the NWS&DB to provide more coverage and improved service. 
                           Consumer metering and billing commenced in 1982. 
                           Rural Water Supply & Sanitation programmes including deep well programmes are also being implemented by the NWS&DB.</p>

                           <p>During the past years, the organization has considerably expanded its scope of activities.</p>

                           <p>The NWS&DB is presently operating 324 Water Supply Schemes which cover 34% of the total population with pipe borne water supply. 
                           10.5% of the population is served with pipe borne water supply by some Local Authorities, 
                           NGO’s and Community Based Organizations. 
                           13% of the population is served with hand pump tube wells. 
                           NWS&DB hopes to increase the coverage with pipe borne water to 45.7% 
                           by 2015 so that the United Nations Millennium Development Goal of 85% safe drinking water coverage can be achieved by that year.
                           The NWS&DB is also in charge of the sewerage system in Colombo and suburbs, Hantane, Koggala, Hikkaduwa, Kataragama and in few housing schemes.</p>
                    
                           <p><a href="http://www.waterboard.lk">Link to our site - www.waterboard.lk</a></p>
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
