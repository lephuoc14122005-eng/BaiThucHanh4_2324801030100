using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProductService.DataAccess.Marten;

public sealed class ProtectedSettersContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        if (!property.Writable && member is PropertyInfo info && info.GetSetMethod(true) is not null)
            property.Writable = true;
        return property;
    }
}
