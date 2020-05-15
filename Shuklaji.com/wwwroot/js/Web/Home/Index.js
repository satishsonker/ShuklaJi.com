$(document).ready(function () {
    var $query = app.methods.url.urlSearchParams();
    if ($query.has('txn')) {
        app.common.token = $query.get('txn');
    }
    $('.current-year').text(new Date().getFullYear());
    api.get(app.api.urls.zodiacManagement.getZodiacList).catch(api.errorHandler).then(function (data) {
        if (data !== undefined && data.data !== undefined) {
     var $zodiacList = '', $yearlyHoroscope = '', $zodicSign = '';

            $(data.data).each(function (ind, ele) {
                $yearlyHoroscope += `<li><a href="#"><img class="zodiacListLogo" src="${ele.logo}" /> ${ele.nameEng} Horoscope ${new Date().getFullYear()}</a></li>`;
                $zodicSign += `<li><a href="#"><img class="zodiacListLogo" src="${ele.logo}" /> ${ele.nameEng}</a></li>`;
                $zodiacList += `<div class="col-sm-4 col-md-2 col-lg-1 col-4">
<a href="/web/zodiac/viewzodiac?name=${ele.nameEng}&reporttype=daily&date=${app.methods.common.getGridDateTime(new Date().toDateString()).split('-')[0].trim()}&id=${ele.id}">
                                <img class="zodiacListLogo" src="${ele.logo}" />
                                <p>${ele.nameEng}</p>
                                <p>${ele.nameHindi}</p>
                                <p>${ele.rangeFrom} - ${ele.rangeTo}</p></a>
                            </div>`;
            });
            //$zodiacList += '<div class="hr"></div>';
            $('.yearly-horoscope').empty().append($yearlyHoroscope);
            $('.zodiac-sign').empty().append($zodicSign);
            $('.zodiacContainer').empty().append($zodiacList);
        }
    });

    api.get(app.api.urls.userManagement.getAstrologer).then(function (data) {
        var $astroList = '',$ind=0;
        $(data.data).each(function (ind, ele) {
            $astroList += `<div class="col-sm-12 col-md-4 col-lg-3 col-12">
            <div class="astro-profile">
                <div class="header">

                    <a href="${app.page.urls.adminArea.userManagement.viewAstrologers}?astrologerid=${ele.astrologerId}`+(app.common.userRole.toLowerCase().indexOf('admin')>-1?'&txntype=admin':'')+`">
                        <img class="b-lazy pull-left img-circle margin_Rgt10 img_profile b-loaded" width="65" height="65" alt="${ele.name}" title="${ele.name}" src="${ele.photo}">
                    </a>
                    <a href="${app.page.urls.adminArea.userManagement.viewAstrologers}?astrologerid=${ele.astrologerId}` + (app.common.userRole.toLowerCase().indexOf('admin') > -1 ? '&txntype=admin' : '') +`" class="as_profile_font">${ele.name}</a>
                    <div class="review_mobile">
                        <div class="h4 star_rating_a">
                            ${ele.rating}
                            <i class="fas fa-star"></i>
                        </div>
                    </div>
                    <div class="clearfix HideTopDesktp"></div>
    
                </div>
                <div class="body">
                    <p title="Price per min for call"><i class="fas fa-rupee-sign" style="color: blue;"></i> <span class="astro-price">${ele.consultPrice}/Min</span></p>
                    <p title="Experts in :  ${ele.experties}"><i class="fas fa-graduation-cap"></i> <span class="astro-expert">${ele.experties.substr(0, 30)}</span></p>
                    <p title="Language : ${ele.language}"><i class="fas fa-language" style="color:green;"></i> <span class="astro-rating">${ele.language.substr(0,30)}</span></p>
                    <p title="Rating"><i class="fas fa-map-marker-alt"></i> <span class="astro-rating">${ele.location}</span></p>
                    <p title="Experience"><i class="fas fa-certificate"  style="color: #fc0;"></i> <span class="astro-exp">${ele.experience} years</span></p>
                    <button>Consult Now <i class="fas fa-arrow-right"></i></button>
                </div>
            </div>
        </div>`;
            if ($ind > 0 && $ind % 4 === 0) {
                $astroList += `<div class="row">${$astroList}</div>`;
            }
            $ind++;
        });
        if ($ind <4) {
            $astroList = '<div class="row">'+$astroList+'</div>';
        }
       $('.astroContainer').empty().append($astroList);
    });
});