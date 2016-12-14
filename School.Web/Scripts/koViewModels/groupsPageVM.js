"use strict";

var GroupsPageVM = function (vmData) {
    var self = this;

    self.groups = ko.observableArray(toArrayOfGroupVMs(vmData.Groups));
    self.groups.errors = ko.validation.group(self.groups);//, { deep: true, live: true });

    self.pageInf = vmData.PageInf;
  
    self.newGroupPopup = createDefaultNewGroupPopupVM();
    function createDefaultNewGroupPopupVM() {
        return new NewGroupPopupVM(createDefaultGroupVM(), onSuccessfulNewGroupSaving, self)
    };
    function onSuccessfulNewGroupSaving(data, selfVMPar) {
        ko.utils.arrayPushAll(selfVMPar.groups, toArrayOfGroupVMs(data.groups));
        selfVMPar.message(data.groups[0].Name + " saved successfully");
        //newGroupPopup.newGroup is observable now
        selfVMPar.newGroupPopup.newGroup(createDefaultGroupVM());

        ++selfVMPar.pageInf.PageSize;

        selfVMPar.newGroupPopup.toggle();
    };
    function createDefaultGroupVM() {
        return new GroupVM();
    };

    self.message = ko.observable("");

    self.addNewGroup = function () {
        self.groups.push(createDefaultGroupVM());

        ++self.pageInf.PageSize;

        $(".selectpicker").selectpicker("render");
    };

    self.deleteGroup = function (group) {
        if (group.Id)
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
};

var NewGroupPopupVM = function (newGroupInitVal, onSuccessfulSaving, parentVM) {
    var self = this;

    self.newGroup = ko.observable(newGroupInitVal || new GroupVM());
    self.visible = ko.observable(false);

    self.toggle = function () {
        var vis = self.visible;
        vis(vis() ? false : true);
    }

    self.save = function () {
        self.newGroup().save(onSuccessfulSaving, parentVM);
    }
};