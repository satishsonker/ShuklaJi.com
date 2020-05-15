let app = {};
app.menu = {};
app.api = {};

//app.baseApiUrl = 'http://api-bhaskarjyotishya.somee.com/api/';
app.baseApiUrl = 'http://localhost:59524/api/';

// API Start
app.api.methodType = {
    get: 'GET',
    post: 'POST'
};
app.api.timeout = 300000;
app.api.dataType = 'json';
app.api.contentType = {
    appJson: 'application/json'
};
//## API End
//------------------------------------------------------------------

// Menu Start
app.menu.position = {
    top: 'Top',
    left: 'Left',
    bottom: 'Bottom',
    rigth: 'Right'
};

app.menu.accessBy = {
    admin: 'Admin',
    user: 'User'
};
//##Menu End

//API Status Code
app.api.statusCode = {
    ok: 200,
    badRequest: 400,
    unauthorized: 401,
    internalServerError: 500
};
//End API Status Code

//API Status Msg
app.api.statusMsg = {
    Saved: 'Saved',
    Error: 'Error',
    Exist: 'Exist',
    Updated: 'Updated',
    BadRequest: 'BadRequest',
    Deleted: 'Deleted',
    NoRecord: 'NoRecord',
    NoDataFromClient: 'NoDataFromClient',
    InvalidResponse: 'InvalidResponse',
    InvalidUser: 'InvalidUser',
    InvalidResetLink: 'InvalidResetLink',
    ValidUser: 'ValidUser',
    BlockUser: 'BlockUser',
    ExpireToken: 'ExpireToken',
    InvalidToken: 'InvalidToken',
    InUse: 'InUse',
    ConnectionTimeOut: 'ConnectionTimeOut',
    InternalServerError: 'InternalServerError',
    PasswordChange: 'PasswordChange',
    ResetCode: 'ResetCode',
    EmailSend: 'EmailSend',
    ReplySend: 'ReplySend'
};
//End API Status Msg

//UserMsg
app.userMsg = {
    alreadyExist: 'Record is already exists.',
    errAtServer:'Some error encountered at server side.',
    recordSaved: 'Record has been saved successfully.',
    recordUpdated: 'Record has been updated successfully.',
    recordDeleted: 'Record has been deleted successfully.',
    noRecord: 'No record found.',
    noDataFromClient: "We didn't receive data from client side.",
    invalidResponse:'We received unexpected response from server.',
    invalidRecord: 'You are trying to modify invalid record.',
    InvalidUser: 'Username or Password is incorrect.',
    blockUser: 'Your account is blocked.',
    validUser: 'Login successfully.',
    badRequest: 'There is some validation errors.',
    expireToken: 'Your session has been expired. Please login again.',
    invalidToken: 'You are unauthorized. Can not access this resource.',
    inUse: 'This data is currently being used. so you cannot delete the record.',
    cannotDeactivateYourSelf: 'You cannot activate or deactivate yourself.',
    connectionTimeOut: 'Request has been timeout.',
    internalServerError: 'We have experienced some server error. Please try again later.',
    passwordChange: 'Your password has been chnaged.',
    resetCode: 'Password reset link has been sent to your email.',
    invalidResetLink: 'Password reset link is expired/Invalid',
    emailSend: 'Your query has been sent.',
    replySend: 'Your reply has been sent.',
    astroScheduleExist: 'This schedule already exist.',
    astrolNotExist:'Unable to find astrologer. Please select astrologer',
};
//End UserMsg

// Toast Msg Start
app.toast = {
    success: '',
    warning: '',
    error: '',
    info: '',
    delayTime: 3000,
    width: 27,
    type: {
        success: 'rgba(255, 200, 76, 0.85)',
        warning: 'rgba(255, 200, 76, 0.85)',
        error: 'rgba(255, 200, 76, 0.85)',
        info: 'rgba(255, 200, 76, 0.85)'
    },
    title: {
        success: 'Success',
        warning: 'Warning',
        error: 'Error',
        info: 'Info'
    }
};
// Toast Msg End

app.api.proc = {
    isDataExist: {
        roleName: 'Rolename',
        userName: 'UserName',
        emailConfig: 'EmailConfig',
        resetCode: 'ResetCode'
    },
    dropdownList: {
        role: 'role',
        emailTempate: 'emailTempate',
        emailGroup: 'emailGroup',
        consultMethod: 'consultMethod',
        zodiac: 'zodiac'
    }
};

//------------------------------------------------------------------
