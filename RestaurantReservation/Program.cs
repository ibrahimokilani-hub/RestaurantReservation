using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Seed;

using var context = new RestaurantReservationDbContext();
DbSeeder.Seed(context);
