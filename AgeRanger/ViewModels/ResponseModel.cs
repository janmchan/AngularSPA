namespace AgeRanger.ViewModels
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            
        }
        public ResponseModel(bool success)
        {
            mapToProperties(success, string.Empty);
        }
        public ResponseModel(bool success, string message)
        {
            mapToProperties(success, message);
        }

        private void mapToProperties(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}