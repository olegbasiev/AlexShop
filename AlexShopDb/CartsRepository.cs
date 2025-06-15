using AlexShopDb.Models;

//using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;

public static class CartsRepository
{
    private static readonly string _connectionString = "Server=localhost;Database=online_shop;uId=root;password=99977788866aaaAa;";

    public static void Add(Product product, string userId)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be empty", nameof(userId));

        var existingCart = TryGetByUserId(userId);

        if (existingCart == null)
        {
            var newCart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>
            {
                new CartItem
                {
                    Quantity = 1,
                    Product = product
                }
            }
            };

            CartsRepository.AddCart(newCart);
        }
        else
        {
            var existingItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                existingCart.Items.Add(new CartItem
                {
                    Quantity = 1,
                    Product = product
                });
            }

            CartsRepository.UpdateCart(existingCart);
        }
    }


    public static Cart TryGetByUserId(string userId)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            // Получаем корзину
            var cartQuery = "SELECT * FROM Carts WHERE UserId = @UserId";
            using (var cartCommand = new MySqlCommand(cartQuery, connection))
            {
                cartCommand.Parameters.AddWithValue("@UserId", userId);

                using (var cartReader = cartCommand.ExecuteReader())
                {
                    if (cartReader.Read())
                    {
                        var cart = new Cart
                        {
                            Id = cartReader.GetInt32("Id"),
                            UserId = cartReader.GetString("UserId")
                        };

                        // Получаем товары корзины
                        cart.Items = GetCartItems(cart.Id, connection);
                        return cart;
                    }
                }
            }
        }
        return null;
    }

    public static void AddCart(Cart cart)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Добавляем корзину
                    var cartQuery = "INSERT INTO Carts (UserId) VALUES (@UserId); SELECT LAST_INSERT_ID();";
                    using (var cartCommand = new MySqlCommand(cartQuery, connection, transaction))
                    {
                        cartCommand.Parameters.AddWithValue("@UserId", cart.UserId);
                        cart.Id = Convert.ToInt32(cartCommand.ExecuteScalar());
                    }

                    // Добавляем товары корзины
                    foreach (var item in cart.Items)
                    {
                        var itemQuery = @"INSERT INTO CartItems (CartId, ProductId, Quantity) 
                                         VALUES (@CartId, @ProductId, @Quantity)";
                        using (var itemCommand = new MySqlCommand(itemQuery, connection, transaction))
                        {
                            itemCommand.Parameters.AddWithValue("@CartId", cart.Id);
                            itemCommand.Parameters.AddWithValue("@ProductId", item.Product.Id);
                            itemCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                            itemCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public static void UpdateCart(Cart cart)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Удаляем старые товары корзины
                    var deleteQuery = "DELETE FROM CartItems WHERE CartId = @CartId";
                    using (var deleteCommand = new MySqlCommand(deleteQuery, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@CartId", cart.Id);
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Добавляем обновленные товары
                    foreach (var item in cart.Items)
                    {
                        var insertQuery = @"INSERT INTO CartItems (CartId, ProductId, Quantity) 
                                          VALUES (@CartId, @ProductId, @Quantity)";
                        using (var insertCommand = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@CartId", cart.Id);
                            insertCommand.Parameters.AddWithValue("@ProductId", item.Product.Id);
                            insertCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                            insertCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    private static List<CartItem> GetCartItems(int cartId, MySqlConnection connection)
    {
        var items = new List<CartItem>();

        var query = @"SELECT ci.*, p.Name, p.Price, p.ImagePath, p.Description 
                     FROM CartItems ci
                     JOIN Products p ON ci.ProductId = p.Id
                     WHERE ci.CartId = @CartId";

        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@CartId", cartId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    items.Add(new CartItem
                    {
                        Id = reader.GetInt32("Id"),
                        Quantity = reader.GetInt32("Quantity"),
                        Product = new Product
                        {
                            Id = reader.GetInt32("ProductId"),
                            Name = reader.GetString("Name"),
                            Price = reader.GetDecimal("Price"),
                            ImagePath = reader.GetString("ImagePath"),
                            Description = reader.GetString("Description")
                        }
                    });
                }
            }
        }

        return items;
    }
}








//using MySql.Data.MySqlClient;
//using System.Collections.Generic;
//using System.Linq;

//namespace AlexShop
//{
//    public static class CartsRepository
//    {
//        private static List<Cart> carts = new List<Cart>();

//        public static Cart TryGetByUserId(string userId)
//        {
//            return carts.FirstOrDefault(x => x.UserId == userId);

//        }

//        internal static void Add(Product product, string userId)
//        {
//            var existingCart = TryGetByUserId(userId);

//            if (existingCart == null)
//            {
//                var newCart = new Cart
//                {
//                    UserId = userId,
//                    Items = new List<CartItem>()
//            {
//                new CartItem
//                {
//                    Quantity = 1,
//                    Product = product
//                }
//            }
//                };

//                // Добавляем в базу данных (Id будет присвоен автоматически)
//                CartsRepository.AddCart(newCart);
//            }
//            else
//            {
//                var existingItem = existingCart.Items.FirstOrDefault(x => x.Product.Id == product.Id);

//                if (existingItem != null)
//                {
//                    existingItem.Quantity += 1;
//                }
//                else
//                {
//                    existingCart.Items.Add(new CartItem
//                    {
//                        Quantity = 1,
//                        Product = product
//                    });
//                }

//                // Обновляем корзину в базе
//                CartsRepository.UpdateCart(existingCart);
//            }
//        }










        //internal static void Add(Product product, string userId)
        //{
        //    var existingproduct = TryGetByUserId(userId);
        //    if (existingproduct == null)

        //    {
        //        var newproduct = new Cart
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = userId,
        //            Items = new List<CartItem>()
        //            {
        //                new CartItem
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Quantity = 1,
        //                    Product = product


        //                }
        //            }
        //        };

        //        products.Add(newproduct);

        //    }
        //    else
        //    {
        //        var existingproductItem = existingproduct.Items.FirstOrDefault(x => x.Product.Id == product.Id);
        //        if (existingproduct != null)
        //        {
        //            existingproductItem.Quantity += 1;
        //        }
        //        else
        //        {
        //            existingproduct.Items.Add(new CartItem
        //            {
        //                Id = Guid.NewGuid(),
        //                Quantity = 1,
        //                Product = product
        //            }
        //            );
        //        }

        //    }
        //}
    