using proeduedge.DAL;
using proeduedge.DAL.Entities;

namespace proeduedge.Repository
{
    public class MeetingRepository : IMeeting
    {
        private readonly AppDBContext _context;
        public MeetingRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Meetings> CreateMeeting(Meetings meeting)
        {
            _context.Meetings.Add(meeting);
            await SaveAsync();
            return meeting;
        }

        public async Task<bool> DeleteMeeting(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
                await SaveAsync();
            }
            return true;
        }

        public async Task<Meetings> GetMeetingById(int id)
        {
            var m = await _context.Meetings.FindAsync(id);
            if (m != null)
            {
                return m;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Meetings>> GetMeetings()
        {
            var meetings = _context.Meetings;
            return meetings;
        }

        public async Task<Meetings> UpdateMeeting(Meetings meeting)
        {
            var m = await _context.Meetings.FindAsync(meeting.Id);
            if (m != null)
            {
                m.RoomId = meeting.RoomId;
                m.IsActive = meeting.IsActive;
                await SaveAsync();
                return m;
            }
            else
            {
                return null;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
