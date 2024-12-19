namespace CycleShare.Server.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBikeRepository BikeRepository { get; }

        IAddressRepository AddressRepository { get; }

        Task SaveAsync();
    }
}
