using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingDemo.Common
{
    public class ConfigureHelper
    {
        public static readonly ConfigureHelper Singleton = new ConfigureHelper();

        private ConfigureHelper()
        {
        }

        private Configuration _configurationFile;
        public Configuration ConfigurationFile
        {
            get
            {
                if (_configurationFile == null)
                    _configurationFile = CreateConfiguration();
                return _configurationFile;
            }
        }

        private Configuration CreateConfiguration()
        {
            try
            {
                Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Create the custom section entry   
                // in <configSections> group and the  
                // related target section in <configuration>.
                if (configFile.Sections["ApperanceSection"] == null)
                {
                    configFile.Sections.Add("ApperanceSection", new ApperanceSection());
                }
                return configFile;
            }
            catch (ConfigurationErrorsException err)
            {
                MessageBox.Show("CreateConfigurationFile: {0}", err.ToString());
                return null;
            }            
        }


        public void LoadConfiguration()
        {
            ApperanceSection section = 
                ConfigurationFile.GetSection(nameof(ApperanceSection)) as ApperanceSection;
            PaletteHelper palette = new PaletteHelper();
            palette.ReplacePrimaryColor(section.PrimarySwatchName);
            palette.ReplaceAccentColor(section.AccentSwatchName);
            palette.SetLightDark(section.IsDarkTheme);
        }

        public void SaveConfiguration()
        {
            ((ApperanceSection)ConfigurationFile.Sections[nameof(ApperanceSection)]).IsDarkTheme
                 = ApperanceHelper.Singleton.IsDarkTheme;
            ((ApperanceSection)ConfigurationFile.Sections[nameof(ApperanceSection)]).PrimarySwatchName
                 = ApperanceHelper.Singleton.CurrentPrimary.Name;
            ((ApperanceSection)ConfigurationFile.Sections[nameof(ApperanceSection)]).AccentSwatchName
                 = ApperanceHelper.Singleton.CurrentAccent.Name;
            ConfigurationFile.Save();
        }
    }

    
    #region  ApperanceSection
    public sealed class ApperanceSection : ConfigurationSection
    {
        public ApperanceSection()
        {
            PrimarySwatchName = @"yellow";
            AccentSwatchName = @"green";
            IsDarkTheme = false;
        }

        [ConfigurationProperty("primarySwatchName",
         DefaultValue = "deeppurple",
         IsRequired = true)]
        public string PrimarySwatchName
        {
            get
            {
                return (string)this["primarySwatchName"];
            }
            set
            {
                this["primarySwatchName"] = value;
            }
        }

        [ConfigurationProperty("accentSwatchName",
         DefaultValue = "lime",
         IsRequired = true)]
        public string AccentSwatchName
        {
            get
            {
                return (string)this["accentSwatchName"];
            }
            set
            {
                this["accentSwatchName"] = value;
            }
        }

        [ConfigurationProperty("isDarkTheme",
         DefaultValue = "false",
         IsRequired = true)]
        public bool IsDarkTheme
        {
            get
            {
                return (bool)this["isDarkTheme"];
            }
            set
            {
                this["isDarkTheme"] = value;
            }
        }
    }
    #endregion



    #region MSDN Sample
    /*
    class UsingConfigurationClass
    {


        // Show how to create an instance of the Configuration class 
        // that represents this application configuration file.   
        static void CreateConfigurationFile()
        {
            try
            {

                // Create a custom configuration section.
                CustomSection customSection = new CustomSection();

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                // Create the custom section entry   
                // in <configSections> group and the  
                // related target section in <configuration>. 
                if (config.Sections["CustomSection"] == null)
                {
                    config.Sections.Add("CustomSection", customSection);
                }

                // Create and add an entry to appSettings section. 

                string conStringname = "LocalSqlServer";
                string conString = @"data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
                string providerName = "System.Data.SqlClient";

                ConnectionStringSettings connStrSettings = new ConnectionStringSettings();
                connStrSettings.Name = conStringname;
                connStrSettings.ConnectionString = conString;
                connStrSettings.ProviderName = providerName;

                config.ConnectionStrings.ConnectionStrings.Add(connStrSettings);

                // Add an entry to appSettings section. 
                int appStgCnt =
                    ConfigurationManager.AppSettings.Count;
                string newKey = "NewKey" + appStgCnt.ToString();

                string newValue = DateTime.Now.ToLongDateString() +
                  " " + DateTime.Now.ToLongTimeString();

                config.AppSettings.Settings.Add(newKey, newValue);

                // Save the configuration file.
                customSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);

                Console.WriteLine("Created configuration file: {0}",
                    config.FilePath);

            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("CreateConfigurationFile: {0}", err.ToString());
            }

        }

        // Show how to use the GetSection(string) method. 
        static void GetCustomSection()
        {
            try
            {

                CustomSection customSection;

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                customSection =
                    config.GetSection("CustomSection") as CustomSection;

                Console.WriteLine("Section name: {0}", customSection.Name);
                Console.WriteLine("Url: {0}", customSection.Url);
                Console.WriteLine("Port: {0}", customSection.Port);

            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("Using GetSection(string): {0}", err.ToString());
            }

        }


        // Show how to use different modalities to save  
        // a configuration file. 
        static void SaveConfigurationFile()
        {
            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Save the full configuration file and force save even if the file was not modified.
                config.SaveAs("MyConfigFull.config", ConfigurationSaveMode.Full, true);
                Console.WriteLine("Saved config file as MyConfigFull.config using the mode: {0}",
                    ConfigurationSaveMode.Full.ToString());

                config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Save only the part of the configuration file that was modified. 
                config.SaveAs("MyConfigModified.config", ConfigurationSaveMode.Modified, true);
                Console.WriteLine("Saved config file as MyConfigModified.config using the mode: {0}",
                    ConfigurationSaveMode.Modified.ToString());

                config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Save the full configuration file.
                config.SaveAs("MyConfigMinimal.config");
                Console.WriteLine("Saved config file as MyConfigMinimal.config using the mode: {0}",
                    ConfigurationSaveMode.Minimal.ToString());

            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("SaveConfigurationFile: {0}", err.ToString());
            }

        }


        // Show how use the AppSettings and ConnectionStrings  
        // properties. 
        static void GetSections(string section)
        {
            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Get the selected section. 
                switch (section)
                {
                    case "appSettings":
                        try
                        {
                            AppSettingsSection appSettings =
                                config.AppSettings as AppSettingsSection;
                            Console.WriteLine("Section name: {0}",
                                    appSettings.SectionInformation.SectionName);

                            // Get the AppSettings section elements.
                            Console.WriteLine();
                            Console.WriteLine("Using AppSettings property.");
                            Console.WriteLine("Application settings:");
                            // Get the KeyValueConfigurationCollection  
                            // from the configuration.
                            KeyValueConfigurationCollection settings =
                              config.AppSettings.Settings;

                            // Display each KeyValueConfigurationElement. 
                            foreach (KeyValueConfigurationElement keyValueElement in settings)
                            {
                                Console.WriteLine("Key: {0}", keyValueElement.Key);
                                Console.WriteLine("Value: {0}", keyValueElement.Value);
                                Console.WriteLine();
                            }
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine("Using AppSettings property: {0}",
                                e.ToString());
                        }
                        break;

                    case "connectionStrings":
                        ConnectionStringsSection
                            conStrSection =
                            config.ConnectionStrings as ConnectionStringsSection;
                        Console.WriteLine("Section name: {0}",
                            conStrSection.SectionInformation.SectionName);

                        try
                        {
                            if (conStrSection.ConnectionStrings.Count != 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Using ConnectionStrings property.");
                                Console.WriteLine("Connection strings:");

                                // Get the collection elements. 
                                foreach (ConnectionStringSettings connection in
                                  conStrSection.ConnectionStrings)
                                {
                                    string name = connection.Name;
                                    string provider = connection.ProviderName;
                                    string connectionString = connection.ConnectionString;

                                    Console.WriteLine("Name:               {0}",
                                      name);
                                    Console.WriteLine("Connection string:  {0}",
                                      connectionString);
                                    Console.WriteLine("Provider:            {0}",
                                       provider);
                                }
                            }
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine("Using ConnectionStrings property: {0}",
                                e.ToString());
                        }
                        break;

                    default:
                        Console.WriteLine(
                            "GetSections: Unknown section (0)", section);
                        break;
                }

            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("GetSections: (0)", err.ToString());
            }

        }

        // Show how to use the Configuration object properties  
        // to obtain configuration file information. 
        static void GetConfigurationInformation()
        {
            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                Console.WriteLine("Reading configuration information:");

                ContextInformation evalContext =
                    config.EvaluationContext as ContextInformation;
                Console.WriteLine("Machine level: {0}",
                    evalContext.IsMachineLevel.ToString());

                string filePath = config.FilePath;
                Console.WriteLine("File path: {0}", filePath);

                bool hasFile = config.HasFile;
                Console.WriteLine("Has file: {0}", hasFile.ToString());


                ConfigurationSectionGroupCollection
                    groups = config.SectionGroups;
                Console.WriteLine("Groups: {0}", groups.Count.ToString());
                foreach (ConfigurationSectionGroup group in groups)
                {
                    Console.WriteLine("Group Name: {0}", group.Name);
                    // Console.WriteLine("Group Type: {0}", group.Type);
                }


                ConfigurationSectionCollection
                    sections = config.Sections;
                Console.WriteLine("Sections: {0}", sections.Count.ToString());

            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("GetConfigurationInformation: {0}", err.ToString());
            }

        }
    }
    //*/
    #endregion

}
