using System.Configuration;


namespace Freetime.Configuration
{
    public class FreetimeConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("DefaultTheme", IsRequired = false)]
        public string DefaultTheme
        {
            get
            {
                return (string) this["DefaultTheme"];
            }
            set
            {
                this["DefaultTheme"] = value;
            }
        }

        [ConfigurationProperty("DefaultMasterPage", IsRequired = false)]
        public string DefaultMasterPage
        {
            get
            {
                return (string)this["DefaultMasterPage"];
            }
            set
            {
                this["DefaultMasterPage"] = value;
            }
        }

        [ConfigurationProperty("GlobalEventConfigurationSection", IsRequired = false)]
        public string GlobalEventConfigurationSection
        {
            get
            {
                if(this["GlobalEventConfigurationSection"] == null)
                    return string.Empty;
                return (string)this["GlobalEventConfigurationSection"];
            }
            set
            {
                this["GlobalEventConfigurationSection"] = value;
            }
        }

        [ConfigurationProperty("PluginManagementConfigurationSection", IsRequired = true)]
        public string PluginManagementConfigurationSection
        {
            get
            {
                return (string)this["PluginManagementConfigurationSection"];
            }
            set
            {
                this["PluginManagementConfigurationSection"] = value;
            }
        }

        [ConfigurationProperty("DataSessionBuilderConfigurationSection", IsRequired = false)]
        public string DataSessionBuilderConfigurationSection
        {
            get
            {
                return (string)this["DataSessionBuilderConfigurationSection"];
            }
            set
            {
                this["DataSessionBuilderConfigurationSection"] = value;
            }
        }

        [ConfigurationProperty("BusinessLogicBuilderConfigurationSection", IsRequired = false)]
        public string BusinessLogicBuilderConfigurationSection
        {
            get
            {
                return (string)this["BusinessLogicBuilderConfigurationSection"];
            }
            set
            {
                this["BusinessLogicBuilderConfigurationSection"] = value;
            }
        }

        [ConfigurationProperty("LogFilesLocation", IsRequired = true)]
        public string LogFilesLocation
        {
            get
            {
                return (string)this["LogFilesLocation"];
            }
            set 
            {
                this["LogFilesLocation"] = value;
            }
        }
    }
}
