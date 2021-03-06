using System.Collections.Generic;
using ECollegeAPI.Model;

namespace ECollegeAPI.Services.Discussions
{
    public class FetchMyDiscussionResponsesByTopicService : BaseService
    {
        public List<UserDiscussionResponse> Result { get; set; }

        public FetchMyDiscussionResponsesByTopicService(long topicId)
            : base()
        {
            Resource = "me/topics/" + topicId + "/userresponses";
        }

        public override void ProcessResponse(string resp)
        {
            Result = Deserialize<List<UserDiscussionResponse>>(resp, "userResponses");
        }
    }
}