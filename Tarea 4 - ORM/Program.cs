using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_4___ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            Northwind2Entities DataContext = new Northwind2Entities();
            int opc = 0, continuar;

            Console.WriteLine("¡¡¡¡BIENVENIDO!!!!");

            do
            {
                do
                {
                    Console.WriteLine("Escoga una opción para manejar la base de datos Northwind del 1 al 6");
                    opc = int.Parse(Console.ReadLine());
                } while (opc<1 || opc>6);
                
                switch (opc)
                {
                    case 1:
                        //1. SELECT FROM TABLE AS (Proyección de datos con lambda o sin lambda)
                        //select CompanyName as Compañia, Addres as Direccion, City as Ciudad, Phone as Tel, Fax from Suppliers
                        var multiple = (from sup in DataContext.Suppliers
                                        select new
                                        {
                                            Compañia = sup.CompanyName,
                                            Direccion = sup.Address,
                                            Ciudad = sup.City,
                                            Tel = sup.Phone,
                                            Fax = sup.Fax
                                        }); ;
                        foreach (var list in multiple)
                        {
                            Console.WriteLine("{0}, {1}, {2}, {3}, {4}", list.Compañia, list.Direccion, list.Ciudad, list.Tel, list.Fax);
                            Console.WriteLine("**********************************************************");
                        }
                        break;

                    case 2:
                        //2. Where FROM TABLE
                        //Select * from Customers where City='London'
                        var myCustom = from customers in DataContext.Customers where customers.City == "London" select customers;

                        foreach (var list in myCustom)
                        {
                            Console.WriteLine("{0}, {1}, {2}", list.CustomerID, list.ContactName, list.Phone);
                            Console.WriteLine("**********************************************************");
                        }
                        break;

                    case 3:
                        //3. GROUP BY FIELD
                        //select * from Employees group by Country
                        var groupEmployees = from emp in DataContext.Employees group emp by emp.Country;

                        foreach (var list in groupEmployees)
                        {
                            Console.WriteLine();
                            Console.WriteLine(list.Key);
                            foreach (var employees in list)
                            {
                                Console.WriteLine("{0}, {1}, {2}", employees.FirstName, employees.LastName, employees.EmployeeID);
                                Console.WriteLine("**********************************************************");
                            }
                        }
                        break;

                    case 4:
                        //4. ORDER BY FIELD
                        //select * from [Order Details] orderby UnitPrice DESC
                        var sorting = from prices in DataContext.Order_Details orderby prices.UnitPrice descending select prices;
                        foreach (var list in sorting)
                        {
                            Console.WriteLine("{0}, {1}, {2}", list.OrderID, list.UnitPrice, list.Quantity);
                            Console.WriteLine("**********************************************************");
                        }
                        break;

                    case 5:
                        //5. INSERT INTO TABLE
                        //insert into Shippers values('United Flights', '(503) 555-8435');
                        Shippers ship = new Shippers();
                        ship.CompanyName = "Super Shipper";
                        ship.Phone = "(503) 555-4315";

                        DataContext.Shippers.Add(ship);
                        DataContext.SaveChanges();
                        Console.WriteLine("Nuevo Elemento en Shippers");
                        foreach (var list in DataContext.Shippers)
                        {
                            Console.WriteLine("{0}, {1}, {2}", list.CompanyName, list.ShipperID, list.Phone);
                            Console.WriteLine("**********************************************************");
                        }
                        break;

                    case 6:
                        //6. DELETE FROM TABLE
                        //delete from [Order Details] where ProductID=1
                        var borrar = DataContext.Order_Details.Where(b => b.ProductID == 1).First();
                        if (borrar != null)
                        {
                            DataContext.Order_Details.Remove(borrar);
                            DataContext.SaveChanges();
                            Console.WriteLine("Se borró exxitosamente");
                        }
                        else
                        {
                            Console.WriteLine("No se encontró la fila a borrar");
                        }

                        foreach (var list in DataContext.Order_Details)
                        {
                            Console.WriteLine("{0}, {1}, {2}, {3}", list.OrderID, list.ProductID, list.UnitPrice, list.Quantity);
                            Console.WriteLine("**********************************************************");
                        }
                        break;
                }

                do
                {
                    Console.WriteLine("Desea Continuar? (1=si, 0=no");
                    continuar = int.Parse(Console.ReadLine());
                } while (continuar!=1 && continuar!=0);

                Console.Clear();
            } while (continuar == 1);

            Console.WriteLine("Muchas Gracias!!");
        }
    }
}
