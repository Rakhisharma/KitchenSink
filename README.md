# KitchenSink

Present typical UI patterns in a form that is easy to apply in any Starcounter app.

Features:

- Rendering (Breadcrumb, Chart, Datagrid, HTML, Link, Map, Markdown, Pagination, Table)
- Server Push (Async Response, Cookies, Dialog Box, Flash Message, Lazy Loading, Nested Views, Progress Bar, Redirect, Client Local State)
- User Input (Autocomplete, Button, Checkbox, Datepicker, Dropdown, Decimal Input, File Upload, Integer Input, Multiselect, Password, Radio Button, Radiolist, Text Input, Textarea, Toggle Button, Validation)

The guidelines for this project are:

- ❌ Do not have styles or HTML classes that pollute the light DOM.
- ✔️ Only use native HTML without classes in the light DOM. Style them in Shadow DOM.
- ❌ Do not have JavaScript that pollutes the global scope.
- ✔️ Minimize the client side logic. For interactivity use external custom elements installed via Bower.
- ✔️ Make use of external libraries in C# installed via NuGet.

It is recommended to follow these guidelines for Starcounter apps in general.

## Requirements

This repository uses features from C# 7. Thus, **Visual Studio 2017** is required to build. 

If you really want to use VS 2015 and you don't care about IntelliSense warnings, you can build by installing [Microsoft.Net.Compilers](https://www.nuget.org/packages/Microsoft.Net.Compilers/) in the project using NuGet (`Install-Package Microsoft.Net.Compilers`). That will make VS use the C# compiler provided in the package instead of the default one.

## Developer instructions - Contributing

For developer instructions, go to [CONTRIBUTING](CONTRIBUTING.md).

## Video

Intended for 13 October 2015 webinar: http://starcounter.io/video-expressing-your-ui-in-json-plain-data-binding-advanced-data-binding/

## Screenshot

![](KitchenSink.PNG)

## License

MIT
