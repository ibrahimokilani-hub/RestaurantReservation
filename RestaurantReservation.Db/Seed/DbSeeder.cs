using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Seed;

public static class DbSeeder
{
    public static void Seed(RestaurantReservationDbContext context)
    {
        context.Database.Migrate();

        if (context.Restaurants.Any())
            return;

        var customers = BuildCustomers();
        var restaurants = BuildRestaurants();

        context.Customers.AddRange(customers);
        context.Restaurants.AddRange(restaurants);

        var reservations = BuildReservations(restaurants, customers);
        context.Reservations.AddRange(reservations);

        var orders = BuildOrders(reservations, restaurants);
        context.Orders.AddRange(orders);

        context.OrderItems.AddRange(BuildOrderItems(orders, restaurants));

        context.SaveChanges();
    }

    private static List<Customer> BuildCustomers() =>
    [
        new Customer { FirstName = "Alice",   LastName = "Nguyen",   Email = "alice.nguyen@example.com",   PhoneNumber = "555-0101" },
        new Customer { FirstName = "Brian",   LastName = "Osei",     Email = "brian.osei@example.com",     PhoneNumber = "555-0102" },
        new Customer { FirstName = "Carla",   LastName = "Mendez",   Email = "carla.mendez@example.com",   PhoneNumber = "555-0103" },
        new Customer { FirstName = "Dean",    LastName = "Whitfield",Email = "dean.whitfield@example.com", PhoneNumber = "555-0104" },
        new Customer { FirstName = "Elif",    LastName = "Kaya",     Email = "elif.kaya@example.com",      PhoneNumber = "555-0105" },
        new Customer { FirstName = "Farid",   LastName = "Haidari",  Email = "farid.haidari@example.com",  PhoneNumber = "555-0106" },
        new Customer { FirstName = "Grace",   LastName = "Liu",      Email = "grace.liu@example.com",      PhoneNumber = "555-0107" },
        new Customer { FirstName = "Hassan",  LastName = "Amara",    Email = "hassan.amara@example.com",   PhoneNumber = "555-0108" },
    ];

    private static List<Restaurant> BuildRestaurants()
    {
        var restaurants = new List<Restaurant>
        {
            new()
            {
                Name = "Trattoria Verde", Address = "12 Olive St, Springfield", PhoneNumber = "555-0201", OpeningHours = "Mon-Sun 11:00-22:00",
                Tables = [ new() { Capacity = 2 }, new() { Capacity = 4 }, new() { Capacity = 6 } ],
                Employees =
                [
                    new() { FirstName = "Marco",  LastName = "Rossi",   Position = "Manager" },
                    new() { FirstName = "Giulia",  LastName = "Bruno",  Position = "Chef" },
                    new() { FirstName = "Paolo",  LastName = "Conti",   Position = "Waiter" },
                ],
                MenuItems =
                [
                    new() { Name = "Margherita Pizza", Description = "Tomato, mozzarella, basil", Price = 12.50m },
                    new() { Name = "Spaghetti Carbonara", Description = "Egg, pancetta, pecorino", Price = 14.00m },
                    new() { Name = "Tiramisu", Description = "Espresso-soaked ladyfingers", Price = 7.50m },
                    new() { Name = "Bruschetta", Description = "Grilled bread, tomato, garlic", Price = 6.00m },
                ],
            },
            new()
            {
                Name = "Sakura Sushi Bar", Address = "88 Maple Ave, Rivertown", PhoneNumber = "555-0202", OpeningHours = "Tue-Sun 12:00-21:30",
                Tables = [ new() { Capacity = 2 }, new() { Capacity = 2 }, new() { Capacity = 4 } ],
                Employees =
                [
                    new() { FirstName = "Kenji",  LastName = "Sato",   Position = "Manager" },
                    new() { FirstName = "Yuki",   LastName = "Tanaka", Position = "Chef" },
                    new() { FirstName = "Haruto", LastName = "Ito",    Position = "Waiter" },
                ],
                MenuItems =
                [
                    new() { Name = "Salmon Nigiri (2pc)", Description = "Fresh salmon over rice", Price = 6.50m },
                    new() { Name = "California Roll", Description = "Crab, avocado, cucumber", Price = 8.00m },
                    new() { Name = "Miso Soup", Description = "Tofu, seaweed, scallion", Price = 3.50m },
                    new() { Name = "Green Tea Ice Cream", Description = "Matcha soft serve", Price = 5.00m },
                ],
            },
            new()
            {
                Name = "El Fogon Grill", Address = "410 Sunset Blvd, Laketown", PhoneNumber = "555-0203", OpeningHours = "Mon-Sat 10:00-23:00",
                Tables = [ new() { Capacity = 4 }, new() { Capacity = 4 }, new() { Capacity = 8 } ],
                Employees =
                [
                    new() { FirstName = "Diego",  LastName = "Flores", Position = "Manager" },
                    new() { FirstName = "Sofia",  LastName = "Reyes",  Position = "Chef" },
                    new() { FirstName = "Mateo",  LastName = "Vega",   Position = "Waiter" },
                ],
                MenuItems =
                [
                    new() { Name = "Carne Asada", Description = "Grilled steak, chimichurri", Price = 16.00m },
                    new() { Name = "Street Tacos (3)", Description = "Corn tortilla, onion, cilantro", Price = 9.50m },
                    new() { Name = "Elote", Description = "Grilled corn, cotija, chili", Price = 5.50m },
                    new() { Name = "Churros", Description = "Cinnamon sugar, chocolate sauce", Price = 6.50m },
                ],
            },
            new()
            {
                Name = "The Copper Kettle", Address = "5 Baker St, Elmsworth", PhoneNumber = "555-0204", OpeningHours = "Mon-Sun 08:00-20:00",
                Tables = [ new() { Capacity = 2 }, new() { Capacity = 4 }, new() { Capacity = 4 } ],
                Employees =
                [
                    new() { FirstName = "Oliver", LastName = "Hughes", Position = "Manager" },
                    new() { FirstName = "Emma",   LastName = "Clarke", Position = "Chef" },
                    new() { FirstName = "Jack",   LastName = "Baker",  Position = "Waiter" },
                ],
                MenuItems =
                [
                    new() { Name = "Fish and Chips", Description = "Beer-battered cod, mushy peas", Price = 13.00m },
                    new() { Name = "Shepherd's Pie", Description = "Lamb, mash, root veg", Price = 12.00m },
                    new() { Name = "Scones with Clotted Cream", Description = "Served with jam", Price = 5.00m },
                    new() { Name = "English Breakfast Tea", Description = "Pot for two", Price = 3.00m },
                ],
            },
            new()
            {
                Name = "Spice Route", Address = "77 Curry Lane, Northgate", PhoneNumber = "555-0205", OpeningHours = "Mon-Sun 11:30-22:30",
                Tables = [ new() { Capacity = 2 }, new() { Capacity = 6 }, new() { Capacity = 6 } ],
                Employees =
                [
                    new() { FirstName = "Aarav",  LastName = "Sharma", Position = "Manager" },
                    new() { FirstName = "Priya",  LastName = "Nair",   Position = "Chef" },
                    new() { FirstName = "Rohan",  LastName = "Gupta",  Position = "Waiter" },
                ],
                MenuItems =
                [
                    new() { Name = "Butter Chicken", Description = "Tomato cream sauce, basmati", Price = 14.50m },
                    new() { Name = "Paneer Tikka", Description = "Grilled marinated paneer", Price = 11.00m },
                    new() { Name = "Garlic Naan", Description = "Tandoor-baked flatbread", Price = 3.50m },
                    new() { Name = "Mango Lassi", Description = "Yogurt, mango, cardamom", Price = 4.50m },
                ],
            },
        };

        return restaurants;
    }

    private static List<Reservation> BuildReservations(List<Restaurant> restaurants, List<Customer> customers)
    {
        var reservations = new List<Reservation>();
        var baseDate = new DateTime(2026, 8, 10, 18, 0, 0);

        for (var i = 0; i < restaurants.Count; i++)
        {
            var restaurant = restaurants[i];
            var tables = restaurant.Tables.ToList();

            for (var t = 0; t < tables.Count; t++)
            {
                var table = tables[t];
                var customer = customers[(i * 2 + t) % customers.Count];

                reservations.Add(new Reservation
                {
                    Restaurant = restaurant,
                    Table = table,
                    Customer = customer,
                    ReservationDate = baseDate.AddDays(i).AddHours(t),
                    PartySize = Math.Min(table.Capacity, 2 + t),
                });
            }
        }

        return reservations;
    }

    private static List<Order> BuildOrders(List<Reservation> reservations, List<Restaurant> restaurants)
    {
        var orders = new List<Order>();

        // One order per restaurant's first reservation, served by that restaurant's waiter.
        foreach (var restaurant in restaurants)
        {
            var reservation = reservations.First(r => r.Restaurant == restaurant);
            var waiter = restaurant.Employees.First(e => e.Position == "Waiter");

            orders.Add(new Order
            {
                Reservation = reservation,
                Employee = waiter,
                OrderDate = reservation.ReservationDate.AddMinutes(15),
                TotalAmount = 0m, // computed in BuildOrderItems
            });
        }

        return orders;
    }

    private static List<OrderItem> BuildOrderItems(List<Order> orders, List<Restaurant> restaurants)
    {
        var orderItems = new List<OrderItem>();

        foreach (var order in orders)
        {
            var restaurant = restaurants.First(r => r.Employees.Contains(order.Employee));
            var menuItems = restaurant.MenuItems.ToList();

            var picks = new[]
            {
                (Item: menuItems[0], Quantity: 2),
                (Item: menuItems[1], Quantity: 1),
                (Item: menuItems[2], Quantity: 1),
            };

            foreach (var (item, quantity) in picks)
            {
                orderItems.Add(new OrderItem
                {
                    Order = order,
                    MenuItem = item,
                    Quantity = quantity,
                });
            }

            order.TotalAmount = picks.Sum(p => p.Item.Price * p.Quantity);
        }

        return orderItems;
    }
}
