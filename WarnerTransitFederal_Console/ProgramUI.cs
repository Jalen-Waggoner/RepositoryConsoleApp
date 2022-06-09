using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;
using System;
using WarnerTransitFederal_Repo;

    public class ProgramUI
    {
        public readonly OrderRepo _repo = new OrderRepo();

        public void Run(){
            Seed();
            RunMenu();
        }

        public void RunMenu() {
            bool continueToRun = true;

            do {
                Console.Clear();

                Console.WriteLine("Welcome to the Warner Transit Federal Order Repository! Please select from one of the following options to continue:\n" +
                "1. Add New Order\n" +
                "2. Get Order by Order#\n" +
                "3. Get List of All Orders\n" +
                "4. Update Order\n" +
                "5. Delete Order\n" +
                "6. Exit");

            string selection = Console.ReadLine();
            switch(selection) {
                case "1":
                    AddNewOrder();
                    break;
                case "2":
                    GetOneOrder();
                    break;
                case "3":
                    GetAllOrders();
                    break;
                case "4":
                    UpdateExistingOrder();
                    break;
                case "5":
                DeleteOneOrder();
                    break;
                case "6":
                continueToRun = false;
                    break;
                default:
                    Console.WriteLine("Please choose a valid option");
                    WaitForKey();
                    break;
            }
            }while (continueToRun);
        }

        public void AddNewOrder(){
            Console.Clear();
            
            Order newOrder = new Order();
            
            Console.WriteLine("Please enter a customer ID:");
            newOrder.CustomerID = Console.ReadLine();

            Console.WriteLine("Please enter a new Order#:");
            newOrder.OrderNum = Console.ReadLine();
            
            Console.WriteLine("Please enter an order date:");
            newOrder.OrderDate = Console.ReadLine();
            
            Console.WriteLine("Please enter a delivery date:");
            newOrder.DeliveryDate = Console.ReadLine();
            
            Console.WriteLine("Please enter the number corresponding to a delivery status from the following options:\n" +
            "1. Scheduled\n" + 
            "2. EnRoute\n" + 
            "3. Complete\n" + 
            "4. Cancelled\n");
            newOrder.DeliveryStatus = Console.ReadLine() switch {
                "1" => Status.Scheduled,
                "2" => Status.EnRoute,
                "3" => Status.Complete,
                "4" => Status.Canceled,
                _ => Status.Unknown,
            };
            
            Console.WriteLine("Please enter an item number:");
            newOrder.ItemNumber = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Please enter an item quantity:");
            newOrder.ItemQuantity = Convert.ToInt32(Console.ReadLine());
            

            if (_repo.AddOrderToList(newOrder)){
                System.Console.WriteLine($"Order #{newOrder.OrderNum} has been added to the repository.");
                WaitForKey();
            }else {
                Console.WriteLine("Issue adding order. Please try again.");
                WaitForKey();
            }
        }

        public void GetOneOrder(){
            Console.Clear();
            Console.WriteLine("Select Order by order#. Otherwise select option '3' in menu for list of all orders.");
            string orderNum = Console.ReadLine();

            Order item = _repo.GetByOrderNum(orderNum);

            if (item != default){
                Console.WriteLine($"------- Order #{item.OrderNum} -------\n" +
                $"Ordered by: {item.CustomerID}\n" +
                $"Order Status: {item.DeliveryStatus}\n" +
                $"Date Ordered: {item.OrderDate}\n" +
                $"Delivery Date: {item.DeliveryDate}\n" +
                $"Item Number: {item.ItemNumber}\n" +
                $"Item Quantity: {item.ItemQuantity}\n"
                );
            }else{
                Console.WriteLine("Order not found. Double check order#.");
            }
            WaitForKey();
        }

        public void GetAllOrders(){
            List<Order> orderList = _repo.GetList();

            if(orderList.Count < 1){
                System.Console.WriteLine("No orders found. Try adding new orders via option '1' from menu");
            }else{

            foreach (Order order in orderList){
                System.Console.WriteLine($"------- Order #{order.OrderNum} -------\n" +
                $"Ordered by: {order.CustomerID}\n" +
                $"Order Status: {order.DeliveryStatus}\n" +
                $"Date Ordered: {order.OrderDate}\n" +
                $"Delivery Date: {order.DeliveryDate}\n" +
                $"Item Number: {order.ItemNumber}\n" +
                $"Item Quantity: {order.ItemQuantity}\n"
                );
            }
            }
            
            WaitForKey();
        }
        
        private void UpdateExistingOrder(){
            Console.Clear();
            Console.WriteLine("Input the order # for the order that you would like to update:");
            string orderNum = Console.ReadLine();

            Order updatedOrder = new Order();

            Console.WriteLine("Please enter a new Order#:");
            updatedOrder.OrderNum = Console.ReadLine();

            Console.WriteLine("Please enter a new Customer ID:");
            updatedOrder.CustomerID = Console.ReadLine();

            Console.WriteLine("Please enter a new Order Date:");
            updatedOrder.OrderDate = Console.ReadLine();

            Console.WriteLine("Please enter a new Delivery Date:");
            updatedOrder.DeliveryDate = Console.ReadLine();

            Console.WriteLine("Please enter the number corresponding to a delivery status from the following options:\n" +
            "1. Scheduled\n" + 
            "2. EnRoute\n" + 
            "3. Complete\n" + 
            "4. Cancelled\n");
            updatedOrder.DeliveryStatus = Console.ReadLine() switch {
                "1" => Status.Scheduled,
                "2" => Status.EnRoute,
                "3" => Status.Complete,
                "4" => Status.Canceled,
                _ => Status.Unknown,
            };

            Console.WriteLine("Please enter a new Item Number:");
            updatedOrder.ItemNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter a new Item Quantity:");
            updatedOrder.ItemQuantity = Convert.ToInt32(Console.ReadLine());

            if (_repo.UpdateExistingOrder(orderNum, updatedOrder))
        {
            Console.Clear();
            System.Console.WriteLine($"Order {updatedOrder.OrderNum} has updated.");
        }
        else
        {
            Console.Clear();
            System.Console.WriteLine($"{orderNum} could not be found.");
        }

        WaitForKey();
        }

        private void DeleteOneOrder(){
            Console.WriteLine("Select order to delete via order#.");
            string orderNum = Console.ReadLine();

            bool isSuccess = _repo.RemoveOrderFromList(orderNum);

            if (isSuccess){
                Console.WriteLine($"Order #{orderNum} has been deleted.");
            }else{
                Console.WriteLine("Delete unsuccessful. Make sure input is the correct order number.");
            }

            WaitForKey();
        }

        private void WaitForKey(){
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private void Seed() {
            Order coffeeMugs = new Order("6/8/2022", "6/10/2022", Status.Scheduled, 12, 24, "48", "96");
            Order pencilCases = new Order("8/6/2022", "8/12/2022", Status.EnRoute, 3, 6, "12", "24");

            _repo.AddOrderToList(coffeeMugs);
            _repo.AddOrderToList(pencilCases);
        }
    }
