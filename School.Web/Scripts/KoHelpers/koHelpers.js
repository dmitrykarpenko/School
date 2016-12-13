"use strict";

var koHelpers = {
    koRequiredExtender: function (target, overrideMessage) {

        //add some sub-observables to our observable
        target.hasError = ko.observable();
        target.validationMessage = ko.observable();

        //define a function to do validation
        function validate(newValue) {
            target.hasError(newValue ? false : true);
            target.validationMessage(newValue ? "" : overrideMessage || "This field is required");
        }

        //initial validation
        validate(target());

        //validate whenever the value changes
        target.subscribe(validate);

        //return the original observable
        return target;
    },

    koInitValidation: function () {
        ko.validation.init({
            grouping: {
                deep: true,
                live: true,
                observable: false
            }
        });
    },

    increment: function (observable) {
        observable(observable() + 1);
    },
    decrement: function (observable) {
        observable(observable() - 1);
    }
}