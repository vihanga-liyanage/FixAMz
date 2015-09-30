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
                            <div class="col-md-3">
                                <img src="img/fixamz.png" class="logo" />
                            </div>
                            <div class="col-md-5 logo-text hidden-xs hidden-sm">
                                The Web Based Asset Management System

                            </div>
                            <div class="col-md-4" style="padding-top: 10px; padding-right: 0px; text-align: right;">
                                <asp:TextBox ID="SearchTextBox" class="search-box" runat="server"></asp:TextBox>
                                <div id="user-name-box">
                                    <span id="userName" runat="server"></span> | <a id="A1" href="#" runat="server" onserverclick="SignOutLink_clicked">Sign out</a>
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
                    <div id="responseArea" runat="server"></div>
<!--Register New Asset-->
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/AddNewIcon.png" />
                            <div class="expand-item-title" id="AddNewUserHeader">Register New Asset</div>
                            <div class="expand-item-content" id="AddNewUserContent">
                                <div class="col-md-8">
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Asset ID</div>
                                        <div id="AssetId" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <asp:TextBox ID="RegisterAssetNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Subcategory</div>
                                        <asp:DropDownList ID="SubCategoryDropDownList" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Category</div>
                                        <asp:DropDownList ID="CategoryDropDownList" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Value</div>
                                        <asp:TextBox ID="AddValueTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Location</div>
                                        <asp:DropDownList ID="LocationDropDownList" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Owner</div>
                                        <asp:DropDownList ID="OwnerDropDownList" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Person to recommend</div>
                                        <asp:DropDownList ID="PersonToRecommendDropDownList" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="row expand-item-row">
                                            <asp:Button ID="Button5" runat="server" Text="Send for recommendation" class="expand-item-btn" />
                                            <asp:Button ID="Button6" runat="server" Text="Cancel" class="expand-item-btn" />
                                    </div>
                                </div>
                                <div class="col-md-4" style="position:relative; padding-left:0px;">
                                    <div class="register-new-asset-icon hidden-sm hidden-xs"></div>
                                </div>
                            </div>
                        </div>
                    </div>

<!--Advanced Assets Search-->
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/SearchIcon.png" />
                            <div class="expand-item-title" id="AdvancedAssetSearchHeader">Advanced assets search</div>
                            <div class="expand-item-content" id="AdvancedAssetSearchContent">
                                <div class="col-md-8">

                                </div>
                                <div class="col-md-4" style="position:relative; padding-left:0px;">
                                    <div class="advanced-asset-search-icon hidden-sm hidden-xs"></div>
                                </div>
                            </div>
                        </div>
                    </div>

<!--Transfer Assets-->
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/TransferIcon.png" />
                            <div class="expand-item-title" id="TransferAssetHeader">Transfer assets</div>
                            <div class="expand-item-content" id="TransferAssetContent">
                                <div class="col-md-8">
                                    <div class="info-div">You can update the owner or the location of the asset.</div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <div id="Div14" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Category</div>
                                        <div id="Div15" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Sub category</div>
                                        <div id="Div16" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Location</div>
                                        <div id="Div17" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Owner</div>
                                        <div id="Div18" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Value</div>
                                        <div id="Div19" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">New owner</div>
                                        <asp:TextBox ID="TextBox9" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">New location</div>
                                        <asp:TextBox ID="TextBox7" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Person to recommend</div>
                                        <asp:TextBox ID="TextBox8" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="row expand-item-row">
                                            <asp:Button ID="Button3" runat="server" Text="Send for recommendation" class="expand-item-btn" />
                                            <asp:Button ID="Button4" runat="server" Text="Cancel" class="expand-item-btn" />
                                    </div>
                                
                                </div>
                                <div class="col-md-4" style="position:relative; padding-left:0px;">
                                    <div class="transfer-asset-icon hidden-sm hidden-xs"></div>
                                </div>
                            </div>
                        </div>
                    </div>

<!--Upgrade Asset-->
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/UpdateIcon.png" />
                            <div class="expand-item-title" id="UpdateAssetHeader">Upgrade asset</div>
                            <div class="expand-item-content" id="UpdateAssetContent">
                                <div class="col-md-8">
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <div id="Div20" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Category</div>
                                        <div id="Div21" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Sub category</div>
                                        <div id="Div22" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Location</div>
                                        <div id="Div23" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Owner</div>
                                        <div id="Div24" runat="server" class="custom-label"></div>
                                    </div>
                                    <div class="row expand-item-row">
                                        <div class="expand-item-label">Value</div>
                                        <div id="Div25" runat="server" class="custom-label"></div>
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
                                <div class="col-md-4" style="position:relative; padding-left:0px;">
                                    <div class="upgrade-asset-icon hidden-sm hidden-xs"></div>
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
                                            <div class="expand-item-label">Employee ID</div>
                                            <asp:TextBox ID="DisposeAssetIDTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                            <div class="validator" id="DisposeAssetIDValidator" runat="server"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <asp:Button ID="DisposeAssetFindBtn" runat="server" Text="Go" 
                                                class="expand-item-btn" OnClientClick="return isValidDeleteEmpID()" 
                                                onclick="DisposeAssetFindBtn_Click" />
                                        </div>
                                    </div>
                                    <div id="disposeAssetSecondState" runat="server">
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Name</div>
                                            <div id="DisposeItemName" runat="server" class="custom-label"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Category</div>
                                            <div id="DisposeCategory" runat="server" class="custom-label"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Sub category</div>
                                            <div id="DisposeSubcategory" runat="server" class="custom-label"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Location</div>
                                            <div id="DisposeLocation" runat="server" class="custom-label"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Owner</div>
                                            <div id="DisposeOwner" runat="server" class="custom-label"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Value</div>
                                            <div id="DisposeValue" runat="server" class="custom-label"></div>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Description</div>
                                            <asp:TextBox ID="TextBox5" class="expand-item-textbox" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="row expand-item-row">
                                            <div class="expand-item-label">Person to recommend</div>
                                            <asp:DropDownList ID="DisposeAssetPersonToRecommendDropDownList" class="expand-item-textbox" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="row expand-item-row">
                                                <asp:Button ID="Button1" runat="server" Text="Send for recommendation" class="expand-item-btn" />
                                                <asp:Button ID="Button2" runat="server" Text="Cancel" class="expand-item-btn" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4" style="position:relative; padding-left:0px;">
                                    <div class="dispose-asset-icon hidden-sm hidden-xs"></div>
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
                        <div class="col-md-5">Copyright &copy; 2015 National Water Supply and Drainage Board.<br>All Rights Reserved.</div>
		        
                        <div class="col-md-7 developer-link">    
                            <a href="#">Developer site<img src="img/developerIcon.png" /></a>
                        </div>
                        <img src="img/logoSimble.png" style="float:right; width:47px; opacity:0.25" />
                    </div>
                </div>
            </div>
            <script src="Scripts/JQuery-1.11.3.min.js" type="text/javascript"></script>
            <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
            <script src="Scripts/CustomScripts.js" type="text/javascript"></script>
        </form>
    </body>
</html>
