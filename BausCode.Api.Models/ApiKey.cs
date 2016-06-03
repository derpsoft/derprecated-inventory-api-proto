using System;
using ServiceStack.Auth;
using ServiceStack.DataAnnotations;

namespace BausCode.Api.Models
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApiKey : IAuditable
    {
        public ApiKey()
        {
            IsDeleted = false;
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        [ForeignKey(typeof (UserAuth))]
        public int UserAuthId { get; set; }

        [Reference]
        public UserAuth UserAuth { get; set; }

        [Required]
        [Index(Unique = true)]
        public Guid Key { get; set; }

        [Required]
        // ReSharper disable once MemberCanBePrivate.Global
        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(55)]
        public string ApplicationName { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ulong RowVersion { get; set; }
    }
}