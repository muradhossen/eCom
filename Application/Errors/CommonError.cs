using Application.Common;

namespace Application.Errors
{
    public static class CommonError
    {
        public static readonly Error InvalidRequest = new Error("InvalidRequest", "Invalid request");
        public static readonly Error UnknownError = new Error("UnknownError", "Something wents wrong!");
    }
}
