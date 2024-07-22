using System.ComponentModel.DataAnnotations;

namespace AccoSystemWebAPI.DataLayer;

public enum TransactionType : byte
{
    //[Display(Name = "دریافت")]
    Income = 1,
    //[Display(Name = "پرداخت")]
    Cost
}