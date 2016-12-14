"use strict";

var StudentVM = function (id, name, groupId, availableGroups) {
    var self = this;

    self.Id = id || 0;
    self.Name = ko.observable(name || "").extend({ required: true });//"Please enter student's a name" });
    self.GroupId = ko.observable(groupId || null);

    self.Group = ko.computed(function () {
        if (!self.GroupId() || !availableGroups || availableGroups.length == 0)
            return null;
        var avGroupsById = $.grep(availableGroups, function (ag) {
            return ag.Id == self.GroupId();
        });
        return avGroupsById[0] || null;
    }, self);
    //self.Group = ko.observable(group ? new GroupVM(group.Id, group.Name) : new GroupVM());
};

var toArrayOfStudentVMs = function (students, availableGroups) {
    var studentVMs = ko.utils.arrayMap(students, function (student) {
        return new StudentVM(student.Id, student.Name, student.Group ? student.Group.Id : null, availableGroups);
    });
    return studentVMs;
};