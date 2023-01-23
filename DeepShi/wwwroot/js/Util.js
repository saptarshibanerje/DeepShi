//AIzaSyDjbLJIvmQvrUeu3ysqwBbIQw_EYnth2oU
$(document).off('keydown', '.numeric');
$(document).on('keydown', '.numeric', function (e) {
    var dotCount = 0;
    if ($(e.target).prop("readonly") && e.keyCode == 8) {
        e.preventDefault();
    }
    if (e.keyCode == 110 || e.keyCode == 190) {
        for (var i = 0; i < this.value.length; ++i) {
            if (this.value[i] == '.')
                dotCount = dotCount + 1;
        }
        if (dotCount >= 1)
            e.preventDefault();
    }
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 109, 189]) !== -1 ||
        // Allow: Ctrl+A
        (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return true;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});
$(document).off('keydown', '.integer');
$(document).on('keydown', '.integer', function (e) {
    var dotCount = 0;
    if ($(e.target).prop("readonly") && e.keyCode == 8) {
        e.preventDefault();
    }
    if (e.keyCode == 110 || e.keyCode == 190) {
        e.preventDefault();
    }
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 109, 189]) !== -1 ||
        // Allow: Ctrl+A
        (e.keyCode == 65 && e.ctrlKey === true) ||
        // Allow: home, end, left, right
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        // let it happen, don't do anything
        return true;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105) && (e.keyCode != 189)) {
        e.preventDefault();
    }
});
const validateEmail = (email) => {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};

$(document).ready(function () {
    $(document).on('keypress', '.numbertext', function (event) {
        if (((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }
    });
    $(document).on('keypress', '.decimeltext', function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && ((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }
        var text = $(this).val();
        if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2) && (event.which != 0 && event.which != 8) && ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });
    $(document).on('keypress', '.decimalPercent', function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && ((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }
        var text = $(this).val();
        if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 2) && (event.which != 0 && event.which != 8) && ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });
    $(document).on('keyup', '.decimalPercent', function (event) {
        var text = $(this).val();
        if (parseFloat(text) < 0 || parseFloat(text) > 100) {
            event.preventDefault();
            $(this).val('');
        }
    });
    $(document).on('keypress', '.decimelLatLontext', function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && ((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }
        var text = $(this).val();
        if ((text.indexOf('.') != -1) && (text.substring(text.indexOf('.')).length > 15) && (event.which != 0 && event.which != 8) && ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });

    $(document).on('focusout', '.email', function () {
        const email = $(this).val();

        if (validateEmail(email)) {
            if ($(this).hasClass('bg-danger')) {
                $(this).removeClass('bg-danger')
            }
            

        } else {
            $(this).addClass("bg-danger");
            if ($(this).hasClass('bg-success')) {
                $(this).removeClass('bg-success')
            }
            /*$(this).addClass("email-vlidation-error");*/
            $(this).focus();
        }

    });

    $(document).on('keypress', '.multiple-phone', function (event) {
        if (((event.which < 48 || event.which > 57) && (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }
    });

    //     $(document).on('keypress', '.exampleInputEmail1', function (event) {
    //         let text = "123456789";
    //  let result = text.match(/[1-4]/g);
    //  document.getElementById("demo").innerHTML = result;
    //      })
    var PageName = $("#pageName")[0];
    if (PageName != undefined && PageName != null) {
        $(".page_link").addClass("map_menue");
    } else {
        $(".page_link").removeClass("map_menue");
    }
    $(document).on("click", "input.page_link", function () {
        if ($(this).hasClass("map_menue") != true) {
            var LayerId = parseInt($(this).attr("data-layerid"));
            window.location.href = VirtualPath + "map/index?q=" + LayerId;
        }
    });
});
util = {
    downloadFile: function (FilePath) {
        $('#FileDownloader').remove();
        var a = $('<a id="FileDownloader"/>').attr({
            style: "visibility:hidden;display:none"
        }).appendTo(document.body);

        var iframe = $('<iframe id="IframeToDownloadFile"/>').attr({
            src: FilePath,
            style: "visibility:hidden;display:none"
        }).appendTo(a);

        var i = setInterval(function () {
            if ($('#IframeToDownloadFile').contents().find('html').length > 0) {
                clearInterval(i);
            }
        }, 1500);
    },
    showLoader: function () {
        $("#AppLoader").addClass("active");
    },
    hideLoader: function () {
        $("#AppLoader").removeClass("active");
    },
    toCamelCase: function (str) {
        return str.toLowerCase().replace(/(?:^|\s)\w/g, function (match) {
            return match.toUpperCase();
        });
    },
    showRequired: function (item) {
        $(item).addClass('required-show');
        setTimeout(function () { $('.required-show').removeClass('required-show') }, 4000)
    },
    getQueryParamValue(sParam) {
        var sPageURL = window.location.search.substring(1), sURLVariables = sPageURL.split('&'), sParameterName, i;
        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0].toLocaleLowerCase() === sParam.toLocaleLowerCase()) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
    },
    removeURLParameter(url, parameter) {
        //prefer to use l.search if you have a location/link object
        var urlparts = url.split('?');
        if (urlparts.length >= 2) {
            var prefix = encodeURIComponent(parameter) + '=';
            var pars = urlparts[1].split(/[&;]/g);
            //reverse iteration as may be destructive
            for (var i = pars.length; i-- > 0;) {
                //idiom for string.startsWith
                if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                    pars.splice(i, 1);
                }
            }
            return urlparts[0] + (pars.length > 0 ? '?' + pars.join('&') : '');
        }
        return url;
    }
}

