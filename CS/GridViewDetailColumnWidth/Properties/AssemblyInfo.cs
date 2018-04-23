// Developer Express Code Central Example:
// How to keep master and detail view column widths synchronized
// 
// When you have a master-detail view and for some reason don't want to use the
// AutoColumnWidth option, the detail view is still stretched to accommodate the
// entire master view's width. So, in this example we've decided to create a grid
// view that changes this behavior and synchronizes the detail view's width with
// the mater view's columns.
// We create and register a GridView descendant that
// introduces the AutoSynchronizeDetailsLayout option by using a descendant of the
// GridOptionsView class. If this option is enabled and the AutoColumnWidth option
// is disabled the new behavior takes effect. The detail view's width is now
// calculated based on the master view columns' width, so, the detail view ends up
// with the last master view's column. In addition, if the number of detail and
// master view columns is equal, the detail view's columns are aligned with that of
// the master view for even a smoother view.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2454

// Developer Express Code Central Example:
// How to keep master and detail view column widths synchronized
// 
// When you have a master-detail view and for some reason don't want to use the
// AutoColumnWidth option, the detail view is still stretched to accommodate the
// entire master view's width. So, in this example we've decided to create a grid
// view that changes this behavior and synchronizes the detail view's width with
// the mater view's columns.
// We create and register a GridView descendant that
// introduces the AutoSynchronizeDetailsLayout option by using a descendant of the
// GridOptionsView class. If this option is enabled and the AutoColumnWidth option
// is disabled the new behavior takes effect. The detail view's width is now
// calculated based on the master view columns' width, so, the detail view ends up
// with the last master view's column. In addition, if the number of detail and
// master view columns is equal, the detail view's columns are aligned with that of
// the master view for even a smoother view.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2454

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("GridViewDetailColumnWidth")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("GridViewDetailColumnWidth")]
[assembly: AssemblyCopyright("Copyright ©  2010")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a16ccc3e-322d-4ec2-8c71-0a7ce72dc741")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
