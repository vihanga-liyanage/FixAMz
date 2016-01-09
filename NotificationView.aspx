<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotificationView.aspx.cs" Inherits="FixAMz_WebApplication.NotificationView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
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
                                    <li><a href="ManageAssetsUser.aspx">HOME</a> </li>
                                    <li><a href="">ABOUT</a> </li>
                                    <li><a href="">HELP</a> </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<!--Main content-->
        <div class="row">
            <div class="col-md-10 col-xs-offset-1 expand-item-container">
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
            </div>
        </div>
<!--notification content -->
    <div class="row">
    <div class="item-title"><div id="NotificationHeader" runat="server" class="custom-label"></div></div>
    <div class="expand-item-row" runat="server">
        <div class="col-md-8">
<!-- Default notification content-->
            <div id="NotificationContent" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">Asset ID</div>
                    <div id="AssetID" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Name</div>
                    <div id="AssetName" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Category</div>
                    <div id="AssetCategory" runat="server" class="custom-label">
                </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Subcategory</div>
                    <div id="AssetSubcategory" runat="server" class="custom-label">
                </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Value (LKR)</div>
                   <div id="AssetValue" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Salvage Value (LKR)</div>
                    <div id="AssetSalvageValue" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Owner</div>
                    <div id="AssetOwner" runat="server" class="custom-label">
                </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Location</div>
                    <div id="AssetLocation" runat="server" class="custom-label">
                </div>
                </div>
            </div> 

            <!-- Editable content-->
            <div id="EditableNotificationContent" runat="server">
                <div class="col-md-8">
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Asset ID</div>
                        <div id="AddNewAssetId" runat="server" class="custom-label">
                        </div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Name</div>
                        <asp:TextBox ID="AssetNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                        <div class="validator" id="RegisterAssetNameValidator" runat="server">
                        </div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Category</div>
                        <asp:DropDownList ID="AddAssetCategoryDropDown" class="expand-item-textbox" runat="server" OnSelectedIndexChanged="Category_Selected_for_register" AutoPostBack="true">
                        </asp:DropDownList>
                        <div class="validator" id="AddAssetCategoryValidator" runat="server"></div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Subcategory</div>
                        <asp:DropDownList ID="AddAssetSubCategoryDropDown" class="expand-item-textbox" runat="server">
                            <asp:ListItem Text="-- Select Subcategory --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <div class="validator" id="AddAssetSubCategoryValidator" runat="server"></div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Value (LKR)</div>
                        <asp:TextBox ID="AddValueTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                        <div class="validator" id="AddValueValidator" runat="server">
                        </div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Salvage Value (LKR)</div>
                        <asp:TextBox ID="AddSalvageValueTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                        <div class="validator" id="AddSalvageValueValidator" runat="server">
                        </div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Location</div>
                        <asp:DropDownList ID="AddAssetLocationDropDown" class="expand-item-textbox" runat="server">
                        </asp:DropDownList>
                        <div class="validator" id="AddAssetLocationValidator" runat="server"></div>
                    </div>
                    <div class="row expand-item-row">
                        <div class="expand-item-label">Owner</div>
                        <asp:DropDownList ID="AddAssetOwnerDropDown" class="expand-item-textbox" runat="server">
                        </asp:DropDownList>
                        <div class="validator" id="AddAssetOwnerValidator" runat="server"></div>
                    </div>
                    <div class="row expand-item-row">
                        <asp:Button ID="AddAssetRecommendBtn" runat="server" Text="Send for recommendation"
                            OnClick="SendForRecAgainBtn_click" OnClientClick="return isValidAddAsset()" class="expand-item-btn" />
                        <asp:Button ID="Button14" runat="server" Text="Cancel" class="expand-item-btn" OnClick="AddNewAssetBack_Click" />
                    </div>
                </div>
                <!--<div class="col-md-4" style="position: relative; padding-left: 0px;">
                    <div class="register-new-asset-icon hidden-sm hidden-xs">
                    </div>
                </div>-->
            </div>

<!-- Update notification content-->
            <div id="UpdateNotificationContent" runat="server">
                <!-- content-->
            </div>
<!-- AddnewassetState-->
            <div id="AddnewassetState" runat="server">
                <div class="row expand-item-row">
                    <asp:Button ID="AddNewAssetSendforapprove" runat="server" Text="Send for approve" class="expand-item-btn" OnClick="AddNewAssetSendapprove_Click" />
                    <asp:Button ID="AddNewAssetapprovecancel" runat="server" Text="Cancel" class="expand-item-btn"
                         OnClick="AddNewAssetSendapprovecancel_Click" />
                </div>
            </div>
<!-- AddnewassetStateApprove-->
            <div id="AddnewassetStateApprove" runat="server">
                <div class="row expand-item-row">
                    <asp:Button ID="Button1" runat="server" Text="Approve" class="expand-item-btn" OnClick="AddNewAssetapprove_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Back" class="expand-item-btn"  OnClientClick="JavaScript:window.history.back(1);return false;"
                          />
                </div>
            </div>
<!-- AddnewassetStateApprove cancel-->
            <div id="AddnewassetStateApproveCancel" runat="server">
                <div class="row expand-item-row">
                    <asp:Button ID="Button4" runat="server" Text="Back" class="expand-item-btn"  OnClick="AddNewAssetBack_Click"/>
                </div>
            </div>

<!-- UpgradeassetState-->
            <div id="UpgradeassetState" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">Upgrade cost</div>
                    <div id="UpgradeCost" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Description</div>
                    <div id="UpgradeDescription" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="UpgradeAssetsendforapprove" runat="server" Text="Send for approve"  class="expand-item-btn" OnClick="UpgradeAssetsendapprove_Click" />
                    <asp:Button ID="UpgradeAssetapprovecancel" runat="server" Text="Cancel" class="expand-item-btn"
                        OnClick="UpgradeAssetsendapprovecancel_Click" OnClientClick="return UpgradeAssetapprovecancel()"/>
                </div>
            </div>
<!-- UpgradeassetApprove-->
            <div id="UpgradeassetApprove" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">Upgrade cost</div>
                    <div id="UpgradeCostApprove" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">Description</div>
                    <div id="UpgradeDescriptionApprove" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="Button3" runat="server" Text="Approve"  class="expand-item-btn" OnClick="UpgradeAssetapprove_Click" />
                    <asp:Button ID="Button5" runat="server" Text="Back" class="expand-item-btn" OnClick="upgradeAssetBack_Click" />
                </div>
            </div>
<!-- UpgradeassetApprove cancel-->
            <div id="UpgradeassetStateApproveCancel" runat="server">
                <div class="row expand-item-row">
                    <asp:Button ID="Button6" runat="server" Text="Back" class="expand-item-btn"  OnClick="upgradeAssetBack_Click"/>
                </div>
            </div>

<!-- TransferassetState-->
            <div id="TransferassetState" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">New location</div>
                    <div id="TransferNewlocation" style="background-color:#DCDCDC;" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">New owner</div>
                    <div id="TransferNewowner" style="background-color:#DCDCDC;" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="TransferAssetsendforapprove" runat="server" Text="Send for approve"  class="expand-item-btn" OnClick="TransferAssetSendapprove_Click" />
                    <asp:Button ID="TransferAssetapprovecancel" runat="server" Text="Cancel" class="expand-item-btn"
                        OnClick="TransferAssetSendapprovecancel_Click" />
                </div>
            </div>
<!-- TransferassetApproveState-->
            <div id="TransferassetApproveState" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">New location</div>
                    <div id="TransferAssetnewlocation" style="background-color:#DCDCDC;" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">New owner</div>
                    <div id="TransferAssetnewowner" style="background-color:#DCDCDC;" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="Button9" runat="server" Text="Approve"  class="expand-item-btn" OnClick="TransferAssetapprove_Click" />
                    <asp:Button ID="Button11" runat="server" Text="Back" class="expand-item-btn"
                        OnClick="transferAssetBack_Click" />
                </div>
            </div>
<!-- TransferassetCancelState-->
            <div id="TransferassetCancelState" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">New location</div>
                    <div id="TransfernewlocationCancel" style="background-color:#DCDCDC;" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <div class="expand-item-label">New owner</div>
                    <div id="TransfernewownerCancel" style="background-color:#DCDCDC;" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="Button13" runat="server" Text="Back" class="expand-item-btn"
                        OnClick="transferAssetBack_Click" />
                    <asp:Button ID="Button12" runat="server" Text="Edit" class="expand-item-btn"
                         OnClientClick="return transferAssetEdit_Click" />
                </div>
            </div>

<!-- DisposeassetState-->
            <div id="DisposeassetState" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">Description</div>
                    <div id="DisposeDescription" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="DisposeAssetsendforapprove" runat="server" Text="Send for approve"  class="expand-item-btn" OnClick="DisposeAssetsendapprove_Click" />
                    <asp:Button ID="DisposeAssetapprovecancel" runat="server" Text="Cancel" class="expand-item-btn"
                        OnClick="DisposeAssetcancel_Click" />
                </div>
            </div>
<!-- DisposeassetApprove-->
            <div id="DisposeassetApprove" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">Description</div>
                    <div id="DisposeassetApproveDescription" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="Button7" runat="server" Text="Approve"  class="expand-item-btn" OnClick="DisposeAssetapprove_Click" />
                    <asp:Button ID="Button8" runat="server" Text="Back" class="expand-item-btn" OnClick="DisposeAssetBack_Click"
                        />
                </div>
            </div>
<!-- DisposeassetCancel-->
            <div id="DisposeassetCancel" runat="server">
                <div class="row expand-item-row">
                    <div class="expand-item-label">Description</div>
                    <div id="DisposeassetCancelDescription" runat="server" class="custom-label">
                    </div>
                </div>
                <div class="row expand-item-row">
                    <asp:Button ID="Button10" runat="server" Text="Back" class="expand-item-btn"
                        OnClick="DisposeAssetBack_Click" />
                </div>
            </div>

        </div>
        <div class="col-md-4" style="position: relative; padding-left: 0px;">
            <div class="notification-icon hidden-sm hidden-xs">
            </div>
        </div>
    </div>
    </div>
<!--Footer-->
            <div id="footer" class="row">
                <div class="row footer-up">
                    <ul class="footer-nav">
				        <li><a href="#">About</a></li>
				        <li><a href="#">help</a></li>
				        <li><a href="#">site map</a></li>
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
