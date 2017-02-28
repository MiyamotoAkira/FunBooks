namespace Funbooks.Interfaces
{
    public interface IPOModifier
    {
        IPOModifier AddMembership(MembershipType membershipType);
        IPOModifier UpgradeMembership();
        IPOModifier CreateShippingSlip();
        IPOModifier GenerateCommission();
        IPOModifier AddBook(string title);
        IPOModifier AddVideo(string title);
    }
}  