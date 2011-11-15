using System.Configuration;

namespace Freetime.Base.Component.Configuration
{
    public class BusinessLogicBuilderConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("CustomProvider", IsRequired = false)]
        public bool CustomProvider
        {
            get
            {
                bool isCustom;
                bool.TryParse(this["CustomProvider"].ToString(), out isCustom);
                return isCustom;
            }
            set
            {
                this["CustomProvider"] = value;
            }
        }

        [ConfigurationProperty("FactoryType", IsRequired = false)]
        public string FactoryType
        {
            get
            {
                if (Equals(this["FactoryType"], null))
                    return string.Empty;
                return (string)this["FactoryType"];
            }
            set
            {
                this["FactoryType"] = value;
            }
        }

        [
        ConfigurationProperty("Attributes", IsDefaultCollection = false),
        ConfigurationCollection(typeof(BusinessLogicBuilderConfigurationAttributeCollection), AddItemName = "addAttribute", ClearItemsName = "clearAttributes", RemoveItemName = "removeAttribute")
        ]
        public BusinessLogicBuilderConfigurationAttributeCollection Attributes
        {
            get
            {
                return this["Attributes"] as BusinessLogicBuilderConfigurationAttributeCollection;
            }
        }
    }
}
