using System.Configuration;

namespace Freetime.Base.Component.Configuration
{
    public class BusinessLogicBuilderConfigurationAttributeCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public void Add(BusinessLogicBuilderConfigurationAttribute element)
        {
            BaseAdd(element);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new BusinessLogicBuilderConfigurationAttribute();
        }

        public void Remove(BusinessLogicBuilderConfigurationAttribute element)
        {
            BaseRemove(element.Key);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BusinessLogicBuilderConfigurationAttribute)element).Key;
        }
    }
}
