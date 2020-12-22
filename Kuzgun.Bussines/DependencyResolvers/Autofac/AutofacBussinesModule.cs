using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Concrete.Managers;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Core.Utilities.EmailService.Smtp.Google;
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

            builder.RegisterType<GoogleEmailService>().As<IEmailService>();
        }
    }
}
