"use strict";

var StudentsPageVM = function (vmData) {

    var self = this;

    self.students = ko.observableArray(toArrayOfStudentVMs(vmData.Students));
    self.availableGroups = ko.observableArray(toArrayOfGroupVMs(vmData.AvailableGroups));
    self.pageInf = vmData.PageInf;
    self.newStudent = ko.validatedObservable(new StudentVM());
    self.message = ko.observable("");

    self.errors = ko.validation.group(self.students);//, { deep: true, live: true });

    self.addNewStudent = function () {
        self.students.push(ko.validatedObservable(new StudentVM()));
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
                    self.students.remove(function (s) { return s().Id === student.Id; });
                    self.message(student.Name() + " removed");
                }
            });
        else
            self.students.remove(function (s) { return s().Id === student.Id; });
    };

    self.saveAll = function () {
        self.errors.showAllMessages();
        var allAreValid = self.errors().length == 0;

        if (allAreValid)
            $.ajax({
                url: "/Student/Save",
                type: "POST",
                data: ko.toJSON(self.students),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every student as observable, right now not required
                    //ko.mapping.fromJS(data.students(), {}, self.students);
                    self.students(toArrayOfStudentVMs(data.students));
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
                self.students(toArrayOfStudentVMs(data.Students));
                self.availableGroups(toArrayOfGroupVMs(vmData.AvailableGroups));
                self.message("Students retrieved successfully");

                $(".selectpicker").selectpicker("render");
            }
        });
    };

    self.increasePageSizeAndGetPage = function (increasedBy) {
        debugger;
        self.pageInf.PageSize += increasedBy;
        self.getPage();
    };
    self.setPageInfAndGetPage = function (newPageInf) {
        debugger;
        self.pageInf = newPageInf;
        self.getPage();
    };

    //self.exportToFile = function () {
    //    //write formatted data to table.json
    //    var blob = new Blob([ko.toJSON(self, null, 2)], { type: "text/json;charset=utf-8" });
    //    saveAs(blob, "table.json");
    //}

    self.saveNewStudent = function (vmData) {
        $.ajax({
            url: "/Student/Save",
            type: "POST",
            data: ko.toJSON([vmData.newStudent]),
            contentType: "application/json",
            success: function (data) {
                debugger;
                ko.utils.arrayPushAll(self.students, toArrayOfStudentVMs(data.students));
                self.message(data.students[0].Name + " saved successfully");
                
                $(".selectpicker").selectpicker("render");
            }
        });
    };
};

var StudentVM = function (id, name, group) {
    this.Id = id || 0;
    this.Name = ko.observable(name || "").extend({ required: true });//"Please enter student's a name" });
    this.Group = ko.observable(group ? new GroupVM(group.Id, group.Name) : new GroupVM());
};

var GroupVM = function (id, name) {
    this.Id = id || null;
    this.Name = ko.observable(name || "");//.extend({ required: true });//"Please enter group's name" });
};

var toArrayOfStudentVMs = function (students) {
    var studentVMs = ko.utils.arrayMap(students, function (student) {
        return ko.validatedObservable(new StudentVM(student.Id, student.Name, student.Group));
    });
    return studentVMs;
};

var toArrayOfGroupVMs = function (groups) {
    var groupVMs = ko.utils.arrayMap(groups, function (group) {
        return ko.validatedObservable(new GroupVM(group.Id, group.Name));
    });
    return groupVMs;
};