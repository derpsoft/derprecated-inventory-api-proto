﻿namespace BausCode.Api.Models.Routing
{
    using ServiceStack;

    [Route("/api/v1/orders", "POST")]
    public class CreateOrder : IReturn<CreateOrderResponse>
    {
    }
}
