﻿using Domain.Enums;

namespace Web.Models;

public class OrderEditVM
{
    public int OrderId { get; set; }

    public DateTime? OrderFulfilled { get; set; }

    public OrderStatus Status { get; set; }
}
