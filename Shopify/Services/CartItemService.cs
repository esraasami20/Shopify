using Microsoft.EntityFrameworkCore;
using Shopify.Helper;
using Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shopify.Services
{
    public class CartItemService
    {
        private readonly ShopifyContext _db;
        private readonly CartService _cartService;
        public CartItemService(ShopifyContext db , CartService cartService)
        {
            _db = db;
            _cartService = cartService;
        }

        // get cart items

        public Response GetCartItems(int CartId)
        {
            Cart cart = _db.Carts.SingleOrDefault(c => c.CartId == CartId && c.Isdeleted == false);
             if(cart != null)
             {
                return new Response { Status = "Success", data = cart.CartItems.Where(i => i.Isdeleted == true).ToList() };
             }
            return new Response { Status="Error"};
        }



        // add cart item 

        public async Task<Response> AddCartItemAsync(CartItem cartItem , IIdentity customer)
        {
            // product is found
            bool isExist = ProductIsExist(cartItem.ProductId);
            if (isExist)
            {
                // get product
                Product product = GetProduct(cartItem.ProductId);
                // get product quantity
                int ProductQuantity = product.InventoryProducts.Select(c => c.Quantity).Sum();

                // productSealed
                int ProductSealedQuantity = product.QuantitySealed;
                if (ProductQuantity >= cartItem.Quantity)
                { 
                    
                    Cart cart = _db.Carts.Include(c => c.CartItems).SingleOrDefault(c => c.CustomerID == HelperMethods.GetAuthnticatedUserId(customer) && c.Approved == false && c.Isdeleted == false);
                    cartItem.TotalPrice += cartItem.Quantity * product.Price;

                        if (cart == null)
                        {
                            Cart newcart = await _cartService.AddCart(new Cart(), customer);
                            _db.CartItems.Add(new CartItem { CartId = newcart.CartId, ProductId = cartItem.ProductId, Quantity = cartItem.Quantity, TotalPrice = cartItem.TotalPrice });
                         
                          newcart.Cost += cartItem.TotalPrice; 
                        }
                        else
                        {
                            if (cart.CartItems.FirstOrDefault(c => c.ProductId == cartItem.ProductId) == null)
                           {
                            cart.CartItems.Add(cartItem);
                            cart.Cost += cartItem.TotalPrice;
                           }
                           else
                           {
                            return new Response { Status = "Error", Message = "Product Already existed" };

                            }

                          }


                        // update quantity
                        product.InventoryProducts.FirstOrDefault(i => i.ProductId == product.ProductId).Quantity -= cartItem.Quantity;
                        // update sealedQuantity

                        product.QuantitySealed += cartItem.Quantity;
                        _db.SaveChanges();
                        return new Response { Status = "Success", data = cart };
                   

                }
                return new Response { Status = "Error", Message = "Quantity Not Avaliable" };
            }
            return new Response { Status = "Error" , Message="Product Not Found" };
        }





        private bool ProductIsExist(int productId)
        {
           return _db.Products.Include(i => i.InventoryProducts).FirstOrDefault(p => p.ProductId == productId)!=null ;
        }



        private Product GetProduct(int productId)
        {
            return _db.Products.Include(i => i.InventoryProducts).FirstOrDefault(p => p.ProductId == productId) ;
        }







        /// edit cart item

        public async Task<Response> EditCartItemAsync(CartItem cartItem , IIdentity Customer)
        {
             var cartItems =  _db.Carts.Include(c=>c.CartItems).Where(c=>c.CustomerID == HelperMethods.GetAuthnticatedUserId(Customer) && c.Isdeleted==false && c.Approved ==false).Select(c=>c.CartItems).FirstOrDefault();
             var cartItemFound =  cartItems.FirstOrDefault(c=>c.ProductId == cartItem.ProductId && c.Isdeleted == false );
            // get product
            Product product = GetProduct(cartItem.ProductId);
            if (cartItemFound!=null)
            {
                product.QuantitySealed += cartItem.Quantity - cartItemFound.Quantity;
                if (cartItem.Quantity <=(cartItemFound.Quantity + product.InventoryProducts.FirstOrDefault(i => i.ProductId == product.ProductId).Quantity))
                {
                    if (cartItemFound.Quantity > cartItem.Quantity)
                    {
                         product.InventoryProducts.FirstOrDefault(i => i.ProductId == product.ProductId).Quantity += ( cartItemFound.Quantity - cartItem.Quantity);
                        _db.Carts.FirstOrDefault(c => c.CartId == cartItemFound.CartId).Cost -= (cartItemFound.Quantity - cartItem.Quantity) * product.Price;
                    }
                    else
                    {
                        product.InventoryProducts.FirstOrDefault(i => i.ProductId == product.ProductId).Quantity -= product.QuantitySealed ;
                        _db.Carts.FirstOrDefault(c => c.CartId == cartItemFound.CartId).Cost += ( cartItem.Quantity - cartItemFound.Quantity) * product.Price;

                    }
                   // product.QuantitySealed +=   _db.CartItems.Where(p => p.ProductId == product.ProductId).Select(s => s.Quantity).Sum() ;
                    product.QuantitySealed +=  cartItem.Quantity - cartItemFound.Quantity;
                    cartItemFound.Quantity = cartItem.Quantity;
                    cartItemFound.TotalPrice = cartItem.Quantity * product.Price;
                    _db.SaveChanges();
                    return new Response { Status="Success" ,data= cartItem };
                }
                return new Response { Status = "Error", Message="Quantity Not avaliable" };
            }
            return null;
        }




        /// delete cart item 
        public CartItem DeleteCartItemAsync(int cartItemId)
        {
            var cartItem = _db.CartItems.Include(i=>i.Cart).FirstOrDefault(i=>i.CartItemId == cartItemId && i.Isdeleted == false);
            if(cartItem != null && cartItem.Cart.CartItems.Count() > 1)
            {
                 cartItem.Isdeleted = true;
                _db.InventoryProducts.FirstOrDefault(i=>i.ProductId  == cartItem.ProductId).Quantity += cartItem.Quantity;
                _db.Products.FirstOrDefault(i => i.ProductId == cartItem.ProductId).QuantitySealed -= cartItem.Quantity;
                


                _db.SaveChanges();
                return cartItem;
            }
            if (cartItem != null && cartItem.Cart.CartItems.Count() ==1)
            {
                cartItem.Isdeleted = true;
                cartItem.Cart.Isdeleted = true;
                _db.InventoryProducts.FirstOrDefault(i => i.ProductId == cartItem.ProductId).Quantity += cartItem.Quantity;
                _db.Products.FirstOrDefault(i => i.ProductId == cartItem.ProductId).QuantitySealed -= cartItem.Quantity;
                _db.SaveChanges();
                return cartItem;
            }
            return null;
        }

    }
}
