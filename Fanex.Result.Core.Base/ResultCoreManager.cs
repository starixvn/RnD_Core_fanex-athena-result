namespace Fanex.Result.Core.Base
{
    using System;
    using Fanex.Result.Core.Base.Utilities;

    public class ResultCoreManager
    {
        private const string NullRefDataHelperErrorMessage = "Please call ResultCoreManager.RegisterRefDataHelper(\"refDataHelper\") to set refDataHelper, this information is required.";
        private static ResultCoreManager managerInstance = new ResultCoreManager();
        private IRefDataHelper refDataHelper;

        public static ResultCoreManager RegisterRefDataHelper(IRefDataHelper refDataHelper)
        {
            managerInstance.refDataHelper = refDataHelper ?? throw new ArgumentNullException(nameof(refDataHelper));

            return managerInstance;
        }

        public static IRefDataHelper RefDataHelper => managerInstance.refDataHelper ?? throw new System.InvalidOperationException(NullRefDataHelperErrorMessage);
    }
}