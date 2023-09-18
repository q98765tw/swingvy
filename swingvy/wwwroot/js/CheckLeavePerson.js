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
            url: "/CheckLeavePerson/ApproveLeave", // �Юھڱz��Controller���Ѷi��վ�
            type: "POST",
            data: data,
            success: function (result) {
                // �B�z���\�^�Ǫ����G
                console.log(result);
                // alert����
                window.location.href = "/CheckLeaveAll/Index";
            },
            error: function (xhr, status, error) {
                // �B�z���~
                console.error("�o�Ϳ��~:", error);
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
            url: "/CheckLeavePerson/ApproveLeave", // �Юھڱz��Controller���Ѷi��վ�
            type: "POST",
            data: data,
            success: function (result) {
                // �B�z���\�^�Ǫ����G
                console.log(result);
                // alert����
                window.location.href = "/CheckLeaveAll/Index";
            },
            error: function (xhr, status, error) {
                // �B�z���~
                console.error("�o�Ϳ��~:", error);
            }
        });
    }
})