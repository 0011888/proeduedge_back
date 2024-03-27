using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proeduedge.DAL.Entities;
using proeduedge.Repository;

namespace proeduedge.Controllers
{
    [Route("api/proeduedge")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeeting _meetingRepository;

        public MeetingController(IMeeting meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        [HttpGet("all-meetings")]
        public async Task<IActionResult> GetAllMeetings()
        {
            var meetings = await _meetingRepository.GetMeetings();
            return Ok(meetings);
        }

        [HttpGet("meeting/{id}")]
        public async Task<IActionResult> GetMeetingById(int id)
        {
            var meeting = await _meetingRepository.GetMeetingById(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        [HttpPost("create-meeting")]
        public async Task<IActionResult> CreateMeeting([FromBody] Meetings meeting)
        {
            var newMeeting = await _meetingRepository.CreateMeeting(meeting);
            return CreatedAtAction(nameof(GetMeetingById), new { id = newMeeting.Id }, newMeeting);
        }

        [HttpPut("update-meeting")]
        public async Task<IActionResult> UpdateMeeting([FromBody] Meetings meeting)
        {
            var updatedMeeting = await _meetingRepository.UpdateMeeting(meeting);
            return Ok(updatedMeeting);
        }

        [HttpDelete("delete-meeting/{id}")]
        public async Task<IActionResult> DeleteMeeting(int id)
        {
            var result = await _meetingRepository.DeleteMeeting(id);
            return Ok(result);
        }
    }
}
