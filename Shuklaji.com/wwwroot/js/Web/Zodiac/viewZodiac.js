$(document).ready(function () {
    var $query = app.methods.url.urlSearchParams(), $reporttype, $date, $id;
    if ($query.has('reporttype')) {
        $reporttype = $query.get('reporttype');
    }
    if ($query.has('date')) {
        $date = $query.get('date');
    }
    if ($query.has('id')) {
        $id = $query.get('id');
    }
    api.get(app.api.urls.web.zodiac.getHoroscope + `?reporttype=${$reporttype}&date=${$date}&id=${$id}`).then(function (data) {

        if (data.data.table.length > 0) {
            $('.banner-container .banner h1').text(`${data.data.table[0].zodiacNameEng} ${$reporttype} Horoscope`);
            $('.logo').attr('src', `${data.data.table[0].logo}`);
            $('.single-sign-daily-icon').show()
            $('.description').html(data.data.table[0].description);
        }
        else {
            $('.single-sign-daily-icon').hide();
            $('.description').html(`<div class="alert alert-danger">No Data Found</div>`);
        }

        // Append Zodiac
        if (data.data.table1.length > 0) {
            var $zodiac = $('.chooseZodiac'), $signList = '';
            $($zodiac).empty().append(`<option value="">Select Sign</option>`);
            $(data.data.table1).each(function (ind, ele) {
                $signList += `<option value="${ele.id}">${ele.zodiacNameEng}</option>`;
            });
            $($zodiac).append($signList);
        }
    }).then(api.errorHandler)
});