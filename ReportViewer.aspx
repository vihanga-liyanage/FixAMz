﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="FixAMz_WebApplication.ReportViewer" %>

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
                            <div id="generateReportUserNavBar" runat="server" style="display:none;">
                                <div class="collapse navbar-collapse" runat="server" style="float:right;">
                                    <ul class="custom-nav-bar nav nav-tabs navbar-nav">
                                        <li><a href="" class="active">REPORTS</a> </li>
                                        <li><a href="AboutUs.aspx">ABOUT</a> </li>
                                        <li><a href="Help.aspx">HELP</a> </li>
                                    </ul>
                                </div>
                            </div>
                            <div id="manageReportNavBar" runat="server" style="display:none;">
                                <div class="collapse navbar-collapse" runat="server" style="float:right;">
                                    <ul class="custom-nav-bar nav nav-tabs navbar-nav">
                                        <li><a href="ManageAssetsUser.aspx">HOME</a> </li>
                                        <li><a href="" class="active">REPORTS</a> </li>
                                        <li><a href="AboutUs.aspx">ABOUT</a> </li>
                                        <li><a href="Help.aspx">HELP</a> </li>
                                    </ul>
                                </div>
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
<!-- Reports list-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AddNewAssetHeader">
                            Finance Reports
                        </div> 
                        <div class="expand-item-content" id="AddNewAssetContent" runat="server">
                            <div class="col-md-8">
                                <ul class="report-ul">
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="FinanceMasterReport.aspx">Master Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="ApprovedDateBasedFinanceReport.aspx">Approved Datebased Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="CostCenterwiseFinanceReport.aspx">Cost Centerwise Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="LocationwiseFinanceReport.aspx">Locationwise Report</a></li>
                                    </div>
                                     <div class="row expand-item-row">
                                        <li><a class="report-label" href="CategorywiseFinanceReport.aspx">Categorywise Report</a></li>
                                    </div>
                                </ul>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="report-finance-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="GeneralReportsHeader">
                            General Reports
                        </div> 
                        <div class="expand-item-content" id="GeneralReportsContent" runat="server">
                            <div class="col-md-8">
                                <ul class="report-ul">
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="GeneralMasterReport.aspx">Master Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="CostCenterwiseGeneralReport.aspx">Cost Centerwise Report</a></li>
                                    </div>
                                    <!--<div class="row expand-item-row">
                                        <li><a class="report-label" href="AboutUs.aspx">About</a></li>
                                    </div>-->
                                </ul>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="report-general-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AssetTransferHeader">
                            Asset Transfer Reports
                        </div> 
                        <div class="expand-item-content" id="AssetTransferContent" runat="server">
                            <div class="col-md-8">
                                <ul class="report-ul">
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="TransferMasterReport.aspx">Master Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="CostCenterwiseTransferReport.aspx">Cost Centerwise Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="AssetwiseTransferReport.aspx">Assetwise Report</a></li>
                                    </div>
                                </ul>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="report-transfer-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AssetUpgradeHeader">
                            Asset Upgrade Reports
                        </div> 
                        <div class="expand-item-content" id="AssetUpgradeContent" runat="server">
                            <div class="col-md-8">
                                <ul class="report-ul">
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="UpgradeMasterReport.aspx">Master Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="CostCenterwiseUpgradeReport.aspx">Cost Centerwise Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="AssetwiseUpgradeReport.aspx">Assetwise Report</a></li>
                                    </div>
                                </ul>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="report-upgrade-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AssetDisposeHeader">
                            Asset Dispose Reports
                        </div> 
                        <div class="expand-item-content" id="AssetDisposeContent" runat="server">
                            <div class="col-md-8">
                                <ul class="report-ul">
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="DisposeMasterReport.aspx">Master Report</a></li>
                                    </div>
                                    <div class="row expand-item-row">
                                        <li><a class="report-label" href="CostCenterwiseDisposeReport.aspx">Cost Centerwise Report</a></li>
                                    </div>
                                    
                                </ul>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="report-dispose-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
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
                    <li><a href="#">Site map</a></li>
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