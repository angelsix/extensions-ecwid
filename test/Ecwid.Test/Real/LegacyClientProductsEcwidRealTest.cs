﻿// Licensed under the MIT License. See LICENSE in the git repository root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using Ecwid.Legacy;
using Xunit;

namespace Ecwid.Test.Real
{
    /// <summary>
    /// Tests with real http responses from Ecwid API v1.
    /// </summary>
    [SuppressMessage("ReSharper", "ExceptionNotDocumented")]
    [SuppressMessage("ReSharper", "ExceptionNotDocumentedOptional")]
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public class LegacyClientProductsEcwidRealTest
    {
        private const int ShopId = 1003;
        private const string Token = "nmGjgfnmGjgfnmGjgfnmGjgfnmGjgfsd";
        private readonly IEcwidLegacyClient _client = new EcwidLegacyClient(ShopId, Token, Token);

        [Fact]
        public async void GetCategoriesAsync_ReturnCorrectList()
        {
            var result = await _client.GetCategoriesAsync();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetCategoriesAsync_ZeroCategoryId_ReturnCorrectList()
        {
            var result = await _client.GetCategoriesAsync(0);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async void GetCategoriesAsync_InvalidCategory_ReturnNull()
        {
            var result = await _client.GetCategoriesAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async void GetCategoryAsync_ReturnCorrectList()
        {
            var result = await _client.GetCategoryAsync(20671017);

            Assert.NotNull(result.Subcategories);
            Assert.NotEmpty(result.Products);
        }

        [Fact]
        public async void GetCategoryAsync_InvalidCategoryId_ReturnNull()
        {
            var result = await _client.GetCategoryAsync(10221202);

            Assert.Null(result);
        }

        [Fact]
        public async void GetAllProductsAsync_ZeroParams_ReturnCorrectList()
        {
            var result = await _client.GetProductsAsync();

            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData(20671017)]
        [InlineData(0)]
        public async void GetProductsAsync_ValidCategoryId_ReturnCorrectList(int c)
        {
            var result = await _client.GetProductsAsync(c);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetProductsAsyncException_ThrowsException()
            => await Assert.ThrowsAsync<ArgumentException>(() => _client.GetProductsAsync(-1));

        [Fact]
        public async void GetProductsAsyncExceptionWrongToken_ThrowsException_Forbidden()
        {
            var ex = await Assert.ThrowsAsync<EcwidHttpException>(() => _client.GetProductsAsync(null, true));

            Assert.Equal(HttpStatusCode.Forbidden, ex.StatusCode);
        }

        [Fact]
        public async void GetProductsAsyncException404_ReturnNull()
        {
            var result = await _client.GetProductsAsync(221233);
            Assert.Null(result);
        }

        [Fact]
        public async void GetProductAsync_ReturnNotNull()
        {
            var result = await _client.GetProductAsync(70178253);

            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void GetProductAsync_IncorrectInt_ThrowsException(int i)
            => await Assert.ThrowsAsync<ArgumentException>(() => _client.GetProductAsync(i));

        [Fact]
        public async void UpdateProductAsync_ThrowsException_Forbidden()
        {
            var ex = await Assert.ThrowsAsync<EcwidHttpException>(() =>
                _client.UpdateProductAsync(123, new {weight = 600}));

            Assert.Equal(HttpStatusCode.Forbidden, ex.StatusCode);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async void UpdateProductAsync_InvalidInt_ThrowsException(int i) =>
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _client.UpdateProductAsync(i, new {weight = 600}));
    }
}