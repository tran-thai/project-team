using WebApplication3.Models;


namespace WebApplication3.Models.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly Web01Context _context;

        public CommentsService(Web01Context context)
        {
            _context = context;
        }

        public async Task Add(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(); 
        }
    }
}
