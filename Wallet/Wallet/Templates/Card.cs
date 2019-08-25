using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Wallet
{
    public enum ECardType
    {
        Other       = 0,
        DebitCard   = 1,
        CreditCard  = 2,
        Savings     = 3,
        //Crypto    = 4,
    }

    public struct STransactionSubCategory
    {
        public string Label;
        public string Icon;
    }

    public struct STransactionCategory
    {
        public STransactionSubCategory SubCategory;
        public string Label;
        public string Icon;
    }

    public struct SMonthlyTransactions
    {
        STransactionCategory Category;
        public string Label;
        public double Value;
        public DateTime StartDate;
        public DateTime EndDate;
    }

    public struct STransactions
    {
        STransactionCategory Category;
        public string Label;
        public double Value;
        public DateTime Date;
    }

    public struct Widgets
    {
        public string Label;
    }

    public class Card
    {
        public ECardType Type           { get; set; }
        public int ID                   { get; set; }
        public int CardEnd              { get; set; }
        public string Label             { get; set; }
        public string CurrencySymbol    { get; set; }
        public double Balance           { get; set; }

        public STransactions RecentTransaction                   { get; set; }
        public List<SMonthlyTransactions> MonthlyTransactions    { get; set; }
        public List<Widget> WidgetsAvailable                     { get; set; }

        public bool IsSelected  { get; set; }

        /* Holding a list of all the transactions made on this card */
        public List<STransactions> AllTransactions  { get; set; }

        public Card()
        {
            Type            = ECardType.Other;
            ID              = -1;
            CardEnd         = 0000;
            Label           = "Other";
            CurrencySymbol  = "£";
            Balance         = 0.0;

            RecentTransaction   = new STransactions();
            MonthlyTransactions = new List<SMonthlyTransactions>();
            AllTransactions     = new List<STransactions>();
            WidgetsAvailable    = new List<Widget>();

            IsSelected      = false;
        }

        public double GetBalanceRounded()
        {
            return Math.Floor(Balance);
        }

        public int GetPence()
        {
            return (int)((Balance - GetBalanceRounded()) * 100);
        }

        public double GetRecentTransactionToBalancePercentage()
        {
            return (100 * RecentTransaction.Value) / Balance;
        }

        public string DisplayRecentTransactionToBalancePercentage()
        {
            string display = "";

            string percentage = string.Format("{0:0.0}", (100 * RecentTransaction.Value) / (Balance - RecentTransaction.Value));

            if (RecentTransaction.Value < 0)
                display = "- £" + (RecentTransaction.Value - (RecentTransaction.Value * 2)).ToString() + " (" + percentage + "%)";
            else if (RecentTransaction.Value > 0)
                display = "+ £" + RecentTransaction.Value.ToString() + " (" + percentage + "%)";

            return display;
        }

        public ImageSource GetCardIcon()
        {
            ImageSource image = null;

            if (Type == ECardType.DebitCard)
                image = "debit_card_icon.png";
            else if (Type == ECardType.CreditCard)
                image = "credit_card_icon.png";
            else if (Type == ECardType.Savings)
                image = "savings_account_icon.png";

            return image;
        }
    }
}
