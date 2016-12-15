using Stylet;
using StyletIoC;
using CreditCalc.ViewModels;
using System.Windows;
using System;
using System.Windows.Threading;
using FluentValidation;

namespace CreditCalc
{
	class AppBootstrapper : Bootstrapper<MainViewModel>
	{
		//protected override void OnStart()
		//{
		//	This is called just after the application is started, but before the IoC container is set up.
		//	Set up things like logging, etc
		//}

		protected override void ConfigureIoC(IStyletIoCBuilder builder)
		{
			// Bind your own types. Concrete types are automatically self-bound.
			//builder.Bind<IMyInterface>().To<Type>();
			builder.Bind(typeof(IModelValidator<>)).To(typeof(ValidationAdapter<>));
			builder.Bind(typeof(IValidator<>)).ToAllImplementations();
		}

		//protected override void Configure()
		//{
		//	// This is called after Stylet has created the IoC container, so this.Container exists, but before the
		//	// Root ViewModel is launched.
		//	// Configure your services, etc, in here
		//}

		//protected override void OnLaunch()
		//{
		//	// This is called just after the root ViewModel has been launched
		//	// Something like a version check that displays a dialog might be launched from here
		//}

		//protected override void OnExit(ExitEventArgs e)
		//{
		//	// Called on Application.Exit
		//}

		//protected override void OnUnhandledException(DispatcherUnhandledExceptionEventArgs e)
		//{
		//	// Called on Application.DispatcherUnhandledException
		//}
	}
}