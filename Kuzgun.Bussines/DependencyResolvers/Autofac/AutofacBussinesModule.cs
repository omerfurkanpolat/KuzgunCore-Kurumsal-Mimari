using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Concrete.Managers;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Core.Utilities.EmailService.Smtp.Google;
using Kuzgun.Core.Utilities.Interceptors.Autofac;
using Kuzgun.Core.Utilities.Security;
using Kuzgun.Core.Utilities.Security.Jwt;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.DataAccess.Concrete.EntityFramework;

namespace Kuzgun.Bussines.DependencyResolvers.Autofac
{
    public class AutofacBussinesModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<MessageManager>().As<IMessageService>();
            builder.RegisterType<EfMessageDal>().As<IMessageDal>();

            builder.RegisterType<PostCommentManager>().As<IPostCommentService>();
            builder.RegisterType<EfPostCommentDal>().As<IPostCommentDal>();

            builder.RegisterType<PostManager>().As<IPostService>();
            builder.RegisterType<EfPostDal>().As<IPostDal>();

            builder.RegisterType<EfPostStatDal>().As<IPostStatDal>();
            builder.RegisterType<PostStatManager>().As<IPostStatService>();

            builder.RegisterType<SubCategoryManager>().As<ISubCategoryService>();
            builder.RegisterType<EfSubCategoryDal>().As<ISubCategoryDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

            builder.RegisterType<GoogleEmailService>().As<IEmailService>();
        }
    }
}
