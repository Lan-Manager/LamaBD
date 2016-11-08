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
    
    public partial class tournois
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tournois()
        {
            this.comptestournois = new HashSet<comptestournois>();
            this.equipes = new HashSet<equipes>();
            this.prixtournois = new HashSet<prixtournois>();
            this.tours = new HashSet<tours>();
            this.tournoislocaux = new HashSet<tournoislocaux>();
        }
    
        public int idTournoi { get; set; }
        public int idJeu { get; set; }
        public int idCompte { get; set; }
        public int idEtatTournoi { get; set; }
        public Nullable<int> idEquipe_gagnante { get; set; }
        public string nom { get; set; }
        public int minJoueur { get; set; }
        public Nullable<int> maxJoueurParEquipe { get; set; }
        public int minJoueurParEquipe { get; set; }
        public System.DateTime dateEvenement { get; set; }
        public string description { get; set; }
        public bool enCours { get; set; }
        public System.DateTime dateCreation { get; set; }
        public System.DateTime lastUpdated { get; set; }
    
        public virtual comptes comptes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comptestournois> comptestournois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipes> equipes { get; set; }
        public virtual equipes equipes1 { get; set; }
        public virtual etatstournois etatstournois { get; set; }
        public virtual jeux jeux { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<prixtournois> prixtournois { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tours> tours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tournoislocaux> tournoislocaux { get; set; }
    }
}
