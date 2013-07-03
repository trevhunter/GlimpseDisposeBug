Glimpse Dispose Bug
====================

Sample app to demonstrate a dispose bug on view model with glimpse.

# Steps to recreate:
1. Run sample solution in debug mode in Visual Studio with the setting to break on user unhandled exceptions.
2. Enable glimpse
3. Refresh the homepage with glimpse enabled.

You'll notice the ObjectDisposeException being thrown on a property access 
to the DisposableIndexModel.SomeProperty view model property.

It seems that Glimpse is gathering data from the ASP.Net runtime on EndRequest - after controllers are disposed.

This is likely not a valid use case, however, it originally was noticed with a project that was using Entity Framework 
entities as the model - it would blow up in glimpse on access to a lazy load propery after the original context
was disposed.
