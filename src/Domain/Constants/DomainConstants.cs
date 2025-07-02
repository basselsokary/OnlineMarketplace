namespace Domain.Constants;

public static class DomainConstants
{
    public const int GuidIdMaxLength = 63;
    
    public static class Order
    {
        public const int OrderNumberMaxLength = 50;
        public const int DescriptionMaxLength = 500;
    }

    public static class Item
    {
        public const int MaxItemQuantity = 10; // Maximum quantity for an order item;
    }

    public static class Product
    {
        public const int NameMaxLength = 127;
        public const int DescriptionMaxLength = 511;
        public const int ImageUrlMaxLength = 255;
        public const int MaxCategoriesPerProduct = 10;
    }

    public static class Category
    {
        public const int NameMaxLength = 127;
        public const int DescriptionMaxLength = 511;
    }

    public static class Review
    {
        public const int CommentMaxLength = 511;
        public const int RatingMinValue = 1;
        public const int RatingMaxValue = 5;
    }

    public static class Address
    {
        public const int StreetMaxLength = 127;
        public const int CityMaxLength = 63;
        public const int DistrictMaxLength = 63;
        public const int ZipCodeMaxLength = 31;
    }

    public static class Money
    {
        public const int Precision = 18;
        public const int Scale = 2;
        public const int MaxCurrencyLength = 3;
    }

    public static class Customer
    {
        public const int FullNameMaxLength = 127;
        public const int EmailMaxLength = 100;
        public const int PhoneNumberMaxLength = 15;
        public const int AgeMin = 12; // Minimum age for a customer

    }

    public static class User
    {
        public const int UserNameMaxLength = 31;
        public const int EmailMaxLength = 127;
        public const int PhoneNumberMaxLength = 15;
        public const int PasswordMinLength = /*8*/ 3;
        public const int PasswordMaxLength = 63;
    }
}