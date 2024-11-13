using CarRental.Comparer.Web.Services.StateContainer;

namespace CarRental.Comparer.Web.Extensions;

public static class StateContainerExtensions
{
    public static int AddRoutingObjectParameter(this StateContainer stateContainer, object value)
    {
        stateContainer.ObjectTunnel[value.GetHashCode()] = value;
        return value.GetHashCode();
    }

    public static T? GetRoutingObjectParameter<T>(this StateContainer stateContainer, int hashCode)
    {
        var value = stateContainer.ObjectTunnel.PopValue(hashCode);
        
        if(value != null)
        {
            return (T)value;
        }

        return default;
    }
}

