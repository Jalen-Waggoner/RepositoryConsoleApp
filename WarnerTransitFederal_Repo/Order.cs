using System;
namespace WarnerTransitFederal_Repo;
public class Order
{
    public Order(){

    }

    public Order(string orderDate, string deliveryDate, Status deliveryStatus, int itemNumber, int itemQuantity, string customerID, string orderNum) {
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        DeliveryStatus = deliveryStatus;
        ItemNumber = itemNumber;
        ItemQuantity = itemQuantity;
        CustomerID = customerID;
        OrderNum = orderNum;
    }
    
    public string OrderDate { get; set; }
    public string DeliveryDate { get; set; }
    public Status DeliveryStatus { get; set; }
    public int ItemNumber { get; set; }
    public int ItemQuantity { get; set; }
    public string CustomerID { get; set; }
    public string OrderNum { get; set; }
}

public enum Status {Scheduled, EnRoute, Complete, Canceled, Unknown};


//DateTime now = DateTime.Now;
//        return $"Order Date {now}";

/*    Random ranNum = new Random();
        return $"{CustomerID[0]}-{ranNum.Next()}";
    }*/