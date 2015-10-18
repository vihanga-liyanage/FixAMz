﻿//Exapand content function
var expandingItems = {};
$(".expand-item-title").click(function () {

    $header = $(this);
    //getting the next element
    $content = $header.next();
    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
    $content.slideToggle(800, function () { });

    //extracting the expand content id
    var headerId = this.id;
    var contentId = headerId.slice(0, headerId.length - 6) + "Content";
    //store the status of expand item in expandedItems object
    if (expandingItems[contentId] != null) {
        expandingItems[contentId] = !expandingItems[contentId];
    } else {
        expandingItems[contentId] = true;
    }
    //get one by one, inactive if active
    var out = "";
    for (var item in expandingItems) {
        if (item != contentId && expandingItems[item] == true) {
            $(document.getElementById(item)).slideToggle(800, function () { });
            expandingItems[item] = false;
        }
        out += item + " : " + expandingItems[item] + "\n";
    }
    document.forms[0]["expandingItemsHiddenField"].val = expandingItems;
    alert(out);
});

//Function to call above .click function manually by code behind, when page reloads occur
function setExpandingItem(id) {
    expandingItems[id] = true;
}

//Global validation functions=================================================================
function requiredFieldValidator(controller, msg) {
    var content = document.forms[0][controller + "TextBox"].value;
    if (content == "") {
        document.getElementById(controller + "Validator").innerHTML = msg;
        document.forms[0][controller + "TextBox"].style.border="1px solid red";
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "TextBox"].style.border = "1px solid #cacaca";
        return true;
    }
}

function emailValidator(controller) {
    var content = document.forms[0][controller + "TextBox"].value;
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (content == "") {
        document.getElementById(controller + "Validator").innerHTML = "Email cannot be empty.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else if (!re.test(content)) {
        document.getElementById(controller + "Validator").innerHTML = "Enter a valid email address.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "TextBox"].style.border = "1px solid #cacaca";
        return true;
    }
}

function contactValidator(controller) {
    var contact = document.forms[0][controller + "TextBox"].value;
    var prefix = contact.substring(0, 3);
    if (contact == "") {
        document.getElementById(controller + "Validator").innerHTML = "Contact cannot be empty.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else if (contact.length != 10) {
        document.getElementById(controller + "Validator").innerHTML = "Please enter a valid contact.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById(controller + "Validator").innerHTML = "Contact cannot have non-digits.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076" || prefix == "078")) {
        document.getElementById(controller + "Validator").innerHTML = "Please enter a valid contact.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "TextBox"].style.border = "1px solid #cacaca";
        return true;
    }
}

function nameValidator(controller) { //A name can only have a-zA-Z
    var content = document.forms[0][controller + "TextBox"].value;
    var re = /^[a-zA-Z\s]+$/;
    if (!re.test(content)) {
        document.getElementById(controller + "Validator").innerHTML = "Enter a valid name.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "TextBox"].style.border = "1px solid #cacaca";
        return true;
    }
}

function dropDownRequiredFieldValidator(controller) {
    var dropdown = document.forms[0][controller + "DropDown"];
    var content = dropdown.options[dropdown.selectedIndex].value;
    //alert("dropDownRequiredFieldValidator " + controller + " " + content);
    if (content == "") {
        document.getElementById(controller + "Validator").innerHTML = "Please select a valid value";
        document.forms[0][controller + "DropDown"].style.border = "1px solid red";
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "DropDown"].style.border = "1px solid #cacaca";
        return true;
    }
}

//Ajax functions for client side database calling
var usernameNotExists = false;
function usernameValidator(){
    $.ajax({
        type: "POST",
        url: "AdminUserPeopleTab.aspx/checkUsername",
        data: "{'Username':'" + document.forms[0]["AddNewUsernameTextBox"].value + "'}",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (success) {
            //called on ajax call success
            if (success.d == 0) {
                document.getElementById("AddNewUsernameValidator").innerHTML = "";
                document.forms[0]["AddNewUsernameTextBox"].style.border = "1px solid #cacaca";
                usernameNotExists = true;
            } else {
                document.getElementById("AddNewUsernameValidator").innerHTML = "User name already exists.";
                document.forms[0]["AddNewUsernameTextBox"].style.border = "1px solid red";
                usernameNotExists = false;
            }
        },
        error: function (xhr, status, error) {
            alert(error);
            return false;
        }
    });
}

//Function to pause the execution
function pause(millis) {
    var date = new Date();
    var curDate = null;

    do { curDate = new Date(); }
    while (curDate - date < millis);
}

//////////////////////////////////////////////////////////////////////////////////////////////
//      Admin people tab
//////////////////////////////////////////////////////////////////////////////////////////////

//Add new user functions =====================================================================
function addNewClearAll() {
    document.forms[0]["AddNewFirstNameTextBox"].value = "";
    document.forms[0]["AddNewLastNameTextBox"].value = "";
    document.forms[0]["AddNewEmailTextBox"].value = "";
    document.forms[0]["AddNewContactTextBox"].value = "";
    document.forms[0]["AddNewUsernameTextBox"].value = "";
    document.forms[0]["AddNewPasswordTextBox"].value = "";
    document.forms[0]["AddNewConfirmPasswordTextBox"].value = "";
    return true;
}

function isValidAddNew() {
    var confirmPassword = document.forms[0]["AddNewConfirmPasswordTextBox"].value;
    var password = document.forms[0]["AddNewPasswordTextBox"].value;

    var isValidFirstName = requiredFieldValidator("AddNewFirstName", "First name cannot be empty.") && nameValidator("AddNewFirstName");
    var isValidLastName = requiredFieldValidator("AddNewLastName", "Last name cannot be empty.") && nameValidator("AddNewLastName");
    var isValidEmail = emailValidator("AddNewEmail");
    var isValidContact = contactValidator("AddNewContact");

    var isValidUsername = requiredFieldValidator("AddNewUsername", "User name cannot be empty.");
    if (isValidUsername) {
        usernameValidator();
        pause(300);
        isValidUsername = usernameNotExists;
        //alert(isValidUsername);
    }
    var isValidPassword = requiredFieldValidator("AddNewPassword", "Password cannot be empty.");

    var isValidConfirmPassword = true;
    if (confirmPassword == "") {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "Confirm password cannot be empty.";
        document.forms[0]["AddNewConfirmPasswordTextBox"].style.border = "1px solid red";
        isValidConfirmPassword = false;
    } else if (confirmPassword != password) {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "Confirm password does not match with password.";
        document.forms[0]["AddNewConfirmPasswordTextBox"].style.border = "1px solid red";
        isValidConfirmPassword = false;
    } else {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "";
        document.forms[0]["AddNewConfirmPasswordTextBox"].style.border = "1px solid #cacaca";
        isValidConfirmPassword = true;
    }
    return (isValidFirstName && isValidLastName && isValidEmail && isValidContact && isValidUsername && isValidPassword && isValidConfirmPassword);
}

//Update user functions ======================================================================
function updateClearAll() {
    document.forms[0]["UpdateFirstNameTextBox"].value = "";
    document.forms[0]["UpdateLastNameTextBox"].value = "";
    document.forms[0]["UpdateEmailTextBox"].value = "";
    document.forms[0]["UpdateContactTextBox"].value = "";
    document.getElementById("updateUserInitState").style.display = "block";
    document.getElementById("updateUserSecondState").style.display = "none";
    document.forms[0]["UpdateEmpIDTextBox"].value = "";
    return false;
}

function isValidUpdateEmpID() {
    return requiredFieldValidator("UpdateEmpID", "Employee ID cannot be empty.");
}

function isValidUpdate() {
    var isValidFirstname = requiredFieldValidator("UpdateFirstName", "First name cannot be empty.") && nameValidator("UpdateFirstName");
    var isValidLastname = requiredFieldValidator("UpdateLastName", "Last name cannot be empty.") && nameValidator("UpdateLastName");
    var isValidEmail = emailValidator("UpdateEmail");
    var isValidContact = contactValidator("UpdateContact");

    return (isValidFirstname && isValidLastname && isValidEmail && isValidContact);
}

//Advanced user search functions =============================================================
function searchClearAll() {
    document.forms[0]["SearchEmployeeIDTextBox"].value = "";
    document.forms[0]["SearchFirstNameTextBox"].value = "";
    document.forms[0]["SearchLastNameTextBox"].value = "";
    document.forms[0]["SearchEmailTextBox"].value = "";
    document.forms[0]["SearchContactTextBox"].value = "";
    document.forms[0]["SearchUsernameTextBox"].value = "";
    return true;
}

//Delete user functions ======================================================================
function isValidDeleteEmpID() {
    return requiredFieldValidator("DeleteUserEmpID", "Employee ID cannot be empty.");
}

//////////////////////////////////////////////////////////////////////////////////////////////
//      Admin system tab
//////////////////////////////////////////////////////////////////////////////////////////////

//Add new location functions =================================================================
function isValidAddLoc() {
    var isValidLocName = requiredFieldValidator("AddLocationName", "Location name cannot be empty.") && nameValidator("AddLocationName");
    var isValidLocAddress = requiredFieldValidator("AddLocationAddress", "Location address cannot be empty.");
    var isValidContact = contactValidator("AddLocationContact");
    var isValidLocManagerOffice = requiredFieldValidator("AddLocationManagerOffice", "Manager office cannot be empty.");
    var isValidLocDepartment = requiredFieldValidator("AddLocationDepartment", "Department cannot be empty.");
    var isValidLocBranch = requiredFieldValidator("AddLocationBranch", "Branch cannot be empty.");
    var isValidLocZonalOffice = requiredFieldValidator("AddLocationZonalOffice", "Zonal office cannot be empty.");

    return (isValidLocName && isValidLocAddress && isValidContact && isValidLocManagerOffice && isValidLocDepartment && isValidLocZonalOffice && isValidLocBranch);
}

function addLocationClearAll() {
    document.forms[0]["AddLocationNameTextBox"].value = "";
    document.forms[0]["AddLocationAddressTextBox"].value = "";
    document.forms[0]["AddLocationContactTextBox"].value = "";
    document.forms[0]["AddLocationManagerOfficeTextBox"].value = "";
    document.forms[0]["AddLocationDepartmentTextBox"].value = "";
    document.forms[0]["AddLocationBranchTextBox"].value = "";
    document.forms[0]["AddLocationZonalOfficeTextBox"].value = "";
    return true;
}

//Update location functions ==================================================================
function isValidUpdateLoc() {
    var isValidUpLocname = requiredFieldValidator("UpdateLocName", "Location name cannot be empty.") && nameValidator("UpdateLocName");
    var isValidUpLocaddress = requiredFieldValidator("UpdateLocAddress", "Location address cannot be empty.");
    var isValidUpLoccontact = contactValidator("UpdateLocContact");
    var isValidUpLocdepartment = requiredFieldValidator("UpdateLocDepartment", "Department cannot be empty.");
    var isValidUpLocbranch = requiredFieldValidator("UpdateLocBranch", "Branch cannot be empty.");
    var isValidUpLoczonaloffice = requiredFieldValidator("UpdateLocZonalOffice", "Zonal Office cannot be empty.");
    var isValidUpLocmanageroffice = requiredFieldValidator("UpdateLocManagerOffice", "Manager Office cannot be empty.");

    return (isValidUpLocname && isValidUpLocaddress && isValidUpLoccontact && isValidUpLocdepartment && isValidUpLocbranch && isValidUpLoczonaloffice && isValidUpLocmanageroffice);
}

function isValidUpdateLocID() {
    return requiredFieldValidator("UpdateLocationID", "Location ID cannot be empty.");
}

function updateLocationClearAll() {
    document.forms[0]["UpdateLocNameTextBox"].value = "";
    document.forms[0]["UpdateLocAddressTextBox"].value = "";
    document.forms[0]["UpdateLocContactTextBox"].value = "";
    document.forms[0]["UpdateLocDepartmentTextBox"].value = "";
    document.forms[0]["UpdateLocManagerOfficeTextBox"].value = "";
    document.forms[0]["UpdateLocBranchTextBox"].value = "";
    document.forms[0]["UpdateLocZonalOfficeTextBox"].value = "";
    document.forms[0]["UpdateLocationIDTextBox"].value = "";
    document.getElementById("updatelocationInitState").style.display = "block";
    document.getElementById("updatelocationSecondState").style.display = "none";
    
    return false;
}

//Add new category functions =================================================================
function isValidAddCat() {
    return requiredFieldValidator("AddCategoryName", "Enter Category Name.") && nameValidator("AddCategoryName");
}

function addCategoryClearAll() {
    document.forms[0]["AddCategoryNameTextBox"].value = "";
    return true;
}

//Update category functions ==================================================================
function isValidCategoryCatID() {
    return requiredFieldValidator("UpdateCategoryID", "Enter category ID");
}

function isValidUpdateCat() {
    return requiredFieldValidator("UpdateCategoryName", "Enter Category Name.") && nameValidator("UpdateCategoryName");
}

function updateCategoryClearAll() {
    document.forms[0]["UpdateCategoryNameTextBox"].value = "";
    document.forms[0]["updateCategorySecondState"].style.display = "none";
    document.forms[0]["updateCategoryInitState"].style.display = "block";
    return true;
}

//Add new sub category functions =============================================================
function isValidAddSubCategory() {

    var isValidName = requiredFieldValidator("AddSubCategoryName", "Sub category name cannot be empty.") && nameValidator("AddSubCategoryName");
    var isValidDepreciation = requiredFieldValidator("AddSubCategoryDepreciationRate", "Depreciation rate cannot be empty.");

    if (isValidDepreciation) {
        var depre = document.forms[0]["AddSubCategoryDepreciationRateTextBox"].value;
        var intVal = parseFloat(depre);
        if (!depre.match(/^\d+$/)) {
            document.getElementById("AddSubCategoryDepreciationRateValidator").innerHTML = "Depreciation rate cannot have non-digits.";
            document.forms[0]["AddSubCategoryDepreciationRateTextBox"].style.border = "1px solid red";
            isValidDepreciation = false;
        } else if (intVal > 100.0) {
            document.getElementById("AddSubCategoryDepreciationRateValidator").innerHTML = "Depreciation rate cannot be larger than 100";
            document.forms[0]["AddSubCategoryDepreciationRateTextBox"].style.border = "1px solid red";
            isValidDepreciation = false;
        } else {
            document.getElementById("AddSubCategoryDepreciationRateValidator").innerHTML = "";
            document.forms[0]["AddSubCategoryDepreciationRateTextBox"].style.border = "1px solid #cacaca";
            isValidDepreciation = true;
        }
    }

    var isValidLifetime = requiredFieldValidator("AddSubCategoryLifetime", "Lifetime cannot be empty.");
    if (isValidLifetime) {
        var lifetime = document.forms[0]["AddSubCategoryLifetimeTextBox"].value;
        if (!lifetime.match(/^\d+$/)) {
            document.getElementById("AddSubCategoryLifetimeValidator").innerHTML = "Lifetime cannot have non-digits.";
            document.forms[0]["AddSubCategoryLifetimeTextBox"].style.border = "1px solid red";
            isValidLifetime = false;
        } else {
            document.getElementById("AddSubCategoryLifetimeValidator").innerHTML = "";
            document.forms[0]["AddSubCategoryLifetimeTextBox"].style.border = "1px solid #cacaca";
            isValidLifetime = true;
        }
    }
    return (isValidName && isValidDepreciation && isValidLifetime);
}

function addSubCategoryClearAll() {
    document.forms[0]["AddSubCategoryNameTextBox"].value = "";
    document.forms[0]["AddSubCategoryDepreciationRateTextBox"].value = "";
    document.forms[0]["AddSubCategoryLifetimeTextBox"].value = "";
    return true;
}

//////////////////////////////////////////////////////////////////////////////////////////////
//      Manage asset user
//////////////////////////////////////////////////////////////////////////////////////////////

//Register Asset functions====================================================================
function addNewAssetClearAll() {
    document.forms[0]["RegisterAssetNameTextBox"].value = "";
    //document.forms[0]["SubCategoryDropDownList"].value = "";
    //document.forms[0]["CategoryDropDownList"].value = "";
    document.forms[0]["AddValueTextBox"].value = "";
    /*document.forms[0]["LocationDropDownList"].value = "";
    document.forms[0]["OwnerDropDownList"].value = "";
    document.forms[0]["PersonToRecommendDropDownList"].value = "";*/
    return true;
}

function requiredFieldValidatorValue(controller, msg) {
    var content = document.forms[0][controller + "TextBox"].value;
    var re = /^\d+(\.\d{1,2})?$/;
    if (content == "") {
        document.getElementById(controller + "Validator").innerHTML = msg;
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    }else if (!re.test(content)) {
        document.getElementById(controller + "Validator").innerHTML = "Enter a valid value.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else if (parseFloat(content) < 5000) {
        document.getElementById(controller + "Validator").innerHTML = "Value must be greater than 5000 LKR";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "TextBox"].style.border = "1px solid #cacaca";
        return true;
    }
}

function validate() {
    if (document.getElementById("ddlList").value == "") {
        alert("Please select value"); // prompt user
        document.getElementById("ddlList").focus(); //set focus back to control
        return false;
    }
}

function isValidAddAsset() {
    var isValidAssetName = requiredFieldValidator("RegisterAssetName", "Asset name cannot be empty.") && nameValidator("RegisterAssetName") //numbers should be also used as asset name;
    var isValidValue = requiredFieldValidatorValue("AddValue", "Value cannot be empty.");
    document.forms[0]["AddValueTextBox"].value = "";
    return true;
}

function isValidAddAsset() {
    var isValidAssetName = requiredFieldValidator("RegisterAssetName", "Asset name cannot be empty.");
    var isValidValue = requiredFieldValidatorValue("AddValue", "Value cannot be empty.");
    var isValidSubcategory = dropDownRequiredFieldValidator("AddAssetSubCategory");
    var isValidCategory = dropDownRequiredFieldValidator("AddAssetCategory");
    var isValidLocation = dropDownRequiredFieldValidator("AddAssetLocation");
    var isValidOwner = dropDownRequiredFieldValidator("AddAssetOwner");
    var isValidRecommend = dropDownRequiredFieldValidator("AddAssetPersonToRecommend");

    return (isValidAssetName && isValidValue && isValidSubcategory && isValidCategory && isValidLocation && isValidOwner && isValidRecommend);
}

//Advanced asset search functions=============================================================
function isValidAssetSearch() {
    var id = document.forms[0]["AssetSearchIDTextBox"].value;
    var name = document.forms[0]["AssetSearchNameTextBox"].value;
    var dropdown = document.forms[0]["AssetSearchCategoryDropDown"];
    var category = dropdown.options[dropdown.selectedIndex].value;
    dropdown = document.forms[0]["AssetSearchSubCategoryDropDown"];
    var subCategory = dropdown.options[dropdown.selectedIndex].value;
    var value = document.forms[0]["AssetSearchValueTextBox"].value;
    dropdown = document.forms[0]["AssetSearchLocationDropDown"];
    var location = dropdown.options[dropdown.selectedIndex].value;
    dropdown = document.forms[0]["AssetSearchOwnerDropDown"];
    var owner = dropdown.options[dropdown.selectedIndex].value;
    if (id == "" && name == "" && category == "" && subCategory == "" && value == "" && location == "" && owner == "") {
        alert("Please fill at least one field");
        return false;
    } else {
        return true;
    }
    
}

//Transfer Asset functions ===================================================================
function transferClearAll() {
    document.forms[0]["TransferAssetIDTextBox"].value = "";
    document.getElementById("transferAssetSecondState").style.display = "none";
    document.getElementById("transferAssetInitState").style.display = "block";
    return false;
}

function isValidTransferAsset() {
    var TransAssetSendForRecommendDropDown = dropDownRequiredFieldValidator("TransAssetSendForRecommend");
    return (TransAssetSendForRecommendDropDown);
}

function transferAssetClearAll() {
    document.forms[0]["TransferAssetIDTextBox"].value = "";
    //document.forms[0]["TransferItemName"].value = "";
    //document.forms[0]["TransferCategory"].value = "";
    //document.forms[0]["TransferSubcategory"].value = "";
    //document.forms[0]["TransferValue"].value = "";
    document.forms[0]["TransferLocationDropDown"].value = "";
    document.forms[0]["TransferOwnerDropDown"].value = "";
    document.getElementById("transferAssetInitState").style.display = "block";
    document.getElementById("transferAssetSecondState").style.display = "none";

    return false;
}

function isValidAssetID() {
    return requiredFieldValidator("TransferAssetID", "Asset ID cannot be empty.");
}

//Dispose Asset functions ====================================================================
function disposeClearAll() {
    document.forms[0]["DisposeAssetIDTextBox"].value = "";
    document.getElementById("disposeAssetSecondState").style.display = "none";
    document.getElementById("disposeAssetInitState").style.display = "block";
    return true;
}

function isValidDisposeAssetDescription() {
    var DisposeAssetDescription = requiredFieldValidator("DisposeAssetDescription", "Description cannot be empty.");
    return (DisposeAssetDescription);
}