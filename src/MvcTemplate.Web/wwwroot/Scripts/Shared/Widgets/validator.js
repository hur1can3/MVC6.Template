Validator = {
    init: function () {
        var lang = document.documentElement.lang;

        Wellidate.prototype.rules.date.isValid = function () {
            return !this.element.value || Globalize.parseDate(this.element.value);
        };

        Wellidate.prototype.rules.number.isValid = function () {
            return !this.element.value || !isNaN(Globalize.parseFloat(this.element.value));
        };

        Wellidate.prototype.rules.min.isValid = function () {
            return !this.element.value || this.min <= Globalize.parseFloat(this.element.value);
        };

        Wellidate.prototype.rules.max.isValid = function () {
            return !this.element.value || Globalize.parseFloat(this.element.value) <= this.max;
        };

        Wellidate.prototype.rules.range.isValid = function () {
            var value = Globalize.parseFloat(this.element.value);

            return !this.element.value || this.min <= value && value <= this.max;
        };

        Wellidate.prototype.rules.greater.isValid = function () {
            var value = Globalize.parseFloat(this.element.value);

            return !this.element.value || this.min <= value && value <= this.max;
        };

        document.addEventListener('wellidate-error', function (e) {
            if (event.target.classList.contains('mvc-lookup-value')) {
                var wellidate = e.detail.validatable.wellidate;
                var control = new MvcLookup(event.target).control;

                control.classList.add(wellidate.inputErrorClass);
                control.classList.remove(wellidate.inputValidClass);
                control.classList.remove(wellidate.inputPendingClass);
            }
        });

        document.addEventListener('wellidate-success', function (e) {
            if (event.target.classList.contains('mvc-lookup-value')) {
                var wellidate = e.detail.validatable.wellidate;
                var control = new MvcLookup(event.target).control;

                control.classList.add(wellidate.inputValidClass);
                control.classList.remove(wellidate.inputErrorClass);
                control.classList.remove(wellidate.inputPendingClass);
            }
        });

        [].forEach.call(document.querySelectorAll('.mvc-lookup-value.input-validation-error'), function (value) {
            new MvcLookup(value).control.classList.add('input-validation-error');
        });

        [].forEach.call(document.querySelectorAll('form'), function (form) {
            new Wellidate(form, {
                wasValidatedClass: 'validated'
            });
        });

        Globalize.cultures.en = null;
        Globalize.addCultureInfo(lang, window.cultures.globalize[lang]);
        Globalize.culture(lang);
    }
};
