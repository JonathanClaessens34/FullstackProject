using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrderManagement.AppLogic;
using OrderManagement.Domain;

namespace OrderManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        //algemene controller voor orders aan te maken en draken enzo aan order toe te voegen

        //QUESTION: zijn dit ze alle?
        //QUESTION: word tafel er automatisch aan toegevoegd of nie of was de bedoeling van tafel?
        //Endpoints:

        //Initiate new order [POST] --
        //add item to order [UPDATE] --
        //delete item from order [DELETE] --
        //Cancle order [Delete] --
        //Finelize order [POST] --
        //Order Histroy (via user) [GET] --
        //Order status?

        IOrderRepository _orderRepository;
        IOrderService _orderService;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }


        [HttpPost("/new")]
        public async Task<IActionResult> InitializeNewOrder(string customerId, string barId)
        {
            Order newOrder = await _orderService.InitializeNewOrderAsync(customerId, barId);
            return Ok(newOrder);//CreatedAtAction(newOrder);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> AddCocktailToOrder(Guid orderId, Guid menuItemId)//mess naar order veranderen ma atm dit hangt af van werking maui
        {
            Order updatedOrder = await _orderService.AddCocktailToOrderAsync(orderId, menuItemId);
            return Ok(updatedOrder);
        }

        [HttpDelete("{orderId}/{cocktailSerialNumber}")]
        public async Task<IActionResult> DeleteCocktailFromOrder(Guid orderId, string cocktailSerialNumber, double price)//mess naar order veranderen ma atm dit hangt af van werking maui
        {
            Order updatedOrder = await _orderService.DeleteCocktailFromOrderAsync(orderId, cocktailSerialNumber, price);
            return Ok(updatedOrder);
        }

        [HttpDelete("deleteOrder/{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)//mess naar order veranderen ma atm dit hangt af van werking maui
        {
            await _orderService.DeleteOrderAsync(orderId);
            return Ok();
        }

        [HttpPost("/pay")]//was path variabele hem naar body gedaan maar maybe pat toch beter idk
        public async Task<IActionResult> FinelizeOrder(Guid orderId, int tableNr)//mess naar order veranderen ma atm dit hangt af van werking maui
        {
           
            //zet bestelling
            //betaling??
            await _orderService.FinelizeOrder(orderId, tableNr);
            return Ok();//CreatedAtAction(newOrder); //moet Id teruggeven
        }

        [HttpGet("/history/{customerId}")]
        public async Task<IActionResult> GetOrderHistory(Guid customerId)//mess customer id hangt af van maui
        {
            List<Order> allOrders = await _orderService.GetOrderHistory(customerId);
            return Ok(allOrders);//CreatedAtAction(newOrder); //moet Id teruggeven
        }





    }

}
