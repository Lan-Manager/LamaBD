using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
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

        public async static Task<List<participants>> SelectLoLPlayersAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from jc in ctx.jeuxcomptes
                            join j in ctx.jeux on jc.idJeu equals j.idJeu
                            join p in ctx.participants on jc.idParticipant equals p.idParticipant
                            where j.nom == "League of Legends"
                            select p;
                return await query.ToListAsync();
            }
        }
    }

}
