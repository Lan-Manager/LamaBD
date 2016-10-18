using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class Participant_NomCompte
    {
        public participants Participant { get; set; }
        public string NomCompte { get; set; }
    }

    public class ParticipantHelper
    {
        public async static Task<List<participants>> SelectAllAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.participants
                            orderby p.matricule ascending
                            select p;
                return await query.ToListAsync();
            }
        }

        public async static Task<List<Participant_NomCompte>> SelectLoLPlayersAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from jc in ctx.jeuxcomptes
                            join j in ctx.jeux on jc.idJeu equals j.idJeu
                            join p in ctx.participants on jc.idParticipant equals p.idParticipant
                            where j.nom == "League of Legends"
                            select new Participant_NomCompte { Participant = p, NomCompte = jc.nomCompte };
                return await query.ToListAsync();
            }
        }
    }

}
