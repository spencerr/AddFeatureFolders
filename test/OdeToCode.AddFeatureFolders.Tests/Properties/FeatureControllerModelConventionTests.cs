﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using Controllers;
using Project.Name.Features;
using Project.Name.Features.Admin.ManageGolfers;
using Project.Name.Features.Foo.Bar.Baz;
using NUnit.Framework;

namespace OdeToCode.AddFeatureFolders.Tests
{
    [TestFixture]
    public class FeatureControllerModelConventionTests
    {
        [TestCase(typeof(ManageGolfersController), @"Features\Admin\ManageGolfers")]
        [TestCase(typeof(ManageUsersController), @"Features\Foo\Bar\Baz")]
        [TestCase(typeof(HomeController), @"Features")]
        [TestCase(typeof(AboutController), @"")]
        public void CanBuildPathFromControllerNamespace(Type controller, string expected)
        {
            var options = new FeatureFolderOptions();
            var service = new FeatureControllerModelConvention(options);
            var controllerType = controller.GetTypeInfo();
            var attributes = new List<string>();
            var model = new ControllerModel(controllerType, attributes);

            service.Apply(model);

            Assert.AreEqual(expected, model.Properties["feature"]);
        }

        [Test]
        public void CanUseCustomDerivationStrategy()
        {
            var options = new FeatureFolderOptions()
            {
                DeriveFeatureFolderName = c => @"Features\Foo"
            };
            var service = new FeatureControllerModelConvention(options);
            var controllerType = typeof(ManageUsersController).GetTypeInfo();
            var attributes = new List<string>();
            var model = new ControllerModel(controllerType, attributes);

            service.Apply(model);

            Assert.AreEqual(@"Features\Foo", model.Properties["feature"]);
        }
    }
}

namespace Project.Name.Features.Admin.ManageGolfers
{
    internal class ManageGolfersController
    {
    }
}

namespace Project.Name.Features.Foo.Bar.Baz
{
    internal class ManageUsersController
    {
    }
}

namespace Project.Name.Features
{
    internal class HomeController
    {       
    }
}

namespace Controllers
{
    internal class AboutController
    {
        
    }
}
