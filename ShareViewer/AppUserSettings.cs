using System;
using System.Configuration;
using System.Drawing;

namespace ShareViewer
{
    public class AppUserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("Control")]
        public Color BackgroundColor
        {
            get
            {
                return ((Color)this["BackgroundColor"]);
            }
            set
            {
                this["BackgroundColor"] = (Color)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Default")]
        public String ExtraFolder
        {
            get
            {
                return ((String)this["ExtraFolder"]);
            }
            set
            {
                //this is the value that ExtraFolder must be set to in user.config:                
                //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ShareViewer\Extra";
                this["ExtraFolder"] = (String)value;
            }

        }

        [UserScopedSetting()]
        [DefaultSettingValue("http://www.bsb-software.de/rese/")]
        public string SharesUrl
        {
            get
            {
                return ((String)this["SharesUrl"]);
            }
            set
            {
                this["SharesUrl"] = (String)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Inhalt.txt")]
        public String DataDaysListingFilename
        {
            get
            {
                return ((String)this["DataDaysListingFilename"]);
            }
            set
            {
                this["DataDaysListingFilename"] = (String)value;
            }

        }


    }
}