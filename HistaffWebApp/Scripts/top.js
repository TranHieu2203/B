
$(document).mouseup(function (e) {
    var container = $(".box23.All");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        $(".box23.All").css("display", "none");
    }
    var containerProfile = $(".box23.approveProfile");
    if (!containerProfile.is(e.target) && containerProfile.has(e.target).length === 0) {
        $(".box23.approveProfile").css("display", "none");
    }
});

$(document).ready(function () {
    $(".box23.All .box48").click(function () {
        var dt = $(this).attr("data");

        if ($("#collapse-" + dt + "").is(":visible")) {
            $("#collapse-" + dt + "").slideUp();
            $(this).children("i").removeClass("fa fa-chevron-up");
            $(this).children("i").addClass("fa fa-chevron-down");
        }
        else {
            $("#collapse-" + dt + "").slideDown();
            $(this).children("i").removeClass("fa fa-chevron-down");
            $(this).children("i").addClass("fa fa-chevron-up");
            $("#collapse-" + dt + "").empty();
            $("#collapse-" + dt + "").append("Đang tải dữ liệu... <img src=\"Static/Images/indicator.gif\" style=\"height: 22px;\" /></a>");
            $.ajax({
                type: "POST",
                url: "Default.aspx/getInformationRemind",
                data: '{data: "' + dt + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    $("#collapse-" + dt + "").empty();
                    $("#collapse-" + dt + "").append(response.d);
                }, error: function (response) {

                }, failure: function (response) {

                }
            });
        }
    });
    $(".box23.approveProfile .box48").click(function () {
        var dt = $(this).attr("data");

        if ($("#collapse-" + dt + "").is(":visible")) {
            $("#collapse-" + dt + "").slideUp();
            $(this).children("i").removeClass("fa fa-chevron-up");
            $(this).children("i").addClass("fa fa-chevron-down");
        }
        else {
            $("#collapse-" + dt + "").slideDown();
            $(this).children("i").removeClass("fa fa-chevron-down");
            $(this).children("i").addClass("fa fa-chevron-up");
            $("#collapse-" + dt + "").empty();
            $("#collapse-" + dt + "").append("Đang tải dữ liệu... <img src=\"Static/Images/indicator.gif\" style=\"height: 22px;\" /></a>");
            $.ajax({
                type: "POST",
                url: "Default.aspx/getInformationRemind",
                data: '{data: "' + dt + '"}',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    $("#collapse-" + dt + "").empty();
                    $("#collapse-" + dt + "").append(response.d);
                }, error: function (response) {

                }, failure: function (response) {

                }
            });
        }
    });
    //show thongbao
    $('.box3.All').click(function () {
        debugger;
        $(".box23.All").show();
    });
    //show thongbao
    $('.box3.approveProfile').click(function () {
        debugger;
        $(".box23.approveProfile").show();
    });
});






