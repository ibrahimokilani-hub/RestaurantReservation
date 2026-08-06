using RestaurantReservation.Db.Context;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Seed;
using RestaurantReservation.Services;

using (var seedContext = new RestaurantReservationDbContext())
{
    DbSeeder.Seed(seedContext);
}

using var context = new RestaurantReservationDbContext();

IEmployeeRepository employeeRepository = new EmployeeRepository(context);
IEmployeeService service = new EmployeeService(employeeRepository);

var managers = await service.ListAllManagersAsync();

managers.ForEach(e => Console.WriteLine(e.FirstName + " " + e.LastName));

System.Console.WriteLine("------------------------------------------------");
System.Console.WriteLine("------------------------------------------------");
System.Console.WriteLine();

IReservationRepository reservationRepository = new ReservationRepository(context);

var reservations = await reservationRepository.GetReservationsByCustomer(1);

reservations.ForEach(r => Console.WriteLine($"id: {r.ReservationId}, customer name: {r.Customer.FirstName} {r.Customer.LastName}, Date: {r.ReservationDate}"));