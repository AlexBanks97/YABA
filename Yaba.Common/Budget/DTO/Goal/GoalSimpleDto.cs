﻿using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget.DTO.Goal
{
    public class GoalSimpleDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

        public Recurrence Recurrence { get; set; }

    }
}
