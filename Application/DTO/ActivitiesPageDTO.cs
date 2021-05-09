using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ActivitiesPageDTO
    {
        public int Page { get; set; }
        public List<ActivityDTO> Activities { get; set; }
    }
}
