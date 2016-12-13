"use strict";

var GroupsPageVM = function (vmData) {
    var self = this;

    self.groups = ko.observableArray(toArrayOfGroupVMs(vmData.Groups));
    self.groups.errors = ko.validation.group(self.groups);//, { deep: true, live: true });

    self.pageInf = vmData.PageInf;

    self.newGroup = ko.observable(createDefaultGroupVM());
    self.newGroup.errors = ko.validation.group(self.newGroup);

    self.message = ko.observable("");

    function createDefaultGroupVM() {
        return new GroupVM();
    };

    self.addNewGroup = function () {
        self.groups.push(createDefaultGroupVM());

        ++self.pageInf.PageSize;

        $(".selectpicker").selectpicker("render");
    };

    self.deleteGroup = function (group) {
        if (group.Id !== 0)
            $.ajax({
                url: "/Group/Delete",
                type: "POST",
                data: ko.toJSON({ id: group.Id }),
                contentType: "application/json",
                success: function () {
                    self.groups.remove(function (g) { return g.Id === group.Id; });
                    self.message(group.Name() + " removed");

                    --self.pageInf.PageSize;
                }
            });
        else
            self.groups.remove(function (s) { return s.Id === group.Id; });
    };

    self.saveAll = function () {
        self.groups.errors.showAllMessages();
        var allAreValid = self.groups.errors().length == 0;

        if (allAreValid)
            $.ajax({
                url: "/Group/Save",
                type: "POST",
                data: ko.toJSON(self.groups),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every group as observable, right now not required
                    //ko.mapping.fromJS(data.groups(), {}, self.groups);
                    self.groups(toArrayOfGroupVMs(data.groups));
                    self.message("All groups saved in DB");

                    $(".selectpicker").selectpicker("render");
                }
            });
    };

    self.getPage = function () {
        $.ajax({
            url: "/Group/GetPage",
            type: "POST",
            data: ko.toJSON(self.pageInf),
            contentType: "application/json",
            success: function (data) {
                self.groups(toArrayOfGroupVMs(data.Groups));
                self.message("Groups retrieved successfully");

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

    self.saveNewGroup = function (vmData) {
        self.newGroup.errors.showAllMessages();
        var isValid = self.newGroup.errors().length == 0;
        if (isValid)
            $.ajax({
                url: "/Group/Save",
                type: "POST",
                data: ko.toJSON([vmData.newGroup]),
                contentType: "application/json",
                success: function (data) {
                    ko.utils.arrayPushAll(self.groups, toArrayOfGroupVMs(data.groups));
                    self.message(data.groups[0].Name + " saved successfully");
                    self.newGroup(createDefaultGroupVM());

                    ++self.pageInf.PageSize;

                    $(".selectpicker").selectpicker("render");
                }
            });
    };
};

