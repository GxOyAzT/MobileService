namespace MobileService.Entities
{
    public class ActionReponseModel
    {
        public ActionReponseModel(bool isSucceed, string message)
        {
            IsSucceed = isSucceed;
            Message = message;
        }

        public ActionReponseModel(bool isSucceed)
        {
            IsSucceed = isSucceed;
            Message = string.Empty;
        }

        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }
}
