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
    public class IntegerAdapterTests
    {
        private IntegerAdapter adapter;
        private ClientModelValidationContext context;
        private Dictionary<String, String> attributes;

        public IntegerAdapterTests()
        {
            attributes = new Dictionary<String, String>();
            adapter = new IntegerAdapter(new IntegerAttribute());
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            context = new ClientModelValidationContext(new ActionContext(), metadata, provider, attributes);
        }

        [Fact]
        public void AddValidation_Integer()
        {
            adapter.AddValidation(context);

            Assert.Equal(2, attributes.Count);
            Assert.Equal("true", attributes["data-val"]);
            Assert.Equal(Validation.For("Integer", context.ModelMetadata.PropertyName), attributes["data-val-integer"]);
        }

        [Fact]
        public void GetErrorMessage_Integer()
        {
            String expected = Validation.For("Integer", context.ModelMetadata.PropertyName);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(expected, actual);
        }
    }
}
