using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum UserType
    {
        Client = 1,
        Technical, 
        Owner
    }
    public enum ValueTypeEnum 
    {

        Boolean = 1,
        Intger = 2,
        Fraction = 3,
        text = 4,
        Date=5
    }
    public enum ModuleType
    {
       
        Settings=0,
        Sales=1,
        CRM=2,
        Payroll = 3,
        Purchase = 4,
        Accounting = 5,
        Warehouses = 6,
        FixedAssets = 7
    }
    public enum FiscalPeriodStatus
    {
        Opened=1,
        Closed=2,
        ClosedForRevision=3
    }
    public enum AccountType
    {
        Revenues=1,
        Expenses=2,
        TrialBalance
    }
    public enum ItemType
    {
         Warehouse,
         Service
    }
    public enum CostCalculation
    {
        LastPurchasePrice,
        OpeningPrice,
        ActualAverage,
        HighestPrice

    }
    public enum ItemKind
    {
      Simillar=1,
      Alternative=2

    }
    public enum TypeWarehouseList 
    {
        Single=1,
        Collection=2
    }
    public enum TypeOfSubscription
    {
        MonthlySubscription=1,
        YearlyQuarterSubscription = 2,
        YearlyHalfSubscription=3,
        YearlySubscription= 4,
        FullBuyingSubscription

    }

    public enum PaymentType { 
       
        Online =1,
        Cash =2,
        Trial = 3,
        Free = 4
    }
}
