Glimpse Dispose Bug

====================


Sample app to demonstrate a dispose bug on view model with glimpse.



## Steps to recreate:

1. Run sample solution in debug mode in Visual Studio with the setting to break on user unhandled exceptions.

2. Enable glimpse
3. Refresh the homepage with glimpse enabled.



You'll notice the ObjectDisposeException being thrown on a property access 
to the DisposableIndexModel.SomeProperty 
view model property.

It seems that Glimpse is gathering data from the ASP.Net runtime on EndRequest - after controllers are disposed.



This is likely not a valid use case, however, it originally was noticed with a project that was using Entity Framework 

entities as the model - it would blow up in glimpse on access to a lazy load propery after the original context
was disposed.


## Stack Trace
GlimpseDisposeBug.dll!GlimpseDisposeBug.Models.DisposableIndexModel.ThrowIfDisposed() Line 28	C#
GlimpseDisposeBug.dll!GlimpseDisposeBug.Models.DisposableIndexModel.SomeProperty.get() Line 15 + 0x8 bytes	C#
[Native to Managed Transition]	
System.dll!System.SecurityUtils.MethodInfoInvoke(System.Reflection.MethodInfo method, object target, object[] args) + 0x5f bytes	
System.dll!System.ComponentModel.ReflectPropertyDescriptor.GetValue(object component) + 0x5f bytes	
System.Web.Mvc.dll!System.Web.Mvc.AssociatedMetadataProvider.GetPropertyValueAccessor.AnonymousMethod__a() + 0x30 bytes	
System.Web.Mvc.dll!System.Web.Mvc.ModelMetadata.Model.get() + 0x3c bytes	
System.Web.Mvc.dll!System.Web.Mvc.ModelMetadata.GetSimpleDisplayText() + 0x125 bytes	
System.Web.Mvc.dll!System.Web.Mvc.CachedModelMetadata<System.Web.Mvc.CachedDataAnnotationsMetadataAttributes>.ComputeSimpleDisplayText() + 0x25 bytes	
System.Web.Mvc.dll!System.Web.Mvc.CachedDataAnnotationsModelMetadata.ComputeSimpleDisplayText() + 0x18b bytes	
System.Web.Mvc.dll!System.Web.Mvc.CachedModelMetadata<System.Web.Mvc.CachedDataAnnotationsMetadataAttributes>.GetSimpleDisplayText() + 0x27 bytes	
System.Web.Mvc.dll!System.Web.Mvc.ModelMetadata.SimpleDisplayText.get() + 0x39 bytes	
System.Web.Mvc.dll!System.Web.Mvc.CachedModelMetadata<System.Web.Mvc.CachedDataAnnotationsMetadataAttributes>.SimpleDisplayText.get() + 0x25 bytes	
Glimpse.Mvc4.dll!Glimpse.Mvc.Tab.Metadata.ProcessMetaData(System.Web.Mvc.ModelMetadata metadata) + 0x2fc bytes	
Glimpse.Mvc4.dll!Glimpse.Mvc.Tab.Metadata.GetData(Glimpse.Core.Extensibility.ITabContext context) + 0x175 bytes	
Glimpse.Core.dll!Glimpse.Core.Framework.GlimpseRuntime.ExecuteTabs(Glimpse.Core.Extensibility.RuntimeEvent runtimeEvent) + 0x2bb bytes	
Glimpse.Core.dll!Glimpse.Core.Framework.GlimpseRuntime.EndRequest() + 0x23c bytes	
Glimpse.AspNet.dll!Glimpse.AspNet.HttpModule.EndRequest(System.Web.HttpContextBase httpContext) + 0x5a bytes	
Glimpse.AspNet.dll!Glimpse.AspNet.HttpModule.Init.AnonymousMethod__3(object context, System.EventArgs e) + 0x3f bytes	
System.Web.dll!System.Web.HttpApplication.SyncEventExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute() + 0xee bytes	
System.Web.dll!System.Web.HttpApplication.ExecuteStep(System.Web.HttpApplication.IExecutionStep step, ref bool completedSynchronously) + 0x73 bytes	
System.Web.dll!System.Web.HttpApplication.PipelineStepManager.ResumeSteps(System.Exception error) + 0x701 bytes	
System.Web.dll!System.Web.HttpApplication.ResumeSteps(System.Exception error) + 0x2b bytes	
System.Web.dll!System.Web.HttpApplication.BeginProcessRequestNotification(System.Web.HttpContext context, System.AsyncCallback cb) + 0x91 bytes	
System.Web.dll!System.Web.HttpRuntime.ProcessRequestNotificationPrivate(System.Web.Hosting.IIS7WorkerRequest wr, System.Web.HttpContext context) + 0x236 bytes	
System.Web.dll!System.Web.HttpRuntime.ProcessRequestNotification(System.Web.Hosting.IIS7WorkerRequest wr, System.Web.HttpContext context) + 0x57 bytes	
System.Web.dll!System.Web.Hosting.PipelineRuntime.ProcessRequestNotificationHelper(System.IntPtr rootedObjectsPointer, System.IntPtr nativeRequestContext, System.IntPtr moduleData, int flags) + 0x2e2 bytes	
System.Web.dll!System.Web.Hosting.PipelineRuntime.ProcessRequestNotification(System.IntPtr rootedObjectsPointer, System.IntPtr nativeRequestContext, System.IntPtr moduleData, int flags) + 0x48 bytes	
[Native to Managed Transition]	
[Managed to Native Transition]	
System.Web.dll!System.Web.Hosting.PipelineRuntime.ProcessRequestNotificationHelper(System.IntPtr rootedObjectsPointer, System.IntPtr nativeRequestContext, System.IntPtr moduleData, int flags) + 0x422 bytes	
System.Web.dll!System.Web.Hosting.PipelineRuntime.ProcessRequestNotification(System.IntPtr rootedObjectsPointer, System.IntPtr nativeRequestContext, System.IntPtr moduleData, int flags) + 0x48 bytes	
[Appdomain Transition]	
