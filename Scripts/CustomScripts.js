//Exapand content function
$(".expand-item-title").click(function () {

    $header = $(this);
    //getting the next element
    $content = $header.next();
    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
    $content.slideToggle(800, function () { });
});

//Global validation functions===========================================================================
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
        isValidContact = false;
    } else if (contact.length != 10) {
        document.getElementById(controller + "Validator").innerHTML = "Please enter a valid contact.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        isValidContact = false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById(controller + "Validator").innerHTML = "Contact cannot have non-digits.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        isValidContact = false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076")) {
        document.getElementById(controller + "Validator").innerHTML = "Please enter a valid contact.";
        document.forms[0][controller + "TextBox"].style.border = "1px solid red";
        isValidContact = false;
    } else {
        document.getElementById(controller + "Validator").innerHTML = "";
        document.forms[0][controller + "TextBox"].style.border = "1px solid #cacaca";
        isValidContact = true;
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

function isValidAddNew() {
    var confirmPassword = document.forms[0]["AddNewConfirmPasswordTextBox"].value;
    var password = document.forms[0]["AddNewPasswordTextBox"].value;

    var isValidFirstName = requiredFieldValidator("AddNewFirstName", "First name cannot be empty.");
    var isValidLastName = requiredFieldValidator("AddNewLastName", "Last name cannot be empty.");
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

//Update user functions ===================================================================

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
    var isValid = [true, true, true, true];
    var email = document.forms[0]["UpdateEmailTextBox"].value;
    var contact = document.forms[0]["UpdateContactTextBox"].value;
    
    isValid[0] = requiredFieldValidator("UpdateFirstName", "First name cannot be empty.");
    isValid[1] = requiredFieldValidator("UpdateLastName", "Last name cannot be empty.");
    isValid[2] = emailValidator("UpdateEmail");
    isValid[3] = contactValidator("UpdateContact");

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
    return requiredFieldValidator("DeleteUserEmpID", "Employee ID cannot be empty.");
}

//Add new location functions ===================================================================

function isValidAddLoc() {
    //var contact = document.forms[0]["AddLocationContactTextBox"].value;

    var isValidLocName = requiredFieldValidator("AddLocationName", "Location name cannot be empty.");
    var isValidLocAddress = requiredFieldValidator("AddLocationAddress", "Location address cannot be empty.");

    var isValidContact = contactValidator("AddLocationContact");
    /*var prefix = contact.substring(0, 3);
    if (contact == "") {
        document.getElementById("AddLocationContactValidator").innerHTML = "Contact cannot be empty.";
        isValidContact = false;
    } else if (contact.length != 10) {
        document.getElementById("AddLocationContactValidator").innerHTML = "Please enter a valid contact.";
        isValidContact = false;
    } else if (!contact.match(/^\d{10}$/)) {
        document.getElementById("AddLocationContactValidator").innerHTML = "Contact cannot have non-digits.";
        isValidContact = false;
    } else if (!(prefix == "077" || prefix == "071" || prefix == "072" || prefix == "075" || prefix == "076")) {
        document.getElementById("AddLocationContactValidator").innerHTML = "Please enter a valid contact.";
        isValidContact = false;
    } else {
        document.getElementById("AddLocationContactValidator").innerHTML = "";
        isValidContact = true;
    }*/

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
    document.forms[0]["AddLocationTypeTextBox"].value = "";
    document.forms[0]["AddLocationManagerOfficeTextBox"].value = "";
    document.forms[0]["AddLocationDepartmentTextBox"].value = "";
    document.forms[0]["AddLocationBranchTextBox"].value = "";
    document.forms[0]["AddLocationZonalOfficeTextBox"].value = "";
    return false;
}

//Update location functions ===================================================================

function isValidUpdateLoc() {
    var locname = document.forms[0]["UpdateLocNameTextBox"].value;
    if (locname == "") {
        document.getElementById("UpdateLocationNameValidator").innerHTML = "Enter Location Name.";
        return false
    } else {
        document.getElementById("UpdateLocationNameValidator").innerHTML = "";
        return true;
    }
}

function updateLocationClearAll() {
    document.forms[0]["UpdateLocNameTextBox"].value = "";
    document.forms[0]["updatelocationInitState"].style.display = block;
    return false;
}

//Add new category functions ===================================================================

function isValidAddCat() {
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

