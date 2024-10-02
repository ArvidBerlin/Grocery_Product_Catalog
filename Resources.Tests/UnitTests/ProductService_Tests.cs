using Moq;
using NuGet.Frameworks;
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Tests.UnitTests;

public class ProductService_Tests
{
    private readonly Mock<IProductService<Product, Product>> _mockProductService = new();

    [Fact]
    public void CreateProduct__ShouldReturnSuccessResponse__WhenProductIsCreated()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid().ToString(), Name = "Socker", Price = 22 };
        var expectedResponse = new ServiceResponse<Product> { Succeeded = true, Result = product, Message = "Product was created." };

        _mockProductService.Setup(productService => productService.CreateProduct(product)).Returns(expectedResponse);
        var productService = _mockProductService.Object;

        // Act
        var response = productService.CreateProduct(product);
        
        // Assert
        Assert.True(response.Succeeded);
        Assert.Equal(product, response.Result);
    }

    [Fact]
    public void GetAllProductsFromList__ShouldReturnListOfProducts()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid().ToString(), Name = "Socker", Price = 22 };
        var products = new List<Product> { product };
        var expectedResponse = new ServiceResponse<IEnumerable<Product>> { Succeeded = true, Result = products };

        _mockProductService.Setup(productService => productService.GetAllProductsFromList()).Returns(expectedResponse);
        var productService = _mockProductService.Object;

        // Act
        var response = productService.GetAllProductsFromList();

        // Assert
        Assert.True(response.Succeeded);
        Assert.Equal(products, response.Result);
    }

    [Fact]
    public void DeleteProduct__ShouldReturnSuccessResponse__WhenProductIsDeleted()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var expectedResponse = new ServiceResponse<Product> { Succeeded = true };

        _mockProductService.Setup(productService => productService.DeleteProduct(productId)).Returns(expectedResponse);
        var productService = _mockProductService.Object;

        // Act
        var response = productService.DeleteProduct(productId);

        // Assert
        Assert.True(response.Succeeded);
    }

    [Fact]
    public void UpdateProduct__ShouldReturnUpdatedProduct__WhenProductIsUpdated()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var product = new Product { Id = productId, Name = "Socker", Price = 22 };
        var updatedProduct = new Product { Id = productId, Name = "Mjöl", Price = 32 };
        var expectedResponse = new ServiceResponse<Product> { Succeeded = true, Result = updatedProduct };

        _mockProductService.Setup(productService => productService.UpdateProduct(productId, updatedProduct)).Returns(expectedResponse);
        var productService = _mockProductService.Object;

        // Act
        var response = productService.UpdateProduct(productId, updatedProduct);

        // Assert
        Assert.True(response.Succeeded);
        Assert.NotEqual(product, response.Result);
    }
}
