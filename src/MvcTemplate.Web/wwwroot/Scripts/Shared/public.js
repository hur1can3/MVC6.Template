// Widgets init
(function () {
    Validator.init();
    Alerts.init();
})();

// Input focus binding
(function () {
    var invalidInput = document.querySelector('.input-validation-error[type=text]:not([readonly]):not(.datepicker):not(.datetimepicker)');
    if (invalidInput) {
        invalidInput.setSelectionRange(invalidInput.value.length, invalidInput.value.length);
        invalidInput.focus();
    } else {
        var input = document.querySelector('input[type=text]:not([readonly]):not(.datepicker):not(.datetimepicker)');
        if (input) {
            input.setSelectionRange(input.value.length, input.value.length);
            input.focus();
        }
    }
})();
