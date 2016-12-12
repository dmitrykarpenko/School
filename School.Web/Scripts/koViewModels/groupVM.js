"use strict";

var GroupVM = function (id, name) {
    var self = this;

    self.Id = id || null;
    self.Name = ko.observable(name || "")//.extend({ required: true });//"Please enter group's name" });
};

var toArrayOfGroupVMs = function (groups) {
    var groupVMs = ko.utils.arrayMap(groups, function (group) {
        return ko.validatedObservable(new GroupVM(group.Id, group.Name));
    });
    return groupVMs;
};