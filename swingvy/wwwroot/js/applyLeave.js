//
$('#postData').on('click', () => {
    var leaveType = $("#leaveType").val();
    var startTime = $("#startTime").val();
    var endTime = $("#endTime").val();
    if (leaveType == "" || startTime == "" || endTime == "") {
        alert("leaveType startTime endTime can not null");
        return
    }
    var confirmation = confirm("Are you sure");
    if (confirmation) {    
        var reason = $("#reason").val();
        var currentDate = new Date().toLocaleDateString();
        console.log(typeof (currentDate))
        console.log(leaveType, startTime, endTime, reason, currentDate)
        var data = {
            leaveType: parseInt(leaveType),
            startTime: startTime,
            endTime: endTime,
            reason: reason,
            applyTime:currentDate
        };
        $.ajax({
            url: "/ApplyLeave/ApplyLeave", // 請根據您的Controller路由進行調整
            type: "POST",
            data: data,
            success: function (result) {
                // 處理成功回傳的結果
                console.log(result);
                // alert視窗
                window.location.href = window.location.href;
            },
            error: function (xhr, status, error) {
                // 處理錯誤
                console.error("發生錯誤:", error);
            }
        });
    }
})