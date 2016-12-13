"use strict";

var StudentsPageVM = function (vmData) {
    var self = this;

    self.availableGroups = toArrayOfGroupVMs(vmData.AvailableGroups);

    self.students = ko.observableArray(toArrayOfStudentVMs(vmData.Students, self.availableGroups));
    self.students.errors = ko.validation.group(self.students);//, { deep: true, live: true });

    self.pageInf = vmData.PageInf;

    self.newStudent = ko.observable(createDefaultStudentVM());
    self.newStudent.errors = ko.validation.group(self.newStudent);

    self.countOfAllStudents = ko.observable(vmData.CountOfAllStudents);

    self.message = ko.observable("");

    function createDefaultStudentVM(student) {
        return new StudentVM(null, null, null, self.availableGroups);
    };

    self.addNewStudent = function () {
        self.students.push(createDefaultStudentVM());

        ++self.pageInf.PageSize;
        koHelpers.increment(self.countOfAllStudents);

        $(".selectpicker").selectpicker("render");
    };

    self.deleteStudent = function (student) {
        if (student.Id !== 0)
            $.ajax({
                url: "/Student/Delete",
                type: "POST",
                data: ko.toJSON({ id: student.Id }),
                contentType: "application/json",
                success: function () {
                    self.students.remove(function (s) { return s.Id === student.Id; });
                    self.message(student.Name() + " removed");

                    --self.pageInf.PageSize;
                    koHelpers.decrement(self.countOfAllStudents);
                }
            });
        else
            self.students.remove(function (s) { return s.Id === student.Id; });
    };

    self.saveAll = function () {
        self.students.errors.showAllMessages();
        var allAreValid = self.students.errors().length == 0;

        if (allAreValid)
            $.ajax({
                url: "/Student/Save",
                type: "POST",
                data: ko.toJSON(self.students),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every student as observable, right now not required
                    //ko.mapping.fromJS(data.students(), {}, self.students);
                    self.students(toArrayOfStudentVMs(data.students, self.availableGroups));
                    self.message("All students saved in DB");

                    $(".selectpicker").selectpicker("render");
                }
            });
    };

    self.getPage = function () {
        $.ajax({
            url: "/Student/GetPage",
            type: "POST",
            data: ko.toJSON(self.pageInf),
            contentType: "application/json",
            success: function (data) {
                self.students(toArrayOfStudentVMs(data.Students, self.availableGroups));
                self.availableGroups = toArrayOfGroupVMs(data.AvailableGroups);
                self.countOfAllStudents(data.CountOfAllStudents);
                self.message("Students retrieved successfully");

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

    //self.exportToFile = function () {
    //    //write formatted data to table.json
    //    var blob = new Blob([ko.toJSON(self, null, 2)], { type: "text/json;charset=utf-8" });
    //    saveAs(blob, "table.json");
    //}

    self.saveNewStudent = function (vmData) {
        self.newStudent.errors.showAllMessages();
        var isValid = self.newStudent.errors().length == 0;
        if (isValid)
            $.ajax({
                url: "/Student/Save",
                type: "POST",
                data: ko.toJSON([vmData.newStudent]),
                contentType: "application/json",
                success: function (data) {
                    ko.utils.arrayPushAll(self.students, toArrayOfStudentVMs(data.students, self.availableGroups));
                    self.message(data.students[0].Name + " saved successfully");
                    self.newStudent(createDefaultStudentVM());

                    ++self.pageInf.PageSize;
                    koHelpers.increment(self.countOfAllStudents);

                    $(".selectpicker").selectpicker("render");
                }
            });
    };
};

