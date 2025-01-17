﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MvcTemplate.Resources;
using MvcTemplate.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace MvcTemplate.Components.Mvc.Tests
{
    public class EmailAddressAdapterTests
    {
        private EmailAddressAdapter adapter;
        private ClientModelValidationContext context;
        private Dictionary<String, String> attributes;

        public EmailAddressAdapterTests()
        {
            attributes = new Dictionary<String, String>();
            adapter = new EmailAddressAdapter(new EmailAddressAttribute());
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            context = new ClientModelValidationContext(new ActionContext(), metadata, provider, attributes);
        }

        [Fact]
        public void AddValidation_Email()
        {
            adapter.AddValidation(context);

            Assert.Equal(2, attributes.Count);
            Assert.Equal("true", attributes["data-val"]);
            Assert.Equal(Validation.For("Email", context.ModelMetadata.PropertyName), attributes["data-val-email"]);
        }

        [Fact]
        public void GetErrorMessage_Email()
        {
            String expected = Validation.For("Email", context.ModelMetadata.PropertyName);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(expected, actual);
        }
    }
}
