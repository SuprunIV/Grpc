using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcProvider;
using ProviderService.Models;

namespace ProviderService.Services;

public class ProviderApiService:GrpcProvider.ProviderService.ProviderServiceBase
{
    private static int id = 0;

    private static List<Provider> providers = new()
    {
        new Provider(++id, "KalugaTime", 40),
        new Provider(++id, "Ttl99", 99)
        
    };
    public override Task<ListReply> ListProviders(Empty request, ServerCallContext context)
    {
        var listReply = new ListReply();
        var providerList = providers.Select(p =>
        
            new ProviderReply
            {
                Id = p.Id, 
                Name = p.Name,
                Region = p.Region
            }
        ).ToList();
        
        listReply.Providers.AddRange(providerList);
        return Task.FromResult(listReply);
    }

    public override Task<ProviderReply> GetProvider(GetProviderRequest request, ServerCallContext context)
    {
        var provider = providers.Find(p => p.Id == request.Id);
        if (provider == null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, "Provider not found"));
        }

        return Task.FromResult(new ProviderReply()
        {
            Id = provider.Id,
            Name = provider.Name,
            Region = provider.Region
        });
    }

    public override Task<ProviderReply> CreateProvider(CreateProviderRequest request, ServerCallContext context)
    {
        var provider = new Provider(++id, request.Name, request.Region);
        providers.Add(provider);
        
        return Task.FromResult(new ProviderReply()
        {
            Id = provider.Id,
            Name = provider.Name,
            Region = provider.Region
        });
    }
    public override Task<ProviderReply> DeleteProvider(DeleteProviderRequest request, ServerCallContext context)
    {
        var provider = providers.Find(p => p.Id == request.Id);
        if (provider == null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, "Provider not found"));
        }

        providers.Remove(provider);
        return Task.FromResult(new ProviderReply()
        {
            Id = provider.Id,
            Name = provider.Name,
            Region = provider.Region
        });
    }
    public override Task<ProviderReply> UpdateProvider(UpdateProviderRequest request, ServerCallContext context)
    {
        var provider = providers.Find(p => p.Id == request.Id);
        if (provider == null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, "Provider not found"));
        }

        provider.Name = request.Name;
        provider.Region = request.Region;
        
        return Task.FromResult(new ProviderReply()
        {
            Id = provider.Id,
            Name = provider.Name,
            Region = provider.Region
        });
    }

}