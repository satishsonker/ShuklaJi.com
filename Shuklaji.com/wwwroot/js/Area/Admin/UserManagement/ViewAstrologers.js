var $txtfeedback;
$(document).ready(function () {
    $query = app.methods.url.urlSearchParams();
    
    let $astrologerId = parseInt($query.get('astrologerid'));
    if ($query.has('astrologerid')) {
       
        api.get(app.api.urls.userManagement.getAstrologer + `?astrologerid=${$astrologerId}`).then(function (data) {
            $('.astro-profile').attr('src', data.data[0].photo);
            $('.astro-name').text(data.data[0].name);
            $('.astroname').text(data.data[0].name);
            $('.astro-price').text(`${data.data[0].consultPrice}/Min`);

            $('.astro-exp').text(`${data.data[0].experience} years`);
            $('.astro-expert').text(`${data.data[0].experties}`);
            $('.astro-biography').html(data.data[0].biography);

            $('.astro-language').text(data.data[0].language);

            $('.astro-location').text(data.data[0].location);
            api.get(app.api.urls.userManagement.getAstrologerSchedule + `?astrologerid=${$astrologerId}`).then(function (data) {
                let $list = "";
                $(data.data).each(function (ind, ele) {
                    $list += ` <tr class="text-center">
                        <td class="col-md-4 table_right_border">
                         ${ele.day}
                        </td>
                        <td class="col-md-4 table_right_border">
                            ${ele.fromTime.substr(0, 5)} - ${ele.toTime.substr(0, 5)}
                        </td>
                    </tr>`;
                });
                $('.astro-availability tbody').empty().append($list);
            });

            api.get(app.api.urls.userManagement.getAstrologerRating + `?astrologerid=${$astrologerId}`).then(function (data) {
                let $list = "";
                let $totalRating = 0;
                if (data.data.length > 0) {
                    if (data.data.filter(x => x.userId === app.common.userId).length > 0)
                        $('.user-rating-container').hide();
                    else {
                        $('.user-rating-container').show();
                    }
                    $('.astro-review').text(data.data.length);
                    $(data.data).each(function (ind, ele) {
                        let $date = new Date(ele.date);
                        let $stars = '';
                        $totalRating += parseInt(ele.stars);
                        for (var i = 0; i < ele.stars; i++) {
                            $stars += '<i class="fas fa-star"></i> ';
                        }
                        $list += `<div class="row userReview-container">
                    <div class="col-lg-2 col-sm-12 user-data">
                        <div><div class="user-image" style="background:rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 1)">${ele.userName.substr(0, 1)}</div>
                        <div class="user-name">${ele.userName} - ${$date.toLocaleDateString()}</div>
                        <div class="user-star">${$stars}</div></div>
                    </div>
                    <div class="col-lg-10 col-sm-12">
                        <div class="userReview">${ele.review}</p>
                    </div></div>
                </div>`;
                    });
                    $('.user-feedback').empty().append($list);
                    let $finalRating = ($totalRating / data.data.length).toFixed(1);
                    $('.astro-rating').text(`Rating : ${isNaN($finalRating) ? 0 : $finalRating}/5`);
                }
                else if (app.common.userId !== undefined && app.common.userId > 0 && app.common.userRole !== undefined && app.common.userRole.toLowerCase().indexOf('admin') === -1) {
                    $('.user-rating-container').show();
                }
            });
        });
    } else {
        app.toast.warning("Unable to find Astrologer");
    }

    $txtfeedback = $('#txtfeedback').dxTextArea({
        value: "",
        placeholder: "Enter feedback source",
        showClearButton: true
    }).dxValidator({
        validationRules: [{
            type: "required",
            message: "Feedback source is required."
        }]
    }).dxTextArea('instance');


    $btn = $("#btnSave").dxButton({
        icon: "save",
        type: "success",
        text: "Submit",
        onClick: function () {
            if ($query.has('astrologerid') && parseInt($query.get('astrologerid')) > 0) {
                var $param = {
                    UserId: app.common.userId,
                    Review: $txtfeedback.option('value'),
                    Stars: $('.userStars').data('totalstart'),
                    AstrologerId: parseInt($query.get('astrologerid'))
                }
                api.post(app.api.urls.userManagement.saveAstrologerRating, $param)
                    .then(api.successMsgHandler)
                    .catch(api.errorHandler);
            }
            else {
                app.toast.warning(app.userMsg.astrolNotExist);
            }
        }
    }).dxButton('instance');
});

var addSchedule = function () {
    app.methods.url.redirectTo(`${app.page.urls.adminArea.userManagement.astrologerSchedule}?astrologerId=${$query.get('astrologerid')}`);
}

$(document).on('click', '.userStars li i', function () {
    var $star = parseInt($(this).data('star'));
    $('.userStars li i').removeClass('starColor');
    for (var i = 1; i <= $star; i++) {
        $(`.s${i}`).addClass('starColor');
        $('.userStars').data('totalstart', i);
    }    
});