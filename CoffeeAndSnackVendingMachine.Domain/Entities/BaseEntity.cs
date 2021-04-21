﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeAndSnackVendingMachine.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
