<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageAssetsUser.aspx.cs"
    Inherits="FixAMz_WebApplication.ManaageAssetsUser" %>

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
                    <div class="col-md-3">
                        <img src="img/fixamz.png" class="logo" />
                    </div>
                    <div class="col-md-5 logo-text hidden-xs hidden-sm">
                        The Web Based Asset Management System
                    </div>
                    <div class="col-md-4" style="padding-top: 10px; padding-right: 0px; text-align: right;">
                        <asp:TextBox ID="SearchTextBox" class="search-box" runat="server"></asp:TextBox>
                        <div id="user-name-box">
                            <span id="userName" runat="server"></span>| <a id="A1" href="#" runat="server" onserverclick="SignOutLink_clicked">
                                Sign out</a>
                        </div>
                    </div>
                    <ul class="custom-nav-bar nav nav-tabs">
                        <li><a href="#"><u>HOME</u></a> </li>
                        <li><a href="#">ABOUT</a> </li>
                        <li><a href="#">HELP</a> </li>
                    </ul>
                </div>
            </div>
        </div>
<!--Main content-->
        <div class="row">
            <div class="col-md-10 col-xs-offset-1 expand-item-container">
                <div id="responseArea" runat="server">
                </div>
<!--Register New Asset-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/AddNewIcon.png" />
                        <div class="expand-item-title" id="AddNewUserHeader">
                            Register New Asset</div>
                        <div class="expand-item-content" id="AddNewUserContent">
                            <div class="col-md-8">
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Asset ID</div>
                                    <div id="AddNewAssetId" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Name</div>
                                    <asp:TextBox ID="RegisterAssetNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="RegisterAssetNameValidator" runat="server">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Subcategory</div>
                                    <asp:DropDownList ID="AddAssetSubCategoryDropDown" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Category</div>
                                    <asp:DropDownList ID="AddAssetCategoryDropDown" class="expand-item-textbox" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Value</div>
                                    <asp:TextBox ID="AddValueTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    <div class="validator" id="AddValueValidator" runat="server">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Location</div>
                                    <asp:DropDownList ID="AddAssetLocationDropDown" class="expand-item-textbox" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Owner</div>
                                    <asp:DropDownList ID="AddAssetOwnerDropDown" class="expand-item-textbox" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">
                                        Person to recommend</div>
                                    <asp:DropDownList ID="AddAssetPersonToRecommendDropDown" class="expand-item-textbox"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="row expand-item-row">
                                    <asp:Button ID="AddAssetButttonReco" runat="server" Text="Send for recommendation"
                                        OnClick="SendForRecommendationBtn_Click" OnClientClick="return isValidAddAsset()"
                                        class="expand-item-btn" />
                                    <asp:Button ID="Button6" runat="server" Text="Cancel" class="expand-item-btn" />
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
                            Advanced assets search</div>
                        <div class="expand-item-content" id="AdvancedAssetSearchContent">
                            <div class="col-md-8">
                                <div id="AssetSearchInitState" runat="server">
                                    <div class="info-div">Enter any information you have on the asset, to begin.</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Asset ID</div>
                                        <asp:TextBox ID="AssetSearchIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Name</div>
                                        <asp:TextBox ID="AssetSearchNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Subcategory</div>
                                        <asp:DropDownList ID="AssetSearchSubCategoryDropDown" class="expand-item-textbox"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Category</div>
                                        <asp:DropDownList ID="AssetSearchCategoryDropDown" class="expand-item-textbox" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Value</div>
                                        <asp:TextBox ID="AssetSearchValueTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Location</div>
                                        <asp:DropDownList ID="AssetSearchLocationDropDown" class="expand-item-textbox" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Owner</div>
                                        <asp:DropDownList ID="AssetSearchOwnerDropDown" class="expand-item-textbox" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="SearchAssetBtn" runat="server" Text="Search" class="expand-item-btn"
                                            OnClick="SearchAssetBtn_Click" />
                                        <asp:Button ID="CancelSearchBtn" runat="server" Text="Cancel" class="expand-item-btn" />
                                    </div>
                                </div>
                                <div id="AssetSearchSecondState" runat="server">
                                    <asp:GridView ID="AssetSearchGridView" runat="server"></asp:GridView>
                                </div>
                            </div>
                            <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                <div class="advanced-asset-search-icon hidden-sm hidden-xs">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
<!--Transfer Assets-->
                <div class="row expand-item">
                    <div class="col-md-12">
                        <img src="img/TransferIcon.png" />
                        <div class="expand-item-title" id="TransferAssetHeader">
                            Transfer assets</div>
                        <div class="expand-item-content" id="TransferAssetContent" runat="server">
                            <div class="col-md-8">
                                <div id="transferAssetInitState" runat="server">
                                    <div class="info-div">
                                        Enter asset ID to transfer the asset</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Asset ID</div>
                                        <asp:TextBox ID="TransferAssetIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        <div class="validator" id="TransferAssetIDValidator" runat="server">
                                        </div>

                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="TransferAssetFindBtn" runat="server" Text="Go" class="expand-item-btn"
                                            OnClientClick="return isValidDeleteEmpID()" OnClick="TransferAssetFindBtn_Click" />
                                    </div>
                                </div>
                                <div id="transferAssetSecondState" runat="server">
                                    <div class="info-div">
                                        You can update the owner or the location of the asset.</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Name</div>
                                        <div id="TransferItemName" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Category</div>
                                        <div id="TransferCategory" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Sub category</div>
                                        <div id="TransferSubcategory" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Location</div>
                                        <div id="TransferLocation" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Owner</div>
                                        <div id="TransferOwner" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Value</div>
                                        <div id="TransferValue" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            New owner</div>
                                        <asp:TextBox ID="TextBox9" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            New location</div>
                                        <asp:TextBox ID="TextBox7" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Person to recommend</div>
                                        <asp:TextBox ID="TextBox8" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="Button3" runat="server" Text="Send for recommendation" class="expand-item-btn" />
                                        <asp:Button ID="Button4" runat="server" Text="Cancel" class="expand-item-btn" />
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
                        <div class="expand-item-title" id="UpdateAssetHeader">Upgrade asset</div>
                        <div class="expand-item-content" id="UpdateAssetContent" runat="server">
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
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Location</div>
                                    <div id="UpgradeLocation" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Owner</div>
                                    <div id="UpgradeOwner" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Value</div>
                                    <div id="UpgradeValue" runat="server" class="custom-label">
                                    </div>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Updated value</div>
                                    <asp:TextBox ID="TextBox10" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Description</div>
                                    <asp:TextBox ID="TextBox11" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="row expand-item-row">
                                    <div class="expand-item-label">Person to recommend</div>
                                    <asp:TextBox ID="TextBox12" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="row expand-item-row">
                                    <asp:Button ID="Button7" runat="server" Text="Send for recommendation" class="expand-item-btn" />
                                    <asp:Button ID="Button8" runat="server" Text="Cancel" class="expand-item-btn" />
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
                        <div class="expand-item-title" id="DisposeAssetHeader">
                            Dispose asset</div>
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
                                        <div class="expand-item-label">
                                            Name</div>
                                        <div id="DisposeItemName" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Category</div>
                                        <div id="DisposeCategory" runat="server" class="custom-label">
                                        </div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">
                                            Sub category</div>
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
                                        <div class="expand-item-label">
                                            Person to recommend</div>
                                        <asp:DropDownList ID="DisposeAssetPersonToRecommendDropDown" class="expand-item-textbox"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <asp:Button ID="DisposeAssetRecommendBtn" OnClick="DisposeAssetRecommendBtn_Click"
                                            OnClientClick="return isValidDisposeAssetDescription()" runat="server" Text="Send for recommendation"
                                            class="expand-item-btn" />
                                        <asp:Button ID="Button2" runat="server" Text="Cancel" class="expand-item-btn" />
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
                    <li><a href="#">About</a></li>
                    <li><a href="#">help</a></li>
                    <li><a href="#">site map</a></li>
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
    <script src="Scripts/JQuery-1.11.3.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/CustomScripts.js" type="text/javascript"></script>
    </form>
</body>
</html>
