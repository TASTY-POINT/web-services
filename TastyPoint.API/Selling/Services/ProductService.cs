using TastyPoint.API.Selling.Domain.Models;
using TastyPoint.API.Selling.Domain.Repositories;
using TastyPoint.API.Selling.Domain.Services;
using TastyPoint.API.Selling.Domain.Services.Communication;
using TastyPoint.API.Shared.Domain.Repositories;

namespace TastyPoint.API.Selling.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPackRepository _packRepository;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IPackRepository packRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _packRepository = packRepository;
    }

    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _productRepository.ListAsync();
    }

    public async Task<IEnumerable<Product>> ListByPackIdAsync(int packId)
    {
        return await _productRepository.FindByPackIdAsync(packId);
    }

    public async Task<ProductResponse> FindByIdAsync(int productId)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);
        if (existingProduct == null)
            return new ProductResponse("Product not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred while saving the pack: {e.Message}");
        }
    }

    public async Task<ProductResponse> SaveAsync(Product product)
    {
        var existingPack = await _packRepository.FindByIdAsync(product.PackId);
        if (existingPack == null)
            return new ProductResponse("Invalid Pack");

        var existingProductWithName = await _productRepository.FindByNameAsync(product.Name);

        if (existingProductWithName != null)
            return new ProductResponse("Product name already exists");
        try
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return new ProductResponse(product);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred while saving the product: {e.Message}");
        }
    }

    public async Task<ProductResponse> UpdateAsync(int productId, Product product)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);
        if (existingProduct == null)
            return new ProductResponse("Product not found");

        var existingPack = await _packRepository.FindByIdAsync(product.PackId);

        if (existingPack == null)
            return new ProductResponse("Invalid Pack");

        var existingProductWithName = await _productRepository.FindByNameAsync(product.Name);
        if (existingProductWithName != null && existingProductWithName.Id != existingProduct.Id)
            return new ProductResponse("This Product name already exists");

        existingProduct.Name = product.Name;
        existingProduct.Type = product.Type;

        try
        {
            _productRepository.Update(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred while saving the product: {e.Message}");
        }
    }

    public async Task<ProductResponse> DeleteAsync(int productId)
    {
        var existingProduct = await _productRepository.FindByIdAsync(productId);

        if (existingProduct == null)
            return new ProductResponse("Tutorial not found.");
        try
        {
            _productRepository.Remove(existingProduct);
            await _unitOfWork.CompleteAsync();

            return new ProductResponse(existingProduct);
        }
        catch (Exception e)
        {
            return new ProductResponse($"An error occurred while saving the product: {e.Message}");
        }
    }
}