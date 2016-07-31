using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BausCode.Api.Jobs.Models
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "woeid")]
        // ReSharper disable once InconsistentNaming
        public int WOEID { get; set; }
    }
}
