using System;
using System.Collections.Generic;
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
        [DefaultSettingValue("Default")]
        public String AllTablesFolder
        {
            get
            {
                return ((String)this["AllTablesFolder"]);
            }
            set
            {
                //this is the value that ExtraFolder must be set to in user.config:                
                //Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ShareViewer\AllTables";
                this["AllTablesFolder"] = (String)value;
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

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public String AllTableDataStart
        {
            get
            {
                return ((String)this["AllTableDataStart"]);
            }
            set
            {
                this["AllTableDataStart"] = (String)value;
            }

        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public String AllTableDataEnd
        {
            get
            {
                return ((String)this["AllTableDataEnd"]);
            }
            set
            {
                this["AllTableDataEnd"] = (String)value;
            }

        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public String AllTableTradingSpan
        {
            get
            {
                return ((String)this["AllTableTradingSpan"]);
            }
            set
            {
                this["AllTableTradingSpan"] = (String)value;
            }

        }

        [UserScopedSetting()]
        [DefaultSettingValue("Row,Date,Day,TimeFrom,TimeTo,F,FP,FV")]
        public String InitialAllTableView
        {
            get
            {
                return ((String)this["InitialAllTableView"]);
            }
            set
            {
                this["InitialAllTableView"] = (String)value;
            }

        }

        [DefaultSettingValue("")]
        [UserScopedSetting()]
        public List<String> AllTableViews
        {
            get
            {
                return (List<String>)this["AllTableViews"];
            }
            set
            {
                this["AllTableViews"] = (List<String>)value;
            }

        }

        [DefaultSettingValue("")]
        [UserScopedSetting()]
        public Param ParamsLazyShare
        {
            get
            {
                return (Param)this["ParamsLazyShare"];
            }
            set
            {
                this["ParamsLazyShare"] = (Param)value;
            }

        }



    }
}