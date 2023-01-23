var Helper = {
    getDropdownValue: function (identity) {
        return $(identity + ' option:selected').val();
    },
    getDropdownText: function (identity) {
        return $(identity + ' option:selected').text();
    },
    setDropdownValue: function (identity, value) {
        $(identity).val(value);
    },
    getTextboxValue: function (identity) {
        return $(identity).val();
    },
    setTextboxValue: function (identity, value) {
        $(identity).val(value);
    },
    getDatepickerDateString: function (identity) {
        var dateObj = $(identity).datepicker("getDate");
        if (dateObj) {
            var d = dateObj.getDate();
            d = d < 10 ? '0' + d : d;
            var m = (dateObj.getMonth() + 1);
            m = m < 10 ? '0' + m : m;
            var y = dateObj.getFullYear();

            return d + '/' + m + '/' + y;
        }
        else {
            return "";
        }
    },
    getDatepickerDate: function (identity) {
        return $(identity).datepicker("getDate");
    },
    setDatepickerDate: function (identity, dateString) {
        $(identity).datepicker("update", dateString);
    },
    getCheckboxIsChecked: function (identity) {
        return $(identity).is(':checked');
    },
    setCheckboxChecked: function (identity, boolval) {
        $(identity).prop("checked", boolval);
    },
    getRadiobuttonValue: function (identityName) {
        return $("input[type='radio'][name='" + identityName + "']:checked").val();
    },
    setRadiobuttonValue: function (identityName, value) {
        $("input[type='radio'][name='" + identityName + "'][value='" + value + "']").click();
    },
    setDisable: function (identity) {
        $(identity).attr("disabled", 'disabled');
    },
    setDateTimeDisable: function (identity) {
        $(identity).children().attr("disabled", "disabled")
    },
    setDisableClear: function (identity) {
        $(identity).attr("disabled", 'disabled').val('');
    },
    setEnable: function (identity) {
        $(identity).removeAttr("disabled");
    },
    setDateTimeEnable: function (identity) {
        $(identity).children().removeAttr("disabled")
    },
    multiselectOption: {
        load: function (identity, textvalue) {
            //if multiselect already bound
            if ($(identity).parent().hasClass('multiselect-native-select')) {
                $(identity).multiselect('destroy');
            }
            $(identity).multiselect({
                includeSelectAllOption: true,
                nonSelectedText: textvalue
            });
            $(identity).multiselect('refresh');
        },
        enable: function (identity) {
            $(identity).multiselect('enable');
        },
        disable: function (identity) {
            $(identity).multiselect('disable');
        },
        destroy: function (identity) {
            $(identity).multiselect('destroy');
        },
        refresh: function (identity) {
            $(identity).multiselect('refresh');
        },
        getValues: function (identity) {
            return $(identity).val();
        },
        setValues: (function (identity, commaSeperatedValues) {
            //Deselect All checked items
            $(identity).parent().find('.multiselect-option input[type="checkbox"]:checked').trigger('click');

            var v = commaSeperatedValues.split(',');
            for (var i = 0; i < v.length; i++) {
                var chechBox = $(identity).parent().find('.multiselect-option input[type="checkbox"][value="' + v[i] + '"]');
                if (!chechBox.prop('checked')) {
                    chechBox.trigger('click');
                }
            }

        }),
        setAllCheckboxChecked: (function (identity, boolvalue) {
            var checkBox = $(identity).parent().find('.dropdown-menu li input[type="checkbox"]');
            checkBox.prop('checked', boolvalue);
        }),
        getSelectedText: (function (identity) {
            var txt = [];
            $(identity + " option:selected").each(function () {
                var $this = $(this);
                if ($this.length) {
                    var selText = $this.text();
                    txt.push(selText);
                }
            });
            return txt;
        })
    },
    getMultipleTextboxValue: function (identify) {
        var value = "";
        $(identify).each(function () {
            value = value + "," + $(this).val();
        });
        return value;
    },
    dataAttribute: {
        set: function (identity, attr, val) {
            $(identity).data(attr, val);
        },
        get: function (identity, attr) {
            return $(identity).data(attr);
        }
    },
    display: {
        hide: function (identity) {
            $(identity).css("display", "none");
        },
        show: function (identity) {
            $(identity).css("display", "block");
        }
    },
    nonEditableFields: function (noneditableFields) {
        var ddlArry = noneditableFields.Data.filter(function (el) { return el.Type == "ddl"; })
        var txtArry = noneditableFields.Data.filter(function (el) { return el.Type == "txt"; })
        var dpArry = noneditableFields.Data.filter(function (el) { return el.Type == "dp"; })
        var MddllArry = noneditableFields.Data.filter(function (el) { return el.Type == "Mddl"; })
        var div = noneditableFields.Data.filter(function (el) { return el.Type == "Display" })
        var chkArry = noneditableFields.Data.filter(function (el) { return el.Type == "chk" })
        var btnArry = noneditableFields.Data.filter(function (el) { return el.Type == "btn"; })
        if (ddlArry.length > 0) {
            for (var i = 0; i < ddlArry.length; i++) {
                Helper.setDisable(ddlArry[i].ID);
            }
        }
        if (txtArry.length > 0) {
            for (var i = 0; i < txtArry.length; i++) {
                Helper.setDisable(txtArry[i].ID);
            }
        }
        if (dpArry.length > 0) {
            for (var i = 0; i < dpArry.length; i++) {
                Helper.setDateTimeDisable(dpArry[i].ID);
            }
        }
        if (MddllArry.length > 0) {
            for (var i = 0; i < MddllArry.length; i++) {
                Helper.multiselectOption.disable(MddllArry[i].ID);
            }
        }
        if (chkArry.length > 0) {
            for (var i = 0; i < chkArry.length; i++) {
                Helper.setDisable(chkArry[i].ID);
            }
        }
        if (div.length > 0) {
            for (var i = 0; i < div.length; i++) {
                Helper.display.hide(div[i].ID);
            }
        }
        if (btnArry.length > 0) {
            for (var i = 0; i < btnArry.length; i++) {
                Helper.setDisable(btnArry[i].ID);
            }
        }
    },
    html: {
        add: function (identity, htmlvalue) {
            $(identity).html(htmlvalue);
        },
        append: function (identity, htmlvalue) {
            $(identity).append(htmlvalue);
        },
        remove: function (identity) {
            $(identity).empty();
        }
    }
}
