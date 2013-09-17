var RUIKit = {
    RDatePicker: {
        pickBirthday: {
            fillYear: function (id) {
                var startYear = 1900;
                var currentYear = new Date().getFullYear();
                for (var i = startYear; i <= currentYear; i++) {
                    $("#" + id + " .year:first").append("<option>" + i + "</option>");
                }
            },
            fillMonth: function (id) {
                var currentYear = new Date().getFullYear();
                var year = $("#" + id + " .year:first option:selected").val();
                var month = 12;
                var monthSelecter = $("#" + id + " .month:first");
                monthSelecter.children().remove();
                if (year == currentYear) {
                    month = new Date().getMonth() + 1;
                }
                for (var i = 1; i <= month; i++) {
                    monthSelecter.append("<option>" + i + "</option>");
                }
            },
            fillDay: function (id) {
                $("#" + id + " .day:first").children().remove();
                var year = $("#" + id + " .year:first option:selected").val();
                var month = $("#" + id + " .month:first option:selected").val();
                var currentYear = new Date().getFullYear();
                var currentMonth = new Date().getMonth() + 1;
                var day = 31;
                if (year == currentYear && month == currentMonth) {
                    day = new Date().getDate();
                }
                else {
                    switch (month) {
                        case "1":
                        case "3":
                        case "5":
                        case "7":
                        case "8":
                        case "10":
                        case "12":
                            day = 31;
                            break;
                        case "2":
                            day = 28;
                            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) {
                                day = 29;
                            }
                            break;
                        default:
                            day = 30;
                            break;
                    }
                }
                for (var i = 1; i <= day; i++) {
                    $("#" + id + " .day:first").append("<option>" + i + "</option>");
                }
            },
            init: function (id) {
                $("#" + id).html('<select class="year"></select>年<select class="month"></select>月 <select class="day"></select>日');
                this.fillYear(id);
                this.fillMonth(id);
                this.fillDay(id);
                var fm = this.fillMonth, fd = this.fillDay;
                $("#"+id+" .year:first option:nth-child(90)").prop("selected","selected");
                $("#" + id + " .year:first").bind("change", function () {
                    fm(id);
                    fd(id);
                });
                $("#" + id + " .month:first").bind("change", function () {
                    fd(id);
                });
            },
            getBirthday: function (id) {
                var oPicker = $("#"+id);
                var sBirthday = oPicker.children(".year:first").val()+"-"+oPicker.children(".month:first").val()+"-"+oPicker.children(".day:first").val();
                return sBirthday;
            },
            setBirthday: function (id,year,month,day) {
        
                var oPicker = $("#" + id);
                oPicker.children(".year:first").val(year);
                oPicker.children(".month:first").val(month);
                oPicker.children(".day:first").val(day);
            }
        }
    }
};
