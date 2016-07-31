using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    [Route("/1.1/statuses/update.json")]
    public class UpdateStatus : IReturn<UpdateStatusResponse>
    {
        [DataMember(Name = "status")]
        [Alias("status")]
        public string Status { get; set; }

        [DataMember(Name = "in_reply_to_status_id")]
        [Alias("in_reply_to_status_id")]
        public long InReplyToStatusId { get; set; }

        /// <summary>
        ///     Conditional serializing method to skip params that are not set, like since_id and max_id
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool? ShouldSerialize(string fieldName)
        {
            switch (fieldName)
            {
                case "in_reply_to_status_id":
                    return InReplyToStatusId > 0;
                default:
                    return true;
            }
        }
    }
}