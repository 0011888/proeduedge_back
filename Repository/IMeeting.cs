using proeduedge.DAL.Entities;

namespace proeduedge.Repository
{
    public interface IMeeting
    {
        Task<IEnumerable<Meetings>> GetMeetings();
        Task<Meetings> GetMeetingById(int id);
        Task<Meetings> CreateMeeting(Meetings meeting);
        Task<Meetings> UpdateMeeting(Meetings meeting);
        Task<bool> DeleteMeeting(int id);
    }
}
