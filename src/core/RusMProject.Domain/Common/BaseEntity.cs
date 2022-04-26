using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusMProject.Domain.Common
{
    public class BaseEntity:IEntity
    {
        public Int32 Id { get; set; }
        
        public Int32 CreatorUser { get; set; }
        public DateTime CreatorDate { get; set; }
        public Int32? EditorUser { get; set; }
        public DateTime? EditorDate { get; set; }
        public Boolean IsActive { get; set; }

    }
}
