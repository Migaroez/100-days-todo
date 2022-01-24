namespace Domain.Data.Abstraction
{
    public interface IItemData<out T> where T : IEnumerable<INoteData>

    {
        /// <summary>
        /// Should be set by the repository
        /// </summary>
        Guid? Id { get; }

        string Description { get; }
        T Notes { get; }
        DateTimeOffset CreateDate { get; }
        DateTimeOffset? CompleteDate { get; }
        DateTimeOffset? ArchiveDate { get; }
    }
}