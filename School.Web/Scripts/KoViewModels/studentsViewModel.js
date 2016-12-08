
var StudentsViewModel = function (vmData) {

    var self = this;

    self.students = ko.observableArray(vmData.Students); 
    self.availableGroups = ko.observableArray(vmData.AvailableGroups);
    self.pageInf = ko.observable(vmData.PageInf);

    self.newStudent = ko.observable(new StudentViewModel());
    self.message = ko.observable("");

    //todo: make it work
    self.errors = ko.validation.group(self, { deep: true, live: true });

    self.addNewStudent = function () {
        self.students.push(new StudentViewModel());
    };

    self.deleteStudent = function (student) {
        if (student.Id !== 0)
            $.ajax({
                url: "/Student/Delete",
                type: "POST",
                data: ko.toJSON({ id: student.Id }),
                contentType: "application/json",
                success: function () {
                    self.students.remove(student);
                    self.message(student.Name + " removed");
                }
            });
        else
            self.students.remove(person);
    };

    self.saveAll = function () {
         //fill "errors" correctly
        debugger;
        var isValid = !self.errors().length;
        if (isValid)
            $.ajax({
                url: "/Student/Save",
                type: "POST",
                data: ko.toJSON(self.students),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every person as observable, right now not required
                    //ko.mapping.fromJS(data.students(), {}, self.students);
                    self.students(data.students);
                    self.message("All students saved in DB");
                }
            });
        //else
        //    self.message("Fill first name of each person!");
    };

    self.getPage = function () {
        $.ajax({
            url: "/Student/GetPage",
            type: "POST",
            data: ko.toJSON(self.pageInf),
            contentType: "application/json",
            success: function (data) {
                self.students(data.students);
                self.message("Table refreshed successfully");
            }
        });
    };

    //self.exportToFile = function () {
    //    //write formatted data to table.json
    //    var blob = new Blob([ko.toJSON(self, null, 2)], { type: "text/json;charset=utf-8" });
    //    saveAs(blob, "table.json");
    //}

    //self.saveNewPerson = function (vmData) {
    //    $.ajax({
    //        url: "/Student/SaveNewStudent",
    //        type: "POST",
    //        data: ko.toJSON(vmData.newStudent),
    //        contentType: "application/json",
    //        success: function (data) {
    //            self.students.push(data.newStudent);
    //            self.message(data.newPerson.FirstName + " saved successfully");
    //        }
    //    });
    //};
};

var StudentViewModel = function (id, name) {
    this.Id = ko.observable(id || 0);
    this.Name = ko.observable(name || "").extend({ required: "Please enter student's a name" });
    this.Group = new GroupViewModel();
};

var GroupViewModel = function (id, name) {
    this.Id = ko.observable(id || 0);
    this.Name = ko.observable(name || "").extend({ required: "Please enter group's name" });
};

//var createNewStudent = function () {
//    var newStudent = {
//        Id: ko.observable(0),
//        Name: ko.observable("").extend({ required: "Please enter student's a name" }),
//        Group: ko.observable({
//            Id: ko.observable(0),
//            Name: ko.observable("").extend({ required: "Please enter group's name" })
//        })
//    };
//    return newStudent;
//};
debugger;
ko.validation.init({
    grouping: {
        deep: true,
        observable: false //important ! Needed so object trees are correctly traversed every time so added objects AFTER the initial setup get included
    }
});