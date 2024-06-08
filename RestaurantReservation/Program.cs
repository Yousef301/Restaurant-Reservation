using Microsoft.EntityFrameworkCore;
using RestaurantReservation.ConsoleIO.ConsoleOutput;
using RestaurantReservation.ConsoleIO.Reader;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Helpers;

public class Program
{
    public static async Task Main(string[] args)
    {
        var connectionSettings = AppSettingsManager.GetConnectionString("ConnectionStrings",
            "RestaurantReservationDb");

        string? choice;
        do
        {
            ConsoleDisplay.MessageDisplay("Select One of the Following: ");
            Menus.PrintEntityMenu();
            ConsoleDisplay.MessageDisplay("Enter your selection: ", false);
            choice = ConsoleReader.ReadInput();
            ConsoleDisplay.ClearScreen();
            switch (choice)
            {
                case "1":
                    string? customerOperationChoice;
                    do
                    {
                        Menus.PrintCRUDMenu("Customer");
                        ConsoleDisplay.MessageDisplay("Enter your selection: ", false);
                        customerOperationChoice = ConsoleReader.ReadInput();
                        ConsoleDisplay.ClearScreen();

                        switch (customerOperationChoice)
                        {
                            case "1":
                                using (var context = new RestaurantReservationDbContext(connectionSettings))
                                {
                                    var customerRepository = new CustomerRepository(context);
                                    var customer = CreateCustomer();

                                    if (ValidatorHelper.TryValidate(customer, out var results))
                                    {
                                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                                        await customerRepository.AddCustomerAsync(customer);
                                        ConsoleDisplay.MessageDisplay("Customer added successfully.");
                                        ConsoleDisplay.ResetColor();
                                    }
                                    else
                                    {
                                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                                        ConsoleDisplay.MessageDisplay("Customer entered data is not valid:");
                                        foreach (var validationResult in results)
                                        {
                                            ConsoleDisplay.MessageDisplay($" - {validationResult.ErrorMessage}");
                                        }

                                        ConsoleDisplay.ResetColor();
                                    }
                                }

                                break;

                            case "2":
                                using (var context = new RestaurantReservationDbContext(connectionSettings))
                                {
                                    var customerRepository = new CustomerRepository(context);
                                    var customer = CreateCustomer();
                                    ConsoleDisplay.MessageDisplay("Enter customer ID: ", false);
                                    var customerId = ConsoleReader.ReadInput();
                                    customer.CustomerID = int.Parse(customerId);

                                    if (ValidatorHelper.TryValidate(customer, out var results))
                                    {
                                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                                        ConsoleDisplay.MessageDisplay(
                                            await customerRepository.UpdateCustomerAsync(customer));
                                        ConsoleDisplay.ResetColor();
                                    }
                                    else
                                    {
                                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                                        ConsoleDisplay.MessageDisplay("Customer updated info is not valid:");
                                        foreach (var validationResult in results)
                                        {
                                            ConsoleDisplay.MessageDisplay($" - {validationResult.ErrorMessage}");
                                        }

                                        ConsoleDisplay.ResetColor();
                                    }
                                }

                                break;

                            case "3":
                                using (var context = new RestaurantReservationDbContext(connectionSettings))
                                {
                                    var customerRepository = new CustomerRepository(context);
                                    ConsoleDisplay.MessageDisplay("Enter customer ID: ", false);
                                    var customerId = ConsoleReader.ReadInput();
                                    ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                                    ConsoleDisplay.MessageDisplay(
                                        await customerRepository.DeleteCustomerAsync(int.Parse(customerId)));
                                    ConsoleDisplay.ResetColor();
                                }

                                break;

                            case "4":
                                using (var context = new RestaurantReservationDbContext(connectionSettings))
                                {
                                    var customerRepository = new CustomerRepository(context);
                                    ConsoleDisplay.MessageDisplay("Enter customer ID: ", false);
                                    var customerId = ConsoleReader.ReadInput();
                                    var customer = await customerRepository.GetCustomerAsync(int.Parse(customerId));
                                    if (customer != null)
                                    {
                                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                                        ConsoleDisplay.MessageDisplay($"Customer ID: {customer.CustomerID}");
                                        ConsoleDisplay.MessageDisplay($"First Name: {customer.FirstName}");
                                        ConsoleDisplay.MessageDisplay($"Last Name: {customer.LastName}");
                                        ConsoleDisplay.MessageDisplay($"Email: {customer.Email}");
                                        ConsoleDisplay.MessageDisplay($"Phone Number: {customer.PhoneNumber}");
                                        ConsoleDisplay.ResetColor();
                                    }
                                    else
                                    {
                                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                                        ConsoleDisplay.MessageDisplay("Customer not found.");
                                        ConsoleDisplay.ResetColor();
                                    }
                                }

                                break;
                        }
                    } while (customerOperationChoice != "5");

                    break;
                case "2":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        var employeeRepository = new EmployeeRepository(context);

                        var managers = await employeeRepository.ListManagerAsync();

                        if (managers.Count == 0)
                        {
                            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                            ConsoleDisplay.MessageDisplay("No managers found.");
                            ConsoleDisplay.ResetColor();
                        }

                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                        foreach (var manager in managers)
                        {
                            ConsoleDisplay.MessageDisplay($"Manager ID: {manager.EmployeeID}");
                            ConsoleDisplay.MessageDisplay($"First Name: {manager.FirstName}");
                            ConsoleDisplay.MessageDisplay($"Last Name: {manager.LastName}");
                            ConsoleDisplay.MessageDisplay($"Position: {manager.Position}\n");
                        }

                        ConsoleDisplay.ResetColor();
                    }

                    break;
                case "3":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        var reservationRepository = new ReservationRepository(context);
                        var customerRepository = new CustomerRepository(context);

                        ConsoleDisplay.MessageDisplay("Enter customer ID: ", false);

                        var customerId = ConsoleReader.ReadInput();

                        if (!int.TryParse(customerId, out _))
                        {
                            InvalidInputMessage("Invalid customer ID.");
                            break;
                        }

                        var customer = await customerRepository.GetCustomerAsync(int.Parse(customerId));

                        if (customer != null)
                        {
                            var reservations =
                                await reservationRepository.GetReservationsByCustomerAsync(customer.CustomerID);

                            ConsoleDisplay.SetForegroundColor(ConsoleColor.DarkCyan);
                            if (reservations.Count == 0)
                            {
                                ConsoleDisplay.MessageDisplay("No reservations for found.");
                            }

                            ConsoleDisplay.ResetColor();

                            ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                            foreach (var reservation in reservations)
                            {
                                ConsoleDisplay.MessageDisplay($"Reservation ID: {reservation.ReservationID}");
                                ConsoleDisplay.MessageDisplay($"Reservation Date: {reservation.ReservationDate}");
                                ConsoleDisplay.MessageDisplay($"Party Size: {reservation.PartySize}\n");
                            }

                            ConsoleDisplay.ResetColor();
                        }
                        else
                        {
                            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                            ConsoleDisplay.MessageDisplay("Customer not found.");
                            ConsoleDisplay.ResetColor();
                        }
                    }

                    break;
                case "4":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        var reservationRepository = new ReservationRepository(context);

                        ConsoleDisplay.MessageDisplay("Enter reservation ID: ", false);

                        var reservationId = ConsoleReader.ReadInput();

                        if (!int.TryParse(reservationId, out _))
                        {
                            InvalidInputMessage("Invalid reservation ID.");
                            break;
                        }

                        var reservations = await reservationRepository.ListOrdersAndMenuItemsAsync(int.Parse(reservationId));

                        if (reservations != null)
                        {
                            ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                            foreach (var order in reservations.Orders)
                            {
                                ConsoleDisplay.MessageDisplay($"Order ID: {order.OrderID}");
                                ConsoleDisplay.MessageDisplay($"Order Date: {order.OrderDate}");
                                ConsoleDisplay.MessageDisplay($"Total Amount: {order.TotalAmount}");
                                ConsoleDisplay.MessageDisplay("Order Items:");
                                foreach (var orderItem in order.OrderItems)
                                {
                                    ConsoleDisplay.MessageDisplay($"- {orderItem.Item.Name} ({orderItem.Quantity})");
                                }

                                ConsoleDisplay.MessageDisplay("\n");
                            }

                            ConsoleDisplay.ResetColor();
                        }
                        else
                        {
                            ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
                            ConsoleDisplay.MessageDisplay("Reservation not found.");
                            ConsoleDisplay.ResetColor();
                        }
                    }

                    break;
                case "5":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        var menuItem = new MenuItemRepository(context);

                        ConsoleDisplay.MessageDisplay("Enter reservation ID: ", false);

                        var reservationId = ConsoleReader.ReadInput();

                        if (!int.TryParse(reservationId, out _))
                        {
                            InvalidInputMessage("Invalid reservation ID.");
                            break;
                        }

                        var items = await menuItem.ListOrderedMenuItemsAsync(int.Parse(reservationId));

                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                        foreach (var item in items)
                        {
                            ConsoleDisplay.MessageDisplay($"Item ID: {item.ItemID}");
                            ConsoleDisplay.MessageDisplay($"Name: {item.Name}");
                            ConsoleDisplay.MessageDisplay($"Description: {item.Description}");
                            ConsoleDisplay.MessageDisplay($"Price: {item.Price}\n");
                        }

                        ConsoleDisplay.ResetColor();
                    }

                    break;
                case "6":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        var employeeRepository = new EmployeeRepository(context);

                        ConsoleDisplay.MessageDisplay("Enter employee ID: ", false);

                        var employeeId = ConsoleReader.ReadInput();

                        if (!int.TryParse(employeeId, out _))
                        {
                            InvalidInputMessage("Invalid employee ID.");
                            break;
                        }

                        var employee = await employeeRepository.GetEmployeeAsync(int.Parse(employeeId));
                        var averageOrderAmount =
                            await employeeRepository.CalculateAverageOrderAmountAsync(int.Parse(employeeId));

                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                        ConsoleDisplay.MessageDisplay(
                            $"Average Order Amount for {employee.FirstName} {employee.LastName} is: {averageOrderAmount}\n");
                        ConsoleDisplay.ResetColor();
                    }

                    break;
                case "7":
                    string? viewOperationChoice;
                    do
                    {
                        Menus.PrintViewsMenu();
                        ConsoleDisplay.MessageDisplay("Enter your selection: ", false);
                        viewOperationChoice = ConsoleReader.ReadInput();
                        ConsoleDisplay.ClearScreen();

                        switch (viewOperationChoice)
                        {
                            case "1":
                                ConsoleDisplay.ClearScreen();
                                using (var context = new RestaurantReservationDbContext(connectionSettings))
                                {
                                    var viewsRepository = new ViewsRepository(context);

                                    var reservations = await viewsRepository.GetReservationsDetailsAsync();

                                    ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                                    foreach (var reservation in reservations)
                                    {
                                        ConsoleDisplay.MessageDisplay($"Reservation ID: {reservation.ReservationID}");
                                        ConsoleDisplay.MessageDisplay($"Customer Name: {reservation.CustomerName}");
                                        ConsoleDisplay.MessageDisplay(
                                            $"Restaurant Name: {reservation.RestaurantName}\n");
                                    }

                                    ConsoleDisplay.ResetColor();
                                }

                                break;
                            case "2":
                                ConsoleDisplay.ClearScreen();
                                using (var context = new RestaurantReservationDbContext(connectionSettings))
                                {
                                    var viewsRepository = new ViewsRepository(context);

                                    var employeeDetails = await viewsRepository.GetEmployeesDetailsAsync();

                                    ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                                    foreach (var employeeDetail in employeeDetails)
                                    {
                                        ConsoleDisplay.MessageDisplay($"Employee Name: {employeeDetail.EmployeeName}");
                                        ConsoleDisplay.MessageDisplay(
                                            $"Restaurant Name: {employeeDetail.RestaurantName}");
                                        ConsoleDisplay.MessageDisplay(
                                            $"Restaurant Address: {employeeDetail.RestaurantAddress}");
                                        ConsoleDisplay.MessageDisplay($"Position: {employeeDetail.Position}\n");
                                    }

                                    ConsoleDisplay.ResetColor();
                                }

                                break;
                            case "3":
                                break;
                        }
                    } while (viewOperationChoice != "3");

                    break;
                case "8":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        ConsoleDisplay.MessageDisplay("Enter restaurant ID: ", false);

                        var restaurantId = ConsoleReader.ReadInput();

                        if (!int.TryParse(restaurantId, out _))
                        {
                            InvalidInputMessage("Invalid restaurant ID.");
                            break;
                        }

                        var results = context.Restaurants.Where(r => r.RestaurantID == int.Parse(restaurantId))
                            .Select(r => context.GetTotalRevenueByRestaurantId(r.RestaurantID))
                            .FirstOrDefaultAsync();

                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                        ConsoleDisplay.MessageDisplay($"Total Restaurant Revenue: {results.Result}");
                        ConsoleDisplay.ResetColor();
                    }

                    break;
                case "9":
                    using (var context = new RestaurantReservationDbContext(connectionSettings))
                    {
                        var customerRepository = new CustomerRepository(context);

                        ConsoleDisplay.MessageDisplay("Enter party size: ", false);

                        var partySize = ConsoleReader.ReadInput();

                        if (!int.TryParse(partySize, out _))
                        {
                            InvalidInputMessage("Invalid party size.");
                            break;
                        }

                        var results = await customerRepository.GetCustomersByPartySizeAsync(int.Parse(partySize));

                        ConsoleDisplay.SetForegroundColor(ConsoleColor.Cyan);
                        foreach (var customer in results)
                        {
                            ConsoleDisplay.MessageDisplay($"Customer ID: {customer.CustomerID}");
                            ConsoleDisplay.MessageDisplay($"Full Name: {customer.FirstName} {customer.LastName}");
                            ConsoleDisplay.MessageDisplay($"Email: {customer.Email}");
                            ConsoleDisplay.MessageDisplay($"Phone Number: {customer.PhoneNumber}\n");
                        }

                        ConsoleDisplay.ResetColor();
                    }

                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        } while (choice != "10");
    }

    private static Customer CreateCustomer()
    {
        ConsoleDisplay.MessageDisplay("Enter customer first name: ", false);
        var firstName = ConsoleReader.ReadInput();
        ConsoleDisplay.MessageDisplay("Enter customer last name: ", false);
        var lastName = ConsoleReader.ReadInput();
        ConsoleDisplay.MessageDisplay("Enter customer email: ", false);
        var email = ConsoleReader.ReadInput();
        ConsoleDisplay.MessageDisplay("Enter customer phone number: ", false);
        var phoneNumber = ConsoleReader.ReadInput();
        return new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber
        };
    }

    private static void InvalidInputMessage(string message)
    {
        ConsoleDisplay.SetForegroundColor(ConsoleColor.Red);
        ConsoleDisplay.MessageDisplay(message);
        ConsoleDisplay.ResetColor();
    }
}