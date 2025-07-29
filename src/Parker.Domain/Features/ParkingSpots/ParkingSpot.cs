using Parker.Domain.Common;

namespace Parker.Domain.Features.ParkingSpots;

// These properties would usually be ValueObjects with validation within, but for demo purposes they are primitives
public sealed class ParkingSpot : AggregateRoot, IEntity
{
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public string Type { get; private set; }
    public string Size { get; private set; }

#pragma warning disable CS8618
    private ParkingSpot() // Parameterless constructor is needed for EF Core to instantiate this model
    {
    }
#pragma warning restore CS8618

    public static ParkingSpot Create(string name, string description, string location, string type, string size)
    {
        // Some validation would usually be done here, along with adding domain events if warranted
        return new ParkingSpot
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Location = location,
            Type = type,
            Size = size
        };
    }

    public void UpdateDetails(string name, string type, string size)
    {
        // Validation goes here
        Name = name;
        Type = type;
        Size = size;

        // Add domain events if you want
    }
}