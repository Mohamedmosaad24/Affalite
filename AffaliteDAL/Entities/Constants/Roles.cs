namespace AffaliteDAL.Entities.Constants;

public static class Roles
{
    public const string Affiliate = "Affiliate";
    public const string Merchant = "Merchant";
    public const string Admin = "Admin";

    public static readonly string[] All =
    {
        Affiliate,
        Merchant,
        Admin
    };
}
