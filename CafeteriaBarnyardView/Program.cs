using CafeteriaBarnyardBisinessLogic.BusinessLogics;
using CafeteriaBarnyardBisinessLogic.HelperModels;
using CafeteriaBarnyardBisinessLogic.Interfaces;
using CafeteriaBarnyardBisinessLogic.ViewModels;
using CafeteriaBarnyardDatabaseImplement.Implements;
using System;
using System.Configuration;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace CafeteriaBarnyardView
{
    static class Program
    {
        public static ClientViewModel Client { get; set; }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            MailLogic.MailConfig(new MailConfig
            {
                SmtpClientHost = ConfigurationManager.AppSettings["SmtpClientHost"],
                SmtpClientPort = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClientPort"]),
                MailLogin = ConfigurationManager.AppSettings["MailLogin"],
                MailPassword = ConfigurationManager.AppSettings["MailPassword"],
            });
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormEnter>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IDishLogic, DishLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductLogic, ProductLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRequestLogic, RequestLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HelpOrderLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
