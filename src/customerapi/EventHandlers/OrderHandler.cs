using EasyNetQ;
using messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace customerapi.EventHandlers
{
    public class OrderHandler
    {
        public static IBus bus;
        public static void CheckCustomerCredit(OrderCreatedMessage orderCreatedMessage)
        {
            //IBus bus = RabbitHutch.CreateBus(Configuration["MQ:Con"]);
            if (orderCreatedMessage.CustomerCode == "C0001")
            {
                bus.PubSub.Publish<CustomerOrderCreditAvailableMessage>(
                    new CustomerOrderCreditAvailableMessage { OrderId = orderCreatedMessage.OrderId }
                    , "Order.CreditAvailable"
                );
            }
            else
            {
                bus.PubSub.Publish<CustomerOrderCreditNotAvailableMessage>(
                    new CustomerOrderCreditNotAvailableMessage { OrderId = orderCreatedMessage.OrderId }
                    , "Order.CreditNotAvailable"
                );
            }
        }
    }
}
