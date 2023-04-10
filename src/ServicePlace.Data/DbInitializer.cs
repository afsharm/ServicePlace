using ServicePlace.Data;
using ServicePlace.Model.Entities;

public static class DbInitializer
{
    public static void Initialize(ServicePlaceContext context)
    {

        if (context.Services.Any()
            && context.Providers.Any())
        {
            return;   // DB has been seeded
        }

        var dentistryService = new Service { Name = "Dentistry" };
        var homeRenovationService = new Service { Name = "Home Renovation" };
        var languageSchoolService = new Service { Name = "Language School" };
        var medicalImagingService = new Service { Name = "Medical Imaging" };
        var coworkingSpace = new Service { Name = "Coworking Space" };
        var rehabService = new Service { Name = "Rehab" };
        var foodShopService = new Service { Name = "Food Shop" };

        var providers = new Provider[]
        {
                new Provider
                    {
                        Name = "Dr. Shayan",
                        Service = dentistryService
                    },
                new Provider
                    {
                        Name = "Dr. Zarghami",
                        Service = dentistryService
                    },
                new Provider
                    {
                        Name = "Uni. Tehran",
                        Service = dentistryService
                    },
                new Provider
                    {
                        Name = "Milad Hospital",
                        Service = dentistryService
                    },
                new Provider
                    {
                        Name = "Dr. Darwishpour",
                        Service = dentistryService
                    },
                new Provider
                    {
                        Name = "Helger",
                        Service = homeRenovationService
                    },
                new Provider
                    {
                        Name = "Sarvchin",
                        Service = homeRenovationService
                    },
                new Provider
                    {
                        Name = "Sanjagh",
                        Service = homeRenovationService
                    },
                new Provider
                    {
                        Name = "Bazsazi Tehran",
                        Service = homeRenovationService
                    },
                new Provider
                    {
                        Name = "Khedmatazma",
                        Service = homeRenovationService
                    },
                new Provider
                    {
                        Name = "Afarinesh",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "Safir",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "Jahad",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "Iran-Europe",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "Pardisan",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "ILI",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "Nobel",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "IranCanada",
                        Service = languageSchoolService
                    },
                new Provider
                    {
                        Name = "Athari",
                        Service = medicalImagingService
                    },
                new Provider
                    {
                        Name = "Parsian",
                        Service = medicalImagingService
                    },
                new Provider
                    {
                        Name = "Shafa",
                        Service = medicalImagingService
                    },
                new Provider
                    {
                        Name = "Mehr",
                        Service = medicalImagingService
                    },
                new Provider
                    {
                        Name = "Firouzgar",
                        Service = medicalImagingService
                    },
                new Provider
                    {
                        Name = "Hamava",
                        Service = coworkingSpace
                    },                    
                new Provider
                    {
                        Name = "Karmana",
                        Service = coworkingSpace
                    },                    
                new Provider
                    {
                        Name = "Haftohasht",
                        Service = coworkingSpace
                    },                    
                new Provider
                    {
                        Name = "Box",
                        Service = coworkingSpace
                    },                    
                new Provider
                    {
                        Name = "Tiwan",
                        Service = coworkingSpace
                    },                    
                new Provider
                    {
                        Name = "Karnik",
                        Service = rehabService
                    },                    
                new Provider
                    {
                        Name = "Naft Hospital",
                        Service = rehabService
                    },                    
                new Provider
                    {
                        Name = "Tavanafza",
                        Service = rehabService
                    },                    
                new Provider
                    {
                        Name = "Saba",
                        Service = rehabService
                    },                    
                new Provider
                    {
                        Name = "Shayesteh",
                        Service = rehabService
                    },                    
                new Provider
                    {
                        Name = "Canbo",
                        Service = foodShopService
                    },
                new Provider
                    {
                        Name = "OK",
                        Service = foodShopService
                    },
                new Provider
                    {
                        Name = "Hyperstar",
                        Service = foodShopService
                    },
                new Provider
                    {
                        Name = "Mowlavi",
                        Service = foodShopService
                    },
                new Provider
                    {
                        Name = "Haft",
                        Service = foodShopService
                    },
        };

        context.Providers.AddRange(providers);
        context.SaveChanges();
    }
}