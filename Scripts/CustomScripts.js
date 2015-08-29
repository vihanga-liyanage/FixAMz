$(".expand-item-title").click(function () {

    $header = $(this);
    //getting the next element
    $content = $header.next();
    //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
    $content.slideToggle(800, function () {});

});

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