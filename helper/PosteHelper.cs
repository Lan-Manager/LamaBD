using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class PosteHelper
    {
        public async static Task<List<postes>> SelectAllByNumeroLocalAsync(string numLocal)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.postes
                            orderby p.numeroPoste ascending
                            join l in ctx.locaux on p.idLocal equals l.idLocal
                            where l.numero == numLocal
                            select p;
                return await query.ToListAsync();
            }
        }

        public async static Task<etatspostes> SelectEtatAsync(int idPoste)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.postes
                            orderby p.numeroPoste ascending
                            join e in ctx.etatspostes on p.idEtatPoste equals e.idEtatPoste
                            where p.idPoste == idPoste
                            select e;
                etatspostes obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }

        /// <summary>
        /// Fonction qui met a jour l'état d'un poste.
        /// </summary>
        /// <param name="numeroPoste">Numéro du poste</param>
        /// <param name="numeroLocal">Numéro du local auqel le poste appartient</param>
        /// <param name="etat">Le nouvel état à sauvegarder.</param>
        /// <returns></returns>
        public static async Task<bool> UpdateEtatAsync(int numeroPoste, string numeroLocal, string etat)
        {
            using (var ctx = new Connexion420())
            {
                var queryP = from p in ctx.postes
                             join e in ctx.etatspostes on p.idEtatPoste equals e.idEtatPoste
                             join l in ctx.locaux on p.idLocal equals l.idLocal
                             where p.numeroPoste == numeroPoste && l.numero == numeroLocal
                             select p;

                var poste = await queryP.SingleOrDefaultAsync();

                var queryE = from e in ctx.etatspostes
                             where e.nom == etat
                             select e.idEtatPoste;
                var idEtat = await queryE.SingleOrDefaultAsync();

                poste.idEtatPoste = idEtat;

                await ctx.SaveChangesAsync();
                return true;
            }
            //Erreur possible
            return false;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="numeroPoste"></param>
        /// <param name="numeroLocal"></param>
        /// <param name="etat"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateModificateurtAsync(int numeroPoste, string numeroLocal, string nomUtilisateur)
        {
            using (var ctx = new Connexion420())
            {
                var queryP = from p in ctx.postes
                             join l in ctx.locaux on p.idLocal equals l.idLocal
                             where p.numeroPoste == numeroPoste && l.numero == numeroLocal
                             select p;

                var poste = await queryP.SingleOrDefaultAsync();

                var queryC = from c in ctx.comptes
                             where c.nomUtilisateur == nomUtilisateur
                             select c.idCompte;
                var idCompte = await queryC.SingleOrDefaultAsync();

                poste.idCompte = idCompte;

                await ctx.SaveChangesAsync();
                return true;
            }
            //Erreur possible
            return false;
        }

    }
}
