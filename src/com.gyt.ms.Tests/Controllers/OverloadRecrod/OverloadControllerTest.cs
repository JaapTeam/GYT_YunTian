using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.gyt.ms.Controllers;
using FluentAssertions.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Zer.Services.OverloadRecrod;
using Zer.Services.OverloadRecrod.Dto;

namespace com.gyt.ms.Tests.Controllers.OverloadRecrod
{
    [TestFixture]
    public class OverloadRecrodControllerTest : ControllerTestBase
    {
        //[Test]
        //[Category("OverloadInfo")]
        //public void TestForExport_InputDefaul_ReturnErrorView()
        //{
        //    using (var ctrl=new OverloadRecrodController(MockService<IOverloadRecrodService,OverloadRecrodDto>.Mock()))
        //    {
        //        int[] ids = new int[]{};

        //        var actval = ctrl.Export(ids);

        //        actval.Should().BeRedirectResult("index", "Error", "请选择需要导出的记录！");
        //    }    
        //}
    }
}
