﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using MvcTemplate.Tests;
using NSubstitute;
using System;
using Xunit;

namespace MvcTemplate.Components.Mvc.Tests
{
    public class NotTrimmedAttributeTests
    {
        [Fact]
        public void NotTrimmedAttribute_SetsBinderType()
        {
            Type actual = new NotTrimmedAttribute().BinderType;
            Type expected = typeof(NotTrimmedAttribute);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void BindModelAsync_DoesNotTrimValue()
        {
            ModelMetadata metadata = new EmptyModelMetadataProvider().GetMetadataForProperty(typeof(AllTypesView), "StringField");
            DefaultModelBindingContext context = new DefaultModelBindingContext();
            NotTrimmedAttribute attribute = new NotTrimmedAttribute();

            context.ModelMetadata = metadata;
            context.ModelName = "StringField";
            context.ActionContext = new ActionContext();
            context.ModelState = new ModelStateDictionary();
            context.ValueProvider = Substitute.For<IValueProvider>();
            context.ActionContext.HttpContext = new DefaultHttpContext();
            context.HttpContext.RequestServices = Substitute.For<IServiceProvider>();
            context.ValueProvider.GetValue(context.ModelName).Returns(ValueProviderResult.None);
            context.ValueProvider.GetValue("StringField").Returns(new ValueProviderResult(" Value  "));
            context.HttpContext.RequestServices.GetService(typeof(ILoggerFactory)).Returns(Substitute.For<ILoggerFactory>());

            await attribute.BindModelAsync(context);

            ModelBindingResult expected = ModelBindingResult.Success(" Value  ");
            ModelBindingResult actual = context.Result;

            Assert.Equal(expected, actual);
        }
    }
}
