[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Twits.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Twits.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace Twits.WEB.App_Start
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            string connectionName;

#if DEBUG
            connectionName = "LocalConnection";
#else

            connectionName = "WebConnection";
#endif

            string connectionString = System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings[connectionName].ToString();


            Ninject.Modules.INinjectModule[] modules = new Ninject.Modules.INinjectModule[]
            {
                new BLL.Modules.NIModule(connectionString)
            };

            var kernel = new StandardKernel(modules);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new CustomResolver(kernel));
        }        
    }

    class CustomResolver : System.Web.Mvc.IDependencyResolver
    {
        private Ninject.IKernel kernel;

        public CustomResolver(Ninject.IKernel kernel)
        {
            this.kernel = kernel;
            AddBinds();
        }

        public object GetService(Type serviceType)
        {
            return kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBinds()
        {
            kernel.Bind<BLL.Interfaces.IAccount>()
                .To<BLL.Services.AccountService>();

            kernel.Bind<BLL.Interfaces.IMessage>()
                .To<BLL.Services.MessageService>();

            kernel.Bind<BLL.Interfaces.IUser>()
                .To<BLL.Services.UserService>();
        }
    }
}
