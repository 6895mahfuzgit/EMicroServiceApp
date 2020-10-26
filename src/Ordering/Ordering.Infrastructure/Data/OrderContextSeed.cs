using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailable = retry.Value;

            try
            {
                orderContext.Database.Migrate();

                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreconfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailable > 3)
                {
                    retryForAvailable++;

                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(ex.Message);

                    await SeedAsync(orderContext, loggerFactory, retryForAvailable);
                }
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {

            return new List<Order>
            {
                new Order() { UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "meh@ozk.com", AddressLine = "Bahcelievler", TotalPrice = 5239 },
                new Order() { UserName = "swn", FirstName = "Selim", LastName = "Arslan", EmailAddress ="sel@ars.com", AddressLine = "Ferah", TotalPrice = 3486 }
            };
        }
    }
}
