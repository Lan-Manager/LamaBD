//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LamaBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class tours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tours()
        {
            this.parties = new HashSet<parties>();
        }
    
        public int idTour { get; set; }
        public int idTournoi { get; set; }
        public int numTour { get; set; }
        public bool estFinal { get; set; }
        public System.DateTime dateDebut { get; set; }
        public Nullable<System.DateTime> dateFin { get; set; }
        public System.DateTime lastUpdated { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<parties> parties { get; set; }
        public virtual tournois tournois { get; set; }
    }
}
