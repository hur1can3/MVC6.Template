﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvcTemplate.Resources;
using MvcTemplate.Tests;
using System;
using System.Collections.Generic;
using Xunit;

namespace MvcTemplate.Components.Mvc.Tests
{
    public class GreaterThanAdapterTests
    {
        private GreaterThanAdapter adapter;
        private ClientModelValidationContext context;
        private Dictionary<String, String> attributes;

        public GreaterThanAdapterTests()
        {
            attributes = new Dictionary<String, String>();
            adapter = new GreaterThanAdapter(new GreaterThanAttribute(128));
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "Int32Field");
            context = new ClientModelValidationContext(new ActionContext(), metadata, provider, attributes);
        }

        [Fact]
        public void AddValidation_GreaterThan()
        {
            adapter.AddValidation(context);

            Assert.Equal(3, attributes.Count);
            Assert.Equal("true", attributes["data-val"]);
            Assert.Equal("128", attributes["data-val-greater-than"]);
            Assert.Equal(Validation.For("GreaterThan", context.ModelMetadata.PropertyName, 128), attributes["data-val-greater"]);
        }

        [Fact]
        public void GetErrorMessage_GreaterThan()
        {
            String expected = Validation.For("GreaterThan", context.ModelMetadata.PropertyName, 128);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(expected, actual);
        }
    }
}
