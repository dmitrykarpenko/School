"use strict";

var CoursesPageVM = function (vmData) {
    var self = this;

    self.courses = ko.observableArray(toArrayOfCourseVMs(vmData.Courses));
    self.courses.errors = ko.validation.group(self.courses);//, { deep: true, live: true });

    self.pageInf = vmData.PageInf;
  
    self.newCoursePopup = createDefaultNewCoursePopupVM();
    function createDefaultNewCoursePopupVM() {
        return new NewCoursePopupVM(createDefaultCourseVM(), onSuccessfulNewCourseSaving, self)
    };
    function onSuccessfulNewCourseSaving(data, selfVMPar) {
        ko.utils.arrayPushAll(selfVMPar.courses, toArrayOfCourseVMs(data.courses));
        selfVMPar.message(data.courses[0].Name + " saved successfully");
        //newCoursePopup.newCourse is observable now
        selfVMPar.newCoursePopup.newCourse(createDefaultCourseVM());

        ++selfVMPar.pageInf.PageSize;

        selfVMPar.newCoursePopup.toggle();
    };
    function createDefaultCourseVM() {
        return new CourseVM();
    };

    self.message = ko.observable("");

    self.addNewCourse = function () {
        self.courses.push(createDefaultCourseVM());

        ++self.pageInf.PageSize;

        $(".selectpicker").selectpicker("render");
    };

    self.deleteCourse = function (course) {
        if (course.Id)
            $.ajax({
                url: "/Course/Delete",
                type: "POST",
                data: ko.toJSON({ id: course.Id }),
                contentType: "application/json",
                success: function () {
                    self.courses.remove(function (c) { return c.Id === course.Id; });
                    self.message(course.Name() + " removed");

                    --self.pageInf.PageSize;
                }
            });
        else
            self.courses.remove(function (s) { return s.Id === course.Id; });
    };

    self.saveAll = function () {
        self.courses.errors.showAllMessages();
        var allAreValid = self.courses.errors().length == 0;

        if (allAreValid)
            $.ajax({
                url: "/Course/Save",
                type: "POST",
                data: ko.toJSON(self.courses),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every course as observable, right now not required
                    //ko.mapping.fromJS(data.courses(), {}, self.courses);
                    self.courses(toArrayOfCourseVMs(data.courses));
                    self.message("All courses saved in DB");

                    $(".selectpicker").selectpicker("render");
                }
            });
    };

    self.getPage = function () {
        $.ajax({
            url: "/Course/GetPage",
            type: "POST",
            data: ko.toJSON(self.pageInf),
            contentType: "application/json",
            success: function (data) {
                self.courses(toArrayOfCourseVMs(data.Courses));
                self.message("Courses retrieved successfully");

                $(".selectpicker").selectpicker("render");
            }
        });
    };

    self.increasePageSizeAndGetPage = function (increaseBy) {
        self.pageInf.PageSize += increaseBy;
        self.getPage();
    };
    self.setPageInfAndGetPage = function (newPageInf) {
        self.pageInf = newPageInf;
        self.getPage();
    };
};

var NewCoursePopupVM = function (newCourseInitVal, onSuccessfulSaving, parentVM) {
    var self = this;

    self.newCourse = ko.observable(newCourseInitVal || new CourseVM());
    self.visible = ko.observable(false);

    self.toggle = function () {
        var vis = self.visible;
        vis(vis() ? false : true);
    }

    self.save = function () {
        self.newCourse().save(onSuccessfulSaving, parentVM);
    }
};