﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShareViewer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Control")]
        public global::System.Drawing.Color BackgroundColor {
            get {
                return ((global::System.Drawing.Color)(this["BackgroundColor"]));
            }
            set {
                this["BackgroundColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Default")]
        public string ExtraFolder {
            get {
                return ((string)(this["ExtraFolder"]));
            }
            set {
                this["ExtraFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Default")]
        public string AllTablesFolder {
            get {
                return ((string)(this["AllTablesFolder"]));
            }
            set {
                this["AllTablesFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.bsb-software.de/rese/")]
        public string SharesUrl {
            get {
                return ((string)(this["SharesUrl"]));
            }
            set {
                this["SharesUrl"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Inhalt.txt")]
        public string DataDaysListingFilename {
            get {
                return ((string)(this["DataDaysListingFilename"]));
            }
            set {
                this["DataDaysListingFilename"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"Row,Date,Day,TimeFrom,TimeTo,F,FP,FV\"")]
        public string InitialAllTableView {
            get {
                return ((string)(this["InitialAllTableView"]));
            }
            set {
                this["InitialAllTableView"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AllTableDataStart {
            get {
                return ((string)(this["AllTableDataStart"]));
            }
            set {
                this["AllTableDataStart"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AllTableDataEnd {
            get {
                return ((string)(this["AllTableDataEnd"]));
            }
            set {
                this["AllTableDataEnd"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AllTableTradingSpan {
            get {
                return ((string)(this["AllTableTradingSpan"]));
            }
            set {
                this["AllTableTradingSpan"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>Make Slow (Five minutes) Prices SP,0,2,4,6,7,8,10,11,18,21,24</string>
  <string>Make Five minutes Price Gradients PG,0,2,4,6,7,8,10,11,12,18,19,21,22,24,25</string>
  <string>Find direction and Turning,0,2,4,6,7,8,10,48</string>
  <string>Find Five minutes Gradients Figure PGF,0,2,4,6,7,8,10,48</string>
  <string>Related volume Figure (RPGFV) of biggest PGF,0,2,4,6,7,8,10,48</string>
  <string>Make High Line HL,0,2,4,6,7,8,10,48</string>
  <string>Make Low Line LL,0,2,4,6,7,8,10,48</string>
  <string>Make Slow Volumes SV,0,2,4,6,7,8,10,48</string>
  <string>Slow Volume Figure SVFac,0,2,4,6,7,8,10,48</string>
  <string>Slow Volume Figure SVFbd,0,2,4,6,7,8,10,48</string>
  <string>Identify Lazy Shares,0,2,4,6,7,8,10,48</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection AllTableViews {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["AllTableViews"]));
            }
            set {
                this["AllTableViews"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::ShareViewer.LazyShareParam ParamsLazyShare {
            get {
                return ((global::ShareViewer.LazyShareParam)(this["ParamsLazyShare"]));
            }
            set {
                this["ParamsLazyShare"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::ShareViewer.SlowPriceParam ParamsSlowPrice {
            get {
                return ((global::ShareViewer.SlowPriceParam)(this["ParamsSlowPrice"]));
            }
            set {
                this["ParamsSlowPrice"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.Specialized.StringCollection Holidays {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Holidays"]));
            }
            set {
                this["Holidays"] = value;
            }
        }
    }
}
