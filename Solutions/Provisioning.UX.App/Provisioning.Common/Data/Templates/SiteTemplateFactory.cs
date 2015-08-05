using Provisioning.Common.Configuration;
using Provisioning.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provisioning.Common.Data.Templates
{
    /// <summary>
    /// Factory Class for working with Site Templates
    /// </summary>
    public sealed class SiteTemplateFactory : ISiteTemplateFactory
    {
        #region Private Instance Members
        private static readonly SiteTemplateFactory _instance = new SiteTemplateFactory();
        #endregion

        #region Constructors
        /// <summary>
        /// Static constructor to handle beforefieldinit
        /// </summary>
        static SiteTemplateFactory()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        SiteTemplateFactory()
        {
        }
        #endregion

        /// <summary>
        /// Returns an <see cref="Provisioning.Common.Configuration.TemplateISiteTemplateManager"/> interface. This member reads from your configuration file and the app setting TemplateProviderType and must be defined. 
        /// Custom implementations must implement <see cref="Provisioning.Common.Configuration.TemplateISiteTemplateManager"/>
        /// <add key="TemplateProviderType"  value="Provisioning.Common.Configuration.Template.Impl.XMLSiteTemplateManager, Provisioning.Common"/>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Configuration.ConfigurationErrorsException"></exception>
        /// <exception cref="Provisioning.Common.Data.FactoryException">Exception that occurs when the factory encounters an exception.</exception>
        public ISiteTemplateManager GetManager()
        {
            Log.Info("Provisioning.Common.Data.Templates", "Entered");

            var _configManager = new ConfigManager();
            var _module = _configManager.GetModuleByName(ModuleKeys.MASTERTEMPLATEPROVIDER_KEY);
            var _managerTypeString = _module.ModuleType;

            Log.Info("Provisioning.Common.Data.Templates", "Fetched _managerTypeString of '{0}'", _managerTypeString);

            if (string.IsNullOrEmpty(_managerTypeString))
            {
                //TODO LOG
                throw new ConfigurationErrorsException(PCResources.Exception_Template_Provider_Missing_Config_Message);
            }
            try
            {
                Log.Info("Provisioning.Common.Data.Templates", "About to get type name and instantiate..");

                var type = _managerTypeString.Split(',');
                var typeName = type[0].Trim();
                var assemblyName = type[1].Trim();

                Log.Info("Provisioning.Common.Data.Templates", "Fetched typeName of '{0}'", typeName);
                Log.Info("Provisioning.Common.Data.Templates", "Fetched assemblyName of '{0}'", assemblyName);

                Log.Info("Provisioning.Common.Data.Templates", "About to instantiate 1..");
                var foo = Activator.CreateInstance(assemblyName, typeName).Unwrap();

                Log.Info("Provisioning.Common.Data.Templates", "About to instantiate 2..");

                // COB - issue is in the cast to AbstractModule..
                var instance = (AbstractModule)Activator.CreateInstance(assemblyName, typeName).Unwrap();

                Log.Info("Provisioning.Common.Data.Templates", "Successfully instantiated");

                Log.Info("Provisioning.Common.Data.Templates", "About to set conn string to '{0}' and container to '{1}'..",
                    _module.ConnectionString, _module.Container);

                instance.ConnectionString = _module.ConnectionString;
                instance.Container = _module.Container;

                Log.Info("Provisioning.Common.Data.Templates", "Done all that..");

                Log.Info("Provisioning.Common.Data.Templates", PCResources.SiteTemplate_Factory_Created_Instance, _managerTypeString);
                return (ISiteTemplateManager)instance;
            }
            catch (Exception _ex)
            {
                var _message = String.Format(PCResources.SiteTemplate_Factory_Created_Instance_Exception, _managerTypeString);
                Log.Info("Provisioning.Common.Data.Templates", _message);
                throw new FactoryException(_message, _ex);
            }
        }

        public static ISiteTemplateFactory GetInstance()
        {
            return _instance;
        }

    }
}
