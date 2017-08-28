using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PendingListItems.Helpers
{
    public static class Helpers
    {
        public static string ToDecimalAmount(decimal? item)
        {
            decimal? valueFormatter;

            if (item == null)
                valueFormatter = (decimal?)0;
            else
                valueFormatter = item;

            return valueFormatter.Value.ToString("C");
        }

        public static string CreateEditDelete()
        {
            return string.Empty;
        }
    }
}