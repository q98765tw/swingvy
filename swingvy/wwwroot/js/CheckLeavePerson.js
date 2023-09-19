$('#approve').on('click', function () {
    var confirmation = confirm("Are you sure approve");
    if (confirmation) {
        var leaveOrderId = $(this).data("leaveorder-id");
        var memberId = $(this).data("member-id");
        var name = $("#leaveType").text();
        var startTime = $("#startTime").text();
        var endTime = $("#endTime").text();
        console.log(leaveOrderId, memberId, name, startTime, endTime)
        var data = {
            leaveOrder_id: leaveOrderId,
            member_id: memberId,
            name: name,
            startTime: startTime,
            endTime: endTime,
        };
        $.ajax({
            url: "/CheckLeavePerson/ApproveLeave", // 請根據您的Controller路由進行調整
            type: "POST",
            data: data,
            success: function (result) {
                // 處理成功回傳的結果
                console.log(result);
                // alert視窗
                window.location.href = "/CheckLeaveAll/Index";
            },
            error: function (xhr, status, error) {
                // 處理錯誤
                console.error("發生錯誤:", error);
            }
        });
    }
})
$('#reject').on('click', function () {
    var confirmation = confirm("Are you sure reject");
    if (confirmation) {
        var leaveOrderId = $(this).data("leaveorder-id");
        var memberId = $(this).data("member-id");
        var name = $("#leaveType").text();
        var startTime = $("#startTime").text();
        var endTime = $("#endTime").text();
        console.log(leaveOrderId, memberId, name, startTime, endTime)
        var data = {
            leaveOrder_id: leaveOrderId,
            member_id: memberId,
            name: name,
            startTime: startTime,
            endTime: endTime,
        };
        $.ajax({
            url: "/CheckLeavePerson/ApproveLeave", // 請根據您的Controller路由進行調整
            type: "POST",
            data: data,
            success: function (result) {
                // 處理成功回傳的結果
                console.log(result);
                // alert視窗
                window.location.href = "/CheckLeaveAll/Index";
            },
            error: function (xhr, status, error) {
                // 處理錯誤
                console.error("發生錯誤:", error);
            }
        });
    }
})