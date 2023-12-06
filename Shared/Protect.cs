namespace Polaris.Shared
{

    // Define an empty interface because we would use extensions to specify the methods that the interface would have without modifying the empty interface
    // The reason for the empty interface is that, if we should defined some methods in the interface, then the class would be forced to implement it
    //Extension methods provide a way to add new methods to existing types without modifying them.
    //You can define extension methods for an interface, and those methods will be available for any class that implements the interface.
    public interface IAgainstGuardClause
    {
    }
    public class Protect : IAgainstGuardClause
    {
        //This means that whenever you access Protect.Against, you get a reference to a single, shared instance of the Protect class.
        public static IAgainstGuardClause Against { get;  } = new Protect();

    }
}
