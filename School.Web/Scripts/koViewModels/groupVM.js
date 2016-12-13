"use strict";

var GroupVM = function (id, name) {
    var self = this;

    self.Id = id || null;
    self.Name = ko.isComputed(name) ? name : ko.observable(name || "");
    self.Name.extend({ required: true });//"Please enter group's name" });
    self.errors = ko.validation.group(self);

    self.save = function (vmData, onSuccess, parentVM) {
        self.errors.showAllMessages();
        var isValid = self.errors().length == 0;
        if (isValid)
            $.ajax({
                url: "/Group/Save",
                type: "POST",
                data: ko.toJSON([vmData]),
                contentType: "application/json",
                success: function (data) {
                    onSuccess(data, parentVM);
                }
            });
    };
};

var toArrayOfGroupVMs = function (groups, getGroupById) {
    var groupVMs = ko.utils.arrayMap(groups, function (group) {
        return new GroupVM(group.Id, group.Name);
    });
    return groupVMs;
};