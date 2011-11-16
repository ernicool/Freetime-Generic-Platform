using System.Configuration;
using Freetime.Base.Component.Configuration;
using Freetime.Base.Framework.Diagnostics;
using System;

namespace Freetime.Base.Component
{
    public class BusinessLogicBuilder : IBusinessLogicBuilder
    {
        private static IBusinessLogicBuilder s_businessLogicBuilder;
        private static IBusinessLogicFactory s_businessLogicFactory;

        private static IBusinessLogicFactory BusinessLogicFactory
        {
            get
            {
                s_businessLogicFactory = s_businessLogicFactory ?? new BusinessLogicFactory();
                return s_businessLogicFactory;
            }
            set { s_businessLogicFactory = value; }
        }


        public static IBusinessLogicBuilder Current 
        { 
            get { s_businessLogicBuilder = s_businessLogicBuilder ?? new BusinessLogicBuilder();
                return s_businessLogicBuilder;
            }
        }

        public static void SetBusinessLogicFactory(IBusinessLogicFactory factory)
        {
            s_businessLogicFactory = factory;
        }

        static BusinessLogicBuilder()
        {
            var configSection =
                Freetime.Configuration.ConfigurationManager.FreetimeConfiguration.
                    BusinessLogicBuilderConfigurationSection;
            if (string.IsNullOrEmpty(configSection))
                return;

            try
            {
                var section = ConfigurationManager.GetSection(configSection);
                if (!typeof(BusinessLogicBuilderConfiguration).IsAssignableFrom(section.GetType()))
                    throw new Exception(string.Format("ConfigurationSection {0} is not of Type Freetime.Base.Component.Configuration.BusinessLogicBuilderConfiguration", configSection));
                
                var businessLogicBuilderConfig = section as BusinessLogicBuilderConfiguration;

                if(Equals(businessLogicBuilderConfig, null))
                    throw new Exception(string.Format("ConfigurationSection {0} is not of Type Freetime.Base.Component.Configuration.BusinessLogicBuilderConfiguration", configSection));

                if(businessLogicBuilderConfig.CustomProvider)
                {
                    var factoryType = Type.GetType(businessLogicBuilderConfig.FactoryType);

                    if(Equals(factoryType, null))
                    {
                        TraceUtil.WriteLine(string.Format("Unknow Type {0}", businessLogicBuilderConfig.FactoryType));
                        throw new Exception(string.Format("Unknow Type {0}", businessLogicBuilderConfig.FactoryType));
                    }

                    var logicFactory = Framework.Activator.CreateInstance(factoryType);

                    if(Equals(logicFactory, null))
                    {
                        TraceUtil.WriteLine(string.Format("Unable to create IBusinessLogicFactory instance of type {0}", businessLogicBuilderConfig.FactoryType), TraceUtil.Category.Critical);
                        throw new Exception(string.Format("Unable to create IBusinessLogicFactory instance of type {0}", businessLogicBuilderConfig.FactoryType));
                    }

                    if (!typeof(IBusinessLogicFactory).IsAssignableFrom(logicFactory.GetType()))
                    {
                        TraceUtil.WriteLine(string.Format("{0} is not of type IBusinessLogicFactory", businessLogicBuilderConfig.FactoryType), TraceUtil.Category.Critical);
                        throw new Exception(string.Format("{0} is not of type IBusinessLogicFactory", businessLogicBuilderConfig.FactoryType));
                    }

                    BusinessLogicFactory = logicFactory as IBusinessLogicFactory;
                    if (Equals(BusinessLogicFactory, null))
                        throw new Exception(string.Format("{0} is not of type IBusinessLogicFactory", businessLogicBuilderConfig.FactoryType));

                    foreach (BusinessLogicBuilderConfigurationAttribute attribute in businessLogicBuilderConfig.Attributes)
                    {
                        BusinessLogicFactory.AddAttribute(attribute.Key, attribute.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                TraceUtil.WriteLine(ex);
                throw;
            }
        }

        internal BusinessLogicBuilder(){}

        public TLogic GetBusinessLogic<TLogic>(Type logicOwnerType, TLogic defaultLogic)
        {
            return BusinessLogicFactory.GetBusinessLogic(logicOwnerType, defaultLogic);
        }

        public TLogic CreateBusinessLogic<TLogic>()
        {
            return BusinessLogicFactory.CreateBusinessLogic<TLogic>();
        }

    }
}
