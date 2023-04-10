using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TargetApp : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public  int Interval { get; set; }
    }
}
