namespace WarnerTransitFederal_Repo;

public class OrderRepo
{
    public readonly List<Order> _orderDirectory = new List<Order>();

    public bool AddOrderToList(Order order) {
        
        int prevOrderCount = _orderDirectory.Count;
        
        _orderDirectory.Add(order);

        if (_orderDirectory.Count > prevOrderCount) {
        return true;
        }
        else {
            return false;
        }
    }

    public Order GetByOrderNum(string orderNum) {
        return _orderDirectory.Find(order => order.OrderNum == orderNum);
    }

    public List<Order> GetList() {
        return _orderDirectory;
    }

    public bool UpdateExistingOrder(string originalOrderNum, Order newOrder){
        Order orderToUpdate = GetByOrderNum(originalOrderNum);

        if (orderToUpdate != default){
            orderToUpdate.OrderDate = newOrder.OrderDate;
            orderToUpdate.DeliveryDate = newOrder.DeliveryDate;
            orderToUpdate.DeliveryStatus = newOrder.DeliveryStatus;
            orderToUpdate.ItemNumber = newOrder.ItemNumber;
            orderToUpdate.ItemQuantity = newOrder.ItemQuantity;
            orderToUpdate.CustomerID = newOrder.CustomerID;
            orderToUpdate.OrderNum = newOrder.OrderNum;
            return true;
        }else {
            return false;
        }
    }

    public bool RemoveOrderFromList(string orderNum) {
        Order orderToDelete = GetByOrderNum(orderNum);

        if (orderToDelete != default) {
            return _orderDirectory.Remove(orderToDelete);
        } else {
            return false;
        }
    }
}
