Grid = {
    init: function () {
        if (typeof MvcGrid == 'function') {
            MvcGridNumberFilter.prototype.isValid = function (value) {
                return value == '' || !isNaN(numbro(value).value());
            };

            [].forEach.call(document.querySelectorAll('.mvc-grid'), function (element) {
                return new MvcGrid(element);
            });
        }
    }
};
