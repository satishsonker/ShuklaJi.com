/// <reference path="constants.js" />
/// <reference path="global.js" />

app.api.area = {
    baseLayout: 'baseLayout',
    admin: 'admin',
    web: 'web'
};

app.api.controller = {
    Layout: `${app.api.area.baseLayout}/layout/`,
    RoleManagement: `${app.api.area.admin}/RoleManagement/`,
    MenuManagement: `${app.api.area.admin}/MenuManagement/`,
    UserManagement: `${app.api.area.admin}/UserManagement/`,
    EmailManagement: `${app.api.area.admin}/EmailConfiguration/`,
    AppSetting: `${app.api.area.admin}/AppSetting/`,
    ZodiacManagement: `${app.api.area.admin}/ZodiacManagement/`,
    Miscellaneous: `${app.api.area.admin}/Miscellaneous/`,
    Dashboard: `${app.api.area.admin}/Dashboard/`,
    Common: 'Common/',
    SignIn: 'SignIn/',
    ContactUs: `${app.api.area.web}/ContactUs/`,
    Zodiac: `${app.api.area.web}/Zodiac/`,
};

app.api.urls = {
    baseLayout: {},
    roleManagement: {},
    menuManagement: {},
    userManagement: {},
    emailManagement: {},
    zodiacManagement: {},
    dashboard: {},
    misc: {},
    common: {},
    signin: {},
    contactUs: {},
    web: {}
};



app.api.urls.baseLayout.layout = {
    getLayoutMenu: `${app.api.controller.Layout}GetLayoutMenu`
};

app.api.urls.roleManagement = {
    addRole: `${app.api.controller.RoleManagement}SaveRoles`,
    getRoles: `${app.api.controller.RoleManagement}GetRoles`,
    updateRoles: `${app.api.controller.RoleManagement}UpdateRoles`,
    deleteRoles: `${app.api.controller.RoleManagement}DeleteRoles`
};

app.api.urls.menuManagement = {
    getMenu: `${app.api.controller.MenuManagement}GetMenu`,
    addMenu: `${app.api.controller.MenuManagement}AddMenu`,
    updateMenu: `${app.api.controller.MenuManagement}UpdateMenu`,
    deleteMenu: `${app.api.controller.MenuManagement}DeleteMenu`
};

app.api.urls.userManagement = {
    getUser: `${app.api.controller.UserManagement}GetUser`,
    addUser: `${app.api.controller.UserManagement}AddUser`,
    updateUser: `${app.api.controller.UserManagement}UpdateUser`,
    deleteUser: `${app.api.controller.UserManagement}DeleteUser`,
    activeDeactiveUser: `${app.api.controller.UserManagement}ActiveDeactiveUser`,
    saveAstrologer: `${app.api.controller.UserManagement}SaveAstrologers`,
    updateAstrologer: `${app.api.controller.UserManagement}UpdateAstrologers`,
    deleteAstrologer: `${app.api.controller.UserManagement}DeleteAstrologers`,
    getAstrologer: `${app.api.controller.UserManagement}GetAstrologers`,
    getAstrologerSchedule: `${app.api.controller.UserManagement}GetAstrologerSchedule`,
    saveAstrologerSchedule: `${app.api.controller.UserManagement}SaveAstrologerSchedule`,
    updateAstrologerSchedule: `${app.api.controller.UserManagement}UpdateAstrologerSchedule`,
    deleteAstrologerSchedule: `${app.api.controller.UserManagement}DeleteAstrologerSchedule`,
    getAstrologerRating: `${app.api.controller.UserManagement}GetAstrologerRating`,
    saveAstrologerRating: `${app.api.controller.UserManagement}SaveAstrologerRating`
};

app.api.urls.emailManagement = {
    getEmailConfig: `${app.api.controller.EmailManagement}GetEmailConfig`,
    saveEmailConfig: `${app.api.controller.EmailManagement}SaveEmailConfig`,
    updateEmailConfig: `${app.api.controller.EmailManagement}UpdateEmailConfig`,
    deleteEmailConfig: `${app.api.controller.EmailManagement}DeleteEmailConfig`,
    setDefaultEmailConfig: `${app.api.controller.EmailManagement}SetDefaultEmailConfig`,
    saveEmailTemplate: `${app.api.controller.EmailManagement}SaveEmailTemplate`,
    getEmailTemplate: `${app.api.controller.EmailManagement}GetEmailTemplate`,
    getEmailsByGroup: `${app.api.controller.EmailManagement}GetEmailsByGroup`,
    updateEmailGroup: `${app.api.controller.EmailManagement}UpdateEmailGroup`
};

app.api.urls.zodiacManagement = {
    getZodiacList: `${app.api.controller.ZodiacManagement}GetZodiacList`,
    saveZodiac: `${app.api.controller.ZodiacManagement}SaveZodiac`,
    saveZodiacReport: `${app.api.controller.ZodiacManagement}SaveZodiacReport`,
    getZodiacReport: `${app.api.controller.ZodiacManagement}GetZodiacReport`,
    updateZodiacReport: `${app.api.controller.ZodiacManagement}UpdateZodiacReport`
};

app.api.urls.dashboard = {
    getDashboardCount: `${app.api.controller.Dashboard}GetDashboardCount`
};


app.api.urls.appSetting = {
    getAppSetting: `${app.api.controller.AppSetting}GetAppSettings`,
    setAppSetting: `${app.api.controller.AppSetting}SetAppSetting`,
    getAppDowntime: `${app.api.controller.AppSetting}GetAppDowntime`,
    saveAppDowntime: `${app.api.controller.AppSetting}SaveAppDowntime`,
    deleteAppDowntime: `${app.api.controller.AppSetting}DeleteAppDowntime`,
    updateAppDowntime: `${app.api.controller.AppSetting}UpdateAppDowntime`
};

app.api.urls.common = {
    isDataExists: `${app.api.controller.Common}IsDataExists`,
    dropdownList: `${app.api.controller.Common}DropdownList`
};

app.api.urls.signin = {
    login: `${app.api.controller.SignIn}Login`,
    setNewPassword: `${app.api.controller.SignIn}SetNewPassword`,
    changePassword: `${app.api.controller.SignIn}ChangePassword`,
    resetCode: `${app.api.controller.SignIn}ResetCode`
};

app.api.urls.web = {
    contactUs: {},
    zodiac: {}
};
app.api.urls.web.contactUs = {
    sendQuery: `${app.api.controller.ContactUs}SendQuery`,
    saveContactDetails: `${app.api.controller.ContactUs}SaveContactDetails`,
    getContactDetails: `${app.api.controller.ContactUs}GetContactDetails`,
    getUserQuery: `${app.api.controller.ContactUs}GetUserQuery`,
    resolveUserQuery: `${app.api.controller.ContactUs}ResolveUserQuery`,
    userQueryReply: `${app.api.controller.ContactUs}UserQueryReply`,
    getUserQueryReply: `${app.api.controller.ContactUs}GetUserQueryReply`,
    saveFeedback: `${app.api.controller.ContactUs}SaveFeedback`,
    updateFeedback: `${app.api.controller.ContactUs}UpdateFeedback`,
    deleteFeedback: `${app.api.controller.ContactUs}DeleteFeedback`,
    getFeedback: `${app.api.controller.ContactUs}GetFeedback`,
};
app.api.urls.web.zodiac = {
    getHoroscope: `${app.api.controller.Zodiac}GetHoroscope`,
}

app.api.urls.misc = {
    refLookupList: `${app.api.controller.Miscellaneous}GetReferenceLookup`,
    saveReferenceLookup: `${app.api.controller.Miscellaneous}SaveReferenceLookup`,
    updateReferenceLookup: `${app.api.controller.Miscellaneous}UpdateReferenceLookup`,
    deleteReferenceLookup: `${app.api.controller.Miscellaneous}DeleteReferenceLookup`,
}

////////////////////////Page Urls////////////////
app.page = {};
app.page.urls = {
    adminArea: {
        roleManagement: {
            roleList: '/admin/RoleManagement/RoleList',
            addRole:'/admin/RoleManagement/AddRole'
        },
        menuManagement: {
            menuList: '/admin/MenuManagement/MenuList',
            addMenu:'/admin/MenuManagement/GetMenu'
        },
        userManagement: {
            userList: '/admin/UserManagement/UserList',
            addUser: '/admin/UserManagement/AddUser',
            addAstrologer: '/admin/UserManagement/AddAstrologers',
            astrologers: '/admin/UserManagement/Astrologers',
            viewAstrologers: '/admin/UserManagement/ViewAstrologer',
            astrologerSchedule: '/admin/UserManagement/AstrologerSchedule'
        },
        AppSetting: {
            getAppSetting:'/admin/AppSetting/GetAppSetting'
        },
        emailManagement: {
            getConfiguration: '/admin/EmailManagement/GetConfiguration',
            configurationDetails: '/admin/EmailManagement/ConfigurationDetails',
            getEmailTemplate: '/admin/EmailManagement/GetEmailTemplate'
        },
        contactManagement: {
            saveContactUs: '/admin/ContactManagement/SaveContactUs',
            userQuery: '/admin/ContactManagement/UserQuery',
        },
        zodiacManagement: {
            zodiacReport: '/admin/ZodiacManagement/ZodiacReport',
            saveZodiacReport: '/admin/ZodiacManagement/SaveZodiacReport',
        }
    },
    common: {
        siginin:
        {
            login: '/signin/login',
            ChangePassword: '/signin/ChangePassword',
            NewPassword: '/signin/NewPassword',
            ForgetPassword: '/signin/ForgetPassword'
        },
        dashboard: {
            getDashboard:'/dashboard/index'
        },
        errorHandler: {
            unauthorize: '/common/errorHandler/unauthorize'
        }
    }
};

///////////////////////End Page Urls///////////////