﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvcTemplate.Resources;
using MvcTemplate.Tests;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace MvcTemplate.Components.Mvc.Tests
{
    public class MinLengthAdapterTests
    {
        [Fact]
        public void GetErrorMessage_MinLength()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            MinLengthAdapter adapter = new MinLengthAdapter(new MinLengthAttribute(128));
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            ModelValidationContextBase context = new ModelValidationContextBase(new ActionContext(), metadata, provider);

            String expected = Validation.For("MinLength", context.ModelMetadata.PropertyName, 128);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validation.For("MinLength"), adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }
    }
}
