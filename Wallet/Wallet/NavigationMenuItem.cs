using System;

namespace Wallet
{
    public class NavigationMenuItem
    {
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public bool Profile         { get; set; }
        public bool BreakCategory   { get; set; }
    }
}
