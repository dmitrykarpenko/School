"use strict";

var CourseVM = function (id, name) {
    var self = this;

    self.Id = id || 0;
    self.Name = ko.isComputed(name) ? name : ko.observable(name || "");
    self.Name.extend({ required: true });//"Please enter course's name" });
    self.errors = ko.validation.group(self, { deep: false });

    self.editUrl = function () {
        return "/Course/AddNewOrEdit/" + (self.Id != 0 ? self.Id : "");
    };

    self.save = function (onSuccess, parentVM) {
        self.errors.showAllMessages();
        var isValid = self.errors().length == 0;
        if (isValid)
            $.ajax({
                url: "/Course/Save",
                type: "POST",
                data: ko.toJSON([self]),
                contentType: "application/json",
                success: function (data) {
                    if (onSuccess && parentVM)
                        onSuccess(data, parentVM);
                }
            });
    };
};

var toArrayOfCourseVMs = function (courses, getCourseById) {
    var courseVMs = ko.utils.arrayMap(courses, function (course) {
        return new CourseVM(course.Id, course.Name);
    });
    return courseVMs;
};