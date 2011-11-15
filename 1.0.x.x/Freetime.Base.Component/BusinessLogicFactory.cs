using System;

namespace Freetime.Base.Component
{
    internal class BusinessLogicFactory : IBusinessLogicFactory
    {
        private bool UseDefaultLogic { get; set; }

        public TLogic GetBusinessLogic<TLogic>(Type logicOwnerType, TLogic defaultLogic)
        {
            if(UseDefaultLogic)
                return defaultLogic;

            var controller = PluginManagement.PluginManager.Current.GetWebControllerByType(logicOwnerType.FullName);

            if (Equals(controller, null))
                return defaultLogic;

            var bType =
                Type.GetType(string.Format("{0}, {1}", controller.BusinessLogicType, controller.BusinessLogicAssembly));

            if (Equals(bType, null))
                return defaultLogic;

            var typeInstance = Framework.Activator.CreateInstance(bType);

            if (!typeof(TLogic).IsAssignableFrom(typeInstance.GetType()))
                return defaultLogic;

            var logicInstance = (TLogic) typeInstance;

            return logicInstance;
        }

        public TLogic CreateBusinessLogic<TLogic>()
        {
            return default(TLogic);
        }

        public void AddAttribute(string key, string value)
        {
            if (key.ToLower() != "usedefaultlogic")
                return;

            if (string.IsNullOrEmpty(value))
                return;

            if (value.ToLower() == "true")
                UseDefaultLogic = true;
        }
    }
}
