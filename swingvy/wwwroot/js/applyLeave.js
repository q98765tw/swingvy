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
        switch (leaveType) {
            case 'Special': {
                leaveType = 0;
                break;
            }
            case 'Personal': {
                leaveType = 1;
                break;
            }
            case 'Sick': {
                leaveType = 2;
                break;
            }
            case 'Menstrual': {
                leaveType = 3;
                break;
            }
        }
        console.log(leaveType, startTime, endTime, reason, currentDate)
        var data = {
            leaveType: leaveType,
            startTime: startTime,
            endTime: endTime,
            reason: reason,
            applyTime:currentDate
        };
        $.ajax({
            url: "/ApplyLeave/ApplyLeave", // �Юھڱz��Controller���Ѷi��վ�
            type: "POST",
            data: data,
            success: function (result) {
                // �B�z���\�^�Ǫ����G
                console.log(result);
                // alert����
                window.location.href = window.location.href;
            },
            error: function (xhr, status, error) {
                // �B�z���~
                console.error("�o�Ϳ��~:", error);
            }
        });
    }
})