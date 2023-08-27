using AppLogic.Events;
using OrderManagement.AppLogic.Events;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.AppLogic
{
    public class OrderService: IOrderService //nog zien hoe me repos best te werken meerdere aanmaken of nie? er moet enkel georderd worden in principe mess een menu service voor het menu te laten zien maar nog bespreken want idk
    {
        private IOrderRepository _orderRepository;
        private ICocktailMenuRepository _cocktailMenuRepositroy;
        private IMenuItemRepository _menuItemRepository;
        private IOrderItemRepository _orderItemRepository;
        private ICocktailRepository _cocktailRepository;
        IEventBus _eventBus; //voor de nabije

        public OrderService(IOrderRepository orderRepository, ICocktailMenuRepository cocktailMenuRepositroy, IMenuItemRepository menuItemRepository, IEventBus eventBus, IOrderItemRepository orderItemRepository, ICocktailRepository cocktailRepository)
        {
            _orderRepository = orderRepository;
            _cocktailMenuRepositroy = cocktailMenuRepositroy;
            _menuItemRepository = menuItemRepository;
            _eventBus = eventBus;
            _orderItemRepository = orderItemRepository;
            _cocktailRepository = cocktailRepository;
        }
        //komende service implementaties

        public async Task<Order> AddCocktailToOrderAsync(Guid orderId, Guid menuItemId)
        {
            Order order = await _orderRepository.GetById(orderId);
            MenuItem menuItem = await _menuItemRepository.GetById(menuItemId);
            //OrderItem orderItem = new OrderItem
            //order.AddCocktail(menuItem);


            bool found = false;
            foreach (OrderItem item in order.OrderItems)
            {
                if (item.Cocktail.SerialNumber == menuItem.Cocktail.SerialNumber)
                {
                    found = true;
                    item.Amount += 1;
                    await _orderItemRepository.CommitTrackedChangesAsync();
                    break;
                }
            }

            if (!found)
            {
                OrderItem orderItem = new OrderItem(1, menuItem);
                await _orderItemRepository.addAsync(orderItem);
                order.AddCocktail(orderItem);
            }

            order.TotalPrice += menuItem.Price;
            order.TotalPrice = Math.Round(order.TotalPrice, 2);
            await _orderRepository.CommitTrackedChangesAsync(); //controle op fouten toevoegen
            return order;
        }

        public async Task<Order> DeleteCocktailFromOrderAsync(Guid orderId, string cocktailSerialNumber, double price)
        {
            Order order = await _orderRepository.GetById(orderId);
            //MenuItem menuItem = await _menuItemRepository.GetById(menuItemId);
            order.DeleteCocktail(cocktailSerialNumber);
            order.TotalPrice -= price;
            order.TotalPrice = Math.Round(order.TotalPrice, 2);
            await _orderRepository.CommitTrackedChangesAsync();
            return order;
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            Order order = await _orderRepository.GetById(orderId);
            await _orderRepository.DeleteOrder(order);
        }

        public async Task<Order> FinelizeOrder(Guid orderId, int tableNr)
        {

            //aantal cocktails doorgeven naar java
            //om de beurt of in 1 keer? ik doe omdebeurt nu

            Order order = await _orderRepository.GetById(orderId);
            order.Payed = true;
            order.Table = tableNr;

            foreach (var item in order.OrderItems)
            {
                //aantal cocktails doorgeven naar java
                //om de beurt of in 1 keer? ik doe omdebeurt nu
                var @event = new CocktailsSoldIntegrationEvent
                {
                    BarId = order.BarId.ToString(),
                    CocktailBarcode = item.Cocktail.SerialNumber.ToString(),
                    Aantal = item.Amount.ToString()    
                };
                _eventBus.Publish(@event);

            }

            


            if (order.CustomerId != null)
            {
                order.TotalPrice *= 0.90;
            }

            order.TotalPrice = Math.Round(order.TotalPrice, 2);

            await _orderRepository.CommitTrackedChangesAsync();
            return order;
        }

        public Task<List<Order>> GetOrderHistory(Guid customerId)
        {
            //async??
            return _orderRepository.getOrderHistory(customerId);  //check in de repo doen op de paied bool?

        }

        public async Task<Order> InitializeNewOrderAsync(string customerId, string barId)
        {
            Order newOrder;
            if (customerId == "none")//customerId == null || customerId == string.Empty || customerId == ""
            {
                newOrder = new Order(barId);
            }
            else
            {
                newOrder = new Order(customerId, barId);

            }
            await _orderRepository.AddAsync(newOrder);
            return newOrder; 

        }

    }
}
