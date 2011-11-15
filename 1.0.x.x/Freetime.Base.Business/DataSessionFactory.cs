using System;
using Freetime.Base.Business.Implementable;

namespace Freetime.Base.Business
{
    internal class DataSessionFactory : IDataSessionFactory
    {
        private bool UseDefaultDataSession { get; set; }

        internal DataSessionFactory()
        {
            UseDefaultDataSession = false;
        }

        TContract IDataSessionFactory.GetDataSession<TContract>(ILogic logic, TContract defaultContract)
        {
            if (UseDefaultDataSession)
                return defaultContract;

            var blogic = PluginManagement.PluginManager.Current.GetBusinessLogic(logic.GetType().FullName);

            if (Equals(blogic, null))
                return defaultContract;

            var sessionType = Type.GetType(string.Format("{0}, {1}", blogic.DataSessionType, blogic.DataSessionAssembly));

            if(Equals(sessionType, null))
                throw new Exception(string.Format("Unknown type: {0}, {1}", blogic.DataSessionType, blogic.DataSessionAssembly));

            var typeInstance = Framework.Activator.CreateInstance(sessionType);

            if(!typeof(TContract).IsAssignableFrom(typeInstance.GetType()))
                return defaultContract;

            var sessionInstance = (TContract) typeInstance;

            return sessionInstance;
        }

        void IDataSessionFactory.AddAttribute(string key, string value)
        {

            if(key.ToLower() != "usedefaultsession")
                return;

            if (string.IsNullOrEmpty(value))
                return;

            if (value.ToLower() == "true")
                UseDefaultDataSession = true;
        }
    }
}
