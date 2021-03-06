﻿using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Repository;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        private readonly BookStoreContext context;

        public CartRL(BookStoreContext context)
        {
            this.context = context;
        }
        public bool AddCart(CartItem cart)
        {
            try
            {
                var employeeRecord = from p in this.context.products.ToList() select p;
                int result = 0;
                Product res = this.context.products.Where(x =>
                                                   x.product_id == cart.product_id
                                                 ).FirstOrDefault();

                CartItem cart1 = new CartItem();
                cart1.product_id = cart.product_id;
                cart1.loginUser = cart.loginUser;
                cart1.quantityToBuy = cart.quantityToBuy;
                foreach (Product item in employeeRecord)
                {
                    if (item.product_id == cart.product_id)
                    {
                        item.quantity = item.quantity - 1;
                    }
                }
                this.context.Add(cart1);
                result = this.context.SaveChanges();


                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartItem> GetCartItems(string LoggedInUser)
        {
            List<CartItem> products = new List<CartItem>();
            try
            {

                List<CartItem> list = (from e in this.context.cartItems
                                       select new CartItem
                                       {
                                           product_id = e.product_id,
                                           quantityToBuy = e.quantityToBuy,
                                           loginUser = e.loginUser
                                       }).Where(x => x.loginUser == LoggedInUser).ToList<CartItem>();
                foreach (CartItem item in list)
                {
                    var res = this.context.products.Where(x =>
                                                    x.product_id == item.product_id
                                                ).FirstOrDefault();
                    item.Product = res;
                    products.Add(item);
                }


                return products;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateCart(CartItem cart)
        {
            CartItem existsCart = this.context.cartItems.Find(x => x.product_id == cart.product_id && x.loginUser == cart.loginUser).FirstOrDefault();
            var employeeRecord = from p in this.context.products.ToList() select p;
            if (existsCart != null)
            {
                existsCart.product_id = cart.product_id;
                existsCart.loginUser = cart.loginUser;
                existsCart.quantityToBuy = existsCart.quantityToBuy + 1;
                foreach (Product item in employeeRecord)
                {
                    if (item.product_id == existsCart.product_id)
                    {
                        item.quantity = item.quantity - 1;
                        existsCart.Price = item.price * existsCart.quantityToBuy;
                    }
                }
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveCartItem(CartItem cart)
        {
            try
            {
                var employeeRecord = from p in this.context.products.ToList() select p;
                CartItem cartItem = this.context.cartItems.Where(x =>
                                                   x.product_id == cart.product_id && x.loginUser == cart.loginUser
                                                 ).FirstOrDefault();
                foreach (Product item in employeeRecord)
                {
                    if (item.product_id == cartItem.product_id)
                    {
                        item.quantity = item.quantity + cartItem.quantityToBuy;
                    }
                }
                this.context.cartItems.Remove(cartItem);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ReduceBookQuantity(CartItem cart)
        {
            try
            {
                var employeeRecord = from p in this.context.products.ToList() select p;
                CartItem cartItem = this.context.cartItems.Where(x =>
                                                   x.product_id == cart.product_id && x.loginUser == cart.loginUser
                                                 ).FirstOrDefault();
                if (cart.quantityToBuy > 0)
                {
                    cartItem.quantityToBuy = cartItem.quantityToBuy - 1;
                    foreach (Product item in employeeRecord)
                    {
                        if (item.product_id == cartItem.product_id)
                        {
                            item.quantity = item.quantity + 1;
                            cartItem.Price = item.price * cartItem.quantityToBuy;
                        }
                    }
                    int result = this.context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //this.context.cartItems.Remove(cartItem);
                return false;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
