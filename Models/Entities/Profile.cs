using CSharpFunctionalExtensions;

namespace ef_core_example.Models
{
    public class Profile : MarketplaceModel
    {
        public const int Max_Profile_Name_Length = 33;

        public string Name { get; private set; }

        protected Profile() { }

        protected Profile(string name)
        {
            Name = name;
        }

        public static Result<Profile, Error> Create(string name) =>
            ValidateName(name)
                .Map(name => new Profile(name));

        public static Result<Profile, Error> EditName(Profile profile, string updatedName) =>
            ValidateName(updatedName)
                .Map(name =>
                {
                    profile.Name = updatedName;
                    return profile;
                });

        private static Result<string, Error> ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.Profile.NameIsInvalid(name);

            if (name.Length > Max_Profile_Name_Length)
                return Errors.Profile.NameIsTooLong(name);

            return name;
        }
    }

}