let $slots = [], $sunFrom, $sunTo, $monFrom, $monTo, $tueFrom, $tueTo, $wedFrom, $wedTo, $thuFrom, $thuTo, $friFrom, $friTo, $satFrom, $satTo, $btn;
$(document).ready(function () {
    var $query = app.methods.url.urlSearchParams();
    $satFrom = $("#satFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $satTo.option('min', $satFrom.option('value'))
        }
    }).dxDateBox('instance');

    $satTo = $("#satTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

    $friFrom = $("#friFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $friTo.option('min', $friFrom.option('value'))
        }
    }).dxDateBox('instance');

    $friTo = $("#friTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

    $thuFrom = $("#thuFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $thuTo.option('min', $thuFrom.option('value'))
        }
    }).dxDateBox('instance');

    $thuTo = $("#thuTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

    $wedFrom = $("#wedFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $wedTo.option('min', $wedFrom.option('value'))
        }
    }).dxDateBox('instance');

    $wedTo = $("#wedTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

    $tueFrom = $("#tueFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $tueTo.option('min', $tueFrom.option('value'))
        }
    }).dxDateBox('instance');

    $tueTo = $("#tueTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

    $monFrom = $("#monFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $monTo.option('min', $monFrom.option('value'))
        }
    }).dxDateBox('instance');

    $monTo = $("#monTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

    $sunFrom=$("#sunFrom").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
        onValueChanged: function (data) {
            $sunTo.option('min', $sunFrom.option('value'))
        }
    }).dxDateBox('instance');

    $sunTo=$("#sunTo").dxDateBox({
        type: "time",
        value: new Date(),
        showClearButton: true,
    }).dxDateBox('instance');

   
    $('.chk').click(function () {
        var $val, $totalTime, $fromTime, $toTime;
        $val = $(this).val();
        if ($(this).is(':checked')) {
            
            $(this).parent().siblings('.col-md-3').children('span').css('text-decoration', 'line-through');
            $totalTime = $(this).parent().siblings('.col-md-3').children('span').text();
            $fromTime = getHours($totalTime.split('-')[0].trim());
            $toTime = getHours($totalTime.split('-')[1].trim());
            
            switch ($val) {
                case 'sun':
                    $sunFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $sunTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                case 'mon':
                    $monFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $monTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                case 'tue':
                    $tueFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $tueTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                case 'wed':
                    $wedFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $wedTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                case 'thu':
                    $thuFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $thuTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                case 'fri':
                    $friFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $friTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                case 'sat':
                    $satFrom.option('value', new Date().setHours($fromTime.h, $fromTime.m, 0, 0));
                    $satTo.option('value', new Date().setHours($toTime.h, $toTime.m, 0, 0));
                    break;
                default:
            }
            $(`edit-time,.${$val}`).show();

        }
        else {
            $(this).parent().siblings('.col-md').children('span').css('text-decoration', 'none');
            $(this).parent().siblings('.col-md-3').children('span').css('text-decoration', 'none');
            $(`edit-time,.${$val}`).hide();
        }
    });
    $btn = $("#btnSave").dxButton({
        icon: "save",
        type: "success",
        text: "Save",
        onClick: function () {
            if ($('.dx-validationsummary').children().length > 0) {
                $('#summary-container').show();
            }
            else {
                if ($query.has('astrologerId') && parseInt($query.get('astrologerId')) > 0) {
                    var $param = [];
                    $('.ckLbl').each(function (ind, ele) {
                        var $span = $(ele).parent().parent().find('.col-md-3.col-sm-12').find('span');
                        if ($span.length > 0) {
                            $($span).each(function (ind, spanEle) {
                                $param.push({
                                    AstrologerId: parseInt($query.get('astrologerId')),
                                    Day: $(ele).text(),
                                    FromTime: get24HourFormat($(spanEle).text().split('-')[0]),
                                    ToTime: get24HourFormat($(spanEle).text().split('-')[1].trim()),
                                    Remark: '',
                                    Is24Hours: false,
                                    UserId: app.common.userId
                                });
                            });
                        }
                    });

                    api.post(app.api.urls.userManagement.saveAstrologerSchedule, $param)
                        .then(api.successMsgHandler)
                        .catch(api.errorHandler);
                }
                else {
                    app.toast.warning(app.userMsg.astrolNotExist);
                }
            }
        }
    }).dxButton('instance');
});

var getHours = function ($timeStr) {
    var $h = parseInt($timeStr.split(":")[0]);
    var $m = parseInt($timeStr.split(":")[1].substr(0, 2));
    var $med = $timeStr.split(":")[1].substr(3, 2);
    $h = $med === "PM" ? $h + 12 : $h;
    return { h: $h, m: $m };
}

var get24HourFormat = function ($timeStr) {
    var $obj = getHours($timeStr);
    return ($obj.h < 10 ? '0' + $obj.h : $obj.h) + ':' + ($obj.m < 10 ? '0' + $obj.m : $obj.m); 
}

$(document).on('click', '.fas.fa-times.text-danger', function () {
    $(this).parent().remove();
});

$(document).on('click', "i.fas.fa-plus", function () {
    var $key = $(this).data('key');
    var $from, $to;
    switch ($key) {
        case 'sun':
            $from=  $sunFrom.option('value');
            $to = $sunTo.option('value');
            break;
        case 'mon':
            $from =   $monFrom.option('value');
            $to =  $monTo.option('value');
            break;
        case 'tue':
            $from =   $tueFrom.option('value');
            $to =  $tueTo.option('value');
            break;
        case 'wed':
            $from =   $wedFrom.option('value');
            $to =  $wedTo.option('value');
            break;
        case 'thu':
            $from =   $thuFrom.option('value');
            $to =  $thuTo.option('value');
            break;
        case 'fri':
            $from =   $friFrom.option('value');
            $to =  $friTo.option('value');
            break;
        case 'sat':
            $from =  $satFrom.option('value');
            $to =  $satTo.option('value');
            break;
        default:
    }

    if ($from !== '' && $to!=='') {
    $from = new Date($from);
        $to = new Date($to);
        var $scheduleContainer = $(`div[data-key="${$key}"]`),
            $isAppended=false,
            $newTime = `${$from.toLocaleTimeString().replace(':00 ', ' ')}-${$to.toLocaleTimeString().replace(':00 ', ' ')}`;
        $($scheduleContainer).find('span').each(function (ind, ele) {
            if ($(ele).text().indexOf($newTime)>-1) {
                $isAppended = true;
            }
        });
        if (!$isAppended) {
            $scheduleContainer.append(`<span class="scheContainer"> ${$newTime}<i data-key="tue" class="fas fa-times text-danger"></i></span>`);
        }
        else {
            app.toast.warning(app.userMsg.astroScheduleExist);
        }
    }
});