
namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBookMarkRepository BookMark { get; }
        IBibleRepository Bible { get; }
        IBibleBookRepository BibleBook { get; }
        IChapterRepository Chapter { get; }
        IFeedbackRepository Feedback { get; }

        void Save();
    }
}
