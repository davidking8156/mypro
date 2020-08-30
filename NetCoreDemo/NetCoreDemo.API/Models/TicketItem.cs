using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;
namespace NetCoreDemo.API.Models
{
    //[DataContract]
    public class TicketItem
    {
        [DataMember(Name = "ID号")]
        public long Id { get; set; }

        [DataMember(Name = "剧院名称")]
        public string Concert { get; set; }
        [DataMember(Name = "演员")]
        public string Artist { get; set; }

        [DataMember(Name = "是否有票")]
        public bool Available { get; set; }
    }
}
