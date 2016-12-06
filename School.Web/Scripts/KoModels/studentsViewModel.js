
var StudentModel = function (vmData) {
    var self = this;
    self.students = ko.observableArray(vmData.students);

    self.newStudent = ko.observable({
        Id: ko.observable(0),
        Name: ko.observable("").extend({ required: "Please enter student's a name" }),
        Group: ko.observable({
            Id: ko.observable(0),
            Name: ko.observable("").extend({ required: "Please enter group's name" })
        })
    });

    self.message = ko.observable("");

    self.addNewStudent = function () {
        self.students.push(createNewStudent());
    };

    self.removeStudent = function (student) {
        if (student.Id !== 0)
            $.ajax({
                url: "/Student/Remove",
                type: "POST",
                data: ko.toJSON(student),
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
        var isValid = true;
        self.students().forEach(function (student) {
            if (!student.Name || !student.Name.isValid)
                isValid = false;
        });
        if (isValid)
            $.ajax({
                url: "/Student/Save",
                type: "POST",
                data: ko.toJSON(self),
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

    self.getAll = function () {
        $.getJSON({
            url: "/Student/GetAll",
            type: "POST",
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

var createNewStudent = function () {
    var newStudent = {
        Id: ko.observable(0),
        Name: ko.observable("").extend({ required: "Please enter student's a name" }),
        Group: ko.observable({
            Id: ko.observable(0),
            Name: ko.observable("").extend({ required: "Please enter group's name" })
        })
    };
    return newStudent;
}