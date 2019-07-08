// Widgets init
(function () {
    Validator.init();
    Alerts.init();
})();

// Input focus binding
(function () {
    var invalidInput = document.querySelector('.input-validation-error[type=text]:not([readonly]):not(.datepicker):not(.datetimepicker)');

    if (invalidInput) {
        invalidInput.setSelectionRange(0, invalidInput.value.length);
        invalidInput.focus();
    } else {
        var input = document.querySelector('input[type=text]:not([readonly]):not(.datepicker):not(.datetimepicker)');

        if (input) {
            input.setSelectionRange(0, input.value.length);
            input.focus();
        }
    }
})();
