/// <reference path="../../lib/jquery/dist/jquery.min.js" />
/// <reference path="../common/apiurls.js" />
/// <reference path="../common/constants.js" />
/// <reference path="../common/global.js" />

$(document).ready(function () {
    app.methods.msg.readMsgQueue();

    var $loginData = JSON.parse(app.methods.storage.getData(app.methods.storage.storageKey.loginData));
    if ($loginData !== null && $loginData.userData !== undefined && $loginData.userData !== null && $loginData.userData.length > 0) {
        app.common.userId = $loginData.userData[0].userId;
        app.common.userData = $loginData.userData[0];
        app.common.token = $loginData.token;
        app.common.userRole = $loginData.userData[0].roleName;
        $('.profileUserName').text($loginData.userData[0].firstName + ' ' + $loginData.userData[0].lastName);
        $('.profileRoleName').text($loginData.userData[0].roleName);
        $('.profileLastLogin').text(app.methods.common.getGridDateTime($loginData.userData[0].lastLogin));
    }

    api.get(app.api.urls.appSetting.getAppSetting).then(function (data) {
        if (data.data.length > 0) {
            $('#appLogo').attr('src', data.data[0].appLogo);
            $('#appName').text(data.data[0].appName);
            app.common.appName = data.data[0].appName;
            app.methods.storage.setData(app.methods.storage.storageKey.appSetting, data.data);
        }
        else {
            app.toast.warning('Unable to get App setting data.');
        }
    }).catch(api.errorHandler);

    api.get(app.api.urls.baseLayout.layout.getLayoutMenu).then(function (data) {
        if (data.statusCode === app.api.statusCode.ok) {
            if (data.data.length > 0) {
                let $headerMenu = $('#headerMenu');
                let $leftMenu = $('#LeftMenu ul');
                $headerMenu.empty();
                $leftMenu.empty();
                let $headerMenuList = '';
                let $leftMenuList = '';
                $(data.data).each(function (ind, ele) {
                    if (ele.position === app.menu.position.top && ele.accessBy===app.menu.accessBy.admin) {
                        if (ele.childMenu === null || ele.childMenu.length === 0) {
                            $headerMenuList += `<li class="nav-item"><a class="nav-link" href="${ele.menuPath}"><i style="color:${ele.iconColor}" class="${ele.iconClass}"></i> ${ele.displayName}</a></li>`;
                        }
                        else {
                            $headerMenuList += `<li class="nav-item dropdown"><a class="nav-link dropdown-toggle" href="${ele.menuPath}" id="navbardrop" data-toggle="dropdown">${ele.displayName}</a><div class="dropdown-menu">`;
                            $(ele.childMenu).each(function (indC, eleC) {
                                $headerMenuList += `<a class="dropdown-item" href="${eleC.menuPath}"><i style="color:${eleC.iconColor}" class="${eleC.iconClass}">${eleC.displayName}</i></a>`;
                            });
                            $headerMenuList += `</div></li>`;
                        }
                    }
                    else if (ele.position === app.menu.position.left) {
                        if (ele.childMenu === null || ele.childMenu.length === 0) {
                            $leftMenuList += `<li data-toggle="popover" data-placement="right" title="${ele.displayName}"><a href="${ele.menuPath}"><i style="color:${ele.iconColor}" class="${ele.iconClass}"></i><span class="dname">${ele.displayName}</span></a></li>`;
                        }
                        else {
                            $leftMenuList += `<li title="${ele.displayName}" data-toggle="collapse" data-target="#navbarSupportedContent${ind}" aria-controls="navbarSupportedContent${ind}" aria-expanded="false" aria-label="Toggle navigation"><i style="color:${ele.iconColor}" class="${ele.iconClass}"></i><span class="dname">${ele.displayName}</span></li>`;
                            $leftMenuList += `<ul class="collapse left-menu-child" id="navbarSupportedContent${ind}">`;
                            $(ele.childMenu).each(function (indC, eleC) {
                                $leftMenuList += `<li class="nav-item" title="${eleC.displayName}"> <a class="nav-link" href="${eleC.menuPath}"><i style="color:${ele.iconColor}" class="${eleC.iconClass}"></i><span class="dname">${eleC.displayName}</span></a></li>`;
                            });
                            $leftMenuList += '</ul>';
                        }
                    }
                });
                $headerMenu.append($headerMenuList);
                $leftMenu.append($leftMenuList);

                //Initialize Tooltip
                //$('[data-toggle="popover"]').popover();

                $('.navbar-left li a').each(function (i, e) {
                    $(this).parent().removeClass('active-menu');
                    if (location.pathname.indexOf($(e).attr('href')) > -1) {
                        $(this).parent().addClass('active-menu');
                        if ($(this).parent().parent().hasClass('left-menu-child')) {
                            $(this).parent().parent().parent().click();
                           // $(this).parent().parent().parent().addClass('active-menu');
                        }
                    }
                });
            }
        }
        else {
            app.toast.warning('Unable to load menu.');
        }
    }).catch(api.errorHandler);

    $('#iclose').on('click', function () {
        $('#' + $(this).data('close')).hide();
    });

   
    $('#profileLogout').on('click', function () {
        app.methods.url.redirectTo(app.page.urls.common.siginin.login);
        app.methods.storage.setData(app.methods.storage.storageKey.loginData,[]);
    });

    $('.help-data').each(function (ind, ele) { //Tooltip for Question mark
        $(ele).html($(ele).data('data'));
    });
});

$(function () {
    $('[data-toggle="popover"]').popover();
});

//$(document).on('click', '#LeftMenu>ul>li', function() {
//    $('.navbar-left ul li[data-toggle="collapse"]:not(.collapsed)').click();
//});

$(document).on('click', '.left-menu-opener', function () {
    if ($(this).find('i').hasClass('la-hand-point-right')) {
        $(this).find('i').removeClass('la-hand-point-right').addClass('la-hand-point-left');
        $('.navbar-left').animate({ width: "250px" }, 300, 'linear', function () {
            $('.dname').show();
        });
    }
    else {
        $(this).find('i').addClass('la-hand-point-right').removeClass('la-hand-point-left');
        $('.navbar-left').animate({ width: "50px" });
        $('.dname').hide();
    }   
});