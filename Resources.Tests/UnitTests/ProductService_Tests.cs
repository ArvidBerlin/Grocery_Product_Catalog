using Moq;
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Tests.UnitTests;

public class ProductService_Tests
{
    private readonly Mock<IProductService<Product, Product>> _mockProductService = new();
}
