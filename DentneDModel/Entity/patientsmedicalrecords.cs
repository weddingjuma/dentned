//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DG.DentneD.Model.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class patientsmedicalrecords
    {
        public int patientsmedicalrecords_id { get; set; }
        public int patients_id { get; set; }
        public int medicalrecordstypes_id { get; set; }
        public string patientsmedicalrecords_value { get; set; }
        public string patientsmedicalrecords_note { get; set; }
    
        public virtual medicalrecordstypes medicalrecordstypes { get; set; }
        public virtual patients patients { get; set; }
    }
}
