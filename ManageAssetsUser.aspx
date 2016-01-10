<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageAssetsUser.aspx.cs" Inherits="FixAMz_WebApplication.ManaageAssetsUser" %>

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
                            <div id="manageAssetUserNavBar" runat="server" style="display:none;">
                                <div class="collapse navbar-collapse" style="float:right;">
                                    <ul class="custom-nav-bar nav nav-tabs navbar-nav">
                                        <li><a href="" class="active">HOME</a> </li>
                                        <li><a href="AboutUs.aspx">ABOUT</a> </li>
                                        <li><a href="Help.aspx">HELP</a> </li>
                                    </ul>
                                </div>
                            </div>
                            <div id="manageReportNavBar" runat="server" style="display:none;">
                                <div class="collapse navbar-collapse" style="float:right;">
                                    <ul class="custom-nav-bar nav nav-tabs navbar-nav">
                                        <li><a href="" class="active">HOME</a> </li>
                                        <li><a href="ReportViewer.aspx">Reports</a> </li>
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
<!--Register New Asset-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AddNewAssetHeader">
                            Register New Asset
                        </div> 
                        <div class="expand-item-content" id="AddNewAssetContent" runat="server">
                            <div class="col-md-8">
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Asset ID</div>
                                    <div id="AddNewAssetId" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Cost Center Name</div>
                                    <div id="AddNewCostID" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Name</div>
                                    <asp:TextBox ID="RegisterAssetNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
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
                                    <div class="expand-item-label">Person to recommend</div>
                                    <div id="AddAssetPersonToRecommend" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <asp:Button ID="AddAssetRecommendBtn" runat="server" Text="Send for recommendation"
                                        OnClick="AddAssetRecommendBtn_Click" OnClientClick="return isValidAddAsset()" class="expand-item-btn" />
                                    <asp:Button ID="Button6" runat="server" Text="Cancel" class="expand-item-btn" onClick="cancel_clicked"/>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="register-new-asset-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Advanced Assets Search-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/SearchIcon.png" />
                        <div class="expand-item-title" id="AdvancedAssetSearchHeader">
                            <a href="AdvancedAssetSearch.aspx">Advanced assets search</a>
                        </div>
                    </div>
                </div>
<!--Transfer Assets-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/TransferIcon.png" />
                        <div class="expand-item-title" id="TransferAssetHeader">Transfer assets</div>
                        <div class="expand-item-content" id="TransferAssetContent" runat="server">
                            <div class="col-md-8">
                                <div id="transferAssetInitState" runat="server">
                                    <div class="info-div">Enter asset ID to transfer the asset</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Asset ID</div>
                                        <asp:TextBox ID="TransferAssetIDTextBox" class="expand-item-textbox" runat="server" Text=""></asp:TextBox>
                                        <div class="validator" id="TransferAssetIDValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="TransferAssetFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidAssetID()" OnClick="TransferAssetFindBtn_Click" />
                                    </div>
                                </div>

                                <div id="transferAssetSecondState" runat="server">
                                    <div class="info-div">You can update the owner or the location of the asset.</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <div id="TransferItemName" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Category</div>
                                        <div id="TransferCategory" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Sub category</div>
                                        <div id="TransferSubcategory" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Value</div>
                                        <div id="TransferValue" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Location</div>
                                        <asp:DropDownList ID="TransferLocationDropDown" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Owner</div>
                                        <asp:DropDownList ID="TransferOwnerDropDown" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Person to recommend</div>
                                        <div id="TransAssetSendForRecommend" runat="server" class="custom-label"></div>
                                    </div>
                                    <!--<div class="row expand-item-row">
                                        <div class="expand-item-label">Person to recommend</div>
                                        <asp:DropDownList ID="TransAssetSendForRecommendDropDown" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                        <div class="validator" id="TransAssetSendForRecommendValidator" runat="server"></div>
                                    </div>-->
                                    <div class="row expand-item-row">
                                        <asp:Button ID="TransferAssetRecommend" runat="server" Text="Send for recommendation" 
                                             onClick="TransferAssetRecommendBtn_click" OnClientClick="return isValidTransferAsset()" class="expand-item-btn" />
                                        <asp:Button ID="TransferAssetcancel" runat="server" Text="Cancel" class="expand-item-btn"
                                            onClick="cancel_clicked"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="transfer-asset-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Upgrade Asset-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/UpdateIcon.png" />
                        <div class="expand-item-title" id="UpgradeAssetHeader">Upgrade asset</div>
                        <div class="expand-item-content" id="UpgradeAssetContent" runat="server">
                            <div class="col-md-8">
                                <div id="upgradeAssetInitState" runat="server">
                                    <div class="info-div">Enter asset ID to upgrade the asset</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Asset ID</div>
                                        <asp:TextBox ID="UpgradeAssetIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="UpgradeAssetIDValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="UpgradeAssetFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidDeleteEmpID()" OnClick="UpgradeAssetFindBtn_Click" />
                                    </div>
                                </div>
                                <div id="upgradeAssetSecondState" runat="server">
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Name</div>
                                    <div id="UpgradeAssetName" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Category</div>
                                    <div id="UpgradeAssetCategory" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Sub category</div>
                                    <div id="UpgradeAssetSubcategory" runat="server" class="custom-label">
                                    </div>
                                </div>
                             <!--   <div class="row expand-item-row">
                                    <div class="expand-item-label">Location</div>
                                    <div id="UpgradeLocation" runat="server" class="custom-label">
                                    </div> 
                                </div>-->
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Owner</div>
                                    <div id="UpgradeOwner" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Value(LKR)</div>
                                    <div id="UpgradeValue" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Upgrade cost</div>
                                    <asp:TextBox ID="UpgradeAssetValueTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="UpgradeAssetValueValidator" runat="server"></div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Description</div>
                                    <asp:TextBox ID="UpgradeAssetDescriptionTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="UpgradeAssetDescriptionValidator" runat="server"></div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Person to recommend</div>
                                    <div id="UpgradeAssetPersonToRecommend" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <asp:Button ID="UpgradeAssetRecommendBtn" OnClick="UpgradeAssetRecommendBtn_Click" runat="server" Text="Send for recommendation" class="expand-item-btn" OnClientClick="return isValidUpgradeAsset()" />

                                    <asp:Button ID="Button8" runat="server" Text="Cancel" class="expand-item-btn" OnClick="cancel_clicked" />

                                    

                                </div>
                            </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="upgrade-asset-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Dispose Asset-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/DeleteIcon.png" />
                        <div class="expand-item-title" id="DisposeAssetHeader">Dispose asset</div>
                        <div class="expand-item-content" id="DisposeAssetContent" runat="server">
                            <div class="col-md-8">
                                <div id="disposeAssetInitState" runat="server">
                                    <div class="info-div">Enter asset ID to dispose the asset</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Asset ID</div>
                                        <asp:TextBox ID="DisposeAssetIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="DisposeAssetIDValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="DisposeAssetFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidDeleteEmpID()" OnClick="DisposeAssetFindBtn_Click" />
                                    </div>
                                </div>
                                <div id="disposeAssetSecondState" runat="server">
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            AssetID</div>
                                        <div id="DisposeAssetID" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <div id="DisposeItemName" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Category</div>
                                        <div id="DisposeCategory" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Sub category</div>
                                        <div id="DisposeSubCategory" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Location</div>
                                        <div id="DisposeLocation" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Owner</div>
                                        <div id="DisposeOwner" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Value</div>
                                        <div id="DisposeValue" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Description</div>
                                        <asp:TextBox ID="DisposeAssetDescriptionTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="DisposeAssetDescriptionValidator" runat="server">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Person to recommend</div>
                                        <div id="DisposeAssetPersonToRecommend" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="DisposeAssetRecommendBtn" OnClick="DisposeAssetRecommendBtn_Click"
                                            OnClientClick="return isValidDisposeAsset()" runat="server" Text="Send for recommendation"
                                            class="expand-item-btn" />
                                        <asp:Button ID="Button2" runat="server" Text="Cancel" class="expand-item-btn" onClick="cancel_clicked" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="dispose-asset-icon hidden-sm hidden-xs">
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
                    <li><a href="ManageAssetUserSitemap.aspx">Site map</a></li>
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
