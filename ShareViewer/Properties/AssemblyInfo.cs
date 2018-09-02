using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("ShareViewer")]
[assembly: AssemblyDescription("Share Trade inspector")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ShareViewerCo")]
[assembly: AssemblyProduct("ShareViewer")]
[assembly: AssemblyCopyright("Copyright ©  2018, Gunther Lu. Developer: Leon van Dyk")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4fbafb84-e148-4c87-ad0e-76b887eb0f85")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.7.0")]
[assembly: AssemblyFileVersion("1.2.7.0")]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]