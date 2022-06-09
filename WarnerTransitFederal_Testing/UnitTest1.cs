using System.Reflection;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace WarnerTransitFederal_Testing;
using WarnerTransitFederal_Repo;

public class UnitTest1
{
    [Fact]
    public void IsAddOrderToListSuccess()
    {
        Order coffeeMugs = new Order("6/8/2022", "6/10/2022", Status.Scheduled, 12, 24, "48", "96");
        OrderRepo repo = new OrderRepo();

        bool isAddOrderSuccess = repo.AddOrderToList(coffeeMugs);

        Assert.True(isAddOrderSuccess);
    }

    /*[Fact]
    public void RemoveOrderFromListSuccess()
    {
        Order coffeeMugs = new Order("6/8/2022", "6/10/2022", Status.Scheduled, 12, 24, "48", "96");
        OrderRepo repo = new OrderRepo();

        repo.AddOrderToList(coffeeMugs);

        bool expected = true;
        bool actual = repo.RemoveOrderFromList("96");

        Assert.Equal(expected, actual);
    }*/
}