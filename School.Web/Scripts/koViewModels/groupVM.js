"use strict";

var GroupVM = function (id, name) {
    var self = this;

    self.Id = id || null;
    self.Name = ko.isComputed(name) ? name : ko.observable(name || "");
    self.Name.extend({ required: true });//"Please enter group's name" });
};

var toArrayOfGroupVMs = function (groups, getGroupById) {
    var groupVMs = ko.utils.arrayMap(groups, function (group) {
        return new GroupVM(group.Id, group.Name);
    });
    return groupVMs;
};