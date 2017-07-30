﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework;
using Zer.Framework.Dependency;
using Zer.GytDataService;
using Zer.GytDataService.Impl;

namespace com.gyt.ms.Tests.Flow
{
    [TestFixture]
    public class FlowTest
    {
        [Test]
        [Category("Flow")]
        public void TestFor_LoadFlow_Correct()
        {
            //IocManager.Instance.Register(
            //    Types.FromAssemblyContaining(typeof(IDataService))
            //        .Where(t => t.Name.EndsWith("DataService"))
            //        .WithServiceFirstInterface()
            //);

            //IocManager.Instance.Register(
            //    Types.FromAssemblyContaining(typeof(IAppService))
            //        .Where(t => t.Name.EndsWith("Service"))
            //        .WithServiceFirstInterface()
            //);

            IocManager.Instance.Register(
                Component.For<ICompanyInfoDataService,CompanyInfoDataService>()
                         .ImplementedBy<CompanyInfoDataService>()
                         .LifestyleTransient()
                );

            var appService = IocManager.Instance.Resolve<ICompanyInfoDataService>();

            var actaul = appService.GetById(1);

            actaul.Should().NotBeNull();
        }

    }
}
