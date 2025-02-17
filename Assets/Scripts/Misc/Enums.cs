namespace Misc
{
    public enum IngredientType
    {
        HotDogBread = BreadType.HotDogBread,
        HamburgerBread = BreadType.HamburgerBread,
        
        HotDogMeat = MeatType.HotDogMeat,
        HamburgerMeat = MeatType.HamburgerMeat,
    }

    public enum BreadType
    {
        HotDogBread = 812371,
        HamburgerBread = 291851,
    }
    
    public enum MeatType
    {
        HotDogMeat = 455412,
        HamburgerMeat = 91824,
    }
    
    public enum FoodType
    {
        HotDog,
        Hamburger,
        Fries
    }
    
    public enum IngredientResourceType
    {
        HotDogBreadResource,
        HamburgerBreadResource,
        HotDogMeatResource,
        HamburgerMeatResource,
    }
    
}