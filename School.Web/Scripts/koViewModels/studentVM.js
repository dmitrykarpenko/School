"use strict";

var StudentVM = function (id, name, group) {
    var self = this;

    self.Id = id || 0;
    self.Name = ko.observable(name || "").extend({ required: true });//"Please enter student's a name" });
    self.Group = ko.observable(group ? new GroupVM(group.Id, group.Name) : new GroupVM());
};

var toArrayOfStudentVMs = function (students) {
    var studentVMs = ko.utils.arrayMap(students, function (student) {
        return ko.validatedObservable(new StudentVM(student.Id, student.Name, student.Group));
    });
    return studentVMs;
};