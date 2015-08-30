﻿//Exapand content function
$(".expand-item-title").click(function () {

    $header = $(this);
    //getting the next element
    $content = $header.next();
    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
    $content.slideToggle(800, function () { });
});

//Global validation function for required field
function requiredFieldValidator(controller, msg) {
    var content = document.forms[0][controller + "TextBox"].value;
    if (content == "") {
        document.getElementById(controller + "Validator").innerHTML = msg;
        return false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
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
                usernameNotExists = true;
            } else {
                document.getElementById("AddNewUsernameValidator").innerHTML = "User name already exists.";
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

//Add new user functions ===================================================================

function addNewClearAll() {
    document.forms[0]["AddNewFirstNameTextBox"].value = "";
    document.forms[0]["AddNewLastNameTextBox"].value = "";
    document.forms[0]["AddNewEmailTextBox"].value = "";
    document.forms[0]["AddNewContactTextBox"].value = "";
    document.forms[0]["AddNewUsernameTextBox"].value = "";
    document.forms[0]["AddNewPasswordTextBox"].value = "";
    document.forms[0]["AddNewConfirmPasswordTextBox"].value = "";
    return false;
}

function isValidAddNew() { //try to write functions for overlaps
    var email = document.forms[0]["AddNewEmailTextBox"].value;
    var contact = document.forms[0]["AddNewContactTextBox"].value;
    var confirmPassword = document.forms[0]["AddNewConfirmPasswordTextBox"].value;
    var password = document.forms[0]["AddNewPasswordTextBox"].value;

    var isValidFirstName = requiredFieldValidator("AddNewFirstName", "First name cannot be empty.");
    var isValidLastName = requiredFieldValidator("AddNewLastName", "Last name cannot be empty.");

    var isValidEmail = true;
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (email == "") {
        document.getElementById("AddNewEmailValidator").innerHTML = "Email cannot be empty.";
        isValidEmail = false;
    } else if (!re.test(email)) {
        document.getElementById("AddNewEmailValidator").innerHTML = "Enter a valid email address.";
        isValidEmail = false;
    } else {
        document.getElementById("AddNewEmailValidator").innerHTML = "";
        isValidEmail = true;
    }

    var isValidContact = true;
    var prefix = contact.substring(0, 3);
    if (contact == "") {
        document.getElementById("AddNewContactValidator").innerHTML = "Contact cannot be empty.";
        isValidContact = false;
    } else if (contact.length != 10) {
        document.getElementById("AddNewContactValidator").innerHTML = "Please enter a valid contact.";
        isValidContact = false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById("AddNewContactValidator").innerHTML = "Contact cannot have non-digits.";
        isValidContact = false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076")) {
        document.getElementById("AddNewContactValidator").innerHTML = "Please enter a valid contact.";
        isValidContact = false;
    } else {
        document.getElementById("AddNewContactValidator").innerHTML = "";
        isValidContact = true;
    }

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
        isValidConfirmPassword = false;
    } else if (confirmPassword != password) {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "Confirm password does not match with password.";
        isValidConfirmPassword = false;
    } else {
        document.getElementById("AddNewConfirmPasswordValidator").innerHTML = "";
        isValidConfirmPassword = true;
    }

    return (isValidFirstName && isValidLastName && isValidEmail && isValidContact && isValidUsername && isValidPassword && isValidConfirmPassword);
}

//Update user functions ===================================================================

function updateClearAll() {
    document.forms[0]["UpdateFirstNameTextBox"].value = "";
    document.forms[0]["UpdateLastNameTextBox"].value = "";
    document.forms[0]["UpdateEmailTextBox"].value = "";
    document.forms[0]["UpdateContactTextBox"].value = "";
    document.forms[0]["updateUserInitState"].style.display = block;
    return false;
}

function isValidUpdateEmpID() {
    return requiredFieldValidator("UpdateEmpID", "Employee ID cannot be empty.");
}

function isValidUpdate() {
    var isValid = [true, true, true, true];
    var email = document.forms[0]["UpdateEmailTextBox"].value;
    var contact = document.forms[0]["UpdateContactTextBox"].value;
    
    isValid[0] = requiredFieldValidator("UpdateFirstName", "First name cannot be empty.");
    isValid[1] = requiredFieldValidator("UpdateLastName", "Last name cannot be empty.");

    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (email == "") {
        document.getElementById("UpdateEmailValidator").innerHTML = "Email cannot be empty.";
        isValid[2] = false;
    } else if (!re.test(email)) {
        document.getElementById("UpdateEmailValidator").innerHTML = "Enter a valid email address.";
        isValid[2] = false;
    } else {
        document.getElementById("UpdateEmailValidator").innerHTML = "";
        isValid[2] = true;
    }

    var prefix = contact.substring(0, 3);
    if (contact == "") {
        document.getElementById("UpdateContactValidator").innerHTML = "Contact cannot be empty.";
        isValid[3] = false;
    } else if (contact.length != 10) {
        document.getElementById("UpdateContactValidator").innerHTML = "Please enter a valid contact.";
        isValid[3] = false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById("UpdateContactValidator").innerHTML = "Contact cannot have non-digits.";
        isValid[3] = false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076")) {
        document.getElementById("UpdateContactValidator").innerHTML = "Please enter a valid contact.";
        isValid[3] = false;
    } else {
        document.getElementById("UpdateContactValidator").innerHTML = "";
        isValid[3] = true;
    }

    for (var i = 0; i < isValid.length; i++) {
        if (!isValid[i]) { return false; }
    }
    return true;
}

//Advanced user search functions ===================================================================

function searchClearAll() {
    document.forms[0]["SearchEmployeeIDTextBox"].value = "";
    document.forms[0]["SearchFirstNameTextBox"].value = "";
    document.forms[0]["SearchLastNameTextBox"].value = "";
    document.forms[0]["SearchEmailTextBox"].value = "";
    document.forms[0]["SearchContactTextBox"].value = "";
    document.forms[0]["SearchUsernameTextBox"].value = "";
    return false;
}

//Delete user functions ===================================================================

function isValidDeleteEmpID() {
    return requiredFieldValidator("DeleteEmpID", "Employee ID cannot be empty.");
}

//Add new category functions ===================================================================

function isValidAddCat() {
    //var isValid = true ;
    var catname = document.forms[0]["AddCategoryNameTextBox"].value;
    if (catname == "") {
        document.getElementById("AddCategoryValidator").innerHTML = "Enter Category Name.";
        return false
    } else {
        document.getElementById("AddCategoryValidator").innerHTML = "";
        return true;
    }
}

function addCategoryClearAll() {
    document.forms[0]["AddCategoryNameTextBox"].value = "";
    return false;
}

//Update category functions ===================================================================

function isValidUpdateCat() {
    var catname = document.forms[0]["UpdateCatNameTextBox"].value;
    if (catname == "") {
        document.getElementById("UpdateCategoryNameValidator").innerHTML = "Enter Category Name.";
        return false
    } else {
        document.getElementById("UpdateCategoryNameValidator").innerHTML = "";
        return true;
    }
}

function updateCategoryClearAll() {
    document.forms[0]["UpdateCatNameTextBox"].value = "";
    document.forms[0]["updatecategoryrInitState"].style.display = block;
    return false;
}

//Add new location functions ===================================================================

function isValidAddLoc() {
    //var isValid = true ;
    var catname = document.forms[0]["AddLocationNameTextBox"].value;

    if (catname == "") {
        document.getElementById("AddLocationValidator").innerHTML = "Enter Location Name";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("AddLocationValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }

}

function addLocationClearAll() {
    document.forms[0]["AddLocationNameTextBox"].value = "";
    document.forms[0]["AddLocationAddressTextBox"].value = "";
    document.forms[0]["AddLocationContactTextBox"].value = "";
    document.forms[0]["AddLocationTypeTextBox"].value = "";
    document.forms[0]["AddLocationManagerOfficeTextBox"].value = "";
    document.forms[0]["AddLocationDepartmentTextBox"].value = "";
    document.forms[0]["AddLocationBranchTextBox"].value = "";
    document.forms[0]["AddLocationZonalOfficeTextBox"].value = "";
    return false;
}

//Update location functions ===================================================================

function isValidUpdateLoc() {
    //var isValid = true ;
    var locname = document.forms[0]["UpdateLocNameTextBox"].value;

    if (locname == "") {
        document.getElementById("UpdateLocationNameValidator").innerHTML = "Enter Location Name.";
        //isValid[0] = false;
        return false
    } else {
        document.getElementById("UpdateLocationNameValidator").innerHTML = "";
        //isValid[0] = true;
        return true;
    }

}

function updateLocationClearAll() {
    document.forms[0]["UpdateLocNameTextBox"].value = "";
    document.forms[0]["updatelocationInitState"].style.display = block;
    return false;
}