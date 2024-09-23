using Resources.Models;
using System.Diagnostics;

namespace Resources.Services;

public class ProductService
{
    private readonly List<Product> _products = [];

    public ServiceResponse CreateProduct(Product product)
    {
        try
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return new ServiceResponse { Succeeded = false, Message = "\n\t No name was given to product." };
            }

            if (product.Price <= 0)
            {
                return new ServiceResponse { Succeeded = false, Message = "\n\t Product price can't be 0, please set a price." };
            }

            if (_products.Any(x => x.Name == product.Name))
            {
                return new ServiceResponse { Succeeded = false, Message = "\n\t Product with same name already exists in the inventory" };
            }

            _products.Add(product);
            return new ServiceResponse { Succeeded = true, Message = "\n\t Product was added to the inventory." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResponse { Succeeded = false, Message = ex.Message };
        }
    }

    public ServiceResponse GetAllProductsFromList()
    {
        try
        {
            if (_products.Count == 0)
            {
                return new ServiceResponse { Succeeded = false, Content = _products, Message = "\n\t No products in inventory." };
            }
            else
            {
                return new ServiceResponse { Succeeded = true, Content = _products };
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new ServiceResponse { Succeeded=false, Message = ex.Message };
        }

    }

    public ServiceResponse DeleteProduct(Product product)
    {
        try
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return new ServiceResponse { Succeeded = false, Message = "\n\t You did not enter a product to remove." };
            }

            var productToRemove = _products.FirstOrDefault(x => x.Name == product.Name);

            if (productToRemove == null)
            {
                return new ServiceResponse { Succeeded = false, Message = "\n\t Product does not exist in inventory." };
            }

            _products.Remove(productToRemove);
            return new ServiceResponse { Succeeded = true, Message = "\n\t Product was removed from the inventory." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new ServiceResponse { Succeeded = false, Message = ex.Message };
        }
    }
}

