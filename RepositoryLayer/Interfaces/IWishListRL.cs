using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRL
    {
        bool AddWishlist(Wishlist productId);
        List<Wishlist> GetWishlist(string LoggedInUser);
    }
}
