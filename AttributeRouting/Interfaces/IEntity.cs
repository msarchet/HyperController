namespace AttributeRouting.Interfaces
{
    public interface IEntity<TEntityId>
    {
        TEntityId Id { get; set; }
        void Update(IEntity<TEntityId> entity);
    }
}
