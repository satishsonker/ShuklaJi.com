namespace ShuklaJi.ModelLayer.Common.ApiResponse
{
    public class ApiResponseModel
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public int ObjectId { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
