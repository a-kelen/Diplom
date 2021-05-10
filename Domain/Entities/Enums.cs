using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public enum ReportStatus
    {
        Active,
        Admitted,
        Rejected
    }

    public enum ElementType
    {
        VueJS,
        VueTS,
        ReactJS,
        ReactTS,
        AngularJS,
        AngularTS
    }
}
