using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ViewModel
{
    public class ComponentVM
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public List<PropVM> Props { get; set; }
        public List<EventVM> Events { get; set; }
        public List<SlotVM> Slots { get; set; }
    }
}
