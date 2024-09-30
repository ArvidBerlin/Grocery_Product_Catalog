using Resources.Models;

namespace Resources.Interfaces;

public interface IProductService<T, TResult> where T : class where TResult : class
{
    ServiceResponse<TResult> CreateProduct(T product);
    ServiceResponse<IEnumerable<TResult>> GetAllProductsFromList();
    ServiceResponse<TResult> DeleteProduct(T product);
    ServiceResponse<TResult> UpdateProduct(string productName, T updatedProduct);
}
