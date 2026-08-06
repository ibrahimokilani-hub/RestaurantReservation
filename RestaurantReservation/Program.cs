using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Seed;
using RestaurantReservation.Services;

using (var seedContext = new RestaurantReservationDbContext())
{
    DbSeeder.Seed(seedContext);
}

using var context = new RestaurantReservationDbContext();

IEmployeeRepository repository = new EmployeeRepository(context);
IEmployeeService service = new EmployeeService(repository);

var managers = await service.ListAllManagersAsync();

managers.ForEach(e => Console.WriteLine(e.FirstName + " " + e.LastName));