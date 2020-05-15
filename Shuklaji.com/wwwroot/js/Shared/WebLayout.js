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
        $('.profileLastLogin').text($loginData.userData[0].lastLogin);
        if (app.common.userId > 0) {
            $('.web-logout').show();
            $('.web-login').hide();
            if (app.common.userRole.toLowerCase().indexOf('admin') > -1) {
                $('.web-admin').show();
            }
            else {
                $('.web-admin').hide();
            }
        }
        else {
            $('.web-logout').hide();
            $('.web-login').show();
        }
    }

    api.get(app.api.urls.appSetting.getAppSetting).then(function (data) {
        if (data.data.length > 0) {
            $('.appLogo').attr('src', data.data[0].appLogo);
            $('.appName').text(data.data[0].appName);
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
                    if (ele.position === app.menu.position.top && ele.accessBy=== app.menu.accessBy.user) {
                        if (ele.childMenu === null || ele.childMenu.length === 0) {
                            $headerMenuList += `<li><a class="menu-button" href="${ele.menuPath}">` + (ele.badge!==null?`<div class="badge badge-danger menuBadge">${ele.badge}</div>`:'')+`<i style="color:${ele.iconColor};" class="${ele.iconClass}"></i> ${ele.displayName}</a></li>`;
                        }
                        else {
                            $headerMenuList += ` <li>
                                <div class="dropdown-wrapper menu-button">
                                    <a class="menu-button" href="#">${ele.displayName}</a>
                                    <div class="drop-menu">`;
                            $(ele.childMenu).each(function (indC, eleC) {
                                $headerMenuList += `<a class="menu-button" href="${eleC.menuPath}"><i style="color:${eleC.iconColor};" class="${eleC.iconClass}"> </i>${eleC.displayName}</a>`;
                            });
                            $headerMenuList += `</div></div></li>`;
                        }
                    }
                    else if (ele.position === app.menu.position.left) {
                        if (ele.childMenu === null || ele.childMenu.length === 0) {
                            $leftMenuList += `<li data-toggle="popover" data-placement="right" title="${ele.displayName}"><a href="${ele.menuPath}"><i class="${ele.iconClass}"></i></a></li>`;
                        }
                        else {
                            $leftMenuList += `<li title="${ele.displayName}" data-toggle="collapse" data-target="#navbarSupportedContent${ind}" aria-controls="navbarSupportedContent${ind}" aria-expanded="false" aria-label="Toggle navigation"><i class="${ele.iconClass}"></i></li>`;
                            $leftMenuList += `<ul class="collapse left-menu-child" id="navbarSupportedContent${ind}">`;
                            $(ele.childMenu).each(function (indC, eleC) {
                                $leftMenuList += `<li class="nav-item" title="${eleC.displayName}"> <a class="nav-link" href="${eleC.menuPath}"><i class="${eleC.iconClass}"></i></a></li>`;
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
        app.methods.storage.setData(app.methods.storage.storageKey.loginData, []);
    });
});

$(function () {
    $('[data-toggle="popover"]').popover();
});