using Newtonsoft.Json;
using Resources.Interfaces;
using Resources.Models;
using System.Diagnostics;

namespace Resources.Services;

public class ProductService : IProductService<Product, Product>
{
    private readonly FileService _fileService;
    private List<Product> _products = [];

    public ProductService(string filePath)
    {
        _fileService = new FileService(filePath);
        _products = [];
        GetAllProductsFromList();
    }

    public ServiceResponse<Product> CreateProduct(Product product)
    {
        try
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return new ServiceResponse<Product> { Succeeded = false, Message = "\n\t No name was given to product." };
            }

            if (product.Price <= 0)
            {
                return new ServiceResponse<Product> { Succeeded = false, Message = "\n\t Product price can't be 0, please set a price." };
            }

            if (_products.Any(x => x.Name == product.Name))
            {
                return new ServiceResponse<Product> { Succeeded = false, Message = "\n\t Product with same name already exists in the inventory" };
            }

            _products.Add(product);

            var json = JsonConvert.SerializeObject(_products);
            var result = _fileService.SaveToFile(json);

            if (result.Succeeded)
            {
                return new ServiceResponse<Product> { Succeeded = true, Message = "\n\t Product was added to the inventory." };
            }
            else
            {
                return new ServiceResponse<Product> { Succeeded = false, Message = result.Message };
            }
            
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResponse<Product> { Succeeded = false, Message = ex.Message };
        }
    }

    public ServiceResponse<IEnumerable<Product>> GetAllProductsFromList()
    {
        try
        {
            var content = _fileService.LoadFromFile();

            if (content.Succeeded)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(content.Result!)!;

                if (_products.Any())
                {
                    return new ServiceResponse<IEnumerable<Product>> { Succeeded = true, Result = _products };
                }
                else
                {
                    return new ServiceResponse<IEnumerable<Product>> { Succeeded = false, Result = _products, Message = "\n\t No products in inventory." };
                }
            }
            else
            {
                return new ServiceResponse<IEnumerable<Product>> { Succeeded = false, Message = "No file was found." };
            }
           
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResponse<IEnumerable<Product>> { Succeeded = false, Message = ex.Message };
        }

    }

    public ServiceResponse<Product> DeleteProduct(Product product)
    {
        try
        {
            var content = _fileService.LoadFromFile();

            if (content.Succeeded)
            {
                if (string.IsNullOrEmpty(product.Name))
                {
                    return new ServiceResponse<Product> { Succeeded = false, Message = "\n\t You did not enter a product to remove." };
                }

                var productToRemove = _products.FirstOrDefault(x => x.Name == product.Name);

                if (productToRemove == null)
                {
                    return new ServiceResponse<Product> { Succeeded = false, Message = "\n\t Product does not exist in inventory." };
                }

                _products.Remove(productToRemove);

                var json = JsonConvert.SerializeObject(_products, Formatting.Indented);
                var result = _fileService.SaveToFile(json);
                if (result.Succeeded)
                {
                    return new ServiceResponse<Product> { Succeeded = true, Message = "\n\t Product was removed from the inventory." };
                }
            }
            else
            {
                return new ServiceResponse<Product> { Succeeded = false, Message = "\n\t No file was found. " };
            }
            return null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResponse<Product> { Succeeded = false, Message = ex.Message };
        }
    }
}

