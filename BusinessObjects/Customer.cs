using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class Customer : User
    {
        [Range(0, int.MaxValue)]
        public int RewardPoints { get; set; } = 0;
    }
}
