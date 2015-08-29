<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUserSystemTab.aspx.cs" Inherits="FixAMz_WebApplication.AdminUserSystemTab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
                            <div id="user-name">
                                System Admin | <a href="#">Sign out</a>
                            </div>
                        </div>
                        <ul class="custom-nav-bar nav nav-tabs">
                            <li><a href="#">HOME</a> </li>
                            <li><a href="AdminUserPeopleTab.aspx">PEOPLE</a></li>
                            <li><a href="#"><u>SYSTEM</u></a> </li>
                            <li><a href="#">ABOUT</a> </li>
                            <li><a href="#">HELP</a> </li>
                        </ul>
                    </div>

                    
                  
                    
                  
                </div>
                 
                  </div>               
        
        
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10 expand-item-container">
                    <div id="responseArea" runat="server"></div>
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/AddNewIcon.png" />
                            <div class="expand-item-title">Add Location</div>
                            <div class="expand-item-content">
                                <div class="col-md-8">
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <asp:TextBox ID="AddLocationNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Address</div>
                                        <asp:TextBox ID="AddLocationAddressTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Contact</div>
                                        <asp:TextBox ID="AddLocationContactTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Type</div>
                                        <asp:DropDownList ID="AddLocationTypeList" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Branch" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Head Office" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Manager Office</div>
                                        <asp:DropDownList ID="AddLocationManagerOfficeList" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="Select Manager Office" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Head Office, Rathmalana" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Colombo" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Department</div>
                                        <asp:DropDownList ID="AddLocationDepartmentList" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="IT" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Finance" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Branch</div>
                                        <asp:DropDownList ID="AddLocationBranchList" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Zonal Office</div>
                                        <asp:DropDownList ID="AddLocationZonalOfficeList" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="Western" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <asp:Button ID="AddUserBtn" runat="server" Text="Add Location" class="expand-item-btn" />
                                        <asp:Button ID="CancelBtn" runat="server" Text="Cancel" class="expand-item-btn" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <div id="Div2" runat="server"></div>
                            <img src="img/AddNewIcon.png" />
                            <div class="expand-item-title">Add Category</div>
                            <div class="expand-item-content">
                                <div class="col-md-8">
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Category ID</div>
                                        <div id="AddNewCatID" runat="server"></div>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <asp:TextBox ID="AddCategoryNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <asp:Button ID="AddCategoryUserBtn" runat="server" Text="Add Category"
                                            class="expand-item-btn-addcategory" OnClick="AddCategoryBtn_Click" OnClientClick="return isValidAddCat()" />
                                        <asp:Button ID="AddCategoryCancelBtn" runat="server" Text="Cancel" class="expand-item-btn"
                                            OnClientClick="return addCategoryClearAll()" />
                                    </div>
                                </div>
                                <div class="col-md-4" style="position: relative; padding-left: 0px;">
                                    <div class="add-categoy-icon"></div>
                                    <div class="validator-container">
                                        <div class="validator" id="AddCategoryValidator" runat="server"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/AddNewIcon.png" />
                            <div class="expand-item-title">Add Sub Category</div>
                            <div class="expand-item-content">
                                <div class="col-md-8">
                             </div>

                            </div>
                        </div>
                    </div>


                
                                

                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/UpdateIcon.png" />
                            <div class="expand-item-title">Update Location</div>
                            <div class="expand-item-content">
                                <div class="col-md-8">
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Name</div>
                                        <asp:TextBox ID="TextBox1" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Address</div>
                                        <asp:TextBox ID="TextBox2" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Contact</div>
                                        <asp:TextBox ID="TextBox3" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Type</div>
                                        <asp:DropDownList ID="DropDownList1" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Branch" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Head Office" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Manager Office</div>
                                        <asp:DropDownList ID="DropDownList2" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="Select Manager Office" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Head Office, Rathmalana" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Colombo" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Department</div>
                                        <asp:DropDownList ID="DropDownList3" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="IT" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Finance" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Branch</div>
                                        <asp:DropDownList ID="DropDownList4" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="-" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Zonal Office</div>
                                        <asp:DropDownList ID="DropDownList5" class="expand-item-textbox" runat="server">
                                            <asp:ListItem Text="Western" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="expand-item-row">
                                        <asp:Button ID="Button1" runat="server" Text="Add Location" class="expand-item-btn" />
                                        <asp:Button ID="Button2" runat="server" Text="Cancel" class="expand-item-btn" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                                   
<!-- Update Category-->                
                <div class="row expand-item">
                    <div class="col-md-12">
                    <div id="Div1" runat="server"></div>
                        <img src="img/UpdateIcon.png" />
                        <div class="expand-item-title">Update Category</div>
                        <div class="expand-item-content" id="updateCategory" runat="server">
                            <div class="col-md-8">
                                <div id="updatecategoryrInitState" runat="server">
                                    <div class="info-div">Enter category ID to start </div>                                
                                <div class="expand-item-row">
                                    <div class="expand-item-label">Category ID</div>
                                    <asp:TextBox ID="UpdateCategoryTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                </div>
                                <div class="expand-item-row"> 
                                    <asp:Button ID="CatFindBtn" runat="server" Text="Find" class="expand-item-btn" OnClientClick="return isValidCategoryCatID()" onclick="CatFindBtn_Click"  />
                                </div>
                                </div>

                                <div id="updateCategorySecondState" runat="server">
                                    <div class="expand-item-row"> 
                                        <div class="expand-item-label">Category ID</div>
                                        <div id="UpdateCatID" runat="server"></div>   
                                    </div>
                                    <div class="expand-item-row">
                                        <div class="expand-item-label">Category Name</div>
                                        <asp:TextBox ID="UpdateCatNameTextBox" class="expand-item-textbox" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="expand-item-row">
                                        <asp:Button ID="UpdateCatBtn" runat="server" Text="Update Category" class="expand-item-btn-addcategory" 
                                           OnClientClick="return isValidUpdateCat()"  onclick="UpdateCatBtn_click" />
                                        <asp:Button ID="updateCatCancelBtn" runat="server" Text="Cancel" class="expand-item-btn" 
                                            OnClientClick="return updateCategoryClearAll() " />
                                    </div> 
                                </div>
                             </div>
                        <div class="col-md-4" style="position:relative; padding-left:0px;">
                                <div class="update-category-icon"></div>
                                <div class="validator-container">
                                    <div class="validator" id="UpdateCatIDValidator" runat="server"></div>
                                    <div class="validator" id="UpdateCategoryNameValidator" runat="server"></div>
                                </div>
                            </div>
                        </div>
                     </div>
                  </div>
          
            
                
                    <div class="row expand-item">
                        <div class="col-md-12">
                            <img src="img/UpdateIcon.png" />
                            <div class="expand-item-title">Update Sub Category</div>
                            <div class="expand-item-content">
                                <div class="col-md-8">

                                </div>

                            </div>



                        </div>
                    </div>
                </div>
                <div class="col-md-1"></div>
            </div>
             <div class="col-lg-1"></div>
        </div>
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

