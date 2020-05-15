namespace ShuklaJi.Utilities
{
    public static class UtilityConstant
    {
        public static class AppConfigConstant
        {
            public static string AppVariable = "AppVariable";
            public static string DefaultConnectionStringName = "DefaultConnectionStringName";
            public static string CommandTimeout = "CommandTimeout";
            public static string ConnectionTimeout = "ConnectionTimeout";
            public static string ConnectionStrings = "ConnectionStrings";
            public static string SaltKey = "SaltKey";
            public static string AppUrl = "AppUrl";
            public static string ApiUrl = "ApiUrl";
            public static string RefreshTokenTime = "RefreshTokenTime";
        }

        public static class Regex
        {
            public static string Base64 = "AppVariable";
        }

        public static class Procedures
        {
            public const string Mst_AddRoles = "Mst_AddRoles";
            public const string GetLayoutMenu = "GetLayoutMenu";
            public const string Mst_GetAppSetting = "Mst_GetAppSetting";
            public const string Mst_IsDataExists = "Mst_IsDataExists";
            public const string Mst_GetRoles = "Mst_GetRoles";
            public const string Mst_UpdateRoles = "Mst_UpdateRoles";
            public const string Mst_DeleteRoles = "Mst_DeleteRoles";
            public const string Mst_UpdateAppSetting = "Mst_UpdateAppSetting";
            public const string Mst_AddMenu = "Mst_AddMenu";
            public const string Mst_DeleteMenu = "Mst_DeleteMenu";
            public const string Mst_UpdateMenu = "Mst_UpdateMenu";
            public const string Mst_GetUsers = "Mst_GetUsers";
            public const string Mst_AddUser = "Mst_AddUser";
            public const string Mst_AddExternalUser = "Mst_AddExternalUser";
            public const string Mst_UpdateUser = "Mst_UpdateUser";
            public const string Mst_DeleteUser = "Mst_DeleteUser";
            public const string Mst_ActiveDeactiveUser = "Mst_ActiveDeactiveUser";
            public const string Mst_DropdownList = "Mst_DropdownList";
            public const string Usp_CheckUserCredential = "Usp_CheckUserCredential";
            public const string Mst_GetEmailConfig = "Mst_GetEmailConfig";
            public const string Mst_SaveEmailConfig = "Mst_SaveEmailConfig";
            public const string Mst_DeleteEmailConfig = "Mst_DeleteEmailConfig";
            public const string Mst_UpdateEmailConfig = "Mst_UpdateEmailConfig";
            public const string Mst_SetDefaultEmailConfig = "Mst_SetDefaultEmailConfig";
            public const string Mst_GetEmailTemplateByMapping = "Mst_GetEmailTemplateByMapping";
            public const string Mst_AddEmailTemplate = "Mst_AddEmailTemplate";
            public const string Mst_GetEmailTemplateById = "Mst_GetEmailTemplateById";
            public const string Usp_ChangePassword = "usp_ChangePassword";
            public const string Usp_AddResetCode = "Usp_AddResetCode";
            public const string Usp_SetNewPassword = "usp_SetNewPassword";
            public const string Mst_GetEmailGroup = "Mst_GetEmailGroup";
            public const string Usp_Mst_UpdateEmailGroup = "Usp_Mst_UpdateEmailGroup";
            public const string Mst_SaveContactDetails = "Mst_SaveContactDetails";
            public const string Mst_GetContactDetails = "Mst_GetContactDetails";
            public const string Mst_SaveUserQuery = "Mst_SaveUserQuery";
            public const string Mst_GetUserQuery = "Mst_GetUserQuery"; 
            public const string Mst_ResolveUserQuery = "Mst_ResolveUserQuery";
            public const string Mst_SaveUserQueryReply = "Mst_SaveUserQueryReply";
            public const string Mst_GetUserQueyReply = "Mst_GetUserQueyReply";
            public const string Mst_GetZodiac = "Mst_GetZodiac";
            public const string Mst_SaveZodiac = "Mst_SaveZodiac";
            public const string Mst_SaveZodiacReport = "Mst_SaveZodiacReport";
            public const string Mst_UpdateZodiacReport = "Mst_UpdateZodiacReport";
            public const string Mst_GetZodiacReport = "Mst_GetZodiacReport"; 
            public const string Mst_GetReferenceLookup = "Mst_GetReferenceLookup";
            public const string Mst_SaveReferenceLookup = "Mst_SaveReferenceLookup";
            public const string Mst_UpdateReferenceLookup = "Mst_UpdateReferenceLookup";
            public const string Mst_DeleteReferenceLookup = "Mst_DeleteReferenceLookup";
            public const string Mst_SaveAstrologers = "Mst_SaveAstrologers";
            public const string Mst_UpdateAstrologers = "Mst_UpdateAstrologers";
            public const string Mst_DeleteAstrologers = "Mst_DeleteAstrologers";
            public const string Mst_GetAstrologers = "Mst_GetAstrologers";
            public const string Mst_GetAstrologerSchedule = "Mst_GetAstrologerSchedule";
            public const string Mst_SaveAstrologerSchedule = "Mst_SaveAstrologerSchedule";
            public const string Mst_UpdateAstrologerSchedule = "Mst_UpdateAstrologerSchedule";
            public const string Mst_DeleteAstrologerSchedule = "Mst_DeleteAstrologerSchedule";
            public const string Mst_GetAstrologerRating = "Mst_GetAstrologerRating";
            public const string Mst_GetDashboardCount = "Mst_GetDashboardCount";
            public const string Mst_GetAppDowntime = "Mst_GetAppDowntime";
            public const string Mst_AddAppDowntime = "Mst_AddAppDowntime";
            public const string Mst_UpdateAppDowntime = "Mst_UpdateAppDowntime";
            public const string Mst_DeleteAppDowntime = "Mst_DeleteAppDowntime";
            public const string Mst_HasAppDowntime = "Mst_HasAppDowntime";
            public const string Mst_GetFeedback = "Mst_GetFeedback";
            public const string Mst_SaveFeedback = "Mst_SaveFeedback";
            public const string Mst_UpdateFeedback = "Mst_UpdateFeedback";
            public const string Mst_DeleteFeedback = "Mst_DeleteFeedback";
            public const string Mst_SaveAstroRating = "Mst_SaveAstroRating";
            public const string Mst_GetHoroscope = "Mst_GetHoroscope";
        }

        public static class StatusCode
        {
            public static int Ok = 200;
            public static int BadRequest = 400;
            public static int Unauthorized = 401;
            public static int InternalServerError = 500;
        }

        public static class Status
        {
            public static string Ok = "Ok";
            public static string BadRequest = "BadRequest";
            public static string Unauthorized = "Unauthorized";
            public static string InternalServerError = "InternalServerError";
        }

        public static class UserResponseStatus
        {
            public static string NoDataFromClient = "NoDataFromClient";
            public static string Error = "Error";
        }

    }
}
