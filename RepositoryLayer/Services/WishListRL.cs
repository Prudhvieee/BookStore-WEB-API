using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        public bool AddWishlist(Wishlist productId)
        {
            throw new NotImplementedException();
        }

        public List<Wishlist> GetWishlist(string LoggedInUser)
        {
            throw new NotImplementedException();
        }
    }
}
