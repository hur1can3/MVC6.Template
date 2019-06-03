Lookup = {
    init: function () {
        if (typeof MvcLookup == 'function') {
            [].forEach.call(document.querySelectorAll('.mvc-lookup'), function (element) {
                return new MvcLookup(element);
            });
        }
    }
};
