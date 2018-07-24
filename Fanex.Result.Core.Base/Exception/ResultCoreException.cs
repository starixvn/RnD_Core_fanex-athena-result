namespace Fanex.Result.Core.Base.Exception
{
    using System;
    using System.Runtime.Serialization;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;

    [Serializable]
    public class ResultCoreException : Exception
    {
        private readonly ISerializer _serializer = new JsonSerializer();
        private readonly string _templateName;
        private readonly RazorBinderModel _binderModel;

        public ResultCoreException()
        {
        }

        public ResultCoreException(string message)
            : base(message)
        {
        }

        public ResultCoreException(
            string mesasge, 
            Exception innerException, 
            string templateName = null,
            RazorBinderModel binderModel = null)
            : base(mesasge, innerException)
        {
            _templateName = templateName;
            _binderModel = binderModel ?? new RazorBinderModel();
        }

        protected ResultCoreException(SerializationInfo serializationInfo, StreamingContext steamContext)
            : base(serializationInfo, steamContext)
        {
        }

        public override string Message
            => $"Result.Core Exception: {base.Message} {GetTemplateName()} {GetBinderModelString()}";

        private string GetTemplateName()
            => string.IsNullOrWhiteSpace(_templateName)
                    ? string.Empty
                    : $"\r\nTemplate: {_templateName}";

        private string GetBinderModelString()
        {
            try
            {
                return _binderModel != null
                        ? $"\r\nBinderModel: {_serializer.Serialize(_binderModel)}"
                        : string.Empty;
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}