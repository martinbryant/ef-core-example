using System.Reflection;
using ef_core_example.Models;

public sealed class ErrorTests
{
    [Fact(DisplayName = "[Error Codes] - Codes should be unique]")]
    public void ErrorMustBeUnique()
    {
        //register Errors nested classes here
        var classes = new Type[] 
        { 
            typeof(Errors.General), 
            typeof(Errors.Profile), 
            typeof(Errors.Depot) 
        };

        var foundClasses = typeof(Errors)
            .GetMembers(BindingFlags.Static | BindingFlags.Public)
            .Where(x => x.MemberType == MemberTypes.NestedType);
        
        List<MethodInfo> methods = classes
            .SelectMany(c => c
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.ReturnType == typeof(Error))
                .ToList())
            .ToList();

        int numberOfUniqueCodes = methods
            .Select(GetErrorCode)
            .Distinct()
            .Count();

        //if fails an Errors class has not been registered above or declared
        Assert.Equal(classes.Count(), foundClasses.Count());

        //if fails there is a duplicate Code that has been used
        Assert.Equal(methods.Count, numberOfUniqueCodes);
    }

    private string GetErrorCode(MethodInfo method)
    {
        object[] parameters = method.GetParameters()
            .Select<ParameterInfo, object>(x =>
            {
                if (x.ParameterType == typeof(string))
                    return string.Empty;

                if (x.ParameterType == typeof(long))
                    return 0;

                if (x.ParameterType == typeof(Guid))
                    return Guid.NewGuid();

                throw new Exception($"{x.ParameterType} is being used here");
            })
            .ToArray();

        var error = (Error)method.Invoke(null, parameters);
        return error.Code;
    }
}