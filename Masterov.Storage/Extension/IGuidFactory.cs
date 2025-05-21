namespace Masterov.Storage.Extension;

internal interface IGuidFactory
{
    Guid Create();
}

internal class GuidFactory : IGuidFactory
{
    public Guid Create() => Guid.NewGuid();
}