namespace ProviderService.Models;

public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Region { get; set; }

    public Provider(int id, string name, int region)
    {
        Id = id;
        Name = name;
        Region = region;
    }
}