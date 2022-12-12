using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.iRepository
{
    public interface IShoppingCartRepository: IRepository<ShoppingCart>
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}