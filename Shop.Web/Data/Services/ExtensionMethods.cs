namespace Shop.Web.Data.Services
{
    public static class ExtensionMethods
    {
        public static decimal GetDiscountPercent(this decimal price, int percent)
        {
            var result = (decimal)(price - (price * percent / 100));
            return result;
        }
    }
}
