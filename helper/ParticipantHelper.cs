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

        [Obsolete]
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


        /// <summary>
        /// Retour async de tout les participants ayant un compte league of legends.
        /// Le jeuxcomptes du jeu League of Legends associé aux participants est garanti d'être présent.
        /// </summary>
        /// <returns>Une task contenant une liste d'objets participants</returns>
        public async static Task<List<participants>> SelectAllLoLPlayersAsync()
        {
            using (var ctx = new Connexion420())
            {
                var part = ctx.participants.Include(x => x.jeuxcomptes);

                //Ne filtre pas
                //TODO: fix
                var jeu = ctx.jeux
                    .Where(x => x.nom == "League of Ls")
                    .FirstOrDefault();

                var compt = ctx.jeuxcomptes
                    .Where(x => x.idJeu == jeu.idJeu);


                return await part.ToListAsync();
            }
        }

        public static async Task<bool> CreationParticipant(List<participants> list)
        {
            using (var ctx = new Connexion420())
            {
                //http://stackoverflow.com/questions/5943394/why-is-inserting-entities-in-ef-4-1-so-slow-compared-to-objectcontext/5943699#5943699
                ctx.Configuration.AutoDetectChangesEnabled = false;

                foreach (participants entity in list)
                {
                    ctx.participants.Add(entity);
                }
                try
                {
                    await ctx.SaveChangesAsync();
                }
                catch (Exception exct)
                {
                    Console.WriteLine(exct.Message);
                    return false;
                }

                return true;

            }
        }
    }

}
